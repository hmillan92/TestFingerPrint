using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DPUruNet;
using Entidades;
using FormFingerPrint;
using FuncionesLogicas;

namespace UareUSampleCSharp
{
    public partial class Enrollment : Form
    {
        /// <summary>
        /// Holds the main form with many functions common to all of SDK actions.
        /// </summary>
        public Form_Main _sender;
        List<Fmd> preenrollmentFmds;
        Helper oHelper = new Helper();
        int count;
        bool conexion;
        bool switche;
        private ReaderCollection _readers;

        Funciones funciones = new Funciones();
        DataResult<Fmd> result;

        public Enrollment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enrollment_Load(object sender, System.EventArgs e)
        {
            progressBar1.Value = 0;
            CheckForIllegalCrossThreadCalls = false;
            txtEnroll.Text = string.Empty;
            preenrollmentFmds = new List<Fmd>();
            preenrollmentFmds.Clear();
            count = 0;
            switche = false;
         
            SendMessage(Action.SendMessage, oHelper.ColocarHuella);

            if (!_sender.OpenReader())
            {
                this.Close();
            }

            if (!_sender.StartCaptureAsync(this.OnCaptured))
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                // Check capture quality and throw an error if bad.
                if (!_sender.CheckCaptureResult(captureResult)) return;

                result = null;
                switche = true;

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                //REVISAR LOS HILOS AQUI
                txtEnroll.Clear();

                SendMessage(Action.SendMessage, "Capturando...");
                Thread.Sleep(400);
                
                //intervalo de 2 segundos
                conexion = funciones.ValidaConexionSQL();
                if (conexion)
                {
                    txtEnroll.Clear();
                    SendMessage(Action.SendMessage, oHelper.HuellaCapturada);
                    count++;

                    switch (count)
                    {
                        case 1:
                            progressBar1.Value = progressBar1.Value = 25;
                            break;
                        case 2:
                            progressBar1.Value = progressBar1.Value = 50;
                            break;
                        case 3:
                            progressBar1.Value = progressBar1.Value = 75;
                            break;
                        default:
                            break;
                    }

                    if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        count--;
                        _sender.Reset = true;
                        throw new Exception(resultConversion.ResultCode.ToString());
                        
                    }
                    SendMessage(Action.SendMessage, +count + "/" + "4");
                    preenrollmentFmds.Add(resultConversion.Data);

                    if (count >= 4)
                    {
                        count = 0;
                        conexion = funciones.ValidaConexionSQL();
                        if (conexion)
                        {                           
                            DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                            if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                            {
                                result = resultEnrollment;
                                Operador oOperadores = funciones.CompararHuella(result.Data);

                                if (oOperadores == null)
                                {
                                    progressBar1.Value = 100;
                                    Thread.Sleep(800);
                                    progressBar1.Value = 0;
                                    switche = false;
                                    Aceptar();
                                }

                                else
                                {
                                    progressBar1.Value = 0;
                                    switche = false;
                                    MessageBox.Show(oHelper.HuellaExiste, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtEnroll.Clear();
                                    SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                                }

                                preenrollmentFmds.Clear();
                                count = 0;
                                return;
                            }

                            else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                            {
                                switche = false;
                                SendMessage(Action.SendMessage, oHelper.FmdError);
                                SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                                preenrollmentFmds.Clear();
                                count = 0;
                                return;
                            }
                        }

                        else
                        {
                            switche = false;
                            txtEnroll.Clear();
                            MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }

                else
                {
                    switche = false;
                    txtEnroll.Clear();
                    MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                count--;
                // Send error message, then close form
                SendMessage(Action.SendMessage, oHelper.Error + ex.Message);
            }  
        }

        /// <summary>
        /// Close window.
        /// </summary>
        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close window.
        /// </summary>
        private void Enrollment_Closed(object sender, System.EventArgs e)
        {
                    
            _readers = ReaderCollection.GetReaders();

            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            if (_readers.Count == 0)
            {
                MessageBox.Show(oHelper.LectorOff, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _sender.btnListar.Enabled = false;
                _sender.btnEnroll.Enabled = false;
                _sender.btnVerify.Enabled = false;
                switche = false;
            }
        }

        #region SendMessage
        private enum Action
        {
            SendMessage
        }

        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.txtEnroll.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            txtEnroll.Text += payload + "\r\n\r\n";
                            txtEnroll.SelectionStart = txtEnroll.TextLength;
                            txtEnroll.ScrollToCaret();
                            break;
                    }
                }
                if (payload == oHelper.Error + "DP_DEVICE_FAILURE")
                {
                    switche = false;
                    this.Close();
                }
            }

            catch (Exception)
            {
            }
        }
        #endregion

        private void Aceptar()
        {
            if (result != null)
            {        
                fmrRegistrar registrar = new fmrRegistrar(result.Data);
                registrar._sender = _sender;
                this.Close();
                _sender.Visible = false;
                registrar.ShowDialog();

                //this.Close();

                _sender.Visible = true;
                _sender.Activate();

            }
        }

        private void Enrollment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sender.lblStatusMensaje.Text = oHelper.ServerOn;
            if (switche)
            {
                DialogResult dr = MessageBox.Show("Desea cancelar esta operacion?", oHelper.NombreSistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                
            }                                  
        }
    }
}

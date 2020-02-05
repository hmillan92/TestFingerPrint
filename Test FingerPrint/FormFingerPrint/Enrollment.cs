using System;
using System.Collections.Generic;
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
        bool switche = false;

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
            CheckForIllegalCrossThreadCalls = false;
            txtEnroll.Text = string.Empty;
            btnAceptar.Enabled = false;
            preenrollmentFmds = new List<Fmd>();
            preenrollmentFmds.Clear();
            count = 0;
            
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

                count++;
                result = null;
                btnAceptar.Enabled = false;
                switche = true;
                conexion = funciones.ValidaConexionSQL();
                if (conexion)
                {
                    DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    txtEnroll.Clear();

                    SendMessage(Action.SendMessage, oHelper.HuellaCapturada + "\r\nCount: " + (count) + "/4");

                    if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        _sender.Reset = true;
                        throw new Exception(resultConversion.ResultCode.ToString());
                    }

                    preenrollmentFmds.Add(resultConversion.Data);

                    if (count >= 4)
                    {
                        DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                        if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                        {

                            result = resultEnrollment;
                            conexion = funciones.ValidaConexionSQL();
                            if (conexion)
                            {
                                Operador oOperadores = funciones.CompararHuella(result.Data);

                                if (oOperadores == null)
                                {
                                    switche = false;
                                    btnAceptar.Enabled = true;
                                    btnAceptar.PerformClick();
                                }

                                else
                                {
                                    switche = false;
                                    MessageBox.Show(oHelper.HuellaExiste, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtEnroll.Clear();
                                    SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                                }
                            }

                            else
                            {
                                MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtEnroll.Clear();
                                SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                            }


                            preenrollmentFmds.Clear();
                            count = 0;
                            return;
                        }


                        else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                        {
                            SendMessage(Action.SendMessage, oHelper.FmdError);
                            SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                            preenrollmentFmds.Clear();
                            count = 0;
                            return;
                        }
                    }
                }

                else
                {
                    MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
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
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
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
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (result != null)
            {
                string mensaje = oHelper.HuellaCapturada;               
                fmrRegistrar registrar = new fmrRegistrar(mensaje, result.Data);
                registrar.ShowDialog();
                this.Close();
            }         
        }

        private void Enrollment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (switche)
            {
                DialogResult dr = MessageBox.Show("Desea cancelar esta operacion?", "Pesaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}

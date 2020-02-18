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
        
        Funciones funciones = new Funciones();
        
        DataResult<Fmd> result;

        public Enrollment()
        {
            InitializeComponent();
        }

        #region Eventos
        private void Enrollment_Load(object sender, System.EventArgs e)
        {
            preenrollmentFmds = new List<Fmd>();
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
                LimpiarConsola();
                // Check capture quality and throw an error if bad.
                if (!_sender.CheckCaptureResult(captureResult)) return;
                
                result = null;
                switche = true;

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                SendMessage(Action.SendMessage, "Capturando...");
                Thread.Sleep(400);

                conexion = funciones.ValidaConexionSQL();
                if (conexion)
                {
                    SendMessage(Action.SendMessage, "Huella Capturada");
                    Thread.Sleep(300);

                    count++;
                    SendMessage(Action.SendMessage, "Numero de Intentos: " + count + "/" + "4");

                    if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        count--;
                        _sender.Reset = true;
                        throw new Exception(resultConversion.ResultCode.ToString());
                        
                    }
                    preenrollmentFmds.Add(resultConversion.Data);
                    SendMessage(Action.SendMessage, oHelper.ColocarHuella + " nuevamente");

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
                                    switche = false;
                                    Siguiente();
                                }

                                else
                                {
                                    switche = false;
                                    MessageBox.Show(oHelper.HuellaExiste, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    LimpiarConsola();
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
                            MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CerrarEnrollmentForm();
                        }
                    }
                }

                else
                {
                    switche = false;
                    MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CerrarEnrollmentForm();
                }
            }
            catch (Exception ex)
            {

                count--;
                // Send error message, then close form
                SendMessage(Action.SendMessage, oHelper.Error + ex.Message);
                if (captureResult.ResultCode == Constants.ResultCode.DP_DEVICE_FAILURE)
                {
                    switche = false;
                    CerrarEnrollmentForm();
                }
            }  
        }

        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }
        
        private void Enrollment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (switche)
            {
                DialogResult dr = MessageBox.Show("Desea cancelar esta operacion?", oHelper.NombreSistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
        }

        private void Enrollment_Closed(object sender, System.EventArgs e)
        {

            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            preenrollmentFmds.Clear();
            txtEnroll.Clear();
            if (_sender.CurrentReader == null)
            {
                MessageBox.Show(oHelper.LectorOff, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _sender.btnListar.Enabled = false;
                _sender.btnEnroll.Enabled = false;
                _sender.btnVerify.Enabled = false;
                switche = false;
            }
        }

        #endregion

        #region Delegados
        private enum Action
        {
            SendMessage
        }

        private delegate void SendMessageCallback(Action action, string payload);
        private delegate void MiDelegado();

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

            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }

        private void LimpiarConsola()
        {           
            try
            {
                int n = 0;
                if (this.txtEnroll.InvokeRequired)
                {
                    MiDelegado d = new MiDelegado(LimpiarConsola);
                    this.Invoke(d);
                }

                else if (n == 0)
                {
                    txtEnroll.Clear();
                }
            }

            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }

        private void Siguiente()
        {
            try
            {
                int n = 0;
                if (this._sender.InvokeRequired)
                {
                    MiDelegado d = new MiDelegado(Siguiente);
                    this.Invoke(d);
                }
                else if (n == 0)
                {
                    Aceptar();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }        
        }

        private void CerrarEnrollmentForm()
        {
            try
            {
                int n = 0;
                if (this.InvokeRequired)
                {
                    MiDelegado d = new MiDelegado(CerrarEnrollmentForm);
                    this.Invoke(d);
                }
                else if (n == 0)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }
        #endregion

        private void Aceptar()
        {
            if (result != null)
            {
                fmrRegistrar registrar = new fmrRegistrar(result.Data);
                registrar._sender = this._sender;
                registrar.ShowDialog();
                this.Close();

            }
        }

        

    }
}

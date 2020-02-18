using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DPUruNet;
using Entidades;
using FuncionesLogicas;

namespace UareUSampleCSharp
{
    public partial class Verification : Form
    {
        /// <summary>
        /// Holds the main form with many functions common to all of SDK actions.
        /// </summary>
        public Form_Main _sender;

        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
        private int count;
        bool conexion;
        private Operador OperadorEncontrado = new Operador();
        Funciones funciones = new Funciones();
        Helper oHelper = new Helper();

        public Verification()
        {
            InitializeComponent();
        }

        private void Verification_Load(object sender, System.EventArgs e)
        {
            txtVerify.Text = string.Empty;
            firstFinger = null;
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
                LimpiarConsola();
                // Check capture quality and throw an error if bad.
                if (!_sender.CheckCaptureResult(captureResult)) return;

                SendMessage(Action.SendMessage, "Capturando...");
                Thread.Sleep(300);
                LimpiarConsola();


                conexion = funciones.ValidaConexionSQL();
                if (conexion)
                {
                    DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                    SendMessage(Action.SendMessage, oHelper.HuellaCapturada);
                    Thread.Sleep(200);

                    if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        _sender.Reset = true;
                        throw new Exception(resultConversion.ResultCode.ToString());
                    }

                    if (count == 0)
                    {
                        firstFinger = resultConversion.Data;

                        conexion = funciones.ValidaConexionSQL();
                        if (conexion)
                        {
                            OperadorEncontrado = funciones.CompararHuella(firstFinger);

                            if (OperadorEncontrado != null)
                            {
                                SendMessage(Action.SendMessage, "Huella coincide con operador " + OperadorEncontrado.empID);
                                SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                            }

                            else
                            {
                                SendMessage(Action.SendMessage, oHelper.HuellaNoEncontrada);
                                SendMessage(Action.SendMessage, oHelper.ColocarHuella);
                            }

                            count = 0;
                        }

                        else
                        {
                            SendMessage(Action.SendMessage, oHelper.ErrorServidor);
                            MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CerrarEnrollmentForm();
                        }
                    }
                }

                else
                {
                    SendMessage(Action.SendMessage, oHelper.ErrorServidor);
                    MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CerrarEnrollmentForm();
                }
                        
            }

            catch (Exception ex)
            {
                // Send error message, then close form
                SendMessage(Action.SendMessage, oHelper.Error + ex.Message);
                if (captureResult.ResultCode == Constants.ResultCode.DP_DEVICE_FAILURE)
                {
                    CerrarEnrollmentForm();
                }

            }
        }

        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Verification_Closed(object sender, System.EventArgs e)
        {           
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            if (_sender.CurrentReader == null)
            {
                MessageBox.Show(oHelper.LectorOff, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _sender.btnListar.Enabled = false;
                _sender.btnEnroll.Enabled = false;
                _sender.btnVerify.Enabled = false;
            }
            
        }

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
                if (this.txtVerify.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
               
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            txtVerify.Text += payload + "\r\n\r\n";
                            txtVerify.SelectionStart = txtVerify.TextLength;
                            txtVerify.ScrollToCaret();
                            break;
                    }
                }
            }

            catch (Exception)
            {
            }

        }

        private void LimpiarConsola()
        {
            try
            {
                int n = 0;
                if (this.txtVerify.InvokeRequired)
                {
                    MiDelegado d = new MiDelegado(LimpiarConsola);
                    this.Invoke(d);
                }

                else if (n == 0)
                {
                    txtVerify.Clear();
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

    }
}
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
            
            SendMessage(Action.SendMessage, oHelper.Mensaje1);

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

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                txtEnroll.Clear();

                SendMessage(Action.SendMessage, oHelper.Mensaje2  +"\r\nCount: "   + (count)+"/4");

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

                        //comparar antes de ir al siguiente form
                        Operador oOperadores = funciones.CompararHuella(result.Data);

                        if (oOperadores == null)
                        {
                            btnAceptar.Enabled = true;
                            SendMessage(Action.SendMessage, oHelper.Mensaje3);
                        }

                        else
                        {
                            MessageBox.Show("Ya existe una huella con este registro", "Pesaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtEnroll.Clear();
                            SendMessage(Action.SendMessage, oHelper.Mensaje1);
                        }

                        preenrollmentFmds.Clear();
                        count = 0;                       
                        return;
                    }

                    
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        SendMessage(Action.SendMessage, oHelper.Mensaje4);
                        SendMessage(Action.SendMessage, oHelper.Mensaje1);
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
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
                string mensaje = oHelper.Mensaje2;               
                fmrRegistrar registrar = new fmrRegistrar(mensaje, result.Data);
                //this.Close();
                registrar.ShowDialog();
                this.Close();
            }         
        }

        
        //private void Enrollment_FormClosing(object sender, FormClosingEventArgs e)
        //{

        //    DialogResult dr = MessageBox.Show("Desea cancelar esta operacion?", "Pesaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    if (dr == DialogResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //}
    }
}

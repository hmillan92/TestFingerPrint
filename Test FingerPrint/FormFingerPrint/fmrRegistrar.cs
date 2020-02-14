using DPUruNet;
using Entidades;
using FuncionesLogicas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UareUSampleCSharp;

namespace FormFingerPrint
{
    public partial class fmrRegistrar : Form
    {
        public Form_Main _sender;
        string mensaje;
        bool switche = false;
        bool conexion;
        Fmd result;
        Funciones funciones = new Funciones();
        Helper oHelper = new Helper();
        private ReaderCollection _readers;
        public fmrRegistrar()
        {
            InitializeComponent();
        }

        public fmrRegistrar(Fmd presult)
        {
            InitializeComponent();
            result = presult;

        }

        private void fmrRegistrar_Load(object sender, EventArgs e)
        {
            //this.Activate();
            if (result != null)
            {
                txtHuella.Text = oHelper.HuellaCapturada;
                btnAgregar.Enabled = true;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conexion = funciones.ValidaConexionSQL();
            if (conexion)
            {
                int empID = int.Parse(txtCodOperador.Text);

                if (empID <= 0)
                {
                    MessageBox.Show("Debe agregar un ID de operador valido.");
                }

                else
                {
                    mensaje = funciones.CrearOperador(result, empID);
                    MessageBox.Show(mensaje);
                    switche = true;
                    this.Close();

                }
            }

            else
            {
                MessageBox.Show(oHelper.ErrorServidor, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmrRegistrar_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!switche)
            {
                DialogResult dr = MessageBox.Show("Desea cancelar esta operacion?", oHelper.NombreSistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void fmrRegistrar_FormClosed(object sender, FormClosedEventArgs e)
        {
            _readers = ReaderCollection.GetReaders();
            conexion = funciones.ValidaConexionSQL();

            if (_readers.Count == 0)
            {
                MessageBox.Show(oHelper.LectorOff, oHelper.NombreSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _sender.txtReaderSelected.Text = "";
                _sender.btnListar.Enabled = false;
                _sender.btnEnroll.Enabled = false;
                _sender.btnVerify.Enabled = false;
            }
        }
    }
}

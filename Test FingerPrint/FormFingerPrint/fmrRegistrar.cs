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
        string mensaje;
        string nombre;
        Fmd result;
        Funciones funciones = new Funciones();
        Form_Main form_Main = new Form_Main();
        public fmrRegistrar()
        {
            InitializeComponent();
        }

        public fmrRegistrar(string pmensaje, Fmd presult)
        {
            InitializeComponent();
            mensaje = pmensaje;
            result = presult;
            txtNombre.Focus();

        }

        private void fmrRegistrar_Load(object sender, EventArgs e)
        {           
            txtHuella.Text = mensaje;
            if (result != null)
            {
                btnAgregar.Enabled = true;
            }      
        }
    
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            nombre = txtNombre.Text;
            
            mensaje = funciones.CrearOperador(result, nombre);            
            MessageBox.Show(mensaje);
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

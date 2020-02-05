﻿using DPUruNet;
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
        bool switche = false;
        Fmd result;
        Funciones funciones = new Funciones();
        Form_Main form_Main = new Form_Main();
        Helper oHelper = new Helper();

        public fmrRegistrar()
        {
            InitializeComponent();
        }

        public fmrRegistrar(string pmensaje, Fmd presult)
        {
            InitializeComponent();
            mensaje = pmensaje;
            result = presult;
            
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
            string nombre = txtNombre.Text;
            string codOperador = txtCodOperador.Text;
            string status = cbStatus.Text;

            if (string.IsNullOrEmpty(codOperador))
            {
                MessageBox.Show("Debe agregar un codigo de operador valido.");              
            }

            else if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Debe seleccionar un status al operador.");
            }

            else
            {
                mensaje = funciones.CrearOperador(result, codOperador, nombre, status);
                MessageBox.Show(mensaje);
                switche = true;
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
    }
}

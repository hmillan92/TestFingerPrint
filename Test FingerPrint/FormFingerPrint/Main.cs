using FuncionesLogicas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormFingerPrint
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            fmrRegistrar FormRegistrar = new fmrRegistrar();
            FormRegistrar.ShowDialog();
            
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            fmrListar FormVerificar = new fmrListar();
            FormVerificar.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool msj;
            Funciones funciones = new Funciones();
            msj = funciones.ValidaConexionSQL();

            if (msj != true)
            {
                MessageBox.Show("Error al Conectar");
            }

            else
            {
                MessageBox.Show("Conectado!");
            }
        }
    }
}

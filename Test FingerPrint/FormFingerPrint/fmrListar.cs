using Entidades;
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
    public partial class fmrListar : Form
    {
        Funciones funciones = new Funciones();
        public fmrListar()
        {
            InitializeComponent();
        }

        private void fmrListar_Load(object sender, EventArgs e)
        {
            List<OperadorHString> listarOp = funciones.ListarHuellas();

            dgvListar.DataSource = listarOp;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

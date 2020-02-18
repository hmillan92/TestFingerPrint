using System.Windows.Forms;
using System;
using DPUruNet;
using FuncionesLogicas;

namespace UareUSampleCSharp
{
    public partial class ReaderSelection : Form
    {
        private Helper oHelper = new Helper();
        private ReaderCollection _readers;

        private Form_Main _sender;

        public Form_Main Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
    
        private Reader _reader;

        public Reader CurrentReader
        {
            get { return _reader; }
            set { _reader = value; }
        }
                
        public ReaderSelection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clear the control of readers, get readers and display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            cboReaders.Text = string.Empty;
            cboReaders.Items.Clear();
            cboReaders.SelectedIndex = -1;

            try
            {
                _readers = ReaderCollection.GetReaders();

                foreach (Reader Reader in _readers)
                {
                    cboReaders.Items.Add(Reader.Description.SerialNumber);
                }

                if (cboReaders.Items.Count > 0)
                {
                    cboReaders.SelectedIndex = 0;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                MessageBox.Show(text, caption);
            }
        }

        private void btnReaderSelect_Click(System.Object sender, System.EventArgs e)
        {
            if (_sender.CurrentReader != null)
            {
                _sender.CurrentReader.Dispose();
                _sender.CurrentReader = null;
            }
            _sender.CurrentReader = _readers[cboReaders.SelectedIndex];
            this.Close();
        }

        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ReaderSelection_Load(object sender, System.EventArgs e)
        {
            btnRefresh_Click(this, new System.EventArgs());
        }

        private void ReaderSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnRefresh_Click(this, new System.EventArgs());
            _readers = ReaderCollection.GetReaders();

            if (_readers.Count == 0)
            {
                _sender.txtReaderSelected.Text = "";
                _sender.btnListar.Enabled = false;
                _sender.btnEnroll.Enabled = false;
                _sender.btnVerify.Enabled = false;
            }
        }
    }
}

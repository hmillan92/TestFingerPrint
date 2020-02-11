namespace UareUSampleCSharp
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtReaderSelected = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnReaderSelect = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReaderSelected
            // 
            this.txtReaderSelected.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtReaderSelected.Location = new System.Drawing.Point(15, 27);
            this.txtReaderSelected.Name = "txtReaderSelected";
            this.txtReaderSelected.ReadOnly = true;
            this.txtReaderSelected.Size = new System.Drawing.Size(233, 20);
            this.txtReaderSelected.TabIndex = 7;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 15);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "Lector Seleccionado:";
            // 
            // btnReaderSelect
            // 
            this.btnReaderSelect.Location = new System.Drawing.Point(12, 57);
            this.btnReaderSelect.Name = "btnReaderSelect";
            this.btnReaderSelect.Size = new System.Drawing.Size(112, 23);
            this.btnReaderSelect.TabIndex = 8;
            this.btnReaderSelect.Text = "Seleccionar Lector";
            this.btnReaderSelect.Click += new System.EventHandler(this.btnReaderSelect_Click);
            // 
            // btnListar
            // 
            this.btnListar.Enabled = false;
            this.btnListar.Location = new System.Drawing.Point(136, 86);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(112, 23);
            this.btnListar.TabIndex = 20;
            this.btnListar.Text = "Listar Operadores";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);
            // 
            // btnEnroll
            // 
            this.btnEnroll.Enabled = false;
            this.btnEnroll.Location = new System.Drawing.Point(136, 57);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(112, 23);
            this.btnEnroll.TabIndex = 24;
            this.btnEnroll.Text = "Registrar Huella";
            this.btnEnroll.Click += new System.EventHandler(this.btnEnroll_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(12, 86);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(112, 23);
            this.btnVerify.TabIndex = 23;
            this.btnVerify.Text = "Verificar Huella";
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 144);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(258, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusMensaje
            // 
            this.lblStatusMensaje.Name = "lblStatusMensaje";
            this.lblStatusMensaje.Size = new System.Drawing.Size(63, 17);
            this.lblStatusMensaje.Text = "conectado";
            // 
            // Form_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(258, 166);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnEnroll);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.txtReaderSelected);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnReaderSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesaje";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtReaderSelected;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnReaderSelect;
        private System.Windows.Forms.Button btnListar;
        public System.Windows.Forms.Button btnEnroll;
        internal System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel lblStatusMensaje;
    }
}
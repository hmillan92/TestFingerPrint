namespace FormFingerPrint
{
    partial class fmrListar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.components = new System.ComponentModel.Container();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.huellaBdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.empID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellaBdBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListar
            // 
            this.dgvListar.AllowUserToAddRows = false;
            this.dgvListar.AllowUserToDeleteRows = false;
            this.dgvListar.AutoGenerateColumns = false;
            this.dgvListar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvListar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.empID});
            this.dgvListar.DataSource = this.huellaBdBindingSource;
            this.dgvListar.Enabled = false;
            this.dgvListar.Location = new System.Drawing.Point(12, 48);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.ReadOnly = true;
            this.dgvListar.Size = new System.Drawing.Size(106, 161);
            this.dgvListar.TabIndex = 0;
            // 
            // huellaBdBindingSource
            // 
            this.huellaBdBindingSource.DataSource = typeof(Entidades.Operador);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Operadores Registrados";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(43, 215);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // empID
            // 
            this.empID.DataPropertyName = "empID";
            this.empID.HeaderText = "empID";
            this.empID.Name = "empID";
            this.empID.ReadOnly = true;
            this.empID.Width = 63;
            // 
            // fmrListar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(140, 243);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListar);
            this.Name = "fmrListar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pesaje | Listar Operadores";
            this.Load += new System.EventHandler(this.fmrListar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellaBdBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.BindingSource huellaBdBindingSource;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn empID;
    }
}
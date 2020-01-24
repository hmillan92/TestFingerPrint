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
            this.idOperadorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.huellaBdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellaBdBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListar
            // 
            this.dgvListar.AutoGenerateColumns = false;
            this.dgvListar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idOperadorDataGridViewTextBoxColumn,
            this.nombreDataGridViewTextBoxColumn});
            this.dgvListar.DataSource = this.huellaBdBindingSource;
            this.dgvListar.Enabled = false;
            this.dgvListar.Location = new System.Drawing.Point(12, 45);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.Size = new System.Drawing.Size(306, 161);
            this.dgvListar.TabIndex = 0;
            // 
            // idOperadorDataGridViewTextBoxColumn
            // 
            this.idOperadorDataGridViewTextBoxColumn.DataPropertyName = "IdOperador";
            this.idOperadorDataGridViewTextBoxColumn.HeaderText = "IdOperador";
            this.idOperadorDataGridViewTextBoxColumn.Name = "idOperadorDataGridViewTextBoxColumn";
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Nombre";
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            // 
            // huellaBdBindingSource
            // 
            this.huellaBdBindingSource.DataSource = typeof(Entidades.HuellaBd);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Operadores Registrados";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(243, 215);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // fmrListar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 243);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListar);
            this.Name = "fmrListar";
            this.Text = "Listar";
            this.Load += new System.EventHandler(this.fmrListar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellaBdBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idOperadorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource huellaBdBindingSource;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnBack;
    }
}
namespace LAB05_GUI
{
    partial class frmDangKi
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKhoa = new System.Windows.Forms.ComboBox();
            this.cboChuyenNganh = new System.Windows.Forms.ComboBox();
            this.dgvDangKi = new System.Windows.Forms.DataGridView();
            this.btnDangKi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangKi)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(388, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đăng Kí Chuyên Ngành";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(274, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Khoa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(240, 159);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Chuyên Ngành";
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.FormattingEnabled = true;
            this.cboKhoa.Location = new System.Drawing.Point(393, 110);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(353, 33);
            this.cboKhoa.TabIndex = 10;
            this.cboKhoa.SelectedIndexChanged += new System.EventHandler(this.cboKhoa_SelectedIndexChanged);
            // 
            // cboChuyenNganh
            // 
            this.cboChuyenNganh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChuyenNganh.FormattingEnabled = true;
            this.cboChuyenNganh.Location = new System.Drawing.Point(393, 159);
            this.cboChuyenNganh.Name = "cboChuyenNganh";
            this.cboChuyenNganh.Size = new System.Drawing.Size(353, 33);
            this.cboChuyenNganh.TabIndex = 11;
            // 
            // dgvDangKi
            // 
            this.dgvDangKi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDangKi.Location = new System.Drawing.Point(149, 219);
            this.dgvDangKi.Name = "dgvDangKi";
            this.dgvDangKi.RowHeadersWidth = 51;
            this.dgvDangKi.RowTemplate.Height = 24;
            this.dgvDangKi.Size = new System.Drawing.Size(740, 299);
            this.dgvDangKi.TabIndex = 12;
            // 
            // btnDangKi
            // 
            this.btnDangKi.Location = new System.Drawing.Point(91, 538);
            this.btnDangKi.Name = "btnDangKi";
            this.btnDangKi.Size = new System.Drawing.Size(134, 49);
            this.btnDangKi.TabIndex = 13;
            this.btnDangKi.Text = "Đăng Kí";
            this.btnDangKi.UseVisualStyleBackColor = true;
            this.btnDangKi.Click += new System.EventHandler(this.btnDangKi_Click);
            // 
            // frmDangKi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 612);
            this.Controls.Add(this.btnDangKi);
            this.Controls.Add(this.dgvDangKi);
            this.Controls.Add(this.cboChuyenNganh);
            this.Controls.Add(this.cboKhoa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDangKi";
            this.Text = "frmDangKi";
            this.Load += new System.EventHandler(this.frmDangKi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangKi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.ComboBox cboChuyenNganh;
        private System.Windows.Forms.DataGridView dgvDangKi;
        private System.Windows.Forms.Button btnDangKi;
    }
}
namespace ImportExcel
{
    partial class DangNhap
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txttk = new DevExpress.XtraEditors.TextEdit();
            this.txtmk = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txttk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmk.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(75, 76);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tài Khoản ";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(75, 145);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(172, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Mật Khẩu thích gì ghi đó";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.Location = new System.Drawing.Point(162, 221);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 34);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Đăng Nhập";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txttk
            // 
            this.txttk.Location = new System.Drawing.Point(201, 72);
            this.txttk.Margin = new System.Windows.Forms.Padding(4);
            this.txttk.Name = "txttk";
            this.txttk.Size = new System.Drawing.Size(154, 28);
            this.txttk.TabIndex = 3;
            // 
            // txtmk
            // 
            this.txtmk.Location = new System.Drawing.Point(201, 140);
            this.txtmk.Margin = new System.Windows.Forms.Padding(4);
            this.txtmk.Name = "txtmk";
            this.txtmk.Size = new System.Drawing.Size(154, 28);
            this.txtmk.TabIndex = 4;
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 303);
            this.Controls.Add(this.txtmk);
            this.Controls.Add(this.txttk);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DangNhap";
            this.Text = "DangNhap";
            ((System.ComponentModel.ISupportInitialize)(this.txttk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmk.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txttk;
        private DevExpress.XtraEditors.TextEdit txtmk;
    }
}
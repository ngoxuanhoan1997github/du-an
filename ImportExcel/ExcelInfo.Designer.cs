namespace ImportExcel
{
    partial class ExcelInfo
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFirstRow = new DevExpress.XtraEditors.TextEdit();
            this.txtLastRow = new DevExpress.XtraEditors.TextEdit();
            this.txtSheet = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(95, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nhập dòng đầu";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Nhập dòng cuối";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(88, 136);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 16);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Sheet";
            // 
            // txtFirstRow
            // 
            this.txtFirstRow.Location = new System.Drawing.Point(129, 35);
            this.txtFirstRow.Name = "txtFirstRow";
            this.txtFirstRow.Size = new System.Drawing.Size(125, 22);
            this.txtFirstRow.TabIndex = 3;
            // 
            // txtLastRow
            // 
            this.txtLastRow.Location = new System.Drawing.Point(129, 85);
            this.txtLastRow.Name = "txtLastRow";
            this.txtLastRow.Size = new System.Drawing.Size(125, 22);
            this.txtLastRow.TabIndex = 4;
            // 
            // txtSheet
            // 
            this.txtSheet.Location = new System.Drawing.Point(129, 133);
            this.txtSheet.Name = "txtSheet";
            this.txtSheet.Size = new System.Drawing.Size(125, 22);
            this.txtSheet.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(129, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Ok";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ExcelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 260);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSheet);
            this.Controls.Add(this.txtLastRow);
            this.Controls.Add(this.txtFirstRow);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "ExcelInfo";
            this.Text = "ExcelInfo";
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheet.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFirstRow;
        private DevExpress.XtraEditors.TextEdit txtLastRow;
        private DevExpress.XtraEditors.TextEdit txtSheet;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}
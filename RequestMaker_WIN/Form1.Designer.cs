namespace RequestMaker_WIN
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReq = new System.Windows.Forms.Button();
            this.cBoxType = new System.Windows.Forms.ComboBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.btnResp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReq
            // 
            this.btnReq.Location = new System.Drawing.Point(12, 65);
            this.btnReq.Name = "btnReq";
            this.btnReq.Size = new System.Drawing.Size(100, 23);
            this.btnReq.TabIndex = 0;
            this.btnReq.Text = "Make Request";
            this.btnReq.UseVisualStyleBackColor = true;
            this.btnReq.Click += new System.EventHandler(this.Button_Click);
            // 
            // cBoxType
            // 
            this.cBoxType.FormattingEnabled = true;
            this.cBoxType.Location = new System.Drawing.Point(12, 12);
            this.cBoxType.Name = "cBoxType";
            this.cBoxType.Size = new System.Drawing.Size(216, 21);
            this.cBoxType.TabIndex = 1;
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 39);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(216, 20);
            this.searchBox.TabIndex = 2;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 94);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(216, 225);
            this.logBox.TabIndex = 3;
            // 
            // btnResp
            // 
            this.btnResp.Location = new System.Drawing.Point(128, 65);
            this.btnResp.Name = "btnResp";
            this.btnResp.Size = new System.Drawing.Size(100, 23);
            this.btnResp.TabIndex = 4;
            this.btnResp.Text = "Get Response";
            this.btnResp.UseVisualStyleBackColor = true;
            this.btnResp.Click += new System.EventHandler(this.Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 331);
            this.Controls.Add(this.btnResp);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.cBoxType);
            this.Controls.Add(this.btnReq);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReq;
        private System.Windows.Forms.ComboBox cBoxType;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button btnResp;
    }
}


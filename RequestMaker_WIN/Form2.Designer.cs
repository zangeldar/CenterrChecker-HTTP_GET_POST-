﻿namespace RequestMaker_WIN
{
    partial class Form2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chListBox = new System.Windows.Forms.CheckedListBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReq = new System.Windows.Forms.Button();
            this.btnResp = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectBuy = new System.Windows.Forms.Button();
            this.btnSelectSell = new System.Windows.Forms.Button();
            this.btnSelectInvert = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.chListBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.logBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.searchBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.65285F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.34715F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chListBox
            // 
            this.chListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chListBox.FormattingEnabled = true;
            this.chListBox.Location = new System.Drawing.Point(13, 39);
            this.chListBox.MultiColumn = true;
            this.chListBox.Name = "chListBox";
            this.chListBox.Size = new System.Drawing.Size(774, 151);
            this.chListBox.TabIndex = 0;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(13, 277);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(774, 170);
            this.logBox.TabIndex = 31;
            // 
            // searchBox
            // 
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox.Location = new System.Drawing.Point(13, 196);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(774, 20);
            this.searchBox.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnReq, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnResp, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 222);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(774, 49);
            this.tableLayoutPanel2.TabIndex = 35;
            // 
            // btnReq
            // 
            this.btnReq.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReq.Location = new System.Drawing.Point(3, 3);
            this.btnReq.Name = "btnReq";
            this.btnReq.Size = new System.Drawing.Size(100, 43);
            this.btnReq.TabIndex = 33;
            this.btnReq.Text = "Make Request";
            this.btnReq.UseVisualStyleBackColor = true;
            this.btnReq.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnResp
            // 
            this.btnResp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnResp.Location = new System.Drawing.Point(671, 3);
            this.btnResp.Name = "btnResp";
            this.btnResp.Size = new System.Drawing.Size(100, 43);
            this.btnResp.TabIndex = 34;
            this.btnResp.Text = "Get Response";
            this.btnResp.UseVisualStyleBackColor = true;
            this.btnResp.Click += new System.EventHandler(this.Button_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnSelectAll, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSelectBuy, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSelectSell, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSelectInvert, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(13, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(774, 30);
            this.tableLayoutPanel3.TabIndex = 36;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnSelectBuy
            // 
            this.btnSelectBuy.Location = new System.Drawing.Point(84, 3);
            this.btnSelectBuy.Name = "btnSelectBuy";
            this.btnSelectBuy.Size = new System.Drawing.Size(75, 23);
            this.btnSelectBuy.TabIndex = 1;
            this.btnSelectBuy.Text = "Select BUY";
            this.btnSelectBuy.UseVisualStyleBackColor = true;
            this.btnSelectBuy.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnSelectSell
            // 
            this.btnSelectSell.Location = new System.Drawing.Point(165, 3);
            this.btnSelectSell.Name = "btnSelectSell";
            this.btnSelectSell.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSell.TabIndex = 2;
            this.btnSelectSell.Text = "Select SELL";
            this.btnSelectSell.UseVisualStyleBackColor = true;
            this.btnSelectSell.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnSelectInvert
            // 
            this.btnSelectInvert.Location = new System.Drawing.Point(246, 3);
            this.btnSelectInvert.Name = "btnSelectInvert";
            this.btnSelectInvert.Size = new System.Drawing.Size(75, 23);
            this.btnSelectInvert.TabIndex = 3;
            this.btnSelectInvert.Text = "Invert Select";
            this.btnSelectInvert.UseVisualStyleBackColor = true;
            this.btnSelectInvert.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox chListBox;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnReq;
        private System.Windows.Forms.Button btnResp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectBuy;
        private System.Windows.Forms.Button btnSelectSell;
        private System.Windows.Forms.Button btnSelectInvert;
    }
}
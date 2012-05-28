namespace FormPeer
{
    partial class Form1
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
            this.spLoctxt = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.unregisterBtn = new System.Windows.Forms.Button();
            this.artcPrint = new System.Windows.Forms.TextBox();
            this.incoming = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spbcBtn = new System.Windows.Forms.Button();
            this.artclNametxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.artclsrchBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SuperPeer";
            // 
            // spLoctxt
            // 
            this.spLoctxt.Location = new System.Drawing.Point(85, 6);
            this.spLoctxt.Name = "spLoctxt";
            this.spLoctxt.Size = new System.Drawing.Size(392, 20);
            this.spLoctxt.TabIndex = 1;
            // 
            // registerBtn
            // 
            this.registerBtn.Location = new System.Drawing.Point(321, 32);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(75, 23);
            this.registerBtn.TabIndex = 2;
            this.registerBtn.Text = "register";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // unregisterBtn
            // 
            this.unregisterBtn.Enabled = false;
            this.unregisterBtn.Location = new System.Drawing.Point(402, 32);
            this.unregisterBtn.Name = "unregisterBtn";
            this.unregisterBtn.Size = new System.Drawing.Size(75, 23);
            this.unregisterBtn.TabIndex = 3;
            this.unregisterBtn.Text = "unregister";
            this.unregisterBtn.UseVisualStyleBackColor = true;
            this.unregisterBtn.Click += new System.EventHandler(this.unregisterBtn_Click);
            // 
            // artcPrint
            // 
            this.artcPrint.Location = new System.Drawing.Point(15, 132);
            this.artcPrint.Multiline = true;
            this.artcPrint.Name = "artcPrint";
            this.artcPrint.ReadOnly = true;
            this.artcPrint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.artcPrint.Size = new System.Drawing.Size(218, 189);
            this.artcPrint.TabIndex = 4;
            // 
            // incoming
            // 
            this.incoming.Location = new System.Drawing.Point(259, 132);
            this.incoming.Multiline = true;
            this.incoming.Name = "incoming";
            this.incoming.ReadOnly = true;
            this.incoming.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.incoming.Size = new System.Drawing.Size(218, 189);
            this.incoming.TabIndex = 5;
            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Requests Made";
            // 
            // spbcBtn
            // 
            this.spbcBtn.Location = new System.Drawing.Point(321, 362);
            this.spbcBtn.Name = "spbcBtn";
            this.spbcBtn.Size = new System.Drawing.Size(156, 23);
            this.spbcBtn.TabIndex = 7;
            this.spbcBtn.Text = "Became SuperPeer";
            this.spbcBtn.UseVisualStyleBackColor = true;
            this.spbcBtn.Click += new System.EventHandler(this.spbcBtn_Click);
            // 
            // artclNametxt
            // 
            this.artclNametxt.Location = new System.Drawing.Point(85, 67);
            this.artclNametxt.Name = "artclNametxt";
            this.artclNametxt.Size = new System.Drawing.Size(148, 20);
            this.artclNametxt.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Article Name";
            // 
            // artclsrchBtn
            // 
            this.artclsrchBtn.Location = new System.Drawing.Point(239, 67);
            this.artclsrchBtn.Name = "artclsrchBtn";
            this.artclsrchBtn.Size = new System.Drawing.Size(75, 23);
            this.artclsrchBtn.TabIndex = 10;
            this.artclsrchBtn.Text = "Search";
            this.artclsrchBtn.UseVisualStyleBackColor = true;
            this.artclsrchBtn.Click += new System.EventHandler(this.artclsrchBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 397);
            this.Controls.Add(this.artclsrchBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.artclNametxt);
            this.Controls.Add(this.spbcBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.incoming);
            this.Controls.Add(this.artcPrint);
            this.Controls.Add(this.unregisterBtn);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.spLoctxt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Peer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox spLoctxt;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Button unregisterBtn;
        private System.Windows.Forms.TextBox artcPrint;
        private System.Windows.Forms.TextBox incoming;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button spbcBtn;
        private System.Windows.Forms.TextBox artclNametxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button artclsrchBtn;
    }
}


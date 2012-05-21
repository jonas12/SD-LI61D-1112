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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.unregisterBtn = new System.Windows.Forms.Button();
            this.sending = new System.Windows.Forms.TextBox();
            this.incoming = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spbcBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SuperPeer";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(402, 20);
            this.textBox1.TabIndex = 1;
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
            // 
            // sending
            // 
            this.sending.Location = new System.Drawing.Point(15, 132);
            this.sending.Multiline = true;
            this.sending.Name = "sending";
            this.sending.ReadOnly = true;
            this.sending.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sending.Size = new System.Drawing.Size(218, 189);
            this.sending.TabIndex = 4;
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
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 397);
            this.Controls.Add(this.spbcBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.incoming);
            this.Controls.Add(this.sending);
            this.Controls.Add(this.unregisterBtn);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Peer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Button unregisterBtn;
        private System.Windows.Forms.TextBox sending;
        private System.Windows.Forms.TextBox incoming;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button spbcBtn;
    }
}


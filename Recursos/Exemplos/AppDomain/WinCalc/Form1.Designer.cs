namespace WinCalc
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
            this.btAdd = new System.Windows.Forms.Button();
            this.btSub = new System.Windows.Forms.Button();
            this.btMult = new System.Windows.Forms.Button();
            this.btDiv = new System.Windows.Forms.Button();
            this.textOp1 = new System.Windows.Forms.TextBox();
            this.textOp2 = new System.Windows.Forms.TextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.textRes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(174, 25);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(47, 23);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btSub
            // 
            this.btSub.Location = new System.Drawing.Point(174, 55);
            this.btSub.Name = "btSub";
            this.btSub.Size = new System.Drawing.Size(47, 23);
            this.btSub.TabIndex = 1;
            this.btSub.Text = "sub";
            this.btSub.UseVisualStyleBackColor = true;
            this.btSub.Click += new System.EventHandler(this.btSub_Click);
            // 
            // btMult
            // 
            this.btMult.Location = new System.Drawing.Point(238, 24);
            this.btMult.Name = "btMult";
            this.btMult.Size = new System.Drawing.Size(42, 23);
            this.btMult.TabIndex = 2;
            this.btMult.Text = "mult";
            this.btMult.UseVisualStyleBackColor = true;
            this.btMult.Click += new System.EventHandler(this.btMult_Click);
            // 
            // btDiv
            // 
            this.btDiv.Location = new System.Drawing.Point(238, 54);
            this.btDiv.Name = "btDiv";
            this.btDiv.Size = new System.Drawing.Size(42, 23);
            this.btDiv.TabIndex = 3;
            this.btDiv.Text = "div";
            this.btDiv.UseVisualStyleBackColor = true;
            this.btDiv.Click += new System.EventHandler(this.btDiv_Click);
            // 
            // textOp1
            // 
            this.textOp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textOp1.Location = new System.Drawing.Point(12, 28);
            this.textOp1.Name = "textOp1";
            this.textOp1.Size = new System.Drawing.Size(143, 20);
            this.textOp1.TabIndex = 4;
            this.textOp1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textOp2
            // 
            this.textOp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textOp2.Location = new System.Drawing.Point(12, 58);
            this.textOp2.Name = "textOp2";
            this.textOp2.Size = new System.Drawing.Size(143, 20);
            this.textOp2.TabIndex = 5;
            this.textOp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(174, 122);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(106, 23);
            this.btClear.TabIndex = 6;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // textRes
            // 
            this.textRes.BackColor = System.Drawing.SystemColors.Menu;
            this.textRes.Enabled = false;
            this.textRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textRes.Location = new System.Drawing.Point(12, 125);
            this.textRes.Name = "textRes";
            this.textRes.Size = new System.Drawing.Size(143, 20);
            this.textRes.TabIndex = 7;
            this.textRes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "-----------------------------------------------------------------";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 175);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textRes);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.textOp2);
            this.Controls.Add(this.textOp1);
            this.Controls.Add(this.btDiv);
            this.Controls.Add(this.btMult);
            this.Controls.Add(this.btSub);
            this.Controls.Add(this.btAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btSub;
        private System.Windows.Forms.Button btMult;
        private System.Windows.Forms.Button btDiv;
        private System.Windows.Forms.TextBox textOp1;
        private System.Windows.Forms.TextBox textOp2;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.TextBox textRes;
        private System.Windows.Forms.Label label1;
    }
}


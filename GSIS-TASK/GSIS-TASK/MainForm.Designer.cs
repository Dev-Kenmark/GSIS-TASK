
namespace GSIS_TASK
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnTab1 = new System.Windows.Forms.Button();
            this.btnTab2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(816, 84);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(-6, -8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 89);
            this.label2.TabIndex = 6;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(129, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(735, 89);
            this.label4.TabIndex = 8;
            this.label4.Text = "ANG SAYA SA ALLCARD";
            // 
            // panelContainer
            // 
            this.panelContainer.Location = new System.Drawing.Point(232, 178);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(444, 296);
            this.panelContainer.TabIndex = 9;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // btnTab1
            // 
            this.btnTab1.Location = new System.Drawing.Point(232, 151);
            this.btnTab1.Name = "btnTab1";
            this.btnTab1.Size = new System.Drawing.Size(107, 21);
            this.btnTab1.TabIndex = 9;
            this.btnTab1.Text = "Status";
            this.btnTab1.UseVisualStyleBackColor = true;
            this.btnTab1.Click += new System.EventHandler(this.btnTab1_Click);
            // 
            // btnTab2
            // 
            this.btnTab2.Location = new System.Drawing.Point(345, 151);
            this.btnTab2.Name = "btnTab2";
            this.btnTab2.Size = new System.Drawing.Size(107, 21);
            this.btnTab2.TabIndex = 10;
            this.btnTab2.Text = "Date";
            this.btnTab2.UseVisualStyleBackColor = true;
            this.btnTab2.Click += new System.EventHandler(this.btnTab2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 618);
            this.Controls.Add(this.btnTab2);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.btnTab1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnTab1;
        private System.Windows.Forms.Button btnTab2;
    }
}


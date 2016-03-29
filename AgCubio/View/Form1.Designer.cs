namespace View
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.Server_label = new System.Windows.Forms.Label();
            this.Name_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.foodBox = new System.Windows.Forms.TextBox();
            this.massBox = new System.Windows.Forms.TextBox();
            this.fpsBox = new System.Windows.Forms.TextBox();
            this.widthlabel = new System.Windows.Forms.Label();
            this.foodlabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MassLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FPSlabel = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Controls.Add(this.errorBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.ipBox);
            this.panel1.Controls.Add(this.nameBox);
            this.panel1.Controls.Add(this.Server_label);
            this.panel1.Controls.Add(this.Name_label);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 961);
            this.panel1.TabIndex = 0;
            // 
            // errorBox
            // 
            this.errorBox.BackColor = System.Drawing.Color.SkyBlue;
            this.errorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorBox.Location = new System.Drawing.Point(308, 546);
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.Size = new System.Drawing.Size(504, 53);
            this.errorBox.TabIndex = 14;
            this.errorBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 99.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(223, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(576, 152);
            this.label1.TabIndex = 9;
            this.label1.Text = "AgCubio";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(318, 445);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(323, 81);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Play!";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ipBox
            // 
            this.ipBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipBox.Location = new System.Drawing.Point(441, 384);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(200, 38);
            this.ipBox.TabIndex = 3;
            this.ipBox.Text = "localhost";
            // 
            // nameBox
            // 
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(441, 334);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(200, 38);
            this.nameBox.TabIndex = 2;
            // 
            // Server_label
            // 
            this.Server_label.AutoSize = true;
            this.Server_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Server_label.Location = new System.Drawing.Point(301, 384);
            this.Server_label.Name = "Server_label";
            this.Server_label.Size = new System.Drawing.Size(130, 39);
            this.Server_label.TabIndex = 1;
            this.Server_label.Text = "Server:";
            // 
            // Name_label
            // 
            this.Name_label.AutoSize = true;
            this.Name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name_label.Location = new System.Drawing.Point(311, 334);
            this.Name_label.Name = "Name_label";
            this.Name_label.Size = new System.Drawing.Size(120, 39);
            this.Name_label.TabIndex = 0;
            this.Name_label.Text = "Name:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Orange;
            this.panel2.Controls.Add(this.widthBox);
            this.panel2.Controls.Add(this.foodBox);
            this.panel2.Controls.Add(this.massBox);
            this.panel2.Controls.Add(this.fpsBox);
            this.panel2.Controls.Add(this.widthlabel);
            this.panel2.Controls.Add(this.foodlabel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.MassLabel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.FPSlabel);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 34);
            this.panel2.TabIndex = 1;
            // 
            // widthBox
            // 
            this.widthBox.BackColor = System.Drawing.Color.Orange;
            this.widthBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.widthBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthBox.Location = new System.Drawing.Point(768, 7);
            this.widthBox.Name = "widthBox";
            this.widthBox.ReadOnly = true;
            this.widthBox.ShortcutsEnabled = false;
            this.widthBox.Size = new System.Drawing.Size(100, 19);
            this.widthBox.TabIndex = 23;
            // 
            // foodBox
            // 
            this.foodBox.BackColor = System.Drawing.Color.Orange;
            this.foodBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.foodBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodBox.Location = new System.Drawing.Point(590, 7);
            this.foodBox.Name = "foodBox";
            this.foodBox.ReadOnly = true;
            this.foodBox.ShortcutsEnabled = false;
            this.foodBox.Size = new System.Drawing.Size(100, 19);
            this.foodBox.TabIndex = 23;
            // 
            // massBox
            // 
            this.massBox.BackColor = System.Drawing.Color.Orange;
            this.massBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.massBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.massBox.Location = new System.Drawing.Point(421, 7);
            this.massBox.Name = "massBox";
            this.massBox.ReadOnly = true;
            this.massBox.ShortcutsEnabled = false;
            this.massBox.Size = new System.Drawing.Size(100, 19);
            this.massBox.TabIndex = 22;
            // 
            // fpsBox
            // 
            this.fpsBox.BackColor = System.Drawing.Color.Orange;
            this.fpsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpsBox.Location = new System.Drawing.Point(249, 8);
            this.fpsBox.Name = "fpsBox";
            this.fpsBox.ReadOnly = true;
            this.fpsBox.ShortcutsEnabled = false;
            this.fpsBox.Size = new System.Drawing.Size(100, 19);
            this.fpsBox.TabIndex = 21;
            // 
            // widthlabel
            // 
            this.widthlabel.AutoSize = true;
            this.widthlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthlabel.Location = new System.Drawing.Point(762, 7);
            this.widthlabel.Name = "widthlabel";
            this.widthlabel.Size = new System.Drawing.Size(0, 20);
            this.widthlabel.TabIndex = 20;
            // 
            // foodlabel
            // 
            this.foodlabel.AutoSize = true;
            this.foodlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodlabel.Location = new System.Drawing.Point(573, 7);
            this.foodlabel.Name = "foodlabel";
            this.foodlabel.Size = new System.Drawing.Size(0, 20);
            this.foodlabel.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(696, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "WIDTH:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(528, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "FOOD:";
            // 
            // MassLabel
            // 
            this.MassLabel.AutoSize = true;
            this.MassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MassLabel.Location = new System.Drawing.Point(421, 10);
            this.MassLabel.Name = "MassLabel";
            this.MassLabel.Size = new System.Drawing.Size(0, 20);
            this.MassLabel.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(356, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "MASS:";
            // 
            // FPSlabel
            // 
            this.FPSlabel.AutoSize = true;
            this.FPSlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPSlabel.Location = new System.Drawing.Point(254, 10);
            this.FPSlabel.Name = "FPSlabel";
            this.FPSlabel.Size = new System.Drawing.Size(0, 20);
            this.FPSlabel.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(204, 7);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 20);
            this.label21.TabIndex = 0;
            this.label21.Text = "FPS:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 961);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AgCubio";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label Server_label;
        private System.Windows.Forms.Label Name_label;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox errorBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label MassLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FPSlabel;
        private System.Windows.Forms.Label widthlabel;
        private System.Windows.Forms.Label foodlabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.TextBox foodBox;
        private System.Windows.Forms.TextBox massBox;
        private System.Windows.Forms.TextBox fpsBox;
    }
}


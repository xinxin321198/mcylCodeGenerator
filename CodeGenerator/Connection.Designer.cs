namespace CodeGenerator
{
    partial class Connection
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
            this.maskedTextBox_ip = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_databasename = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox_dataBase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_instance = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库IP地址：";
            // 
            // maskedTextBox_ip
            // 
            this.maskedTextBox_ip.Location = new System.Drawing.Point(107, 43);
            this.maskedTextBox_ip.Name = "maskedTextBox_ip";
            this.maskedTextBox_ip.Size = new System.Drawing.Size(191, 21);
            this.maskedTextBox_ip.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库名称：";
            // 
            // textBox_databasename
            // 
            this.textBox_databasename.Location = new System.Drawing.Point(107, 77);
            this.textBox_databasename.Name = "textBox_databasename";
            this.textBox_databasename.Size = new System.Drawing.Size(191, 21);
            this.textBox_databasename.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox_dataBase
            // 
            this.comboBox_dataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_dataBase.FormattingEnabled = true;
            this.comboBox_dataBase.Location = new System.Drawing.Point(107, 17);
            this.comboBox_dataBase.Name = "comboBox_dataBase";
            this.comboBox_dataBase.Size = new System.Drawing.Size(191, 20);
            this.comboBox_dataBase.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "数据库类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "用户名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码：";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(107, 141);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(191, 21);
            this.textBox_username.TabIndex = 9;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(107, 170);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(191, 21);
            this.textBox_password.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "oracle实例名：";
            // 
            // textBox_instance
            // 
            this.textBox_instance.Location = new System.Drawing.Point(107, 111);
            this.textBox_instance.Name = "textBox_instance";
            this.textBox_instance.Size = new System.Drawing.Size(87, 21);
            this.textBox_instance.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "端口：";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(247, 111);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(51, 21);
            this.textBox_port.TabIndex = 14;
            // 
            // Connection
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 244);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_instance);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_dataBase);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_databasename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maskedTextBox_ip);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Connection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接数据库";
            this.Load += new System.EventHandler(this.Connection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_databasename;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox_dataBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_instance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_port;
    }
}
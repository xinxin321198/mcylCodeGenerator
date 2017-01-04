using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class Connection : Form
    {


        public Connection()
        {
            InitializeComponent();
        }


        private void Connection_Load(object sender, EventArgs e)
        {
            try
            {
                Program.checkKey();

                #region 初始化数据库类型
                comboBox_dataBase.Items.Add("oracle");
                //comboBox_dataBase.Items.Add("sqlserver2k");
                comboBox_dataBase.SelectedIndex = 0;
                #endregion

                textBox_databasename.Text = "MCYL_DATA";
                maskedTextBox_ip.Text = "127.0.0.1";
                textBox_instance.Text = "ORCL";
                textBox_port.Text = "1521";
                textBox_username.Text = "mcyl";
                textBox_password.Text = "mcyl";
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.checkKey();

                ConnectionInfo ci = new ConnectionInfo();
                ci.DbIp = maskedTextBox_ip.Text.Trim();
                ci.DbName = textBox_databasename.Text.Trim();
                ci.DbType = comboBox_dataBase.SelectedItem.ToString();
                ci.InstanceName = textBox_instance.Text.Trim();
                ci.Prot = textBox_port.Text.Trim();
                ci.UserName = textBox_username.Text.Trim();
                ci.PassWord = textBox_password.Text.Trim();

                //ci.DbIp = "192.168.1.114";
                //ci.DbName = "MCYL_DATA";
                //ci.DbType = Common.数据库类型.oracle数据库;
                //ci.InstanceName = "ORCL";
                //ci.Prot = "1521";
                //ci.UserName = "mcyl";
                //ci.PassWord = "mcyl";

                //if (null == comboBox_dataBase.SelectedItem)
                //{
                //    throw new Exception("请选择一个数据库");
                //}
                CodeGenerator cg = new CodeGenerator(ci);
                cg.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

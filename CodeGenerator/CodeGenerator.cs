using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CodeGenerator
{
    public partial class CodeGenerator : Form
    {


        /// <summary>
        /// 数据库连接对象
        /// </summary>
        ConnectionInfo ci = null;


        public CodeGenerator()
        {
            InitializeComponent();
        }
        public CodeGenerator(ConnectionInfo ci)
        {
            this.ci = ci;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_数据库类型.Text = this.ci.DbType;
            label_地址.Text = this.ci.DbIp;
            label_端口.Text = this.ci.Prot;
            splitContainer1.Panel2Collapsed = true;

            if (Common.数据库类型.oracle数据库 == this.ci.DbType && null != this.ci.DbType)
            {
                List<oracleDBHelper.DBTable> tables = oracleDBHelper.getTablesAndColumns(ci.getConnectionString(),ci.DbName);
                for (int i = 0; i < tables.Count; i++)
                {
                    comboBox_tables.Items.Add(tables[i]);
                    comboBox_fktable.Items.Add(tables[i]);
                }
                comboBox_tables.DisplayMember = "tableName";
                comboBox_fktable.DisplayMember = "tableName";
                richTextBox_code.Text = @"连接oracle成功!
请选择一张表...";
            }
            else if (Common.数据库类型.sqlserver2000数据库 == this.ci.DbType && null != this.ci.DbType)
            {
                List<sqlServer2kDBHelper.DBTable> tables = sqlServer2kDBHelper.getTables(ci.DbName);
                for (int i = 0; i < tables.Count; i++)
                {
                    comboBox_tables.Items.Add(tables[i]);
                    comboBox_fktable.Items.Add(tables[i]);
                }
                comboBox_tables.DisplayMember = "tableName";
                comboBox_fktable.DisplayMember = "tableName";
                richTextBox_code.Text = @"连接oracle成功!
请选择一张表...";
            }
            else
            {
                throw new Exception("请选择一个数据库");
            }



            #region 初始化生成代码类型
            comboBox_codeType.Items.Add(Common.生成代码类型.Model实体类);
            comboBox_codeType.Items.Add(Common.生成代码类型.基本的DAL和BLL);
            comboBox_codeType.Items.Add(Common.生成代码类型.校验对象空值的方法);
            comboBox_codeType.Items.Add(Common.生成代码类型.校验对象是否相等的方法);
            comboBox_codeType.Items.Add(Common.生成代码类型.初始化分页控件的方法);
            comboBox_codeType.Items.Add(Common.生成代码类型.多表连接方法);
            #endregion



        }



      

        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void button_generator_Click(object sender, EventArgs e)
        {
            try
            {
                Program.checkKey();
                if (comboBox_tables.SelectedItem == null || comboBox_codeType.SelectedItem == null || textBox_ClassNamePrefix.Text == "")
                {
                    return;
                }

                #region 初始化代码文件输出路径
                label_filePath.Text = Common.filePath;
                #endregion

                string dataBaseType =ci.DbType;//数据库类型
                string codeType = comboBox_codeType.SelectedItem.ToString();//生成代码类型
                string lastNameSpace = textBox1_命名空间.Text;
                string className = textBox_ClassNamePrefix.Text;//类名前缀
                string classRemark = richTextBox_ClassRemark.Text;//类说明
                string paramType = textBox_ClassNamePrefix.Text;//model参数类型
                string paramName = Common.fristCharToLower(textBox_ClassNamePrefix.Text)+"Param";//model参数变量名称
                string dataGridViewName = textBox_控件名称.Text;//datagridview控件名称
                string modelString = "";//model代码
                string dalString = "";//dal代码
                string dalImpString = "";//dalimp代码
                string bllString = "";//bllimp代码
                string bllImpString = "";//bllimp代码
                string checkObjectIsEqualString = "";//校验对象是否相等的代码
                string pageControlString = "";//分页控件初始化
                string checkObjectIsNullString = "";//校验对象是否有空值的代码
                string contactObjectsString = "";
                richTextBox_code.Clear();
                switch (dataBaseType)
                {
                    case Common.数据库类型.oracle数据库:
                        {
                            oracleDBHelper.DBTable table = (oracleDBHelper.DBTable)comboBox_tables.SelectedItem;
                            string path = label_filePath.Text + table.TableComment + "\\";
                            Common.createFile(path);//检查并且创建文件夹
                            switch (codeType)
                            {
                                case Common.生成代码类型.Model实体类: 
                                    {
                                        modelString = GeneratorOracleModel.generatorOracleModel(dataBaseType, table, className, classRemark, lastNameSpace);
                                        richTextBox_code.Text = modelString;
                                        string modelFilePath = path + textBox_ClassNamePrefix.Text +".cs";
                                        File.WriteAllText(modelFilePath, modelString);
                                    } break;
                                case Common.生成代码类型.基本的DAL和BLL:
                                    {
                                        if (paramType == "" || paramName == "")
                                        {
                                            MessageBox.Show("生成DAL类需要输入变量类型和变量名称！");
                                            return;
                                        }
                                        dalString = GeneratorOracleDAL.generatorOracleDAL(dataBaseType, table, className, classRemark, paramType, paramName, lastNameSpace);
                                        dalImpString = GeneratorOracleDALImp.generatorOracleDALImp(dataBaseType, table, className, classRemark, paramType, paramName, lastNameSpace);
                                        bllString = GeneratorOracleBLL.generatorOracleBLL(dataBaseType, table, className, classRemark, paramType, paramName, lastNameSpace);
                                        bllImpString = GeneratorOracleBLLImp.generatorOracleBLLImp(dataBaseType, table, className, classRemark, paramType, paramName, lastNameSpace);
                                        richTextBox_code.Text = dalString + dalImpString + bllString + bllImpString;


                                        string dalFilePath = path + textBox_ClassNamePrefix.Text + "DAL.cs";
                                        File.WriteAllText(dalFilePath, dalString);

                                        string dalImpFilePath = path + textBox_ClassNamePrefix.Text + "DALImp.cs";
                                        File.WriteAllText(dalImpFilePath, dalImpString);

                                        string bllFilePath = path + textBox_ClassNamePrefix.Text + "BLL.cs";
                                        File.WriteAllText(bllFilePath, bllString);

                                        string bllImpFilePath = path + textBox_ClassNamePrefix.Text + "BLLImp.cs";
                                        File.WriteAllText(bllImpFilePath, bllImpString);
                                    } break;
                                case Common.生成代码类型.校验对象是否相等的方法:
                                    {
                                        checkObjectIsEqualString = GeneratorCheckObjectIsEqual.generatorCheckObjectIsEqual(table);
                                        richTextBox_code.Text = checkObjectIsEqualString;



                                        string checkObjectIsEqualFilePath = path + textBox_ClassNamePrefix.Text + "CheckObjectIsEqual.cs";
                                        File.WriteAllText(checkObjectIsEqualFilePath, checkObjectIsEqualString);
                                    } break;
                                case Common.生成代码类型.初始化分页控件的方法:
                                    {
                                        pageControlString = GeneratorInitPageControl.generatorInitPageControl();
                                        richTextBox_code.Text = pageControlString;
                                        string pageControlFilePatn = path + textBox_ClassNamePrefix.Text + "PageControl.cs";
                                        File.WriteAllText(pageControlFilePatn, pageControlString);
                                    }break;
                                case Common.生成代码类型.校验对象空值的方法:
                                    {
                                        checkObjectIsNullString = GeneratorCheckObjectIsNull.generatorCheckObjectIsNull(table);
                                        richTextBox_code.Text = checkObjectIsNullString;
                                        string checkObjectIsNullFilePath = path + textBox_ClassNamePrefix.Text + "CheckObjectIsNull.cs";
                                        File.WriteAllText(checkObjectIsNullFilePath, checkObjectIsNullString);
                                    } break;
                                case Common.生成代码类型.多表连接方法:
                                        {
                                            List<oracleDBHelper.TempTabel> ttList = new List<oracleDBHelper.TempTabel>();
                                            for (int i = 0; i < listBox_colum.Items.Count; i++)
                                            {
                                                oracleDBHelper.TempTabel tt = (oracleDBHelper.TempTabel)listBox_colum.Items[i];
                                                ttList.Add(tt);
                                            }
                                            contactObjectsString = GeneratorOracleDALImpFind.generatorOracleDALImpFind(table, paramType, paramName, ttList, dataGridViewName);
                                            richTextBox_code.Text = contactObjectsString;
                                            string contactObjectsFilePath = path + textBox_ClassNamePrefix.Text + "ContactObjects.cs";
                                            File.WriteAllText(contactObjectsFilePath, contactObjectsString);
                                        }break;
                                default:
                                    {
                                       MessageBox.Show("错误的代码类型！");
                                        return;
                                    }
                            }
                        } break;
                    case Common.数据库类型.sqlserver2000数据库:
                        {
                            sqlServer2kDBHelper.DBTable sql2kTable = (sqlServer2kDBHelper.DBTable)comboBox_tables.SelectedItem;

                            switch (codeType)
                            {
                                case "Model": { modelString = ""; } break;
                                case "基本的DAL和BLL":
                                    {
                                        if (paramType == "" || paramName == "")
                                        {
                                            MessageBox.Show("生成DAL类需要输入变量类型和变量名称！");
                                            return;
                                        }
                                        dalString = "";
                                    } break;
                                default:
                                    {
                                        MessageBox.Show("错误的代码类型！");
                                        return;
                                    }
                            }
                        } break;
                    default:
                        {
                            MessageBox.Show("错误的数据库连接类型！");
                            return;
                        }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }





        /// <summary>
        /// 类选择下拉列表选择事件
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void textBox_ClassName_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打开路径按钮
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void button_openFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //psi.Arguments = "/root,c:\\";
            psi.Arguments = Common.filePath;
            System.Diagnostics.Process.Start(psi);
        }

      

        /// <summary>
        /// 主表选择下拉列表选择事件
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void comboBox_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                oracleDBHelper.DBTable table = (oracleDBHelper.DBTable)comboBox_tables.SelectedItem;
                table.Columns = oracleDBHelper.getColumns(ci.getConnectionString(),table.TableName);
                List<oracleDBHelper.DBColumn> columnList = table.Columns;
                for (int i = 0; i < columnList.Count; i++)
                {
                    oracleDBHelper.DBColumn column = columnList[i];
                    if (column.ColumnName.Length > 19)
                    {
                        throw new Exception(table.TableName + " 表的 " + column.ColumnName + " 字段名称过大，字段名称不能大于19个字符，请修改表字段名！");
                    }
                }
                richTextBox_code.Text = "请选择代码生成类型...";
                textBox_ClassNamePrefix.Text = Common.fristCharToUpper(table.TableName.ToLower());
                textBox1_命名空间.Text = table.TableComment;

                if (comboBox_codeType.SelectedItem!=null&&comboBox_codeType.SelectedItem.ToString() == "多表连接方法")
                {




                    if (comboBox_tables.SelectedIndex == -1)
                    {
                        return;
                    }
                    comboBox_columns.Items.Clear();
                    //生成高级查询方法
                    List<oracleDBHelper.DBColumn> columns = table.Columns;
                    for (int i = 0; i < columns.Count; i++)
                    {
                        comboBox_columns.Items.Add(columns[i]);
                    }
                    comboBox_columns.DisplayMember = "ColumnName";

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


            
        }

        /// <summary>
        /// 代码类型下拉列表选择事件
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void comboBox_codeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox_code.Text = @"1.请填写生成的代码名称，点击“生成”按钮
2.如果是生成DAL类型的代码，必须填写“类名”、“参数类型”、“参数变量名”";
            if (comboBox_codeType.SelectedItem==null)
            {
                return;
            }

            //启用多表连接的界面
            if (comboBox_codeType.SelectedItem.ToString() == Common.生成代码类型.多表连接方法)
            {

                if (comboBox_tables.SelectedIndex == -1)
                {
                    return;
                }
                oracleDBHelper.DBTable table = (oracleDBHelper.DBTable)comboBox_tables.SelectedItem;
                comboBox_columns.Items.Clear();
                //生成高级查询方法
                List<oracleDBHelper.DBColumn> columns = table.Columns;
                for (int i = 0; i < columns.Count; i++)
                {
                    comboBox_columns.Items.Add(columns[i]);
                }
                comboBox_columns.DisplayMember = "ColumnName";

                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;

            }
        }

        /// <summary>
        /// 选中字段的下拉列表中的项的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_columns_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                oracleDBHelper.DBColumn column = (oracleDBHelper.DBColumn)comboBox_columns.SelectedItem;
                textBox_columnType.Text = column.ColumnOracleType;
                textBox_columnRemark.Text = column.ColumnComments;
                textBox_columnLength.Text = column.ColumnLength;
                textBox_columnTempTableName.Text = "TT_"+column.ColumnName.ToUpper();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 添加字段按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_addColumn_Click(object sender, EventArgs e)
        {
            try 
	        {	        
		        oracleDBHelper.TempTabel tt = new oracleDBHelper.TempTabel();
                tt.TempTableName = textBox_columnTempTableName.Text.Trim();
                if (comboBox_columns.SelectedIndex==-1)
                {
                    throw new Exception("请选择一个字段再添加！");
                }
                if (!radioButton_code.Checked&&!radioButton_othertable.Checked)
                {
                    throw new Exception("请选择一种字段关联类型");
                }
                if (radioButton_code.Checked&&(textBox_columnCodeTypeName.Text==null||textBox_columnCodeTypeName.Text==""))
                {
                    throw new Exception("请填写此字段在 Model.Common.代码类型值，中的代码类型名称");
                }
                if (radioButton_othertable.Checked&&comboBox_fktable.SelectedIndex==-1)
                {
                    throw new Exception("请选择一张外键关联的表");
                }

                if (radioButton_othertable.Checked&&comboBox_fktable.SelectedIndex!=-1)//如果选中外键表类型并且选中了一张对应的表
                {
                    tt.RelationTable = (oracleDBHelper.DBTable)comboBox_fktable.SelectedItem;
                }
                if (radioButton_code.Checked)
                {
                    tt.ColumnType = oracleDBHelper.TempTabel.字段外键类型.代码类型;
                }
                else if (radioButton_othertable.Checked)
                {
                    tt.ColumnType = oracleDBHelper.TempTabel.字段外键类型.表外键类型;
                }
                tt.Column = (oracleDBHelper.DBColumn)comboBox_columns.SelectedItem;
                tt.ColumnCodeTypeName = textBox_columnCodeTypeName.Text.Trim();
                tt.ObjectName = Common.columnNameConvertToObjectName(tt);
                tt.ObjectGetSetMethodName = Common.fristCharToUpper(tt.ObjectName);
                listBox_colum.Items.Add(tt);
                listBox_colum.DisplayMember = "ColumnName";
	        }
	        catch (Exception ex)
	        {
		
		        MessageBox.Show(ex.Message);
	        }

        }


        /// <summary>
        /// 字段移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_colum.SelectedIndex==-1)
                {
                    throw new Exception("请至少选择一项");
                }
                listBox_colum.Items.RemoveAt(listBox_colum.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 代码单选按钮选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_code_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                this.textBox_columnCodeTypeName.Enabled = true;
                this.label_字段代码类型名称.Visible = true;
                this.textBox_columnCodeTypeName.Visible = true;
                this.label_外键表名称.Visible = false;
                this.comboBox_fktable.Visible = false;
                this.comboBox_fktable.Enabled = false;
            }
        }

        /// <summary>
        /// 单选按钮选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_othertable_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                this.textBox_columnCodeTypeName.Enabled = false;
                this.label_字段代码类型名称.Visible = false;
                this.textBox_columnCodeTypeName.Visible = false;
                this.label_外键表名称.Visible = true;
                this.comboBox_fktable.Visible = true;
                this.comboBox_fktable.Enabled = true;
            }
        }

        /// <summary>
        /// 选中外键表combobox项的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_fktable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                oracleDBHelper.DBTable table = (oracleDBHelper.DBTable)comboBox_fktable.SelectedItem;
                table.Columns = oracleDBHelper.getColumns(ci.getConnectionString(),table.TableName);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CodeGenerator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }




    }
}

namespace CodeGenerator
{
    partial class CodeGenerator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param Name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox_tables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_code = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_codeType = new System.Windows.Forms.ComboBox();
            this.button_generator = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ClassNamePrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox_ClassRemark = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_openFile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1_命名空间 = new System.Windows.Forms.TextBox();
            this.comboBox_columns = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button_addColumn = new System.Windows.Forms.Button();
            this.listBox_colum = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_listviewColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_columnType = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox_columnRemark = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_columnLength = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_columnTempTableName = new System.Windows.Forms.TextBox();
            this.label_字段代码类型名称 = new System.Windows.Forms.Label();
            this.textBox_columnCodeTypeName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.comboBox_fktable = new System.Windows.Forms.ComboBox();
            this.label_外键表名称 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_code = new System.Windows.Forms.RadioButton();
            this.radioButton_othertable = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox_控件名称 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label_数据库类型 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_地址 = new System.Windows.Forms.Label();
            this.label_端口 = new System.Windows.Forms.Label();
            this.label_filePath = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip_listviewColumn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_tables
            // 
            this.comboBox_tables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tables.FormattingEnabled = true;
            this.comboBox_tables.Location = new System.Drawing.Point(95, 79);
            this.comboBox_tables.Name = "comboBox_tables";
            this.comboBox_tables.Size = new System.Drawing.Size(211, 20);
            this.comboBox_tables.TabIndex = 1;
            this.comboBox_tables.SelectedIndexChanged += new System.EventHandler(this.comboBox_tables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "表名";
            // 
            // richTextBox_code
            // 
            this.richTextBox_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_code.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_code.Name = "richTextBox_code";
            this.richTextBox_code.Size = new System.Drawing.Size(346, 628);
            this.richTextBox_code.TabIndex = 9;
            this.richTextBox_code.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "生成代码类型";
            // 
            // comboBox_codeType
            // 
            this.comboBox_codeType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_codeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_codeType.FormattingEnabled = true;
            this.comboBox_codeType.Location = new System.Drawing.Point(97, 108);
            this.comboBox_codeType.Name = "comboBox_codeType";
            this.comboBox_codeType.Size = new System.Drawing.Size(209, 20);
            this.comboBox_codeType.TabIndex = 2;
            this.comboBox_codeType.SelectedIndexChanged += new System.EventHandler(this.comboBox_codeType_SelectedIndexChanged);
            // 
            // button_generator
            // 
            this.button_generator.Location = new System.Drawing.Point(12, 300);
            this.button_generator.Name = "button_generator";
            this.button_generator.Size = new System.Drawing.Size(75, 23);
            this.button_generator.TabIndex = 7;
            this.button_generator.Text = "生成";
            this.button_generator.UseVisualStyleBackColor = true;
            this.button_generator.Click += new System.EventHandler(this.button_generator_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "类名前缀";
            // 
            // textBox_ClassNamePrefix
            // 
            this.textBox_ClassNamePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClassNamePrefix.Location = new System.Drawing.Point(97, 162);
            this.textBox_ClassNamePrefix.Name = "textBox_ClassNamePrefix";
            this.textBox_ClassNamePrefix.Size = new System.Drawing.Size(209, 21);
            this.textBox_ClassNamePrefix.TabIndex = 3;
            this.textBox_ClassNamePrefix.TextChanged += new System.EventHandler(this.textBox_ClassName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "类说明";
            // 
            // richTextBox_ClassRemark
            // 
            this.richTextBox_ClassRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_ClassRemark.Location = new System.Drawing.Point(95, 189);
            this.richTextBox_ClassRemark.Name = "richTextBox_ClassRemark";
            this.richTextBox_ClassRemark.Size = new System.Drawing.Size(211, 105);
            this.richTextBox_ClassRemark.TabIndex = 6;
            this.richTextBox_ClassRemark.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "代码文件输出路径";
            // 
            // button_openFile
            // 
            this.button_openFile.Location = new System.Drawing.Point(12, 342);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(75, 23);
            this.button_openFile.TabIndex = 19;
            this.button_openFile.Text = "打开目录";
            this.button_openFile.UseVisualStyleBackColor = true;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "最下层命名空间";
            // 
            // textBox1_命名空间
            // 
            this.textBox1_命名空间.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1_命名空间.Location = new System.Drawing.Point(97, 135);
            this.textBox1_命名空间.Name = "textBox1_命名空间";
            this.textBox1_命名空间.Size = new System.Drawing.Size(209, 21);
            this.textBox1_命名空间.TabIndex = 33;
            // 
            // comboBox_columns
            // 
            this.comboBox_columns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_columns.FormattingEnabled = true;
            this.comboBox_columns.Location = new System.Drawing.Point(57, 48);
            this.comboBox_columns.Name = "comboBox_columns";
            this.comboBox_columns.Size = new System.Drawing.Size(154, 20);
            this.comboBox_columns.TabIndex = 43;
            this.comboBox_columns.SelectedIndexChanged += new System.EventHandler(this.comboBox_columns_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 44;
            this.label17.Text = "字段名";
            // 
            // button_addColumn
            // 
            this.button_addColumn.Location = new System.Drawing.Point(128, 289);
            this.button_addColumn.Name = "button_addColumn";
            this.button_addColumn.Size = new System.Drawing.Size(61, 23);
            this.button_addColumn.TabIndex = 45;
            this.button_addColumn.Text = "添加字段";
            this.button_addColumn.UseVisualStyleBackColor = true;
            this.button_addColumn.Click += new System.EventHandler(this.button_addColumn_Click);
            // 
            // listBox_colum
            // 
            this.listBox_colum.ContextMenuStrip = this.contextMenuStrip_listviewColumn;
            this.listBox_colum.FormattingEnabled = true;
            this.listBox_colum.ItemHeight = 12;
            this.listBox_colum.Location = new System.Drawing.Point(10, 318);
            this.listBox_colum.Name = "listBox_colum";
            this.listBox_colum.Size = new System.Drawing.Size(339, 268);
            this.listBox_colum.TabIndex = 46;
            // 
            // contextMenuStrip_listviewColumn
            // 
            this.contextMenuStrip_listviewColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除ToolStripMenuItem});
            this.contextMenuStrip_listviewColumn.Name = "contextMenuStrip_listviewColumn";
            this.contextMenuStrip_listviewColumn.Size = new System.Drawing.Size(101, 26);
            // 
            // 移除ToolStripMenuItem
            // 
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.移除ToolStripMenuItem.Text = "移除";
            this.移除ToolStripMenuItem.Click += new System.EventHandler(this.移除ToolStripMenuItem_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(217, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 47;
            this.label18.Text = "类型";
            // 
            // textBox_columnType
            // 
            this.textBox_columnType.Location = new System.Drawing.Point(252, 47);
            this.textBox_columnType.Name = "textBox_columnType";
            this.textBox_columnType.ReadOnly = true;
            this.textBox_columnType.Size = new System.Drawing.Size(97, 21);
            this.textBox_columnType.TabIndex = 48;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(126, 76);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 49;
            this.label19.Text = "备注";
            // 
            // textBox_columnRemark
            // 
            this.textBox_columnRemark.Location = new System.Drawing.Point(161, 73);
            this.textBox_columnRemark.Name = "textBox_columnRemark";
            this.textBox_columnRemark.ReadOnly = true;
            this.textBox_columnRemark.Size = new System.Drawing.Size(188, 21);
            this.textBox_columnRemark.TabIndex = 50;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(18, 76);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 51;
            this.label20.Text = "长度";
            // 
            // textBox_columnLength
            // 
            this.textBox_columnLength.Location = new System.Drawing.Point(53, 73);
            this.textBox_columnLength.Name = "textBox_columnLength";
            this.textBox_columnLength.ReadOnly = true;
            this.textBox_columnLength.Size = new System.Drawing.Size(61, 21);
            this.textBox_columnLength.TabIndex = 52;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 12);
            this.label21.TabIndex = 53;
            this.label21.Text = "字段临时表名";
            // 
            // textBox_columnTempTableName
            // 
            this.textBox_columnTempTableName.Location = new System.Drawing.Point(85, 100);
            this.textBox_columnTempTableName.Name = "textBox_columnTempTableName";
            this.textBox_columnTempTableName.ReadOnly = true;
            this.textBox_columnTempTableName.Size = new System.Drawing.Size(264, 21);
            this.textBox_columnTempTableName.TabIndex = 54;
            // 
            // label_字段代码类型名称
            // 
            this.label_字段代码类型名称.AutoSize = true;
            this.label_字段代码类型名称.Location = new System.Drawing.Point(8, 168);
            this.label_字段代码类型名称.Name = "label_字段代码类型名称";
            this.label_字段代码类型名称.Size = new System.Drawing.Size(245, 12);
            this.label_字段代码类型名称.TabIndex = 55;
            this.label_字段代码类型名称.Text = "Model.Common.代码类型值 中的代码类型名称\r\n";
            this.label_字段代码类型名称.Visible = false;
            // 
            // textBox_columnCodeTypeName
            // 
            this.textBox_columnCodeTypeName.Location = new System.Drawing.Point(10, 189);
            this.textBox_columnCodeTypeName.Name = "textBox_columnCodeTypeName";
            this.textBox_columnCodeTypeName.Size = new System.Drawing.Size(316, 21);
            this.textBox_columnCodeTypeName.TabIndex = 56;
            this.textBox_columnCodeTypeName.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label24.Location = new System.Drawing.Point(6, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(157, 21);
            this.label24.TabIndex = 60;
            this.label24.Text = "关联外键表对象";
            // 
            // comboBox_fktable
            // 
            this.comboBox_fktable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_fktable.FormattingEnabled = true;
            this.comboBox_fktable.Location = new System.Drawing.Point(10, 189);
            this.comboBox_fktable.Name = "comboBox_fktable";
            this.comboBox_fktable.Size = new System.Drawing.Size(316, 20);
            this.comboBox_fktable.TabIndex = 69;
            this.comboBox_fktable.Visible = false;
            this.comboBox_fktable.SelectedIndexChanged += new System.EventHandler(this.comboBox_fktable_SelectedIndexChanged);
            // 
            // label_外键表名称
            // 
            this.label_外键表名称.AutoSize = true;
            this.label_外键表名称.Location = new System.Drawing.Point(95, 168);
            this.label_外键表名称.Name = "label_外键表名称";
            this.label_外键表名称.Size = new System.Drawing.Size(65, 12);
            this.label_外键表名称.TabIndex = 68;
            this.label_外键表名称.Text = "外键表名称";
            this.label_外键表名称.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 141);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 12);
            this.label25.TabIndex = 67;
            this.label25.Text = "字段关联类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_code);
            this.groupBox1.Controls.Add(this.radioButton_othertable);
            this.groupBox1.Location = new System.Drawing.Point(87, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 33);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // radioButton_code
            // 
            this.radioButton_code.AutoSize = true;
            this.radioButton_code.Location = new System.Drawing.Point(51, 12);
            this.radioButton_code.Name = "radioButton_code";
            this.radioButton_code.Size = new System.Drawing.Size(47, 16);
            this.radioButton_code.TabIndex = 63;
            this.radioButton_code.TabStop = true;
            this.radioButton_code.Text = "代码";
            this.radioButton_code.UseVisualStyleBackColor = true;
            this.radioButton_code.CheckedChanged += new System.EventHandler(this.radioButton_code_CheckedChanged);
            // 
            // radioButton_othertable
            // 
            this.radioButton_othertable.AutoSize = true;
            this.radioButton_othertable.Location = new System.Drawing.Point(130, 11);
            this.radioButton_othertable.Name = "radioButton_othertable";
            this.radioButton_othertable.Size = new System.Drawing.Size(83, 16);
            this.radioButton_othertable.TabIndex = 64;
            this.radioButton_othertable.TabStop = true;
            this.radioButton_othertable.Text = "其它表外键";
            this.radioButton_othertable.UseVisualStyleBackColor = true;
            this.radioButton_othertable.CheckedChanged += new System.EventHandler(this.radioButton_othertable_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 222);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(125, 12);
            this.label23.TabIndex = 62;
            this.label23.Text = "datagridview控件名称";
            // 
            // textBox_控件名称
            // 
            this.textBox_控件名称.Location = new System.Drawing.Point(139, 219);
            this.textBox_控件名称.Name = "textBox_控件名称";
            this.textBox_控件名称.Size = new System.Drawing.Size(210, 21);
            this.textBox_控件名称.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 62;
            this.label6.Text = "数据库类型：";
            // 
            // label_数据库类型
            // 
            this.label_数据库类型.AutoSize = true;
            this.label_数据库类型.Location = new System.Drawing.Point(121, 15);
            this.label_数据库类型.Name = "label_数据库类型";
            this.label_数据库类型.Size = new System.Drawing.Size(0, 12);
            this.label_数据库类型.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 64;
            this.label8.Text = "数据库地址：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(50, 64);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 65;
            this.label22.Text = "端口：";
            // 
            // label_地址
            // 
            this.label_地址.AutoSize = true;
            this.label_地址.Location = new System.Drawing.Point(117, 39);
            this.label_地址.Name = "label_地址";
            this.label_地址.Size = new System.Drawing.Size(0, 12);
            this.label_地址.TabIndex = 66;
            // 
            // label_端口
            // 
            this.label_端口.AutoSize = true;
            this.label_端口.Location = new System.Drawing.Point(121, 64);
            this.label_端口.Name = "label_端口";
            this.label_端口.Size = new System.Drawing.Size(0, 12);
            this.label_端口.TabIndex = 67;
            // 
            // label_filePath
            // 
            this.label_filePath.AutoSize = true;
            this.label_filePath.Location = new System.Drawing.Point(19, 397);
            this.label_filePath.Name = "label_filePath";
            this.label_filePath.Size = new System.Drawing.Size(41, 12);
            this.label_filePath.TabIndex = 68;
            this.label_filePath.Text = "label9";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox_tables);
            this.splitContainer1.Panel1.Controls.Add(this.label_filePath);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label_端口);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox_codeType);
            this.splitContainer1.Panel1.Controls.Add(this.label_地址);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label22);
            this.splitContainer1.Panel1.Controls.Add(this.button_generator);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label_数据库类型);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_ClassNamePrefix);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox_ClassRemark);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1_命名空间);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.button_openFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBox_fktable);
            this.splitContainer1.Panel2.Controls.Add(this.label24);
            this.splitContainer1.Panel2.Controls.Add(this.label_外键表名称);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_columnLength);
            this.splitContainer1.Panel2.Controls.Add(this.label25);
            this.splitContainer1.Panel2.Controls.Add(this.label20);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label21);
            this.splitContainer1.Panel2.Controls.Add(this.label23);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_columnRemark);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_控件名称);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_columnTempTableName);
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox_columns);
            this.splitContainer1.Panel2.Controls.Add(this.label_字段代码类型名称);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_columnType);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_columnCodeTypeName);
            this.splitContainer1.Panel2.Controls.Add(this.label18);
            this.splitContainer1.Panel2.Controls.Add(this.button_addColumn);
            this.splitContainer1.Panel2.Controls.Add(this.listBox_colum);
            this.splitContainer1.Size = new System.Drawing.Size(669, 628);
            this.splitContainer1.SplitterDistance = 311;
            this.splitContainer1.TabIndex = 69;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.richTextBox_code);
            this.panel1.Location = new System.Drawing.Point(675, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 628);
            this.panel1.TabIndex = 70;
            // 
            // CodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 628);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CodeGenerator_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip_listviewColumn.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_tables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_codeType;
        private System.Windows.Forms.Button button_generator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ClassNamePrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox_ClassRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1_命名空间;
        private System.Windows.Forms.ComboBox comboBox_columns;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button_addColumn;
        private System.Windows.Forms.ListBox listBox_colum;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_columnType;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_columnRemark;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_columnLength;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_columnTempTableName;
        private System.Windows.Forms.Label label_字段代码类型名称;
        private System.Windows.Forms.TextBox textBox_columnCodeTypeName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_listviewColumn;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox_控件名称;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_code;
        private System.Windows.Forms.RadioButton radioButton_othertable;
        private System.Windows.Forms.Label label_外键表名称;
        private System.Windows.Forms.ComboBox comboBox_fktable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_数据库类型;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label_地址;
        private System.Windows.Forms.Label label_端口;
        private System.Windows.Forms.Label label_filePath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
    }
}


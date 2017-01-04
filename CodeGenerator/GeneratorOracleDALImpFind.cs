using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorOracleDALImpFind
    {
        /// <summary>
        /// oracle生成DAL的查询关联对象的高级方法
        /// </summary>
        /// <param Name="TableName">表名称</param>
        /// <param Name="className">类名</param>
        /// <param Name="classRemark">类说明</param>
        /// <param Name="classRemark">参数类型</param>
        /// <param Name="paramName">参数名称</param>
        /// <returns>接口实现代码</returns>
        public static string generatorOracleDALImpFind(oracleDBHelper.DBTable table, string paramType, string paramName, List<oracleDBHelper.TempTabel> ttList, string datagridviewName)
        {
            List<oracleDBHelper.DBColumn> thisTableColumns = table.Columns;
            string tableName = table.TableName;

            StringBuilder sb = new StringBuilder();
            #region datagridview隐藏基本字段
            sb.Append(@"
        #region UI,datagridview隐藏基本字段，start
        //隱藏列start
        //" + datagridviewName + @".Columns[""ID""].Visible = false;//隐藏此列
        //" + datagridviewName + @".Columns[""ID""].HeaderText = ""主键"";//设置列名
        //" + datagridviewName + @".Columns[""ID""].DisplayIndex = 0;//列的顺序
        //" + datagridviewName + @".Columns[""CSRQ""].DefaultCellStyle.Format = Model.Common.AppConfigHelper.GetAppConfigFordateFormat();//从配置文件中取时间格式设置到此列（用于时间列）
            if (null == " + datagridviewName + @".DataSource)
            {
                return;
            }
");
            for (int i = 0; i < thisTableColumns.Count; i++)
            {
                oracleDBHelper.DBColumn column = thisTableColumns[i];
                if (column.ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
        " + datagridviewName + @".Columns[" + paramType + "." + column.ColumnName.ToUpper() + @"].Visible = false;//" + column.ColumnComments + @"
        " + datagridviewName + @".Columns[" + paramType + "." + column.ColumnName.ToUpper() + @"].HeaderText = """ + column.ColumnComments + @""";
        " + datagridviewName + @".Columns[" + paramType + "." + column.ColumnName.ToUpper() + @"].DefaultCellStyle.Format = Model.Common.AppConfigHelper.GetAppConfigFordateFormat();//从配置文件中取时间格式设置到此列（用于时间列）
");
                }
                else
                {
                    sb.Append(@"
        " + datagridviewName + @".Columns[" + paramType + "." + column.ColumnName.ToUpper() + @"].Visible = false;//" + column.ColumnComments + @"
        " + datagridviewName + @".Columns[" + paramType + "." + column.ColumnName.ToUpper() + @"].HeaderText = """ + column.ColumnComments + @""";
");
                }
            }

            sb.Append(@"
        #endregion UI,datagridview隐藏基本字段，end
");
            #endregion

            #region 实体类字段转换

            sb.Append(@"
        #region UI,新增的实体类字段转换（隐藏原来的英文字段，新增一个中文字段，中文字段中放入转换过后的值）
");
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                string columnComments = tt.Column.ColumnComments;
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
        SkinDataGridViewHelper.dataGridViewObjectConvertToValue(" + datagridviewName + @", """ + tt.ObjectName + @""", """ + tt.ObjectName + @"Name"", """ + columnComments + @""", CommonCode.实体类名称.基础_代码字典实体类名);
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    sb.Append(@"
        SkinDataGridViewHelper.dataGridViewObjectConvertToValue(" + datagridviewName + @", """ + tt.ObjectName + @""", """ + tt.ObjectName + @"Name"", """ + columnComments + @""", CommonCode.实体类名称." + tt.RelationTable.TableComment + @"实体类名);
");
                }
            }
            sb.Append(@"
        #endregion UI,新增的实体类字段转换（隐藏原来的英文字段，新增一个中文字段，中文字段中放入转换过后的值）
");

            sb.Append(@"
        #region UI,新增的实体类字段如果不想转换显示，可以直接隐藏，不必转换
");
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                string columnComments = tt.Column.ColumnComments;
                sb.Append(@"
        //" + datagridviewName + @".Columns[""" + tt.ObjectName + @"""].Visible = false;//" + columnComments + @"
");
            }

            sb.Append(@"
        #endregion UI,新增的实体类字段如果不想转换显示，可以直接隐藏，不必转换

        //控件结束时候处理的一些事件
        SkinDataGridViewHelper.endDataGridView(" + datagridviewName + @");

");
            #endregion

            #region 实体类中新增的相关联对象
            sb.Append(@"


        #region model,实体类新增的对象，start
        
");
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
        /// <summary>
        /// " + tt.Column.ColumnComments + @"对象
        /// </summary>
        private Model.JC.Jc_code " + tt.ObjectName + @";
        /// <summary>
        /// " + tt.Column.ColumnComments + @"对象
        /// </summary>
        public Model.JC.Jc_code " + tt.ObjectGetSetMethodName + @"
        {
            get { return " + tt.ObjectName + @"; }
            set { " + tt.ObjectName + @" = value; }
        }
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    sb.Append(@"
        /// <summary>
        /// " + tt.Column.ColumnComments + @"对象
        /// </summary>
        private Model." + Common.getNameSpace(tt.RelationTable.TableName, Common.fristCharToUpper(tt.RelationTable.TableName.ToLower())) + " " + tt.ObjectName + @";
        /// <summary>
        /// " + tt.Column.ColumnComments + @"对象
        /// </summary>
        public Model." + Common.getNameSpace(tt.RelationTable.TableName, Common.fristCharToUpper(tt.RelationTable.TableName.ToLower())) + " " + tt.ObjectGetSetMethodName + @"
        {
            get { return " + tt.ObjectName + @"; }
            set { " + tt.ObjectName + @" = value; }
        }
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }

            }
            sb.Append(@"
        #endregion model,实体类新增的对象，end
");
            #endregion
            #region BLL层接口
            sb.Append(@"
        #region BLL层的interface,接口，start

        /// <summary>
        /// 根据主键，得到一条记录以及它的相关联对象
        /// </summary>
        ///<param name=""id"">主键</param>
        " + paramType + @" getContactObjectById(string id);

        /// <summary>
        /// 得到所有关联对象,不带分页参数，显示根据条件得到的所有数据
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是模糊查询参数，提供模糊查询功能，只适用于string类型的字段
        /// 第三个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        ///<param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        List<object> getListContactObject(" + paramType + " " + paramName + @"," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);

        /// <summary>
        /// 得到所有关联对象，带分页参数，给分页参数指定pagesize得到第一页的数据或者不指定分页参数，系统使用默认的pagesize值
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是分页参数，如果page参数为null,方法则会new一个默认的page，根据page中的pagesize参数查询出相应的条数
        /// 第三个参数是模糊查询参数，提供模糊查询功能，只适用于string类型的字段
        /// 第四个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        ///<param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        List<object> getListContactObjectByPage(" + paramType + " " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);
       
        #endregion BLL层的interface,接口，end
");
            #endregion

            #region BLL层实现
            string dalVariableName = Common.fristCharToLower(paramType) + "Dal";//DAL的变量名
            sb.Append(@"
        #region BLL层的implement,实现，start

        /// <summary>
        /// 根据主键，得到一条记录以及它的相关联对象
        /// </summary>
        ///<param name=""id"">主键</param>
        public " + paramType + @" getContactObjectById(string id)
        {
            return " + dalVariableName + @".findContactObjectsById(id);
        }


        /// <summary>
        /// 得到所有关联对象,不带分页参数，显示根据条件得到的所有数据
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是模糊查询参数，提供模糊查询功能，只适用于string类型的字段
        /// 第三个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        ///<param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        public List<object> getListContactObject(" + paramType + " " + paramName + @"," + paramType + " " + paramName + @"Like,List<SortInfo> sortList)
        {
            return " + dalVariableName + @".findContactObjects(" + paramName + @",null," + paramName + @"Like,sortList);
        }

        /// <summary>
        /// 得到所有关联对象，带分页参数，给分页参数指定pagesize得到第一页的数据或者不指定分页参数，系统使用默认的pagesize值
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是分页参数，如果page参数为null,方法则会new一个默认的page，根据page中的pagesize参数查询出相应的条数
        /// 第三个参数是模糊查询参数，提供模糊查询功能，只适用于string类型的字段
        /// 第四个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        ///<param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        public List<object> getListContactObjectByPage(" + paramType + " " + paramName + @",Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList)
        {
            if (null==page)
            {
                page = new Model.Common.PageInfo();
                page.AllCount = " + dalVariableName + @".getCount(" + paramName + @"," + paramName + @"Like);
            }
            return " + dalVariableName + @".findContactObjects(" + paramName + @", page," + paramName + @"Like,sortList);
        }
");

            sb.Append(@"
        #endregion BLL层的implement,实现，end
");



            #endregion

            #region DAL层接口
            sb.Append(@"
        #region DAL层的interface,接口，start
        /// <summary>
        /// 查找指定ID的用户,得到相关联的外键对象的方法
        /// </summary>
        /// <remarks>如果id为null、id重复、id未找到就抛异常</remarks>
        /// <returns>" + paramType + @"</returns>
        " + paramType + @" findContactObjectsById(string id);
        
        /// <summary>
        /// 根据用户传入的参数查询得到相关联的外键对象方法（id参数排除在外，如需查询id请使用findById方法）
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是分页参数，传入带数据总行数的分页对象得到默认前PageSiz条数据（取决于page对象中的值），如果传null，则查出所有数据
        /// 第二个参数提供模糊查询功能，只适用于string类型的字段
        /// 第四个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        /// <param name=""" + paramName + @"""></param>
        /// <param name=""page"">分页对象</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>满足条件的所有" + paramType + @"对象集合list</returns>
        List<object> findContactObjects(" + paramType + " " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);
        
        
        #endregion DAL层的interface,接口，end
");
            #endregion

            #region DAL层实现
            sb.Append(@"
        #region DAL层的implement,实现，start

        /// <summary>
        /// 把dataset中的一行转换为model, 包括相关联的外键对象
        /// </summary>
        /// <param name=""row""></param>
        /// <returns></returns>
        private " + paramType + @" dataSetContactObjectsToModel(DataRow row)
        {
            " + paramType + " " + paramName + @" = this.dataSetToModel(row);
                  ");
            #region 循环外键表和CODE字段的dataset转为list

            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    string fkClassName = Common.fristCharToUpper(tt.RelationTable.TableName.ToLower());
                    string startEndString = Common.getStartEndString(tt.RelationTable.TableName.ToUpper());
                    oracleDBHelper.DBTable fkTable = tt.RelationTable;
                    sb.Append(@"
            #region 添加" + fkClassName + @"类别实体对象
            " + fkClassName + @" " + tt.ObjectName + @" = new " + fkClassName + @"();
            ");

                    for (int j = 0; j < fkTable.Columns.Count; j++)//处理字段类型
                    {
                        oracleDBHelper.DBColumn fkColumn = fkTable.Columns[j];
                        string fkColumnGetSet = Common.fristCharToUpper(fkColumn.ColumnName);
                        if (fkColumn.ColumnCSharpType == "string")//如果此字段的类型为string，使用Convert.toString().tirm()
                        {
                            sb.Append("" + tt.ObjectName + "." + fkColumnGetSet + @" = Convert.ToString(row[""" + startEndString + @"_" + fkColumn.ColumnName.ToUpper() + @"""]).Trim(); 
            ");
                        }
                        else if (fkColumn.ColumnIsNull == "y")//如果此字段可以为空，就使用convertDBNullValue转换
                        {
                            sb.Append("" + tt.ObjectName + "." + fkColumnGetSet + @" =  (" + fkColumn.ColumnCSharpType + @"?)this.convertDBNullValue(row[""" + startEndString + @"_" + fkColumn.ColumnName.ToUpper() + @"""]);
            ");//类型转换的判断
                        }
                        else//默认的
                        {
                            sb.Append("" + tt.ObjectName + "." + fkColumnGetSet + @" =  (" + fkColumn.ColumnCSharpType + @")row[""" + startEndString + @"_" + fkColumn.ColumnName.ToUpper() + @"""];
            ");//类型转换的判断
                        }
                    }
                    sb.Append(@"
            " + paramName + "." + tt.ObjectGetSetMethodName + @" = " + tt.ObjectName + @";
            #endregion 添加" + fkClassName + @"类别实体对象
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
             //添加代码类别实体对象," + tt.Column.ColumnComments + @"对象
            Jc_code " + tt.ObjectName + @" = new Jc_code();
            " + tt.ObjectName + @".Id = Convert.ToString(row[""" + tt.ColumnName + @"_ID""]).Trim();
            " + tt.ObjectName + @".Name = Convert.ToString(row[""" + tt.ColumnName + @"_NAME""]).Trim();
            " + tt.ObjectName + @".Value = Convert.ToString(row[""" + tt.ColumnName + @"_VALUE""]).Trim();
            " + tt.ObjectName + @".Fk_code_type = Convert.ToString(row[""" + tt.ColumnName + @"_FKCODETYPE""]).Trim();
            " + tt.ObjectName + @".Zfbz = Convert.ToString(row[""" + tt.ColumnName + @"_ZFBZ""]).Trim();
            " + tt.ObjectName + @".Remark = Convert.ToString(row[""" + tt.ColumnName + @"_REMARK""]).Trim();
            " + paramName + "." + Common.fristCharToUpper(tt.ObjectName) + @" = " + tt.ObjectName + @";
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }

            }
            #endregion 循环外键表和CODE字段的dataset转为list





            sb.Append(@"
            return " + paramName + @";
        }
");


            sb.Append(@"


        /// <summary>
        /// 查找指定ID的用户,得到相关联的外键对象的方法
        /// </summary>
        /// <remarks>如果id为null、id重复、id未找到就抛异常</remarks>
        /// <returns>" + paramType + @"</returns>
        public " + paramType + @" findContactObjectsById(string id)
        {
            if (""""==id||null==id)
            {
                throw new Exception(""主键ID不能为空，请赋值"");
            }
            StringBuilder sql = new StringBuilder(""SELECT A.* "");
            //新建方法findContactObjectsById，在此加入多表连接要显示的项start
            //sql.Append("",B.字段一,B.字段二,B.字段三"");
");
            #region 循环添加外键关联显示的字段
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
             sql.Append(@""
            ," + tt.TempTableName + ".ID AS " + tt.ColumnName + "_ID," + tt.TempTableName + ".NAME AS " + tt.ColumnName + "_NAME," + tt.TempTableName + ".VALUE AS " + tt.ColumnName + "_VALUE," + tt.TempTableName + ".FK_CODE_TYPE AS " + tt.ColumnName + "_FKCODETYPE," + tt.TempTableName + ".ZFBZ AS " + tt.ColumnName + "_ZFBZ," + tt.TempTableName + ".REMARK AS " + tt.ColumnName + @"_REMARK
            "");
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    string startEndString = Common.getStartEndString(tt.RelationTable.TableName.ToUpper());
                    sb.Append(@"
            sql.Append(@""");
                    oracleDBHelper.DBTable fkTale = tt.RelationTable;

                    for (int j = 0; j < fkTale.Columns.Count; j++)
                    {
                        oracleDBHelper.DBColumn fkColumn = fkTale.Columns[j];
                        sb.Append(@"
            ,TT_" + tt.RelationTable.TableName + "." + fkColumn.ColumnName.ToUpper() + " AS " + startEndString + "_" + fkColumn.ColumnName.ToUpper());//SQL语句，全部字母用大写
                    }
                    sb.Append(@"
            "");
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }

            }
            #endregion

            sb.Append(@"
            //新建方法，在此加入多表连接要显示的项end
            sql.Append("" FROM " + tableName + @" A "");
            //新建一个方法，在此加入多表连接的语句start
            //sql.Append(""LEFT JOIN 外键表名 B ON A.外键=B.ID"");
");
            #region 循环添加外键的连接语句
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];

                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
            sql.Append(@"" 
                        LEFT JOIN 
                        (SELECT X.* FROM JC_CODE X WHERE X.FK_CODE_TYPE IN (SELECT Y.ID FROM JC_CODE_TYPE Y WHERE Y.VALUE='"" + Model.Common.CommonCode.代码类型值." + tt.ColumnCodeTypeName + @" + @""')) " + tt.TempTableName + @"
                        ON A." + tt.ColumnName + @" = " + tt.TempTableName + @".VALUE
            "");
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    sb.Append(@"
            sql.Append(@""
            LEFT JOIN " + tt.RelationTable.TableName.ToUpper() + " TT_" + tt.RelationTable.TableName + @" 
            ON A." + tt.ColumnName + " = TT_" + tt.RelationTable.TableName.ToUpper() + @".ID
");

                    sb.Append(@"
            "");
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }
            }
            #endregion



            sb.Append(@"
            //新建一个方法，在此加入多表连接的语句end
            sql.Append("" WHERE A.ID=:id "");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Value = id;
            DataSet ds = OracleHelper.getDataSet(sql.ToString(), pId);
            DataRowCollection rows = ds.Tables[0].Rows;
            if (rows.Count > 1)
            {
                throw new Exception(""此ID的数据有多个，主键不允许重复，请检查数据"");
            }
            if (rows.Count == 0)
            {
                throw new Exception(""此ID的数据不存在，请检查数据"");
            }
            return this.dataSetContactObjectsToModel(rows[0]);
        }

        /// <summary>
        /// 根据用户传入的参数查询（id参数排除在外，如需查询id请使用findById方法）
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是分页参数，传入带数据总行数的分页对象得到默认前PageSiz条数据（取决于page对象中的值），如果传null，则查出所有数据
        /// 第三个参数提供模糊查询功能，只适用于string类型的字段
        /// 第四个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        /// <param name=""" + paramName + @"""></param>
        /// <param name=""page"">分页对象</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>满足条件的所有" + paramType + @"对象集合list</returns>
        public List<object> findContactObjects(" + paramType + " " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList)
        {
            //如果没有传查询条件参数，就new一个空的" + paramType + @"对象，标示查询所有数据
            if (null == " + paramName + @")
            {
                " + paramName + " = new " + paramType + @"();
            }
            if (null!=" + paramName + @".Id&&""""!=" + paramName + @".Id)
            {
                throw new Exception(""此方法不允许指定参数ID，如果需要根据ID查询数据，请调用findById方法；错误出自：find"");
            }

            List<" + paramType + "> " + paramName + "List = new List<" + paramType + @">();//接收查询出的" + paramType + @"对象集合
            List<OracleParameter> sqlparams = new List<OracleParameter>();//保存参数的集合
            StringBuilder sql = new StringBuilder(""SELECT A.* "");
            //新建方法findAllContactObjects(" + paramType + " " + paramName + @", Model.Common.PageInfo page)，在此加入多表连接要显示的项start
            //sql.Append("",B.字段一,B.字段二,B.字段三"");
");
            #region 循环添加外键关联显示的字段
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
             sql.Append(@""
            ," + tt.TempTableName + ".ID AS " + tt.ColumnName + "_ID," + tt.TempTableName + ".NAME AS " + tt.ColumnName + "_NAME," + tt.TempTableName + ".VALUE AS " + tt.ColumnName + "_VALUE," + tt.TempTableName + ".FK_CODE_TYPE AS " + tt.ColumnName + "_FKCODETYPE," + tt.TempTableName + ".ZFBZ AS " + tt.ColumnName + "_ZFBZ," + tt.TempTableName + ".REMARK AS " + tt.ColumnName + @"_REMARK
            "");
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    string startEndString = Common.getStartEndString(tt.RelationTable.TableName.ToUpper());

                    sb.Append(@"
            sql.Append(@""");
                    oracleDBHelper.DBTable fkTale = tt.RelationTable;

                    for (int j = 0; j < fkTale.Columns.Count; j++)
                    {
                        oracleDBHelper.DBColumn fkColumn = fkTale.Columns[j];
                        sb.Append(@"
            ,TT_" + tt.RelationTable.TableName + "." + fkColumn.ColumnName.ToUpper() + " AS " + startEndString + "_" + fkColumn.ColumnName.ToUpper());//SQL语句，全部字母用大写
                    }
                    sb.Append(@"
            "");
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }

            }
            #endregion

            sb.Append(@"
            //新建方法，在此加入多表连接要显示的项end
            sql.Append("" FROM " + tableName + @" A "");
            //新建一个方法，在此加入多表连接的语句start
            ////sql.Append("" LEFT JOIN 表名 B ON A.外键=B.ID"");
");

            #region 循环添加外键的连接语句
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];

                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
                {
                    sb.Append(@"
            sql.Append(@"" 
                        LEFT JOIN 
                        (SELECT X.* FROM JC_CODE X WHERE X.FK_CODE_TYPE IN (SELECT Y.ID FROM JC_CODE_TYPE Y WHERE Y.VALUE='"" + Model.Common.CommonCode.代码类型值." + tt.ColumnCodeTypeName + @" + @""')) " + tt.TempTableName + @"
                        ON A." + tt.ColumnName + @" = " + tt.TempTableName + @".VALUE
            "");
");
                }
                else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    sb.Append(@"
            sql.Append(@""
            LEFT JOIN " + tt.RelationTable.TableName.ToUpper() + " TT_" + tt.RelationTable.TableName + @" 
            ON A." + tt.ColumnName + " = TT_" + tt.RelationTable.TableName.ToUpper() + @".ID
");

                    sb.Append(@"
            "");
");
                }
                else
                {
                    throw new Exception("未知的字段关联类型");
                }
            }
            #endregion


            sb.Append(@"
            //新建一个方法，在此加入多表连接的语句end
            
            ");

            #region 循环主表的条件判断添加
            for (int i = 1; i < thisTableColumns.Count; i++)
            {
                if (thisTableColumns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
                if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + thisTableColumns[i].ColumnName.ToUpper() + "=:" + thisTableColumns[i].ColumnName + @" "");
                    OracleParameter p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" = new OracleParameter("":" + thisTableColumns[i].ColumnName + @""", OracleDbType." + thisTableColumns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @");
                }
                ");
                }
                else if (thisTableColumns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + thisTableColumns[i].ColumnName.ToUpper() + "=:" + thisTableColumns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" = new OracleParameter("":" + thisTableColumns[i].ColumnName + @""", OracleDbType." + thisTableColumns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
                if (null != " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" && """" != " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + thisTableColumns[i].ColumnName.ToUpper() + "=:" + thisTableColumns[i].ColumnName + @" "");
                    OracleParameter p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" = new OracleParameter("":" + thisTableColumns[i].ColumnName + @""", OracleDbType." + thisTableColumns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @");
                }
                ");
                }

            }
            #endregion//循环本表的条件判断

            string paramNameLike = paramName + "Like";//xxParamLike
            sb.Append(@"
            if(" + paramNameLike + @"!=null)
            {
");
            #region 循环添加主表的模糊查询的条件参数
            for (int i = 1; i < thisTableColumns.Count; i++)
            {
                if (thisTableColumns[i].ColumnCSharpType == "string")
                {
                    sb.Append(@"
                if (null != " + paramNameLike + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" && """" != " + paramNameLike + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + thisTableColumns[i].ColumnName.ToUpper() + " LIKE '%'||:" + thisTableColumns[i].ColumnName + @"||'%' "");
                    OracleParameter p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @" = new OracleParameter("":" + thisTableColumns[i].ColumnName + @""", OracleDbType." + thisTableColumns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @".Value = " + paramNameLike + "." + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @";");
                    sb.Append(@"
                    sqlparams.Add(p" + Common.fristCharToUpper(thisTableColumns[i].ColumnName) + @");
                }
                ");
                }

            }
            #endregion

            sb.Append(@"
            }
");

            #region 循环外键表的条件判断
            for (int i = 0; i < ttList.Count; i++)
            {
                oracleDBHelper.TempTabel tt = ttList[i];
                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
                {
                    oracleDBHelper.DBTable fkTable = tt.RelationTable;
                    string fkTableObjectName = paramName + "." + tt.ObjectGetSetMethodName;//外键表对象的完全限定名
                    sb.Append(@"
                if(null!=" + fkTableObjectName + @")
                {
");
                    for (int j = 0; j < fkTable.Columns.Count; j++)
                    {
                        oracleDBHelper.DBColumn fkColumn = fkTable.Columns[j];
                        string fkColumnGetSet = Common.fristCharToUpper(fkColumn.ColumnName.ToLower());
                        string columnObjectName = paramName + "." + tt.ObjectGetSetMethodName + "." + fkColumnGetSet;//字段的完全限定名
                        if (fkColumn.ColumnCSharpType == "DateTime")
                        {
                            sb.Append(@"
                    if (DateTime.MinValue != " + columnObjectName + " &&null != " + columnObjectName + @")
                    {
                        this.isAnd(sql);
                        this.isWhere(sql);
                        sql.Append("" TT_" + tt.RelationTable.TableName.ToUpper() + "." + fkColumn.ColumnName.ToUpper() + "=:" + fkColumn.ColumnName + @" "");
                        OracleParameter p" + Common.fristCharToUpper(fkColumn.ColumnName) + @" = new OracleParameter("":" + fkColumn.ColumnName + @""", OracleDbType." + fkColumn.ColumnODACParamType + @");");
                            sb.Append(@"
                    p" + Common.fristCharToUpper(fkColumn.ColumnName) + @".Value = " + columnObjectName + @";");
                            sb.Append(@"
                    sqlparams.Add(p" + Common.fristCharToUpper(fkColumn.ColumnName) + @");
                    }
                ");
                        }
                        else if (fkColumn.ColumnCSharpType == "decimal")
                        {
                            sb.Append(@"
                    if (null != " + columnObjectName + @")
                    {
                        this.isComma(sql);
                        sql.Append("" TT_" + tt.RelationTable.TableName.ToUpper() + "." + fkColumn.ColumnName.ToUpper() + "=:" + fkColumn.ColumnName + @" "");
                            OracleParameter p" + Common.fristCharToUpper(fkColumn.ColumnName) + @" = new OracleParameter("":" + fkColumn.ColumnName + @""", OracleDbType." + fkColumn.ColumnODACParamType + @");");
                            sb.Append(@"
                        p" + Common.fristCharToUpper(fkColumn.ColumnName) + @".Value = " + columnObjectName + @";");
                            sb.Append(@"
                        sqlparams.Add(p" + Common.fristCharToUpper(fkColumn.ColumnName) + @");

                    }
                        ");
                        }
                        else
                        {
                            sb.Append(@"
                    if (null != " + columnObjectName + " &&null != " + columnObjectName + @")
                    {
                        this.isAnd(sql);
                        this.isWhere(sql);
                        sql.Append("" TT_" + tt.RelationTable.TableName.ToUpper() + "." + fkColumn.ColumnName.ToUpper() + "=:" + fkColumn.ColumnName + @" "");
                        OracleParameter p" + Common.fristCharToUpper(fkColumn.ColumnName) + @" = new OracleParameter("":" + fkColumn.ColumnName + @""", OracleDbType." + fkColumn.ColumnODACParamType + @");");
                            sb.Append(@"
                    p" + Common.fristCharToUpper(fkColumn.ColumnName) + @".Value = " + columnObjectName + @";");
                            sb.Append(@"
                    sqlparams.Add(p" + Common.fristCharToUpper(fkColumn.ColumnName) + @");
                    }
                ");
                        }
                    }

                    sb.Append(@"
                }
");
                }
            }
            #endregion

            //            sb.Append(@"
            //            if(" + paramNameLike + @"!=null)
            //            {
            //");
            //            #region 循环添加外键表的模糊查询的条件参数
            //            for (int i = 0; i < ttList.Count; i++)
            //            {
            //                oracleDBHelper.TempTabel tt = ttList[i];
            //                if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
            //                {
            //                    oracleDBHelper.DBTable fkTable = tt.RelationTable;
            //                    string fkTableObjectName = paramName + "." + tt.ObjectGetSetMethodName;//外键表对象的完全限定名
            //                    sb.Append(@"
            //                if(null!=" + fkTableObjectName + @")
            //                {
            //");
            //                    for (int j = 0; j < fkTable.Columns.Count; j++)
            //                    {
            //                        oracleDBHelper.DBColumn fkColumn = fkTable.Columns[j];
            //                        string fkColumnGetSet = Common.fristCharToUpper(fkColumn.ColumnName.ToLower());
            //                        string columnObjectName = paramName + "." + tt.ObjectGetSetMethodName + "." + fkColumnGetSet;//字段的完全限定名

            //                        if(fkColumn.ColumnCSharpType=="string")
            //                        {
            //                            sb.Append(@"
            //                    if (null != " + columnObjectName + " &&null != " + columnObjectName + @")
            //                    {
            //                        this.isAnd(sql);
            //                        this.isWhere(sql);
            //                        sql.Append("" TT_" + tt.RelationTable.TableName.ToUpper() + "." + fkColumn.ColumnName.ToUpper() + " LIKE '%'||:" + fkColumn.ColumnName + @"||'%' "");
            //                        OracleParameter p" + Common.fristCharToUpper(fkColumn.ColumnName) + @" = new OracleParameter("":" + fkColumn.ColumnName + @""", OracleDbType." + fkColumn.ColumnODACParamType + @");");
            //                            sb.Append(@"
            //                    p" + Common.fristCharToUpper(fkColumn.ColumnName) + @".Value = " + columnObjectName + @";");
            //                            sb.Append(@"
            //                    sqlparams.Add(p" + Common.fristCharToUpper(fkColumn.ColumnName) + @");
            //                    }
            //                ");
            //                        }
            //                    }

            //                    sb.Append(@"
            //                }
            //");
            //                }
            //            }
            //            #endregion

            //            sb.Append(@"
            //            }
            //");

            sb.Append(@"
            this.b_isWhere = false;
            //排序
            this.addSort(sql, sortList);
            //*******分页start1*********如果分页对象为空，则全部数据查出，如果不为空
            List<object> returnList = new List<object>();//返回的List,索引0为List<" + paramType + @">对象,索引1为Model.Common.PageInfo分页信息对象
            if (null != page)
            {
                string sqlParam = sql.ToString();
                sql = new StringBuilder(@"" SELECT * FROM 
                (
                SELECT A.*, ROWNUM RN 
                FROM (""+sqlParam+@"") A 
                WHERE ROWNUM <= :maxIndex
                )
                WHERE RN >= :minIndex "");
                sqlparams.Add(new OracleParameter(""maxIndex"", page.MaxIndex));
                sqlparams.Add(new OracleParameter(""minIndex"", page.MinIndex));
            }
            //*******分页end1*********
            DataSet ds = OracleHelper.getDataSet(sql.ToString(), sqlparams.ToArray());
            DataRowCollection rows = ds.Tables[0].Rows;
            //把每一行数据转换为一个对象放入List<" + paramType + @">中
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i];
                " + paramName + @"List.Add(this.dataSetContactObjectsToModel(row));
                //" + paramName + @"List.Add(this.dataSetToModel(row));
            }
            
             //*******分页start2*********如果没有传入分页对象，则新建一个分页对象当做返回值
            if (null == page)
            {
                page = new Model.Common.PageInfo();
                page.AllCount = rows.Count;
            }
            returnList.Add(" + paramName + "List);//把List<" + paramType + @">放入List<object>中第一个
            returnList.Add(page);//把page对象放入List<object>中第二个
            //*******分页end2*********
            return returnList;
        }
        #endregion DAL层的implement,实现，end

        

");
            #endregion


            return sb.ToString();
        }

    }
}

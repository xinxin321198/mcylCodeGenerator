using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class Common
    {
        //内部使用的方法
        #region
        /// <summary>
        /// 字段是否为空，并且是否是值字段
        /// 
        /// </summary>
        /// <param Name="Column"></param>
        /// <returns>如果是，返回带问号的字段类型</returns>
        public static string getColumnIsNull(oracleDBHelper.DBColumn column)
        {
            //字段不为空，并且类型是值类型，就的加上问号，代表值类型可以为空
            if (column.ColumnIsNull == "y" && column.ColumnIsValueType)
            {
                return column.ColumnCSharpType + "?";
            }
            else
            {
                return column.ColumnCSharpType;
            }

        }

        /// <summary>
        /// 根据表名，以下划线分割，得到命名空间（JC/YW/RBAC），第二个参数是加到后面的命名空间
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="lastNameSpace"></param>
        /// <returns></returns>
        public static string getNameSpace(string tableName, string lastNameSpace)
        {
            string tablePrefix = tableName.Split('_')[0];
            if (lastNameSpace == null || lastNameSpace.Trim() == "")
            {
                return tablePrefix;
            }
            else
            {
                return tablePrefix + "." + lastNameSpace;
            }
            //if (tablePrefix == "JC")
            //{
            //    return "JC." + lastNameSpace;
            //}
            //else if (tablePrefix == "YW")
            //{
            //    return "YW." + lastNameSpace;
            //}
            //else if (tablePrefix == "RBAC")
            //{
            //    return "RBAC." + lastNameSpace;
            //}
            //else if (tablePrefix == "YPKC")
            //{
            //    return "YPKC" + lastNameSpace;
            //}
            //else
            //{
            //    throw new Exception("没有此种表前缀，不能生成命名空间（getNameSpace）");
            //}
        }

        /// <summary>
        /// 第一个字符转换为大写
        /// </summary>
        /// <param Name="s"></param>
        /// <returns></returns>
        public static string fristCharToUpper(string s)
        {
            return s.First().ToString().ToUpper() + s.Substring(1);
        }

        /// <summary>
        /// 第一个字符转换为小写
        /// </summary>
        /// <param Name="s"></param>
        /// <returns></returns>
        public static string fristCharToLower(string s)
        {
            return s.First().ToString().ToLower() + s.Substring(1);
        }


        /// <summary>
        /// 以某种符号分割一个columns的list对象
        /// </summary>
        /// <param Name="thisTableColumns">column的list对象</param>
        /// <returns>分割后的字符串</returns>
        public static string joinForColumns(string separator, List<sqlServer2kDBHelper.DBColumn> columns)
        {
            throw new Exception("SQLSERVER2000的joinForColumns方法，已作废");
            List<string> columnNames = new List<string>();
            for (int i = 0; i < columns.Count; i++)
            {
                columnNames.Add(columns[i].Name);
            }
            return string.Join(",", columnNames);
        }


        /// <summary>
        /// 以某种符号分割一个columns的list对象,并且根据最后的字符串参数的个数，按顺序替换数组中的值
        /// </summary>
        /// <param Name="separator">分隔符</param>
        /// <param Name="thisTableColumns">columns的list对象</param>
        /// <param Name="replaceValue">替换的字符串值</param>
        /// <returns>分割后的字符串</returns>
        public static string joinForColumns(string separator, List<sqlServer2kDBHelper.DBColumn> columns, params string[] replaceValues)
        {
            throw new Exception("SQLSERVER2000的joinForColumns方法，已作废");
            List<string> columnNames = new List<string>();
            for (int i = 0; i < columns.Count; i++)
            {
                columnNames.Add("@" + columns[i].Name);
            }
            for (int i = 0; i < replaceValues.Length; i++)
            {
                columnNames[i] = replaceValues[i];
            }
            return string.Join(",", columnNames);
        }




        /// <summary>
        /// 以某种符号分割一个columns的list对象
        /// </summary>
        /// <param Name="thisTableColumns">column的list对象</param>
        /// <returns>分割后的字符串</returns>
        public static string joinForColumns(string separator, string beforeSeparator, List<oracleDBHelper.DBColumn> columns)
        {
            List<string> columnNames = new List<string>();

            for (int i = 0; i < columns.Count; i++)
            {
                columnNames.Add(columns[i].ColumnName);
            }
            if (beforeSeparator != null)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columnNames[i] = beforeSeparator + columnNames[i];
                }
            }
            return string.Join(separator, columnNames);
        }


        /// <summary>
        /// 以某种符号分割一个columns的list对象,并且根据最后的字符串参数的个数，按顺序替换数组中的值
        /// </summary>
        /// <param Name="separator">分隔符</param>
        /// <param Name="thisTableColumns">columns的list对象</param>
        /// <param Name="replaceValue">替换的字符串值</param>
        /// <returns>分割后的字符串</returns>
        public static string joinForColumns(string separator, string beforeSeparator, List<oracleDBHelper.DBColumn> columns, params string[] replaceValues)
        {
            List<string> columnNames = new List<string>();
            if (beforeSeparator != null)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columnNames.Add(beforeSeparator + columns[i].ColumnName);
                }
            }
            for (int i = 0; i < replaceValues.Length; i++)
            {
                columnNames[i] = replaceValues[i];
            }
            return string.Join(separator, columnNames);
        }

        /// <summary>
        /// 把字段名转换为对象名
        /// 例如：xx_code转为xxCode
        /// </summary>
        /// <param Name="columnName"></param>
        /// <returns></returns>
        public static string columnNameConvertToObjectName(oracleDBHelper.TempTabel tt)
        {
            string objName = null;
            if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.代码类型)
            {
                objName = tt.ColumnName.ToLower().Replace("_code", "Code");
            }
            else if (tt.ColumnType == oracleDBHelper.TempTabel.字段外键类型.表外键类型)
            {
                objName = tt.RelationTable.TableName.ToLower();
            }
            else
            {
                throw new Exception("未知的字段关联类型");
            }
            return objName;
        }


        public static string getStartEndString(string s)
        {
            string returns = null;
            string starts = s.Substring(0, 1);
            string ends = s.Substring(s.Length - 1, 1);
            returns = starts + ends;
            return returns;
        }

        /// <summary>
        /// 拼接insert语句
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static StringBuilder addSqlInsert(List<oracleDBHelper.DBColumn> columns,string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            StringBuilder sql = new StringBuilder(""INSERT INTO " + tableName + "(");
            sb.Append(Common.joinForColumns(",", null, columns));
            sb.Append(") VALUES(");
            sb.Append(Common.joinForColumns(",", ":", columns));
            sb.Append(@")"");
            ");
            return sb;
        }

        /// <summary>
        /// 添加构建sql中的参数，无空值条件判断（单值的）
        ///       sql.Append(" ZFBZ=:zfbzValue ");
        ///       OracleParameter pZfbzValue = new OracleParameter(":zfbzValue", OracleDbType.NChar);
        ///       pZfbzValue.Value = jc_codeUpdate.Zfbz;
        ///       sqlparams.Add(pZfbzValue);
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static StringBuilder addParamInsertForSingle(List<oracleDBHelper.DBColumn> columns, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < columns.Count; i++)
            {
                sb.Append(@"
            OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                sb.Append(@"
            p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                sb.Append(@"
            p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                sb.Append(@"
            sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");");
            }
            return sb;
        }

        /// <summary>
        /// 添加构建sql中的参数，无空值条件判断（数组值的）
        ///       sql.Append(" ZFBZ=:zfbzValue ");
        ///       OracleParameter pZfbzValue = new OracleParameter(":zfbzValue", OracleDbType.NChar);
        ///       pZfbzValue.Value = jc_codeUpdate.Zfbz;
        ///       sqlparams.Add(pZfbzValue);
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static StringBuilder addParamInsertForArray(List<oracleDBHelper.DBColumn> columns, string paramNameList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < columns.Count; i++)
            {
                sb.Append(@"
            OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                sb.Append(@"
            p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                sb.Append(@"
            p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + columns[i].ColumnName + "List.ToArray();");
                sb.Append(@"
            sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");");
            }
            return sb;
        }

        /// <summary>
        /// 拼接update语句(单值的)
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static StringBuilder addSqlUpdateForSingle(List<oracleDBHelper.DBColumn> columns, string tableName, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            StringBuilder sql = new StringBuilder(""UPDATE " + tableName + @" SET "");");
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
            if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" &&  """"!= " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }

            }
            return sb;
        }

        /// <summary>
        /// 拼接update语句(数组值的)
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static StringBuilder addSqlUpdateForArray(List<oracleDBHelper.DBColumn> columns, string tableName, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            StringBuilder sql = new StringBuilder(""UPDATE " + tableName + @" SET "");");
            for (int i = 1; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = columns[i];
                sb.Append(@"
            this.isComma(sql);
            sql.Append("" " + column.ColumnName.ToUpper()+@"=:"+column.ColumnName+@" "");
            OracleParameter p"+Common.fristCharToUpper(column.ColumnName)+@" = new OracleParameter("":" + column.ColumnName + @""", OracleDbType."+column.ColumnODACParamType+@");
            p" + Common.fristCharToUpper(column.ColumnName) + @".Direction = ParameterDirection.Input;
            p" + Common.fristCharToUpper(column.ColumnName) + @".Value = "+columns[i].ColumnName+@"List.ToArray();
            sqlparams.Add(p" + Common.fristCharToUpper(column.ColumnName) + @");
");
            }
            return sb;
        }

        /// <summary>
        /// 声明参数数组
        /// List<string> idList = new List<string>();
        /// List<string> nameList = new List<string>();
        /// List<string> valueList = new List<string>();
        /// List<string> fk_code_typeList = new List<string>();
        /// List<string> zfbzList = new List<string>();
        /// List<string> remarkList = new List<string>();
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static StringBuilder addStatementParamArray(List<oracleDBHelper.DBColumn> columns)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = columns[i];
                sb.Append(@"
            List<" + Common.getColumnIsNull(column) + @"> " + column.ColumnName + "List = new List<" + Common.getColumnIsNull(column) + @">();
");
            }
            return sb;
        }

        /// <summary>
        /// 给参数数组赋值
        /// idList.Add(codeParam.Id);
        /// nameList.Add(codeParam.Name);
        /// valueList.Add(codeParam.Value);
        /// fk_code_typeList.Add(codeParam.Fk_code_type);
        /// zfbzList.Add(codeParam.Zfbz);
        /// remarkList.Add(codeParam.Remark);
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static StringBuilder addParamArrayAssignment(List<oracleDBHelper.DBColumn> columns, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < columns.Count; i++)
            {

                oracleDBHelper.DBColumn column = columns[i];
                sb.Append(@"
            " + column.ColumnName + "List.Add(" + paramName + @"." + Common.fristCharToUpper(column.ColumnName) + @");
");
                
            }
            return sb;
        }

        /// <summary>
        /// 添加构建sql中where条件下的参数，带判断是否为空值的(单值的)
        ///    if (null != jc_codeUpdate.Zfbz && "" != jc_codeUpdate.Zfbz)
        ///    {
        ///       this.isComma(sql);
        ///       sql.Append(" ZFBZ=:zfbzValue ");
        ///       OracleParameter pZfbzValue = new OracleParameter(":zfbzValue", OracleDbType.NChar);
        ///       pZfbzValue.Value = jc_codeUpdate.Zfbz;
        ///       sqlparams.Add(pZfbzValue);
        ///   }
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static StringBuilder addWhereParamAssignmentForSingle(List<oracleDBHelper.DBColumn> columns, string paramName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = columns[i];
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
            if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" &&  """"!= " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
            }

            return sb;
        }

        /// <summary>
        /// 添加构建sql中where条件下的参数，带判断是否为空值的（数组值的）
        ///    if (null != jc_codeUpdate.Zfbz && "" != jc_codeUpdate.Zfbz)
        ///    {
        ///       this.isComma(sql);
        ///       sql.Append(" ZFBZ=:zfbzValue ");
        ///       OracleParameter pZfbzValue = new OracleParameter(":zfbzValue", OracleDbType.NChar);
        ///       pZfbzValue.Value = jc_codeUpdate.Zfbz;
        ///       sqlparams.Add(pZfbzValue);
        ///   }
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static StringBuilder addWhereParamAssignmentForArray(List<oracleDBHelper.DBColumn> columns, string paramName,string paramNameList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = columns[i];
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
            if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" &&  """"!= " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + ".ToArray();");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
            }

            return sb;
        }
        #endregion



        /// <summary>
        /// 代码必要备注
        /// </summary>
        public const string myRemark = @"
    ///作者：罗新鑫
    ///联系电话：15087010221
    ///联系邮箱：362527240@qq.com
        ";

        /// <summary>
        /// 代码输出路径文件夹
        /// </summary>
        public const string filePath = "c:\\美创科技代码生成器生成代码\\";




        public class 数据库类型
        {
            public const string oracle数据库 = "oracle";

            public const string sqlserver2000数据库 = "sqlserver2000";
        }

        public class 生成代码类型
        {
            public const string Model实体类 = "Model数据库实体类";
            public const string 基本的DAL和BLL = "DAL层和BLL层的接口及实现";
            public const string 校验对象是否相等的方法 = "校验对象是否相等的方法";
            public const string 初始化分页控件的方法 = "初始化分页控件的方法";
            public const string 校验对象空值的方法 = "校验对象空值的方法";
            public const string 多表连接方法 = "多表连接方法";

        }

        /// <summary>
        /// 检查路径是否存在，如果不存在就创建
        /// </summary>
        /// <param name="path"></param>
        public static void createFile(string path)
        {
            if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
        }
    }
}

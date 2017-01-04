using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorCheckObjectIsNull
    {
        /// <summary>
        /// 生成校验对象是否有空值的方法
        /// </summary>
        /// <param Name="tableName">表名称</param>
        /// <returns></returns>
        public static string generatorCheckObjectIsNull(oracleDBHelper.DBTable table)
        {
            StringBuilder sb = new StringBuilder();
            List<oracleDBHelper.DBColumn> columns = table.Columns;
            string tableName = table.TableName;

            string className = Common.fristCharToUpper(tableName.ToLower());
            string variableName = tableName.ToLower();
            sb.Append(@"
        /// <summary>
        /// 校验" + className + @"类型的对象是否有空值（根据实际逻辑增减修改判断项）
        /// </summary>
        /// <param name=""" + className + @"""></param>
        private void check" + className + @"IsNull(" + className + @" " + variableName + @")
        {
            bool flag = false;
            StringBuilder sb = new StringBuilder();
");
            for (int i = 0; i < columns.Count; i++)
            {
                string columnName = Common.fristCharToUpper(columns[i].ColumnName);
                string columnComents = columns[i].ColumnComments;
                string columnType = columns[i].ColumnCSharpType;
                if ("DateTime" == columnType)
                {
                    sb.Append(@"
            //" + columnComents + @"
            if (null == " + variableName + @"." + columnName + @" ||DateTime.MinValue==" + variableName + @"." + columnName + @")
            {
                flag = true;
                sb.Append(""请指定‘" + columnComents + @"’的值\n"");
            }
");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            //" + columnComents + @"
            if (null == " + variableName + @"." + columnName + @"||0==" + variableName + "." + columnName + @")
            {
                flag = true;
                sb.Append(""请指定‘" + columnComents + @"’的值\n"");
            }
");
                }
                else
                {
                    sb.Append(@"
            //" + columnComents + @"
            if (null == " + variableName + @"." + columnName + @" ||""""==" + variableName + @"." + columnName + @")
            {
                flag = true;
                sb.Append(""请指定‘" + columnComents + @"’的值\n"");
            }
");
                }
            }

            sb.Append(@"
            if (flag)
            {
                throw new Exception(sb.ToString());
            }
        }
");
            return sb.ToString();
        }



    }
}

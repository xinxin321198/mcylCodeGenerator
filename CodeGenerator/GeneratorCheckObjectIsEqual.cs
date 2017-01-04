using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorCheckObjectIsEqual
    {

        /// <summary>
        /// 生成校验对象是否相等的方法（校验ID除外）
        /// </summary>
        /// <param Name="tableName">表名称</param>
        /// <returns></returns>
        public static string generatorCheckObjectIsEqual(oracleDBHelper.DBTable table)
        {
            List<oracleDBHelper.DBColumn> columns = table.Columns;
            string tableName = table.TableName;
            StringBuilder sb = new StringBuilder();

            string oldObjectName = "old" + Common.fristCharToUpper(tableName.ToLower());
            string newObjectName = "new" + Common.fristCharToUpper(tableName.ToLower());
            string className = Common.fristCharToUpper(tableName.ToLower());
            sb.Append(@"
        /// <summary>
        /// 比较" + className + @"类型的两个对象里的值是否相等，允不允许修改
        /// </summary>
        /// <param name=""" + oldObjectName + @"""></param>
        /// <param name=""" + newObjectName + @"""></param>
        /// <returns>相等true，不相等false</returns>
        private void check" + Common.fristCharToUpper(tableName.ToLower()) + @"IsEqual(" + className + @" " + oldObjectName + @", " + className + @" " + newObjectName + @")
        {
            bool flag = false;
");
            for (int i = 0; i < columns.Count; i++)
            {

                string columnName = columns[i].ColumnName;
                string columnComments = columns[i].ColumnComments;
                if ("id" == columnName)
                {
                    continue;
                }
                sb.Append(@"
            //" + columnComments + @"
            if (" + oldObjectName + @"." + Common.fristCharToUpper(columnName) + @" != " + newObjectName + "." + Common.fristCharToUpper(columnName) + @")
            {
                flag = true;
            }
");
            }
            sb.Append(@"
            if (!flag)
            {
                throw new Exception(""没有修改数据，不需要保存"");
            }
            return;
        }
");
            return sb.ToString();
        }
    }
}

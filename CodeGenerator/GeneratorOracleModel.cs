using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorOracleModel
    {
        /// <summary>
        /// oracle生成model代码
        /// </summary>
        /// <param Name="dataBaseType">数据库类型</param>
        /// <param Name="TableName">表名</param>
        /// <param Name="className">类名</param>
        /// <param Name="classRemark">类说明</param>
        /// <returns></returns>
        public static  string generatorOracleModel(string dataBaseType, oracleDBHelper.DBTable table, string className, string classRemark, string lastNameSpace)
        {
            List<oracleDBHelper.DBColumn> columns = table.Columns;
            string tableName = table.TableName;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model." + Common.getNameSpace(tableName, null) + @"
{
    /// <summary>
    ///" + dataBaseType + @"数据库
    ///" + tableName + @"表的实体类
    ///" + lastNameSpace + @"
    ///此类由代码生成器生成
    ///生成日期：" + DateTime.Now + @"
    /// " + classRemark + @"
" + Common.myRemark + @"
    /// </summary>
    public class " + className + @":Base.IBaseModel
        {

        /// <summary>
        /// 表备注
        /// </summary>
        public static readonly string 表备注 = """ + table.TableComment + @""";

        /// <summary>
        /// 数据库表名
        /// </summary>
        public static readonly string TABEL_NAME = """ + tableName.ToUpper() + @""";
");
            #region 循环添加静态字段
            for (int i = 0; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = new oracleDBHelper.DBColumn();
                column = columns[i];
                sb.Append(@"

        /// <summary>
        /// " + column.ColumnComments + @"(数据库字段名)
        /// </summary>
        public static readonly string " + column.ColumnName.ToUpper() + @" = """ + column.ColumnName.ToUpper() + @""";");
            }
            #endregion

            #region object转本model的方法
            sb.Append(@"
        /// <summary>
        /// 把object对象转换为" + className + @"对象
        /// </summary>
        /// <param name=""obj""></param>
        /// <returns></returns>
        public static Model." + Common.getNameSpace(tableName, null) + @"." + className + " ConvertTo" + className + @"(object obj)
        {
            if (null==obj)
            {
                return new Model." + Common.getNameSpace(tableName, null) + @"." + className + @"();
            }
            return (Model." + Common.getNameSpace(tableName, null) + @"." + className + @")obj;
        }
");
            #endregion

            #region 循环添加属性的方法
            for (int i = 0; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = new oracleDBHelper.DBColumn();
                column = columns[i];
                sb.Append(@"


        /// <summary>
        /// " + column.ColumnComments + @"（对象属性名）
        /// </summary>
        private " + Common.getColumnIsNull(column) + @" " + column.ColumnName + @";
        /// <summary>
        /// " + column.ColumnComments + @"（对象属性名）
        /// </summary>
        public " + Common.getColumnIsNull(column) + @" " + Common.fristCharToUpper(column.ColumnName) + @"
        {
            get { return this." + column.ColumnName + @"; }
            set { this." + column.ColumnName + @" = value; }
        }
");
            }
            sb.Append(@"
        //        //*****************在此之后添加关联对象
    }
}
");
            #endregion

            return sb.ToString();
        }


    }
}

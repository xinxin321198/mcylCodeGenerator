using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
namespace CodeGenerator
{
    public class oracleDBHelper
    {
        //private static string connectionString = ConfigurationManager.ConnectionStrings["oracleConnection"].ConnectionString.ToString().Trim();//从配置文件中读取数据库连接字符串

        public static List<DBTable> getTablesAndColumns(string connectionString, string DBName)
        {
            List<DBTable> tables = new List<DBTable>();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"select a.TABLE_NAME,a.TABLESPACE_NAME,b.comments from user_tables a left join  user_tab_comments b on a.TABLE_NAME=b.table_name where a.TABLESPACE_NAME ='"+DBName+"' ORDER BY A.TABLE_NAME";
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataRowCollection rows = ds.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        DBTable table = new DBTable();
                        table.TableName = row["TABLE_NAME"].ToString();
                        table.TableSpace = row["TABLESPACE_NAME"].ToString();
                        table.TableComment = row["COMMENTS"].ToString();
                        tables.Add(table);
                    }
                }
                
            }
            //for (int i = 0; i < tables.Count; i++)
            //{
            //    tables[i].Columns = getColumns(tables[i].TableName);
            //}


            return tables;
        }

        /// <summary>
        /// 根据表名得到表对象
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DBTable getTableByName(string connectionString, string DBName, string tableName)
        {
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"select a.TABLE_NAME,a.TABLESPACE_NAME,b.comments from user_tables a left join  user_tab_comments b on a.TABLE_NAME=b.table_name where a.TABLESPACE_NAME ='" + DBName + "' and a.TABLE_NAME='"+tableName+"'";
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataRowCollection rows = ds.Tables[0].Rows;
                    if (rows.Count>1)
                    {
                        throw new Exception("有多张同名的表："+tableName);
                    }
                    DataRow row = rows[0];
                    DBTable table = new DBTable();
                    table.TableName = row["TABLE_NAME"].ToString();
                    table.TableSpace = row["TABLESPACE_NAME"].ToString();
                    table.TableComment = row["COMMENTS"].ToString();
                    return table;
                }

            }
        }

        /// <summary>
        /// 复杂版,得到指定表的所有字段信息，字段信息转换为小写
        /// </summary>
        /// <param Name="TableName"></param>
        /// <returns></returns>
        public static List<DBColumn> getColumns(string connectionString, string tableName)
        {
            List<DBColumn> columns = new List<DBColumn>();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"select a.table_name,
                                       a.COLUMN_NAME,
                                       a.DATA_TYPE,
                                       a.DATA_LENGTH,
                                       a.NULLABLE,
                                       b.comments
                                  from dba_tab_columns A
                                  left join user_col_comments B
                                    on A.COLUMN_NAME = B.column_name
                                 where A.table_name = upper('"+tableName+@"')
                                   AND B.table_name = upper('"+tableName+@"')
                                 order by COLUMN_ID
                                ";
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataRowCollection rows = ds.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        DBColumn column = new DBColumn();
                        column.TableName = row["TABLE_NAME"].ToString().ToLower();
                        column.ColumnName = row["COLUMN_NAME"].ToString().ToLower();
                        column.ColumnCSharpType = OracleDbTypeMap.MapCsharpType(row["DATA_TYPE"].ToString().ToLower());
                        column.ColumnOracleType = row["DATA_TYPE"].ToString().ToLower();
                        column.ColumnODACParamType = OracleDbTypeMap.MapODACType(row["DATA_TYPE"].ToString().ToLower());
                        column.ColumnLength = row["DATA_LENGTH"].ToString().ToLower();
                        column.ColumnIsNull = row["NULLABLE"].ToString().ToLower();
                        column.ColumnIsValueType = OracleDbTypeMap.columnTypeIsValueType(column.ColumnCSharpType);
                        column.ColumnComments = row["COMMENTS"].ToString().ToLower();
                        columns.Add(column);
                    }
                }

            }

            return columns;
        }





        //数据库表类的定义
        #region
        

        /// <summary>
        /// 表对象
        /// </summary>
        public class DBTable
        {
            /// <summary>
            /// 表名
            /// </summary>
            private string tableName;
            /// <summary>
            /// 表名
            /// </summary>
            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
            }

            /// <summary>
            /// 表空间
            /// </summary>
            private string tableSpace;
            /// <summary>
            /// 表空间
            /// </summary>
            public string TableSpace
            {
                get { return tableSpace; }
                set { tableSpace = value; }
            }

            /// <summary>
            /// 表注释
            /// </summary>
            private string tableComment;
            /// <summary>
            /// 表注释
            /// </summary>
            public string TableComment
            {
                get { return tableComment; }
                set { tableComment = value; }
            }

            /// <summary>
            /// 表拥有的字段集合
            /// </summary>
            private List<DBColumn> columns;
            /// <summary>
            /// 表拥有的字段集合
            /// </summary>
            public List<DBColumn> Columns
            {
                get { return columns; }
                set { columns = value; }
            }

        }

        /// <summary>
        /// 列对象
        /// </summary>
        public class DBColumn
        {
            /// <summary>
            /// 字段所属表名(小写)
            /// </summary>
            private string tableName;
            /// <summary>
            /// 字段所属表名(小写)
            /// </summary>
            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
            }

            /// <summary>
            /// 字段名称(小写)
            /// </summary>
            private string columnName;
            /// <summary>
            /// 字段名称(小写)
            /// </summary>
            public string ColumnName
            {
                get { return columnName; }
                set { columnName = value; }
            }

           
            /// <summary>
            /// 字段c#的类型
            /// </summary>
            private string columnCSharpType;
            /// <summary>
            /// 字段类型
            /// </summary>
            public string ColumnCSharpType
            {
                get { return columnCSharpType; }
                set { columnCSharpType = value; }
            }

            /// <summary>
            ///  字段在oracle中的类型
            /// </summary>
            private string columnOracleType;
            /// <summary>
            ///  字段在oracle中的类型
            /// </summary>
            public string ColumnOracleType
            {
                get { return columnOracleType; }
                set { columnOracleType = value; }
            }

            /// <summary>
            /// 字段对应在ODAC中的类型
            /// </summary>
            private string columnODACParamType;
            /// <summary>
            /// 字段对应在ODAC中的类型
            /// </summary>
            public string ColumnODACParamType
            {
                get { return columnODACParamType; }
                set { columnODACParamType = value; }
            }

            /// <summary>
            /// 字段长度
            /// </summary>
            private string columnLength;
            /// <summary>
            /// 字段长度
            /// </summary>
            public string ColumnLength
            {
                get { return columnLength; }
                set { columnLength = value; }
            }

            /// <summary>
            /// 字段是否可以为空
            /// </summary>
            private string columnIsNull;
            /// <summary>
            /// 字段是否可以为空
            /// </summary>
            public string ColumnIsNull
            {
                get { return columnIsNull; }
                set { columnIsNull = value; }
            }

            /// <summary>
            /// 是否是值类型
            /// </summary>
            private bool columnIsValueType;

            /// <summary>
            /// 是否是值类型
            /// </summary>
            public bool ColumnIsValueType
            {
                get { return columnIsValueType; }
                set { columnIsValueType = value; }
            }

           


            /// <summary>
            /// 字段说明
            /// </summary>
            private string columnComments;
            /// <summary>
            /// 字段说明
            /// </summary>
            public string ColumnComments
            {
                get { return columnComments; }
                set { columnComments = value; }
            }






 
        }

        /// <summary>
        /// 生成关联代码时候用的code临时表对象
        /// </summary>
        public class TempTabel
        {

            public  enum 字段外键类型
            {
                代码类型 = 0,
                表外键类型 = 1
            }


            /// <summary>
            /// 临时表名称
            /// </summary>
            private string tempTableName;
            /// <summary>
            /// 临时表名称
            /// </summary>
            public string TempTableName
            {
                get { return tempTableName ; }
                set { tempTableName = value; }
            }

            /// <summary>
            /// Model.Common.代码类型值 中的代码类型名称
            /// </summary>
            private string columnCodeTypeName;
            /// <summary>
            /// Model.Common.代码类型值 中的代码类型名称
            /// </summary>
            public string ColumnCodeTypeName
            {
                get { return columnCodeTypeName; }
                set { columnCodeTypeName = value; }
            }

            /// <summary>
            /// 字段对应的对象名称
            /// </summary>
            private string objectName;
            /// <summary>
            /// 字段对应的对象名称
            /// </summary>
            public string ObjectName
            {
                get { return objectName; }
                set { objectName = value; }
            }

            /// <summary>
            /// 字段对应的对象的getset方法名
            /// </summary>
            private string objectGetSetMethodName;
            /// <summary>
            /// 字段对应的对象的getset方法名
            /// </summary>
            public string ObjectGetSetMethodName
            {
                get { return objectGetSetMethodName; }
                set { objectGetSetMethodName = value; }
            }

            /// <summary>
            /// 字段名称
            /// </summary>
            public string ColumnName
            {
                get { return this.Column.ColumnName.ToUpper(); }
                set { this.Column.ColumnName = value; }
            }

            /// <summary>
            /// 相关联的外键表对象
            /// </summary>
            private DBTable relationTable;


            /// <summary>
            /// 相关联的外键表对象
            /// </summary>
            public DBTable RelationTable
            {
                get { return relationTable; }
                set { relationTable = value; }
            }

            /// <summary>
            /// 字段
            /// </summary>
            private DBColumn column;

            /// <summary>
            /// 字段对象
            /// </summary>
            public DBColumn Column
            {
                get { return column; }
                set { column = value; }
            }

            /// <summary>
            /// 字段的外键类型，根据此类型生成相应的代码
            /// </summary>
            private 字段外键类型 columnType;
            /// <summary>
            /// 字段的外键类型，根据此类型生成相应的代码
            /// </summary>
            public 字段外键类型 ColumnType
            {
                get { return columnType; }
                set { columnType = value; }
            }


        }
        


        #endregion

        
        #region
        public class OracleDbTypeMap
        {
            /// <summary>
            /// oracle对应的字段类型转换为c#中的类型
            /// </summary>
            /// <param Name="dbtype"></param>
            /// <returns></returns>
            public static string MapCsharpType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToUpper();
                string csharpType = "object";
                switch (dbtype)
                {
                    case "BFILE": csharpType = "byte[]"; break;//C#中的byte[]对应System.Byte
                    case "BLOB": csharpType = "byte[]"; break;//C#中的byte[]对应System.Byte
                    case "CHAR": csharpType = "string"; break;
                    case "CLOB": csharpType = "string"; break;
                    case "DATE": csharpType = "DateTime"; break;
                    case "FLOAT": csharpType = "decimal"; break;//C#中的decimal对应System.Decimal
                    case "INTEGER": csharpType = "decimal"; break;//C#中的decimal对应System.Decimal
                    case "INTERVAL YEAR TO MONTH": csharpType = "int"; break;//C#中的int对应System.Int32
                    case "INTERVAL DAY TO SECOND": csharpType = "TimeSpan"; break;
                    case "LONG": csharpType = "string"; break;
                    case "LONG RAW": csharpType = "byte[]"; break;//C#中的byte[]对应System.Byte
                    case "NCHAR": csharpType = "string"; break;
                    case "NCLOB": csharpType = "string"; break;
                    case "NUMBER": csharpType = "decimal"; break;//C#中的decimal对应System.Decimal
                    case "NVARCHAR2": csharpType = "string"; break;
                    case "RAW": csharpType = "byte[]"; break;//C#中的byte[]对应System.Byte
                    case "ROWID": csharpType = "string"; break;
                    case "TIMESTAMP": csharpType = "DateTime"; break;
                    case "VARCHAR2": csharpType = "string"; break;
                    default: csharpType = "object"; break;
                }
                return csharpType;
            }

            /// <summary>
            /// oracle中的字段类型转换为Oracle.DataAccess.Client.OracleDbType中的类型
            /// </summary>
            /// <param Name="dbtype"></param>
            /// <returns></returns>
            public static string MapODACType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToUpper();
                string oracleDbTypeName = null;
                switch (dbtype)
                {
                    case "BFILE": oracleDbTypeName = "BFile"; break;
                    case "BLOB": oracleDbTypeName = "Blob"; break;
                    case "CHAR": oracleDbTypeName = "Char"; break;
                    case "CLOB": oracleDbTypeName = "Clob"; break;
                    case "DATE": oracleDbTypeName = "Date"; break;
                    case "INTEGER": oracleDbTypeName = "Int32"; break;
                    case "INTERVAL YEAR TO MONTH": oracleDbTypeName = "IntervalYM"; break;
                    case "INTERVAL DAY TO SECOND": oracleDbTypeName = "IntervalDS"; break;
                    case "LONG": oracleDbTypeName = "Long"; break;
                    case "LONG RAW": oracleDbTypeName = "LongRaw"; break;
                    case "NCHAR": oracleDbTypeName = "NChar"; break;
                    case "NCLOB": oracleDbTypeName = "NClob"; break;
                    case "NUMBER": oracleDbTypeName = "Decimal"; break;
                    case "NVARCHAR2": oracleDbTypeName = "NVarchar2"; break;
                    case "RAW": oracleDbTypeName = "Raw"; break;
                    case "ROWID": oracleDbTypeName = "Varchar2"; break;
                    case "TIMESTAMP": oracleDbTypeName = "TimeStamp"; break;
                    case "VARCHAR2": oracleDbTypeName = "Varchar2"; break;
                    default: oracleDbTypeName = "尚不识别的oracle类型，请到oracleDBHelper的OracleDbTypeMap类中的MapOracleType方法中添加对应的转换"; break;
                }
                return oracleDbTypeName;
            }

            //类型是否是值类型，值类型不允许为空，引用类型可以为空
            public static bool columnTypeIsValueType(string csharpType)
            {
                bool flag = false;
                if (csharpType == null || "" == csharpType)
                {
                    throw new Exception("判断字符类型是否是C#中的值类型，字符不能为空");
                }
                switch (csharpType)
                {
                    case "int": flag = true;break;
                    case "byte[]": flag = true;break;
                    case "decimal": flag = true; break;
                    case "DateTime": flag = true; break;
                    default: flag = false; break;
                }
                return flag;
            }

        }
        #endregion
    }
}

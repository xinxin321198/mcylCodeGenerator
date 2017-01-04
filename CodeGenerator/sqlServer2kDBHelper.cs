using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace CodeGenerator
{
    //[Obsolete("sqlserver2000的代码生成类，不再使用，数据库已换成oracle，请使用oracleDBHelper", false)]
    public class sqlServer2kDBHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["sqlServerConnection"].ConnectionString.ToString().Trim();//从配置文件中读取数据库连接字符串

        public static List<DBTable> getTables(string DBName)
        {
            List<DBTable> tables = new List<DBTable>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"Select Name FROM "+DBName+"..SysObjects Where XType='U' orDER BY Name";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataRowCollection rows = ds.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        DBTable table = new DBTable();
                        table.TableName = row["Name"].ToString();
                        tables.Add(table);
                    }
                }
                
            }

            return tables;
        }

        /// <summary>
        /// 复杂版
        /// </summary>
        /// <param Name="TableName"></param>
        /// <returns></returns>
        public static List<DBColumn> getColumns(string tableName)
        {
            List<DBColumn> columns = new List<DBColumn>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                            [TableName] = i_s.TABLE_NAME, 
                                            [ColumnName] = i_s.COLUMN_NAME,
                                            [Description] = s.value ,
                                            [ColumnType] = i_s.DATA_TYPE
                                        FROM 
                                            INFORMATION_SCHEMA.COLUMNS i_s 
                                        LEFT OUTER JOIN 
                                            sysproperties s 
                                        ON 
                                            s.id = OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME) 
                                            AND s.smallid = i_s.ORDINAL_POSITION 
                                            AND s.name = 'MS_Description' 
                                        WHERE 
                                            OBJECTPROPERTY(OBJECT_ID(i_s.TABLE_SCHEMA+'.'+i_s.TABLE_NAME), 'IsMsShipped')=0 
                                             AND i_s.TABLE_NAME = '"+tableName+@"' 
                                        ORDER BY 
                                            i_s.TABLE_NAME, i_s.ORDINAL_POSITION";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataRowCollection rows = ds.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        DBColumn column = new DBColumn();
                        column.TableName = row["TableName"].ToString();
                        column.Name = row["ColumnName"].ToString();
                        column.Description = row["Description"].ToString();
                        column.Type = SqlServerDbTypeMap.MapCsharpType(row["ColumnType"].ToString());
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
       //[Obsolete("sqlserver2000的表对象，不再使用，数据库已换成oracle，请使用oracleDBHelper", false)]
        public class DBTable
        {
            private string tableName;

            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
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
        //[Obsolete("sqlserver2000的表字段类，不再使用，数据库已换成oracle，请使用oracleDBHelper", false)]
        public class DBColumn
        {
            /// <summary>
            /// 字段所属表名
            /// </summary>
            private string tableName;

            public string TableName
            {
                get { return tableName; }
                set { tableName = value; }
            }

            


            /// <summary>
            /// 字段名称
            /// </summary>
            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            /// <summary>
            /// 字段类型
            /// </summary>
            private string type;

            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            /// <summary>
            /// 字段说明
            /// </summary>
            private string description;

            public string Description
            {
                get { return description; }
                set { description = value; }
            }



 
        }


        #endregion

        #region
        public class SqlServerDbTypeMap
        {
            public static string MapCsharpType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return dbtype;
                dbtype = dbtype.ToLower();
                string csharpType = "object";
                switch (dbtype)
                {
                    case "bigint": csharpType = "long"; break;
                    case "binary": csharpType = "byte[]"; break;
                    case "bit": csharpType = "bool"; break;
                    case "char": csharpType = "string"; break;
                    case "date": csharpType = "DateTime"; break;
                    case "datetime": csharpType = "DateTime"; break;
                    case "datetime2": csharpType = "DateTime"; break;
                    case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                    case "decimal": csharpType = "decimal"; break;
                    case "float": csharpType = "double"; break;
                    case "image": csharpType = "byte[]"; break;
                    case "int": csharpType = "int"; break;
                    case "money": csharpType = "decimal"; break;
                    case "nchar": csharpType = "string"; break;
                    case "ntext": csharpType = "string"; break;
                    case "numeric": csharpType = "decimal"; break;
                    case "nvarchar": csharpType = "string"; break;
                    case "real": csharpType = "Single"; break;
                    case "smalldatetime": csharpType = "DateTime"; break;
                    case "smallint": csharpType = "short"; break;
                    case "smallmoney": csharpType = "decimal"; break;
                    case "sql_variant": csharpType = "object"; break;
                    case "sysname": csharpType = "object"; break;
                    case "text": csharpType = "string"; break;
                    case "time": csharpType = "TimeSpan"; break;
                    case "timestamp": csharpType = "byte[]"; break;
                    case "tinyint": csharpType = "byte"; break;
                    case "uniqueidentifier": csharpType = "Guid"; break;
                    case "varbinary": csharpType = "byte[]"; break;
                    case "varchar": csharpType = "string"; break;
                    case "xml": csharpType = "string"; break;
                    default: csharpType = "object"; break;
                }
                return csharpType;
            }

            public static Type MapCommonType(string dbtype)
            {
                if (string.IsNullOrEmpty(dbtype)) return Type.Missing.GetType();
                dbtype = dbtype.ToLower();
                Type commonType = typeof(object);
                switch (dbtype)
                {
                    case "bigint": commonType = typeof(long); break;
                    case "binary": commonType = typeof(byte[]); break;
                    case "bit": commonType = typeof(bool); break;
                    case "char": commonType = typeof(string); break;
                    case "date": commonType = typeof(DateTime); break;
                    case "datetime": commonType = typeof(DateTime); break;
                    case "datetime2": commonType = typeof(DateTime); break;
                    case "datetimeoffset": commonType = typeof(DateTimeOffset); break;
                    case "decimal": commonType = typeof(decimal); break;
                    case "float": commonType = typeof(double); break;
                    case "image": commonType = typeof(byte[]); break;
                    case "int": commonType = typeof(int); break;
                    case "money": commonType = typeof(decimal); break;
                    case "nchar": commonType = typeof(string); break;
                    case "ntext": commonType = typeof(string); break;
                    case "numeric": commonType = typeof(decimal); break;
                    case "nvarchar": commonType = typeof(string); break;
                    case "real": commonType = typeof(Single); break;
                    case "smalldatetime": commonType = typeof(DateTime); break;
                    case "smallint": commonType = typeof(short); break;
                    case "smallmoney": commonType = typeof(decimal); break;
                    case "sql_variant": commonType = typeof(object); break;
                    case "sysname": commonType = typeof(object); break;
                    case "text": commonType = typeof(string); break;
                    case "time": commonType = typeof(TimeSpan); break;
                    case "timestamp": commonType = typeof(byte[]); break;
                    case "tinyint": commonType = typeof(byte); break;
                    case "uniqueidentifier": commonType = typeof(Guid); break;
                    case "varbinary": commonType = typeof(byte[]); break;
                    case "varchar": commonType = typeof(string); break;
                    case "xml": commonType = typeof(string); break;
                    default: commonType = typeof(object); break;
                }
                return commonType;
            }
        }
        #endregion
    }
}

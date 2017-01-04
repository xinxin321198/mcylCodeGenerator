using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class ConnectionInfo
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        private string dbType;

        public string DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// 数据库IP地址
        /// </summary>
        private string dbIp;

        public string DbIp
        {
            get { return dbIp; }
            set { dbIp = value; }
        }

        /// <summary>
        /// 数据库端口
        /// </summary>
        private string prot;

        public string Prot
        {
            get { return prot; }
            set { prot = value; }
        }

        /// <summary>
        /// oracl数据库实例名
        /// </summary>
        private string instanceName;

        public string InstanceName
        {
            get { return instanceName; }
            set { instanceName = value; }
        }



        /// <summary>
        /// 数据库名称
        /// </summary>
        private string dbName;

        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        /// <summary>
        /// 数据库用户名
        /// </summary>
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 数据库密码
        /// </summary>
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }


        /// <summary>
        /// 得到连接字符串
        /// </summary>
        /// <returns></returns>
        public string getConnectionString()
        {
            string connectionString = null;
            if (dbType == Common.数据库类型.oracle数据库)
            {
                connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+dbIp+")(PORT="+prot+")))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME="+instanceName+")));User Id="+userName+";Password="+passWord+";";
            }
            else if (dbType == Common.数据库类型.sqlserver2000数据库)
            {
                //connectionString = "Data Source=" + dbIp + "; Initial Catalog=mchis;User ID=sa;Password=sa.jcsoft;";
                connectionString = "Data Source=" + dbIp + "; Initial Catalog="+dbName+";User ID="+userName+";Password="+passWord+";";
            }
            else
            {
                throw new Exception("没有实现"+dbType+"类型的数据库");
            }
            return connectionString;
        }
    }
}

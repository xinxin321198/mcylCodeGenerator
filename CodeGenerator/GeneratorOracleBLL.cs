using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorOracleBLL
    {
        /// <summary>
        /// oracle生成BLL的interface代码
        /// </summary>
        /// <param Name="TableName">表名称</param>
        /// <param Name="className">类名</param>
        /// <param Name="classRemark">类说明</param>
        /// <param Name="classRemark">参数类型</param>
        /// <param Name="paramName">参数名称</param>
        /// <returns>接口代码</returns>
        public static string generatorOracleBLL(string dataBaseType, oracleDBHelper.DBTable table, string className, string classRemark, string paramType, string paramName, string lastNameSpace)
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
using System.Transactions;//引入事务
using Oracle.DataAccess.Client;//引入oracle驱动
using Model." + Common.getNameSpace(tableName, null) + @";
using Model.Common;

namespace BLL." + Common.getNameSpace(tableName, lastNameSpace) + @"
{
    /// <summary>
    ///业务逻辑层的接口interface
    ///此类由代码生成器生成
    ///生成日期：" + DateTime.Now + @"
    /// " + classRemark + @"
" + Common.myRemark + @"
    ///如果使用到事务
    ///引入命名空间using System.Transactions;
    ///Windows服务中开启MSDTC（Distributed Transaction Coordinator）
    ///数据库客户端和数据库服务器都必须开启此服务
    /// //如果使用到事务,如下例子
    /*  bool flag1 = false;//指示第一个数据访问是否成功
        bool flag2 = false;//指示第二个数据访问是否成功
        bool flagResul = false//最终返回的结果
            ;
        using (TransactionScope ts = new TransactionScope())//创建一个事务对象
        {
            using (OracleConnection con = DAL.Common.OracleHelper.getConnection())//在这个事务对象中创建OracleConnection，使用事务时必须得使用同一个OracleConnection，否则达不到数据完整性的要求
            {
                //删除type
                flag1 = xxxDal.delete(xxxParam.Id, con);//调用数据访问层的方法
                //删除属于此type类型的所有code
                flag2 = xxxDal.delete(xxxParam.Id, con);//调用数据访问层的方法
                if (flag1 && flag2)
                {
                    flagResul = true;
                    ts.Complete();//执行成功之后提交整个事务
                }
                return flagResul;
            }
        }
     */
    /// </summary>
    public interface " + className + @"BLL
    {

        /// <summary>
        /// 单条记录保存方法，有ID为修改，没有ID为新增
        /// </summary>
        /// <param name=""" + paramName + @""">保存的对象</param>
        /// <returns>bool,是否成功</returns>
        bool save(" + paramType + @" " + paramName + @");

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name=""" + paramName + @"""></param>
        /// <returns>bool,是否成功</returns>
        bool inertList(List<" + paramType + @"> " + paramName + @"List);

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name=""id"">主键ID</param>
        /// <returns>bool,是否成功</returns>
        bool deleteById(string id);

        /// <summary>
        /// 根据ID的集合删除多条记录
        /// </summary>
        /// <param name=""idList"">id的集合</param>
        /// <returns>bool,是否成功</returns>
        bool deleteByIdList(List<string> idList);

        /// <summary>
        /// 批量修改（每个对象必须有ID）
        /// </summary>
        /// <param name=""" + paramName + @"List"">要修改的数据集合</param>
        /// <returns>bool,是否成功</returns>
        bool updateByIdList(List<" + paramType + @"> " + paramName + @"List);

        /// <summary>
        /// 根据参数条件，得到满足条件的" + tableName + @"的所有对象列表
        /// 第一个查询参数，如果查询参数为null则查询所有
        /// 第二个参数提供模糊查询功能，只适用于string类型的字段
        /// 第三个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        ///<param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        ///<param name=""sortList"">排序参数对象的集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        List<object> getList(" + paramType + " " + paramName + @"," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);

        /// <summary>
        /// 根据参数条件，得到满足条件的" + tableName + @"的所有对象列表（使用分页的方式）
        /// 第一个参数是查询参数，如果查询参数为null则查询所有
        /// 第二个参数是分页参数，如果page参数为null,方法则会new一个默认的page，根据page中的pagesize参数查询出相应的条数
        /// 第三个参数是模糊查询参数，提供模糊查询功能，只适用于string类型的字段
        /// 第四个参数是排序的参数，传入排序的对象的集合，如果传null，则默认使用ID字段进行排序
        /// </summary>
        /// <param name=""" + paramName + @""">查询参数</param>
        ///<param name=""" + paramName + @"Like"">模糊查询参数</param>
        /// <param name=""page"">分页对象</param>
        ///<param name=""sortList"">排序对象的参数集合</param>
        /// <returns>List<object>,索引0为数据，索引1为分页对象</returns>
        List<object> getListByPage(" + paramType + @" " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);

        /// <summary>
        /// 根据主键得到一条记录
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        " + paramType + @" getById(string id);

        /// <summary>
        /// 根据条件得到记录数量
        /// 第二个条件是模糊查询的条件
        /// </summary>
        /// <param name=""" + paramName + @"""></param>
        /// <returns></returns>
        int getCount(" + paramType + @" " + paramName + @"," + paramType + " " + paramName + @"Like);

        //*******************在此以后添加新的方法start

        //*******************在此以后添加新的方法end
    }
}
");
            return sb.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorOracleDAL
    {
        /// <summary>
        /// oracle生成DAL的interface代码
        /// </summary>
        /// <param Name="TableName">表名称</param>
        /// <param Name="className">类名</param>
        /// <param Name="classRemark">类说明</param>
        /// <param Name="classRemark">参数类型</param>
        /// <param Name="paramName">参数名称</param>
        /// <returns>接口代码</returns>
        public static string generatorOracleDAL(string dataBaseType, oracleDBHelper.DBTable table, string className, string classRemark, string paramType, string paramName, string lastNameSpace)
        {
            List<oracleDBHelper.DBColumn> columns = table.Columns;
            string tableName = table.TableName;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;//引入事务
using Oracle.DataAccess.Client;//引入oracle驱动
using Model.Common;
using Model." + Common.getNameSpace(tableName, null) + @";

namespace DAL." + Common.getNameSpace(tableName, lastNameSpace) + @"
{
    /// <summary>
    ///数据库访问层的接口interface
    ///此类由代码生成器生成
    ///表名：" + tableName + @"
    ///生成日期：" + DateTime.Now + @"
    /// " + classRemark + @"
" + Common.myRemark + @"
    /// </summary>
    public interface " + className + @"DAL
    {


        /// <summary>
        /// 插入一条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 主键为null时，自动生成主键guid，一般不直接调用此方法，请调用save方法进行单条数据的插入和修改       
        /// </summary>
        /// <param name=""" + paramName + @""">要插入的" + paramType + @"对象</param>
        /// <param name=""con"">Oracle连接对象</param>
        /// <returns>是否添加成功（影响的行数为1时成功）</returns>
        bool insert(" + paramType + " " + paramName + @",OracleConnection con);

        /// <summary>
        /// 插入多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 主键为null时，如果不指定id,方法自动生成主键guid
        /// </summary>
        /// <param name=""" + paramName + @""">要插入的" + paramType + @"对象的集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否添加成功（影响的行数等于保存的对象的数量时成功）</returns>
        bool inertBatch(List<" + paramType + "> " + paramName + @"List, OracleConnection con);

        /// <summary>
        /// 删除指定ID的一条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// </summary>
        /// <param Name=""id"">要删除的数据的guid</param>
        /// <param Name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数为1时成功）</returns>
        bool deleteById(string id,OracleConnection con);

        /// <summary>
        /// 删除指定id集合的多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// </summary>
        /// <param name=""idlist"">要删除的数据的guid的集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数等于要删除的id的数量时成功）</returns>
        bool deleteByIdBatch(List<string> idlist, OracleConnection con);

        /// <summary>
        /// 根据删除条件，删除多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 不指定ID，如果需要根据ID来删除数据，请调用deleteById或deleteByIdBatch方法
        /// </summary>
        /// <param name=""" + paramName + @"Condition"">删除的条件</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>无法确定删除的行数（影响行数大于等于0就算成功）</returns>
        bool deleteByOtherCondition(" + paramType + @" " + paramName + @"Condition , OracleConnection con);

        /// <summary>
        /// 根据ID修改一条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 一般不直接调用此方法，请调用save方法进行插入和修改 
        /// </summary>
        /// <remarks>ID为空抛出异常</remarks>
        /// <param name=""" + paramName + @""">要修改的" + paramName + @"对象</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否修改成功（影响的行数为1时成功）</returns>
        bool updateById(" + paramType + " " + paramName + @",OracleConnection con);

        /// <summary>
        /// 根据codeList中的ID修改多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// </summary>
        /// <param name=" + paramName + @"List"">要修改的" + paramName + @"List对象集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数等于要修改的对象数量时成功）</returns>
        bool updateByIdBatch(List<" + paramType + @"> " + paramName + @"List, OracleConnection con);

        /// <summary>
        /// 根据更新的条件，更新数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 不指定ID，如果要根据ID来更新数据，请使用updateForId或updateByIdBatch方法
        /// </summary>
        /// <param name=""" + paramName + @"Condition"">条件对象</param>
        /// <param name=""" + paramName + @"Update"">要更新的数据对象</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>影响的行数不确定（不为-1就正确）</returns>
        bool updateByOtherCondition(" + paramType + @" " + paramName + @"Condition, " + paramType + @" " + paramName + @"Update, OracleConnection con);

        /// <summary>
        /// 查找指定ID的用户
        /// </summary>
        /// <remarks>id为null、id重复、id未找到，抛异常</remarks>
        /// <returns>" + paramType + @"</returns>
        " + paramType + @" findById(string id);

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
        List<object> find(" + paramType + " " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramName + @"Like,List<SortInfo> sortList);

        /// <summary>
        /// 根据条件得到集合的数量
        /// 第二个条件是模糊查询的条件
        /// </summary>
        /// <returns>int,数量</returns>
        int getCount(" + paramType + " " + paramName + @"," + paramType + " " + paramName + @"Like);

        /// <summary>
        /// 通用的保存方法，可新增或修改，根据用户传入的参数，有ID为修改，没有ID为新增(手动生成OracleConnection对象，此方法用于事务，使用事务必须使用同一个OracleConnection对象)
        /// </summary>
        /// <param name=""" + paramName + @""">要保存或更新的" + paramType + @"对象</param>
        /// <returns>bool，保存是否成功</returns>
        bool save(" + paramType + " " + paramName + @",OracleConnection con);


        //*******************在此以后添加新的方法start

        //*******************在此以后添加新的方法end
    }
}
");
            return sb.ToString();
        }

    }
}

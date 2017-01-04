using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorOracleDALImp
    {
        /// <summary>
        /// oracle生成DAL的implement代码
        /// </summary>
        /// <param Name="TableName">表名称</param>
        /// <param Name="className">类名</param>
        /// <param Name="classRemark">类说明</param>
        /// <param Name="classRemark">参数类型</param>
        /// <param Name="paramName">参数名称</param>
        /// <returns>接口实现代码</returns>
        public static string generatorOracleDALImp(string dataBaseType, oracleDBHelper.DBTable table, string className, string classRemark, string paramType, string paramName, string lastNameSpace)
        {
            List<oracleDBHelper.DBColumn> columns = table.Columns;
            string tableName = table.TableName;
            string paramNameList = paramName + "List";//xxParamList
            string paramNameCondition = paramName + "Condition";//xxParamConditon
            string paramNameUpdate = paramName + "Update";//xxParamUpdate
            string paramNameLike = paramName + "Like";//xxParamLike
            StringBuilder sb = new StringBuilder();
            sb.Append(@"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.Common;
using System.Transactions;//引入事务
using Oracle.DataAccess.Client;//引入oracle驱动
using Model." + Common.getNameSpace(tableName, null) + @";
using Model.Common;

namespace DAL." + Common.getNameSpace(tableName, lastNameSpace) + @"
{
    /// <summary>
    ///数据库访问层的" + className + @"实现类
    ///此类由代码生成器生成
    ///表名：" + tableName + @"
    ///生成日期：" + DateTime.Now + @"
    /// " + classRemark + @"
" + Common.myRemark + @"
    /// </summary>
    public class " + className + "DALImp :Base.BasDAL," + className + @"DAL
    {

        /// <summary>
        /// 插入一条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 主键为null时，自动生成主键guid，一般不直接调用此方法，请调用save方法进行单条数据的插入和修改       
        /// </summary>
        /// <param name=""" + paramName + @""">要插入的" + paramName + @"对象</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否添加成功（影响的行数为1时成功）</returns>
        public bool insert(" + paramType + " " + paramName + @",OracleConnection con)
        {
            if (""""==" + paramName + ".Id||null==" + paramName + @".Id)
            {
                " + paramName + @".Id = Model.Common.GUIDHelper.getGuid();
            }
            List<OracleParameter> sqlparams = new List<OracleParameter>();
            ");
            #region 循环添加insert语句中表字段名的方法（新增）
            sb.Append(Common.addSqlInsert(columns, tableName));
            #endregion


            #region 循环添加条件参数的方法（新增）
            sb.Append(Common.addParamInsertForSingle(columns, paramName));
            #endregion


            sb.Append(@"
            bool result = false;
            int flag = OracleHelper.ExecuteNonQuery(sql.ToString(), con, sqlparams.ToArray());
            if (1 == flag)
            {
                result =  true;
            }
            return result;

        }

        /// <summary>
        /// 插入多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象,并且自动使用事务）
        /// 主键为null时，自动生成主键guid
        /// </summary>
        /// <param name=""" + paramNameList + @""">要插入的" + paramName + @"对象的集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否添加成功（影响的行数等于保存的对象的数量时成功）</returns>
        public bool inertBatch(List<" + paramType + @"> " + paramNameList + @", OracleConnection con)
        {
            if (" + paramNameList + @".Count == 0)
            {
                return true;
            }
");
            #region 循环，声明参数的数组变量
            sb.Append(Common.addStatementParamArray(columns));
            #endregion

            #region 循环给参数的数组赋值
            sb.Append(@"
            for (int i = 0; i < " + paramNameList + @".Count; i++)
            {
                " + paramType + " " + paramName + @" = " + paramNameList + @"[i];
                if ("""" == " + paramName + @".Id || null == " + paramName + @".Id)
                {
                    " + paramName + @".Id = Model.Common.GUIDHelper.getGuid();
                }
");
            sb.Append(Common.addParamArrayAssignment(columns, paramName));
            #endregion
            sb.Append(@"
            }

            List<OracleParameter> sqlparams = new List<OracleParameter>();
");
            #region 循环添加insert语句中表字段名的方法（新增）
            sb.Append(Common.addSqlInsert(columns, tableName));
            #endregion

            #region 循环添加条件参数的方法（新增）
            sb.Append(Common.addParamInsertForArray(columns, paramNameList));
            #endregion

            sb.Append(@"
            bool result = false;
            int flag = -1;
            flag = OracleHelper.ExecuteBatch(sql.ToString(), con, " + paramNameList + @".Count, sqlparams.ToArray());
            if (" + paramNameList + @".Count == flag)
            {
                result  = true;
            }
            return result;
        }


        /// <summary>
        /// 删除指定ID的一条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// </summary>
        /// <param Name=""id"">要删除的数据的guid</param>
        /// <param Name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数为1时成功）</returns>
        public bool deleteById(string id,OracleConnection con)
        {
            if ("""" == id || null == id)
            {
                throw new Exception(""ID不允许为空，请赋值后调用；错误出自：deleteById"");
            }
            StringBuilder sql = new StringBuilder(""DELETE " + tableName + @" WHERE ID=:id"");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Value = id;
            bool result = false;
            int flag = OracleHelper.ExecuteNonQuery(sql.ToString(), con, pId);
            if (1 == flag)
            {
                result =  true;
            }
            else if (0 == flag)
            {
                throw new Exception(""删除了"" + flag + ""条数据，请检查ID是否在数据库中存在；错误出自：deleteById"");
            }
            else if (flag > 1)
            {
                throw new Exception(""删除了"" + flag + ""条数据，数据库中主键重复；错误出自：deleteById"");
            }
            return result;
        }

        /// <summary>
        /// 删除指定id集合的多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象，并且自动使用事务）
        /// </summary>
        /// <param name=""idlist"">要删除的数据的guid的集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数等于要删除的id的数量时成功）</returns>
        public bool deleteByIdBatch(List<string> idList, OracleConnection con)
        {
            if (null==idList||0==idList.Count)
            {
                throw new Exception(""ID的集合不允许为空或者数量为0，请赋值后调用；错误出自：deleteByIdBatch"");
            }
            StringBuilder sql = new StringBuilder(""DELETE " + tableName + @" WHERE ID=:id"");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Value = idList.ToArray();
            bool result = false;
            int flag = OracleHelper.ExecuteBatch(sql.ToString(), con,idList.Count, pId);
            if (idList.Count == flag)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据删除条件，删除多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 不指定ID，如果需要根据ID来删除数据，请调用deleteById或deleteByIdBatch方法
        /// </summary>
        /// <param name=""" + paramNameCondition + @""">删除的条件</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>无法确定删除的行数（影响行数大于等于0就算成功）</returns>
        public bool deleteByOtherCondition(" + paramType + @" " + paramNameCondition + @", OracleConnection con)
        {
            if (null!=" + paramNameCondition + @".Id&&""""!=" + paramNameCondition + @".Id)
            {
                throw new Exception(""不允许指定参数的ID，如果要根据ID删除数据请调用deleteById或者deleteByIdBatch方法；错误出自：deleteByOtherCondition"");
            }
            List<OracleParameter> sqlparams = new List<OracleParameter>();
            StringBuilder sql = new StringBuilder(""DELETE " + tableName + @" "");
");
            #region 循环添加删除的条件
            sb.Append(Common.addWhereParamAssignmentForSingle(columns, paramNameCondition));
            #endregion

            sb.Append(@"
            this.b_isWhere = false;
            bool result = false;
            int flag = OracleHelper.ExecuteNonQuery(sql.ToString(), con, sqlparams.ToArray());
            if (flag>=0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 修改指定ID的一条数据（自动生成OracleConnection对象，直接修改）
        /// 一般不直接调用此方法，请调用save方法进行插入和修改
        /// </summary>
        /// <remarks>ID为空抛出异常</remarks>
        /// <param name=""" + paramName + @"""></param>
        /// <returns>影响的行数</returns>
        public bool updateById(" + paramType + " " + paramName + @",OracleConnection con)
        {
            if ("""" == " + paramName + @".Id || null == " + paramName + @".Id)
            {
                throw new Exception(""ID不允许为空，请赋值后调用；错误出自：updateById"");
            }
            List<OracleParameter> sqlparams = new List<OracleParameter>();
");
            #region 循环添加条件参数（更新）
            sb.Append(Common.addSqlUpdateForSingle(columns, tableName, paramName));
            #endregion

            sb.Append(@"
            sql.Append("" WHERE ID=:id "");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Value = " + paramName + @".Id;
            sqlparams.Add(pId);
            this.b_isComma = false;
            bool result = false;
            int flag = OracleHelper.ExecuteNonQuery(sql.ToString(), con, sqlparams.ToArray());
            if (1 == flag)
            {
                result = true;
            }
            else if (0 == flag)
            {
                throw new Exception(""更新"" + flag + ""条数据，请检查ID是否在数据库中存在；错误出自：updateById"");
            }
            else if (flag > 1)
            {
                throw new Exception(""更新了"" + flag + ""条数据，数据库中主键重复；错误出自：updateById"");
            }

            return result;
        }

        /// <summary>
        /// 根据List中的ID修改多条数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象，并且自动使用事务）
        /// </summary>
        /// <param name=""" + paramNameList + @""">要修改的" + paramType + @"对象集合</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>是否删除成功（影响的行数等于要修改的对象数量时成功）</returns>
        public bool updateByIdBatch(List<" + paramType + @"> " + paramNameList + @", OracleConnection con)
        {

");
            #region 循环，声明参数的数组变量
            sb.Append(Common.addStatementParamArray(columns));
            #endregion

            #region 循环给参数的数组赋值
            sb.Append(@"
            for (int i = 0; i < " + paramNameList + @".Count; i++)
            {
                " + paramType + " " + paramName + @" = " + paramNameList + @"[i];
                if ("""" == " + paramName + @".Id || null == " + paramName + @".Id)
                {
                    throw new Exception(""下标为"" + i + ""的数据，ID不能为空，请赋值后调用；错误出自：updateByIdBatch"");
                }
");
            sb.Append(Common.addParamArrayAssignment(columns, paramName));
            #endregion


            sb.Append(@"
            }


            
            List<OracleParameter> sqlparams = new List<OracleParameter>();
");
            #region 循环添加条件参数（更新）
            sb.Append(Common.addSqlUpdateForArray(columns, tableName, paramName));
            #endregion

            sb.Append(@"
            sql.Append("" WHERE ID=:id "");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Value = idList.ToArray();
            sqlparams.Add(pId);
            this.b_isComma = false;
            bool result = false;
            int flag = OracleHelper.ExecuteBatch(sql.ToString(), con," + paramNameList + @".Count, sqlparams.ToArray());
            if (" + paramNameList + @".Count == flag)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据更新的条件，更新数据（如果需要供事务使用就传入OracleConnection对象，如果不供就传入null，方法自动生成OracleConnection对象）
        /// 不指定ID，如果要根据ID来更新数据，请使用updateForId方法
        /// </summary>
        /// <param name=""" + paramNameCondition + @""">条件对象</param>
        /// <param name=""" + paramNameUpdate + @""">要更新的数据对象</param>
        /// <param name=""con"">Oracle连接对象（为null时自动生成）</param>
        /// <returns>影响的行数不确定（不为-1就正确）</returns>
        public bool updateByOtherCondition(" + paramType + @" " + paramNameCondition + @", " + paramType + @" " + paramNameUpdate + @", OracleConnection con)
        {
            if ((null!=" + paramNameCondition + @".Id&&""!=" + paramNameCondition + @".Id)||(null!=" + paramNameUpdate + @".Id&&""!=" + paramNameUpdate + @".Id))
            {
                throw new Exception(""此方法不允许指定参数的ID，数据库中的ID不允许修改，如需根据ID修改数据，请调用updateById或updateByIdBatch方法；错误出自：updateByOtherCondition"");
            }
            List<OracleParameter> sqlparams = new List<OracleParameter>();
            //添加要修改的值
            StringBuilder sql = new StringBuilder(""UPDATE " + tableName + @" SET "");");
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
            if (DateTime.MinValue != " + paramNameUpdate + @"." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramNameUpdate + @"." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameUpdate + @"." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramNameUpdate + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameUpdate + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
            if (null != " + paramNameUpdate + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" &&  """"!= " + paramNameUpdate + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isComma(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameUpdate + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }

            }


            sb.Append(@"
            //添加条件的参数
            ");
            for (int i = 1; i < columns.Count; i++)
            {
                oracleDBHelper.DBColumn column = columns[i];
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
            if (DateTime.MinValue != " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else if (columns[i].ColumnCSharpType == "decimal")
                {
                    sb.Append(@"
            if (null != " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
            if (null != " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" &&  """"!= " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" " + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameCondition + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
            }

            sb.Append(@"
            this.b_isComma = false;
            this.b_isWhere = false;
            bool result = false;
            int flag = OracleHelper.ExecuteNonQuery(sql.ToString(), con, sqlparams.ToArray());
            if (flag>=0)
            {
                result = true;
            }

            return result;
        }




        /// <summary>
        /// 查找指定ID的用户
        /// </summary>
        /// <remarks>id为null、id重复、id未找到，抛异常</remarks>
        /// <returns>" + paramType + @"</returns>
        public " + paramType + @" findById(string id)
        {
            if (""""==id||null==id)
            {
                throw new Exception(""ID不能为空，请赋值后调用；错误出自：findById"");
            }
            StringBuilder sql = new StringBuilder(""SELECT A.* "");
            //新建方法findContactObjectsById，在此加入多表连接要显示的项start
            //sql.Append("",B.字段一,B.字段二,B.字段三"");
            //新建方法，在此加入多表连接要显示的项end
            sql.Append("" FROM " + tableName + @" A "");
            //新建一个方法，在此加入多表连接的语句start
            //sql.Append(""LEFT JOIN 外键表名 B ON A.外键=B.ID"");
            //新建一个方法，在此加入多表连接的语句end
            sql.Append("" WHERE A.ID=:id "");
            OracleParameter pId = new OracleParameter("":id"", OracleDbType.Varchar2);
            pId.Direction = ParameterDirection.Input;
            pId.Value = id;
            DataSet ds = OracleHelper.getDataSet(sql.ToString(), pId);
            DataRowCollection rows = ds.Tables[0].Rows;
            if (rows.Count > 1)
            {
                throw new Exception(""此ID的数据有多个，主键不允许重复，请检查数据；错误出自：findById"");
            }
            if (rows.Count == 0)
            {
                throw new Exception(""此ID的数据不存在，请检查数据；错误出自：findById"");
            }
            return this.dataSetToModel(rows[0]);
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
        public List<object> find(" + paramType + " " + paramName + @", Model.Common.PageInfo page," + paramType + " " + paramNameLike + @",List<SortInfo> sortList)
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
            //新建方法findContactObjects(" + paramType + " " + paramName + @", Model.Common.PageInfo page)，在此加入多表连接要显示的项start
            //sql.Append("",B.字段一,B.字段二,B.字段三"");
            //新建方法，在此加入多表连接要显示的项end
            sql.Append("" FROM " + tableName + @" A "");
            //新建一个方法，在此加入多表连接的语句start
            ////sql.Append("" LEFT JOIN 表名 B ON A.外键=B.ID"");
            //新建一个方法，在此加入多表连接的语句end
            
            ");
            #region 循环添加条件参数（查找）
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
                if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
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
                sql.Append("" A." + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
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
            if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" && """" != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
            {
                this.isAnd(sql);
                this.isWhere(sql);
                sql.Append("" A." + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
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
            #endregion
            sb.Append(@"
            if(" + paramNameLike + @"!=null)
            {
");
            #region 循环添加模糊查询的条件参数
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "string")
                {
                    sb.Append(@"
                if (null != " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" && """" != " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + columns[i].ColumnName.ToUpper() + " LIKE '%'||:" + columns[i].ColumnName + @"||'%' "");
                    OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                    sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");
                }
                ");
                }

            }
            #endregion

            sb.Append(@"
            }
");

            sb.Append(@"
            this.b_isWhere = false;
            this.addSort(sql, sortList);//排序
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
                //" + paramName + @"List.Add(this.dataSetContactObjectsToModel(row));
                " + paramName + @"List.Add(this.dataSetToModel(row));
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

        
        
        /// <summary>
        /// 根据条件得到集合的数量
        /// 第二个条件是模糊查询的条件
        /// </summary>
        /// <returns>int</returns>
        public int getCount(" + paramType + " " + paramName + @"," + paramType + " " + paramNameLike + @")
        {
            //如果没有传查询条件参数，就new一个空的" + paramType + @"对象，标示查询所有数据
            if (null == " + paramName + @")
            {
                " + paramName + " = new " + paramType + @"();
            }
            List<OracleParameter> sqlparams = new List<OracleParameter>();//保存参数的集合
            StringBuilder sql = new StringBuilder(""SELECT COUNT(ID) FROM " + tableName + @" A"");");

            #region 循环字段添加条件参数(得到集合的数量)
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "DateTime")
                {
                    sb.Append(@"
                if (DateTime.MinValue != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + " &&null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                    OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
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
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");

            }
                        ");
                }
                else
                {
                    sb.Append(@"
                if (null != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" && """" != " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + columns[i].ColumnName.ToUpper() + "=:" + columns[i].ColumnName + @" "");
                    OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");
                }
                ");
                }

            }
            #endregion

            sb.Append(@"
            if(" + paramNameLike + @"!=null)
            {
");
            #region 循环添加模糊查询的条件参数
            for (int i = 1; i < columns.Count; i++)
            {
                if (columns[i].ColumnCSharpType == "string")
                {
                    sb.Append(@"
                if (null != " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" && """" != " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @")
                {
                    this.isAnd(sql);
                    this.isWhere(sql);
                    sql.Append("" A." + columns[i].ColumnName.ToUpper() + " LIKE '%'||:" + columns[i].ColumnName + @"||'%' "");
                    OracleParameter p" + Common.fristCharToUpper(columns[i].ColumnName) + @" = new OracleParameter("":" + columns[i].ColumnName + @""", OracleDbType." + columns[i].ColumnODACParamType + @");");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Direction = ParameterDirection.Input;");
                    sb.Append(@"
                    p" + Common.fristCharToUpper(columns[i].ColumnName) + @".Value = " + paramNameLike + "." + Common.fristCharToUpper(columns[i].ColumnName) + @";");
                    sb.Append(@"
                    sqlparams.Add(p" + Common.fristCharToUpper(columns[i].ColumnName) + @");
                }
                ");
                }

            }
            #endregion

            sb.Append(@"
            }
");

            sb.Append(@"
            this.b_isWhere = false;
            return Int32.Parse(OracleHelper.ExecuteScalar(sql.ToString(),sqlparams.ToArray()).ToString());
        }


        /// <summary>
        /// 通用的保存方法，根据用户传入的参数，有ID为修改，没有ID为新增(手动生成OracleConnection对象，此方法用于事务，使用事务必须使用同一个OracleConnection对象)
        /// </summary>
        /// <param name=""" + paramName + @"""></param>
        /// <returns></returns>
        public bool save(" + paramType + @" " + paramName + @",OracleConnection con)
        {
            if ("""" != " + paramName + ".Id && null != " + paramName + @".Id)
            {
                //修改
                return this.updateById(" + paramName + @",con);
            }
            else
            {
                //新增
                return this.insert(" + paramName + @",con);
            }
        }

        /// <summary>
        /// 把dataset中的一行转换为model
        /// </summary>
        /// <param name=""row""></param>
        /// <returns></returns>
        private " + paramType + @" dataSetToModel(DataRow row)
        {
            " + paramType + " " + paramName + " = new " + paramType + @"();
            ");
            #region 循环字段（dataset转换为modelList）
            for (int i = 0; i < columns.Count; i++)//处理字段类型
            {
                if (columns[i].ColumnCSharpType == "string")//如果此字段的类型为string，使用Convert.toString().tirm()
                {
                    sb.Append("" + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" = Convert.ToString(row[""" + columns[i].ColumnName.ToUpper() + @"""]).Trim(); 
            ");
                }
                else if (columns[i].ColumnIsNull == "y")//如果此字段可以为空，就使用convertDBNullValue转换
                {
                    sb.Append("" + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" =  (" + columns[i].ColumnCSharpType + @"?)this.convertDBNullValue(row[""" + columns[i].ColumnName.ToUpper() + @"""]);
            ");//类型转换的判断
                }
                else//默认的
                {
                    sb.Append("" + paramName + "." + Common.fristCharToUpper(columns[i].ColumnName) + @" =  (" + columns[i].ColumnCSharpType + @")row[""" + columns[i].ColumnName.ToUpper() + @"""];
            ");//类型转换的判断
                }
            }
            #endregion

            sb.Append(@"
            return " + paramName + @";
        }

       
        
        //*******************在此以后添加新的方法start

        //*******************在此以后添加新的方法end
        
    }
}


");

            return sb.ToString();
        }

    }
}



SELECT * FROM mchis.INFORMATION_SCHEMA.TABLES where table_type='base table'


Select Name FROM Master..SysDatabases orDER BY Name --获取数据库名

Select * FROM mchis..SysObjects Where XType='U' orDER BY Name --获取所有表名，XType='U':表示所有用户表，XType='S':表示所有系统表; 

Select * FROM SysColumns Where id=Object_Id('t_user')--获取所有字段名



SELECT 
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
     AND i_s.TABLE_NAME = 't_user' 
ORDER BY 
    i_s.TABLE_NAME, i_s.ORDINAL_POSITION--获取字段信息


SELECT * FROM mchis.INFORMATION_SCHEMA.TABLES where table_type='base table'


Select Name FROM Master..SysDatabases orDER BY Name --��ȡ���ݿ���

Select * FROM mchis..SysObjects Where XType='U' orDER BY Name --��ȡ���б�����XType='U':��ʾ�����û���XType='S':��ʾ����ϵͳ��; 

Select * FROM SysColumns Where id=Object_Id('t_user')--��ȡ�����ֶ���



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
    i_s.TABLE_NAME, i_s.ORDINAL_POSITION--��ȡ�ֶ���Ϣ
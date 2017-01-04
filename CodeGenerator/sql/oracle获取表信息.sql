--获取表信息
select a.TABLE_NAME, a.TABLESPACE_NAME, b.comments
  from user_tables a
  left join user_tab_comments b
    on a.TABLE_NAME = b.table_name


	--获取表字段信息
select a.table_name,
       a.COLUMN_NAME,
       a.DATA_TYPE,
       a.DATA_LENGTH,
       a.NULLABLE,
       b.comments
  from dba_tab_columns A
  left join user_col_comments B
    on A.COLUMN_NAME = B.column_name
 where A.table_name = upper('jc_brxx')
   AND B.table_name = upper('jc_brxx')
 order by COLUMN_ID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.Transformation
{
    public class MetaDataCore
    {
        public string all_tables_SQL()
        {
            string query ="";


            query = "select ROW_NUMBER() OVER (ORDER BY table_name) NO ,cast(0 as bit) as MARCAR, TABLE_NAME from INFORMATION_SCHEMA.TABLES where table_type =  'BASE TABLE'";


            return query;
        }
        public string all_tables_Oracle()
        {
            string query = "";


            query = "select ROW_NUMBER() OVER (ORDER BY TABLE_NAME)  NO,  0 marcar,table_name from USER_TABLES";


            return query;
        }
        public string ALL_TYPE_COLUMNS_SQLMODEL_X_TABLE(string TABLE_NAME)
        {
            string query = "";

            query = @"
                SELECT
                    UPPER(TABLE_NAME) AS TABLE_NAME,
                    UPPER(COLUMN_NAME) AS COLUMN_NAME,
                    ORDINAL_POSITION,
                    ISNULL(column_default, '') AS COLUMN_DEFAULT,
                    case when IS_NULLABLE ='YES' THEN convert(bit , 1) ELSE convert(bit , 0) END IS_NULLABLE
                       ,
                    DATA_TYPE,
                    CASE
                        
                        WHEN DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar', 'binary', 'varbinary') THEN
                            DATA_TYPE + '(' +
                            CASE
                                WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' -- Para varchar(max), nvarchar(max), varbinary(max), etc.
                                ELSE CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)
                            END + ')'

                  
                        WHEN DATA_TYPE IN ('decimal', 'numeric') THEN
                            DATA_TYPE + '(' + CAST(NUMERIC_PRECISION AS VARCHAR) + ', ' + CAST(NUMERIC_SCALE AS VARCHAR) + ')'

                 
                        WHEN DATA_TYPE IN ('money', 'smallmoney') THEN
                            DATA_TYPE + '(19, 4)'  -- Money y smallmoney tienen una precisión fija

                 
                        WHEN DATA_TYPE = 'float' THEN
                            DATA_TYPE + '(' + CAST(NUMERIC_PRECISION AS VARCHAR) + ')'
                        WHEN DATA_TYPE = 'real' THEN 'real(24)' 

                    
                        WHEN DATA_TYPE IN ('datetime2', 'datetimeoffset', 'time') THEN
                            DATA_TYPE + '(' + CAST(DATETIME_PRECISION AS VARCHAR) + ')'

                       
                        WHEN DATA_TYPE IN ('int', 'smallint', 'bigint', 'tinyint', 'bit', 'date', 'datetime', 'smalldatetime', 'uniqueidentifier', 'xml', 'sql_variant', 'hierarchyid', 'geometry', 'geography') THEN
                            DATA_TYPE

                 
                        WHEN DATA_TYPE IN ('text', 'ntext', 'image') THEN
                            DATA_TYPE + ' (Obsoleto)'

                        ELSE DATA_TYPE  
                    END AS DATA_TYPE_DETAIL,

                    CASE
                    
                        WHEN DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar', 'binary', 'varbinary') THEN
                            CASE
                                WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' 
                                ELSE CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)
                            END

                        WHEN DATA_TYPE IN ('decimal', 'numeric') THEN
                            CAST(NUMERIC_PRECISION AS VARCHAR) + ', ' + CAST(NUMERIC_SCALE AS VARCHAR)

                   
                        WHEN DATA_TYPE = 'float' THEN
                            CAST(NUMERIC_PRECISION AS VARCHAR)
                        WHEN DATA_TYPE = 'real' THEN '24'  

                      
                        WHEN DATA_TYPE IN ('datetime2', 'datetimeoffset', 'time') THEN
                            CAST(DATETIME_PRECISION AS VARCHAR)

                        ELSE ' '
                    END AS DATA_LENGTH

                FROM
                    INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = '" + TABLE_NAME.ToString()+ @"'
                ORDER BY
                    TABLE_NAME, ORDINAL_POSITION;";

            return query;   
        }





    }
}

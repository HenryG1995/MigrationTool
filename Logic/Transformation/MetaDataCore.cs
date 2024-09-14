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


            query = "select ROW_NUMBER() OVER (ORDER BY table_name) [#] ,0 as marcar, TABLE_NAME from INFORMATION_SCHEMA.TABLES where table_type =  'BASE TABLE'";


            return query;
        }
        public string all_tables_Oracle()
        {
            string query = "";


            query = "select ROW_NUMBER() OVER (ORDER BY TABLE_NAME)  \"#\",  0 marcar,table_name from USER_TABLES";


            return query;
        }





    }
}

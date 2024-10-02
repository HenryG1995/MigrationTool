using GalaSoft.MvvmLight.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToolMigration.Logic.DataModels;

namespace ToolMigration.Logic.Transformation
{
    class CreaScriptTablasDestino
    {
        public string creaScriptTablas(string table_name, bool utiliza_parametrizacion = false,List<DataTypeConvert> parametros=null)
        {

            string v_query = string.Empty;

            v_query = "CREATE TABLE "+table_name.ToString().ToUpper() + " ( ";



            foreach (DataTypeConvert item in parametros)
            {

                if(utiliza_parametrizacion == true)
                {
                  
                    
                }
                else
                {

                }

            }
            v_query = v_query + ");";

            return v_query;
        }


    }
}

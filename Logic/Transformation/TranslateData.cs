using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolMigration.Logic.Connections;
using ToolMigration.Logic.DataModels;

namespace ToolMigration.Logic.Transformation
{
    public class TranslateData
    {
        public string tabla { get; set; }
        public string campos { get; set; }
        public string indices { get; set; }
        private SeleccionTablas seleccionTablas;
   
        public string script(string tabla_gen)
        {
            var consulta = string.Empty;

            var scritpt_tabla = string.Empty;

            try
            {
                scritpt_tabla = "CREATE TABLE " + tabla_gen;

                //GENERA LISTADO DE CAMPOS EQUIVALENTES


            }
            catch (Exception ex)
            {

                Debug.WriteLine("error: " + ex.Message);
            }



            return scritpt_tabla;
        }

        public List<string> listaCampos(string tabla)
        {
            List<string> ret = new List<string>();
            return ret;
        }

      
    }
}

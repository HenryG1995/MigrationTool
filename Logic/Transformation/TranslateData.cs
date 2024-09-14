using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.Transformation
{
    public class TranslateData
    {
        public string tabla { get; set; }
        public string campos { get; set; }
        public string indices { get; set; }


        public string script(string tabla_gen)
        { 
            var consulta = string.Empty;

            var scritpt_tabla = string.Empty;

            try
            {
            scritpt_tabla= "CREATE TABLE " + tabla_gen;
            
                //GENERA LISTADO DE CAMPOS EQUIVALENTES

            
            }
            catch(Exception ex) {

                Console.WriteLine("error: " + ex.Message);
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

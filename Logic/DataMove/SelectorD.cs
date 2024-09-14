using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ToolMigration.Logic.DataMove
{
    internal class SelectorD
    {


        public SelectorD() { }

        public List<string> ListTables { get; set; }

        public DataTable data {  get; set; }

        public List<string> tablas(string connstr,bool or)
        {

               switch (or)
            {
                case true:
                    {
                        //Oracle

                        Console.WriteLine("Inicia la obtencion de tablas para oracle");



                        break;
                    }
                case false:
                    {
                        //SQL

                        Console.WriteLine("Inicia la obtencion de tablas para SQL");


                        break;
                    }
            }

 
        return ListTables.ToList();
        }

        public bool migration(string constr)
        {





            return true;
        }

        public DataTable llenaTiposOracle(List tipos)
        { 
            DataTable dt = new DataTable();

            dt.Columns.Add("Modificar");
            dt.Columns.Add("Tipo de dato");
            dt.Columns.Add("Observacion");

            if (tipos != null)
            {

            }

            
            return data; }
        public DataTable llenaTiposSQL() 
        { 
            
            
            
            return data; }

    }
}

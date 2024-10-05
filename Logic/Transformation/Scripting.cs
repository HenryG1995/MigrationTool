using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ToolMigration.Logic.DataModels;
using System.Diagnostics;

namespace ToolMigration.Logic.Transformation
{
    public class Scripting
    {
        public string val(string connection)
        {
            var query_tabla = "";
            DataTable lista_tablas = new DataTable();
            DataTable lista_campos = new DataTable();

            return query_tabla;

        }

        public List<string> scriptListRead(string sqlcon,string script) 
        { 
        var scriptList = new List<string>();

            using (SqlConnection connection = new SqlConnection(sqlcon))
            {
                try
                {
                    connection.Open(); 

                    SqlCommand command = new SqlCommand(script, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try 
                            {
                                scriptList.Add(reader.GetString(0));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error parseando  data en la fila: {ex.Message}");
                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"error al abrir comando y conexion : {ex.Message}");
              
                }
            }

            return scriptList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.Tools
{
    public class Tools
    {

        public List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            var result = new List<Dictionary<string, object>>();

            // Recorrer cada fila del DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Crear un diccionario para almacenar la fila actual
                var rowDict = new Dictionary<string, object>();

                // Recorrer todas las columnas de la fila
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Agregar el nombre de la columna como clave y el valor de la fila como valor
                    rowDict[column.ColumnName] = row[column] != DBNull.Value ? row[column] : null;
                }

                // Agregar la fila (como diccionario) a la lista de resultados
                result.Add(rowDict);
            }

            return result;
        }
    }
}

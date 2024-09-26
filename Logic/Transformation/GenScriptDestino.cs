using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Data;
namespace ToolMigration.Logic.Transformation
{
    public class GenScriptDestino
    {
        public List<string> GenScripts { get; set; }


        public void GenScriptO(string rutaArchivo,string formato)
        {

            GenScripts = new List<string>();

            MetaDataCore metaDataCore = new MetaDataCore();

            Task task = Task.Run(() =>
            {
                rutaArchivo = rutaArchivo + formato;

                // Crear un nuevo archivo de texto
                using (StreamWriter escritor = new StreamWriter(rutaArchivo))
                {
                    // generacion de Scripts basado en metadata.



                    // Escribir contenido en el archivo
                    escritor.WriteLine("Esta es la primera línea.");
                    escritor.WriteLine("Esta es la segunda línea.");
                    escritor.WriteLine("¡Hola desde C#!");
                }

                Console.WriteLine("Archivo creado correctamente: " + rutaArchivo);


            });



        }

        public string GENSCRIPT(string tabla)
        {
            var script_insert = "SELECT 'SELECT '''+\r\n(select 'INSERT INTO \"'+  T.TABLE_NAME + '\" ('+ STRING_AGG( '\"'+T.column_name+'\"',',') +') VALUES (''+'  from INFORMATION_SCHEMA.columns T where T.table_name = '"+tabla+"' GROUP BY T.TABLE_NAME) +''''+\r\n(\r\nSELECT\r\nSTRING_AGG(\r\n(        CASE\r\n            WHEN DATA_TYPE IN ('varchar', 'nvarchar', 'char', 'nchar', 'text', 'ntext', 'date', 'datetime', 'datetime2',\r\n                               'smalldatetime', 'time', 'timestamp') THEN CONCAT('''''''+[', COLUMN_NAME, ']+''''''')\r\n            WHEN DATA_TYPE IN ('int', 'bit', 'bigint', 'smallint', 'tinyint', 'decimal', 'numeric', 'float', 'real')\r\n                THEN COLUMN_NAME\r\n            ELSE 'Otro tipo de dato' END )\r\n,',')\r\n    AS COLUMN_NAME_WITH_QUOTES\r\n\r\nFROM INFORMATION_SCHEMA.COLUMNS T where T.table_name = '"+tabla+"')+')'' FROM "+tabla+"'";
            
            return script_insert;
        }
        


    }
}

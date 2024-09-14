using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
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
        


    }
}

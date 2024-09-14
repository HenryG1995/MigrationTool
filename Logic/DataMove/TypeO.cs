using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.DataMove
{
    using System;
    using System.Collections.Generic;

    public class OracleDataType
    {
        public string Nombre { get; set; }       // Nombre del tipo de dato en Oracle
        public string Observacion { get; set; }  // Observación o descripción
        public Type TipoCSharp { get; set; }     // Tipo equivalente en C#

        public OracleDataType(string nombre, string observacion, Type tipoCSharp)
        {
            Nombre = nombre;
            Observacion = observacion;
            TipoCSharp = tipoCSharp;
        }
    }

    public class OracleDataTypes
    {
        public List<OracleDataType> TiposDeDatos { get; set; }

        public OracleDataTypes()
        {
            TiposDeDatos = new List<OracleDataType>
        {
            // Tipos numéricos
            new OracleDataType("NUMBER", "Número de precisión variable", typeof(decimal)),
            new OracleDataType("BINARY_FLOAT", "Número de punto flotante simple", typeof(float)),
            new OracleDataType("BINARY_DOUBLE", "Número de punto flotante doble", typeof(double)),

            // Tipos de cadena
            new OracleDataType("CHAR", "Cadena de caracteres de longitud fija", typeof(string)),
            new OracleDataType("VARCHAR2", "Cadena de caracteres de longitud variable", typeof(string)),
            new OracleDataType("NCHAR", "Cadena de caracteres Unicode de longitud fija", typeof(string)),
            new OracleDataType("NVARCHAR2", "Cadena de caracteres Unicode de longitud variable", typeof(string)),
            new OracleDataType("CLOB", "Cadena de texto de longitud variable", typeof(string)),
            new OracleDataType("NCLOB", "Cadena de texto Unicode de longitud variable", typeof(string)),
            new OracleDataType("LONG", "Cadena de texto de longitud variable (obsoleto)", typeof(string)),
            new OracleDataType("ROWID", "Identificador de fila en una tabla", typeof(string)),
            new OracleDataType("UROWID", "Identificador de fila universal en una tabla", typeof(string)),

            // Tipos de fecha y hora
            new OracleDataType("DATE", "Fecha y hora con precisión de segundos", typeof(DateTime)),
            new OracleDataType("TIMESTAMP", "Fecha y hora con fracciones de segundo", typeof(DateTime)),
            new OracleDataType("TIMESTAMP WITH TIME ZONE", "Fecha, hora y zona horaria", typeof(DateTimeOffset)),
            new OracleDataType("TIMESTAMP WITH LOCAL TIME ZONE", "Fecha, hora con zona horaria local", typeof(DateTime)),

            // Tipos binarios
            new OracleDataType("RAW", "Datos binarios de longitud variable", typeof(byte[])),
            new OracleDataType("BLOB", "Datos binarios de gran tamaño", typeof(byte[])),
            new OracleDataType("BFILE", "Referencia a un archivo binario externo", typeof(string)),
            new OracleDataType("LONG RAW", "Datos binarios de longitud variable (obsoleto)", typeof(byte[])),

            // Tipos espaciales
            new OracleDataType("SDO_GEOMETRY", "Datos espaciales (geometría)", typeof(object)),

            // Tipos booleanos
            new OracleDataType("BOOLEAN", "Valor booleano (TRUE, FALSE o NULL)", typeof(bool)),

            // Tipos de referencias y objetos
            new OracleDataType("REF", "Puntero a una fila de una tabla", typeof(object)),
            new OracleDataType("ROWID", "Identificador único de fila en una tabla", typeof(string)),
            new OracleDataType("UROWID", "Identificador universal de fila en una tabla", typeof(string)),
            new OracleDataType("OBJECT", "Tipo definido por el usuario (objeto)", typeof(object)),

            // Tipos de colección
            new OracleDataType("VARRAY", "Arreglo de tamaño fijo", typeof(Array)),
            new OracleDataType("NESTED TABLE", "Tabla anidada dentro de otra tabla", typeof(object)),

            // Tipos XML
            new OracleDataType("XMLTYPE", "Datos XML", typeof(string)),

            // Tipos de intervalos
            new OracleDataType("INTERVAL YEAR TO MONTH", "Intervalo de años y meses", typeof(TimeSpan)),
            new OracleDataType("INTERVAL DAY TO SECOND", "Intervalo de días, horas, minutos y segundos", typeof(TimeSpan)),

            // Tipos de JSON
            new OracleDataType("JSON", "Datos JSON (implícito, no hay un tipo nativo)", typeof(string)),

            // Tipos obsoletos
            new OracleDataType("LONG", "Cadena de texto de longitud variable (obsoleto)", typeof(string)),
            new OracleDataType("LONG RAW", "Datos binarios de longitud variable (obsoleto)", typeof(byte[])),

            // Tipos específicos de PL/SQL
            new OracleDataType("PLS_INTEGER", "Entero con rango de -2,147,483,647 a 2,147,483,647 (solo en PL/SQL)", typeof(int)),
            new OracleDataType("BINARY_INTEGER", "Entero obsoleto (PL/SQL)", typeof(int)),

            // Otros tipos especiales
            new OracleDataType("ANYDATA", "Datos que pueden almacenar cualquier tipo", typeof(object)),
            new OracleDataType("ANYTYPE", "Tipo que puede definir cualquier tipo de dato", typeof(object)),
            new OracleDataType("ANY", "Datos genéricos (para PL/SQL)", typeof(object)),
            new OracleDataType("REF CURSOR", "Puntero a un cursor (usado en PL/SQL)", typeof(object))
        };
        }
   
        public void cargadata()
        {
            OracleDataTypes oracleTypes = new OracleDataTypes();

            // Mostrar los tipos de datos
            foreach (var tipo in oracleTypes.TiposDeDatos)
            {
                Console.WriteLine($"Nombre: {tipo.Nombre}, Observación: {tipo.Observacion}, Tipo en C#: {tipo.TipoCSharp.Name}");
            }
        }
    }

}

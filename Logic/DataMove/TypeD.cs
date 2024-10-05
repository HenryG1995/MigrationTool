using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ToolMigration.Logic.DataMove
{
  
    public class SqlDataType
    {
        public string Nombre { get; set; }       // Nombre del tipo de dato SQL Server
        public string Observacion { get; set; }  // Observación o descripción
        public Type TipoCSharp { get; set; }     // Tipo equivalente en C#

        public SqlDataType(string nombre, string observacion, Type tipoCSharp)
        {
            Nombre = nombre;
            Observacion = observacion;
            TipoCSharp = tipoCSharp;
        }
    }

    public class SqlServerDataTypes
    {
        public List<SqlDataType> TiposDeDatos { get; set; }

        public SqlServerDataTypes()
        {
            TiposDeDatos = new List<SqlDataType>
        {
            // Tipos numéricos
            new SqlDataType("TINYINT", "Entero sin signo de 1 byte", typeof(byte)),
            new SqlDataType("SMALLINT", "Entero de 2 bytes", typeof(short)),
            new SqlDataType("INT", "Entero de 4 bytes", typeof(int)),
            new SqlDataType("BIGINT", "Entero de 8 bytes", typeof(long)),
            new SqlDataType("BIT", "Valor booleano (1 o 0)", typeof(bool)),

            // Tipos de punto flotante
            new SqlDataType("FLOAT", "Número de punto flotante de 8 bytes", typeof(double)),
            new SqlDataType("REAL", "Número de punto flotante de 4 bytes", typeof(float)),

            // Tipos decimales y monetarios
            new SqlDataType("DECIMAL", "Número decimal con precisión definida", typeof(decimal)),
            new SqlDataType("NUMERIC", "Número decimal con precisión definida", typeof(decimal)),
            new SqlDataType("MONEY", "Tipo de dato monetario de 8 bytes", typeof(decimal)),
            new SqlDataType("SMALLMONEY", "Tipo de dato monetario de 4 bytes", typeof(decimal)),

            // Tipos de fecha y hora
            new SqlDataType("DATE", "Solo fecha (sin hora)", typeof(DateTime)),
            new SqlDataType("TIME", "Solo hora (sin fecha)", typeof(TimeSpan)),
            new SqlDataType("DATETIME", "Fecha y hora", typeof(DateTime)),
            new SqlDataType("SMALLDATETIME", "Fecha y hora con menos precisión", typeof(DateTime)),
            new SqlDataType("DATETIME2", "Fecha y hora con mayor precisión", typeof(DateTime)),
            new SqlDataType("DATETIMEOFFSET", "Fecha y hora con zona horaria", typeof(DateTimeOffset)),

            // Tipos de cadenas
            new SqlDataType("CHAR", "Cadena de caracteres de longitud fija", typeof(string)),
            new SqlDataType("VARCHAR", "Cadena de caracteres de longitud variable", typeof(string)),
            new SqlDataType("TEXT", "Cadena de texto de longitud variable (obsoleto)", typeof(string)),
            new SqlDataType("NCHAR", "Cadena de caracteres Unicode de longitud fija", typeof(string)),
            new SqlDataType("NVARCHAR", "Cadena de caracteres Unicode de longitud variable", typeof(string)),
            new SqlDataType("NTEXT", "Cadena de texto Unicode de longitud variable (obsoleto)", typeof(string)),

            // Tipos binarios
            new SqlDataType("BINARY", "Datos binarios de longitud fija", typeof(byte[])),
            new SqlDataType("VARBINARY", "Datos binarios de longitud variable", typeof(byte[])),
            new SqlDataType("IMAGE", "Datos binarios de longitud variable (obsoleto)", typeof(byte[])),

            // Tipos espaciales
            new SqlDataType("GEOMETRY", "Datos espaciales (planos)", typeof(object)),  // No hay un tipo directo en C#
            new SqlDataType("GEOGRAPHY", "Datos espaciales geográficos", typeof(object)), // No hay un tipo directo en C#

            // Tipos especiales
            new SqlDataType("UNIQUEIDENTIFIER", "Identificador único (GUID)", typeof(Guid)),
            new SqlDataType("ROWVERSION", "Número de versión único por fila (timestamp)", typeof(byte[])),
            new SqlDataType("XML", "Datos XML", typeof(string)),
            new SqlDataType("SQL_VARIANT", "Datos de cualquier tipo excepto TEXT, NTEXT e IMAGE", typeof(object)),
            new SqlDataType("TABLE", "Tabla temporal o variable", typeof(object)),
            new SqlDataType("CURSOR", "Referencia a un cursor", typeof(object)),

            // Tipos de jerarquía
            new SqlDataType("HIERARCHYID", "Datos jerárquicos", typeof(object)),

            // Tipos JSON
            new SqlDataType("JSON", "Representación de datos JSON (implícito, no hay un tipo nativo)", typeof(string))
        };
        }
        public void carga()
        {
            SqlServerDataTypes sqlTypes = new SqlServerDataTypes();

            // Mostrar los tipos de datos
            foreach (var tipo in sqlTypes.TiposDeDatos)
            {
                Debug.WriteLine($"Nombre: {tipo.Nombre}, Observación: {tipo.Observacion}, Tipo en C#: {tipo.TipoCSharp.Name}");
            }
        }
    }

}

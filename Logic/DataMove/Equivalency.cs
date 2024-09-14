using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ToolMigration.Logic.DataMove
{
    class Equivalency
    {

        public class DataTypeMapping
        {
            public string SqlServerType { get; set; }  // Nombre del tipo de dato en SQL Server
            public string OracleType { get; set; }     // Nombre del tipo de dato equivalente en Oracle
            public string Observacion { get; set; }    // Observación o explicación de la equivalencia

            public DataTypeMapping(string sqlServerType, string oracleType, string observacion)
            {
                SqlServerType = sqlServerType;
                OracleType = oracleType;
                Observacion = observacion;
            }
        }

        public class DataTypeMappings
        {
            public List<DataTypeMapping> MapeosDeTipos { get; set; }

            public DataTypeMappings()
            {
                MapeosDeTipos = new List<DataTypeMapping>
            {
            // Tipos Numéricos
            new DataTypeMapping("TINYINT", "NUMBER(3)", "Ambos son enteros pequeños sin signo."),
            new DataTypeMapping("SMALLINT", "NUMBER(5)", "Ambos almacenan números enteros pequeños."),
            new DataTypeMapping("INT", "NUMBER(10)", "Ambos manejan números enteros de 32 bits."),
            new DataTypeMapping("BIGINT", "NUMBER(19)", "Ambos manejan enteros grandes, Oracle usa NUMBER para mayor precisión."),
            new DataTypeMapping("DECIMAL", "NUMBER(p, s)", "Ambos permiten definir precisión y escala."),
            new DataTypeMapping("NUMERIC", "NUMBER(p, s)", "Igual que DECIMAL en SQL Server, se usa NUMBER en Oracle."),
            new DataTypeMapping("FLOAT", "BINARY_DOUBLE", "FLOAT en SQL Server es equivalente a BINARY_DOUBLE en Oracle."),
            new DataTypeMapping("REAL", "BINARY_FLOAT", "REAL en SQL Server es similar a BINARY_FLOAT en Oracle."),
            new DataTypeMapping("BIT", "NUMBER(1)", "NUMBER(1) en Oracle puede usarse como equivalente al tipo BIT en SQL Server."),

            // Tipos Monetarios
            new DataTypeMapping("MONEY", "NUMBER(19, 4)", "MONEY en SQL Server es equivalente a NUMBER en Oracle para valores monetarios."),
            new DataTypeMapping("SMALLMONEY", "NUMBER(10, 4)", "Equivalente a NUMBER en Oracle con menor precisión."),

            // Tipos de Fecha y Hora
            new DataTypeMapping("DATE", "DATE", "Ambos almacenan solo la fecha."),
            new DataTypeMapping("DATETIME", "TIMESTAMP", "Ambos almacenan fecha y hora."),
            new DataTypeMapping("SMALLDATETIME", "TIMESTAMP", "Equivalente más cercano en Oracle para SMALLDATETIME."),
            new DataTypeMapping("DATETIME2", "TIMESTAMP", "Ambos almacenan fecha y hora con mayor precisión."),
            new DataTypeMapping("DATETIMEOFFSET", "TIMESTAMP WITH TIME ZONE", "Ambos almacenan fecha y hora con información de zona horaria."),
            new DataTypeMapping("TIME", "TIMESTAMP", "Oracle no tiene un tipo TIME específico, pero TIMESTAMP puede manejar la hora."),

            // Tipos de Cadenas
            new DataTypeMapping("CHAR", "CHAR", "Ambos almacenan cadenas de longitud fija."),
            new DataTypeMapping("VARCHAR", "VARCHAR2", "VARCHAR en SQL Server es equivalente a VARCHAR2 en Oracle."),
            new DataTypeMapping("TEXT", "CLOB", "Ambos almacenan grandes cantidades de texto."),
            new DataTypeMapping("NCHAR", "NCHAR", "Ambos almacenan cadenas Unicode de longitud fija."),
            new DataTypeMapping("NVARCHAR", "NVARCHAR2", "NVARCHAR en SQL Server es equivalente a NVARCHAR2 en Oracle."),
            new DataTypeMapping("NTEXT", "NCLOB", "Ambos almacenan grandes cantidades de texto Unicode."),

            // Tipos Binarios
            new DataTypeMapping("BINARY", "RAW", "Ambos almacenan datos binarios de longitud fija."),
            new DataTypeMapping("VARBINARY", "RAW", "Ambos almacenan datos binarios de longitud variable."),
            new DataTypeMapping("IMAGE", "BLOB", "Ambos almacenan grandes datos binarios."),

            // Tipos Espaciales
            new DataTypeMapping("GEOMETRY", "SDO_GEOMETRY", "Ambos manejan tipos espaciales de geometría."),
            new DataTypeMapping("GEOGRAPHY", "SDO_GEOMETRY", "SDO_GEOMETRY en Oracle también cubre datos geográficos."),

            // Tipos de Identificadores y Referencias
            new DataTypeMapping("UNIQUEIDENTIFIER", "RAW(16)", "Ambos almacenan identificadores únicos."),
            new DataTypeMapping("ROWVERSION", "ROWID", "Ambos identifican filas de forma única para control de concurrencia."),

            // Tipos Especiales y Colecciones
            new DataTypeMapping("XML", "XMLTYPE", "Ambos manejan datos XML."),
            new DataTypeMapping("SQL_VARIANT", "ANYDATA", "Ambos permiten almacenar múltiples tipos de datos en una columna."),
            new DataTypeMapping("TABLE", "NESTED TABLE", "Ambos manejan estructuras de tablas dentro de otras tablas."),

            // Tipos de Intervalos
            new DataTypeMapping("No aplica", "INTERVAL YEAR TO MONTH", "Oracle maneja intervalos de tiempo en meses y años."),
            new DataTypeMapping("No aplica", "INTERVAL DAY TO SECOND", "Oracle ofrece intervalos precisos hasta segundos."),

            // Tipos Booleanos
            new DataTypeMapping("BIT", "BOOLEAN", "Oracle maneja valores lógicos con BOOLEAN, mientras SQL Server usa BIT.")
        };
            }
        }

        public void carga()
        {
            DataTypeMappings mappings = new DataTypeMappings();

            // Mostrar los mapeos de tipos de datos
            foreach (var mapeo in mappings.MapeosDeTipos)
            {
                Console.WriteLine($"SQL Server: {mapeo.SqlServerType}, Oracle: {mapeo.OracleType}, Observación: {mapeo.Observacion}");
            }


        }
    }
}
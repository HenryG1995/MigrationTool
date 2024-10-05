using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ToolMigration.Logic.DataModels;
using System.Diagnostics;

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
        public class DataConvertPerso 
        {
           

            public List<DataTypeConvert> llenalista(List<DataTypeConvert> ListaValores)
            {

                DataTypeConvert dto = new DataTypeConvert();
                // VARBINARY(MAX)
                dto.NO = 1;
                dto.Tipo = "VARBINARY(MAX)";
                dto.Propiedad = "";
                dto.Equivalencia = "BLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Almacena grandes cantidades de datos binarios en Oracle.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                
               
                // VARCHAR(MAX)
                dto.NO = 2;
                dto.Tipo = "VARCHAR(MAX)";
                dto.Propiedad = "";
                dto.Equivalencia = "CLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Oracle utiliza CLOB para grandes cadenas de caracteres.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // NVARCHAR(MAX)
                dto.NO = 3;
                dto.Tipo = "NVARCHAR(MAX)";
                dto.Propiedad = "";
                dto.Equivalencia = "NCLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Almacena grandes cantidades de texto Unicode en Oracle.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // TINYINT
                dto.NO = 4;
                dto.Tipo = "TINYINT";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(3)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son enteros pequeños sin signo.";

                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // SMALLINT
                dto.NO = 5;
                dto.Tipo = "SMALLINT";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(5)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son enteros pequeños.";

                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // INT
                dto.NO = 6;
                dto.Tipo = "INT";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(10)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Equivalencia de enteros en ambas plataformas.";

                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // BIGINT
                dto.NO = 7;
                dto.Tipo = "BIGINT";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(19)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son enteros grandes.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // BIT
                dto.NO = 8;
                dto.Tipo = "BIT";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(1,0)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle se utiliza NUMBER(1) para simular booleanos.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // DECIMAL
                dto.NO = 9;
                dto.Tipo = "DECIMAL";
                dto.Propiedad = "(p, s)";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(p, s)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos manejan decimales con precisión y escala. este valor es dependiendo del valor de la tabla se recomienda dejarlo estandard";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // NUMERIC
                    dto.NO = 10;
                dto.Tipo = "NUMERIC";
                dto.Propiedad = "(p, s)";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(p, s)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Funcionalmente idéntico al DECIMAL. este valor es dependiendo del valor de la tabla se recomienda dejarlo estandard";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // FLOAT
                dto.NO = 11;
                dto.Tipo = "FLOAT";
                dto.Propiedad = "";
                dto.Equivalencia = "BINARY_DOUBLE";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son tipos de punto flotante de precisión variable. este valor es dependiendo del valor de la tabla se recomienda dejarlo estandard";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // REAL
                dto.NO = 12;
                dto.Tipo = "REAL";
                dto.Propiedad = "";
                dto.Equivalencia = "FLOAT";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle se utiliza FLOAT como equivalente. este valor es dependiendo del valor de la tabla se recomienda dejarlo estandard";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // DATETIME
                dto.NO = 13;
                dto.Tipo = "DATETIME";
                dto.Propiedad = "";
                dto.Equivalencia = "TIMESTAMP";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, TIMESTAMP maneja fecha y hora de manera similar a DATETIME.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // DATE
                dto.NO = 14;
                dto.Tipo = "DATE";
                dto.Propiedad = "";
                dto.Equivalencia = "DATE";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos tipos almacenan únicamente la fecha sin la hora.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // TIME
                dto.NO = 15;
                dto.Tipo = "TIME";
                dto.Propiedad = "";
                dto.Equivalencia = "INTERVAL DAY TO SECOND";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Oracle usa INTERVAL DAY TO SECOND para representar el tiempo.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // CHAR
                dto.NO = 16;
                dto.Tipo = "CHAR";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "CHAR";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son tipos de cadena de longitud fija.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // VARCHAR
                dto.NO = 17;
                dto.Tipo = "VARCHAR";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "VARCHAR2";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle se utiliza VARCHAR2, que es más común para cadenas de longitud variable. observacion (si la cadena es mayor a 4000 automaticamente se cambia a CLOB ya que varchar2 solo tiene espacio para 4000 caracteres, si usted tiene modificada su base de datos debe de modificar la logitud maxima en este campo)";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // TEXT
                dto.NO = 18;
                dto.Tipo = "TEXT";
                dto.Propiedad = "";
                dto.Equivalencia = "CLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, se utiliza CLOB para grandes cantidades de texto.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // BINARY
                dto.NO = 19;
                dto.Tipo = "BINARY";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "RAW";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos almacenan datos binarios de longitud fija.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // VARBINARY
                dto.NO = 20;
                dto.Tipo = "VARBINARY";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "RAW";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Almacenan datos binarios de longitud variable.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // IMAGE
                dto.NO = 21;
                dto.Tipo = "IMAGE";
                dto.Propiedad = "";
                dto.Equivalencia = "BLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, BLOB se usa para almacenar datos binarios de gran tamaño, como imágenes.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // SMALLDATETIME
                dto.NO = 22;
                dto.Tipo = "SMALLDATETIME";
                dto.Propiedad = "";
                dto.Equivalencia = "DATE";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle se usa DATE, que incluye la fecha y la hora con una precisión menor.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // DATETIME2
                dto.NO = 23;
                dto.Tipo = "DATETIME2";
                dto.Propiedad = "(fractional seconds)";
                dto.Equivalencia = "TIMESTAMP";
                dto.EqPropiedad = "(fractional seconds)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos tipos permiten un control preciso sobre las fracciones de segundo.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // DATETIMEOFFSET
                dto.NO = 24;
                dto.Tipo = "DATETIMEOFFSET";
                dto.Propiedad = "(fractional seconds)";
                dto.Equivalencia = "TIMESTAMP WITH TIME ZONE";
                dto.EqPropiedad = "(fractional seconds)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos almacenan la fecha y la hora junto con el huso horario.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // MONEY
                dto.NO = 25;
                dto.Tipo = "MONEY";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(19,4)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Oracle usa NUMBER(19,4) para representar valores monetarios de alta precisión.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // SMALLMONEY
                dto.NO = 26;
                dto.Tipo = "SMALLMONEY";
                dto.Propiedad = "";
                dto.Equivalencia = "NUMBER";
                dto.EqPropiedad = "(10,4)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Oracle usa NUMBER(10,4) para valores monetarios más pequeños.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // UNIQUEIDENTIFIER
                dto.NO = 27;
                dto.Tipo = "UNIQUEIDENTIFIER";
                dto.Propiedad = "";
                dto.Equivalencia = "RAW";
                dto.EqPropiedad = "(16)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, RAW(16) se utiliza para almacenar GUIDs o identificadores únicos.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // XML
                    dto.NO = 28;
                dto.Tipo = "XML";
                dto.Propiedad = "";
                dto.Equivalencia = "XMLTYPE";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos proporcionan un tipo de dato especializado para almacenar y consultar datos XML.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // NVARCHAR
                dto.NO = 29;
                dto.Tipo = "NVARCHAR";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "NVARCHAR2";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son tipos de datos de cadena de longitud variable que soportan Unicode.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // NCHAR
                dto.NO = 30;
                dto.Tipo = "NCHAR";
                dto.Propiedad = "(n)";
                dto.Equivalencia = "NCHAR";
                dto.EqPropiedad = "(n)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Ambos son tipos de datos de cadena de longitud fija que soportan Unicode.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // NTEXT
                dto.NO = 31;
                dto.Tipo = "NTEXT";
                dto.Propiedad = "";
                dto.Equivalencia = "NCLOB";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Oracle utiliza NCLOB para grandes cantidades de texto Unicode.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // ROWVERSION (TIMESTAMP en SQL Server)
                dto.NO = 32;
                dto.Tipo = "ROWVERSION";
                dto.Propiedad = "";
                dto.Equivalencia = "RAW";
                dto.EqPropiedad = "(8)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, se puede usar RAW(8) para almacenar valores de control de versiones en filas.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // GEOGRAPHY
                dto.NO = 33;
                dto.Tipo = "GEOGRAPHY";
                dto.Propiedad = "";
                dto.Equivalencia = "SDO_GEOMETRY";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "En Oracle, SDO_GEOMETRY se usa para almacenar datos espaciales o geográficos.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();
                // HIERARCHYID
                dto.NO = 34;
                dto.Tipo = "HIERARCHYID";
                dto.Propiedad = "";
                dto.Equivalencia = "N/A";
                dto.EqPropiedad = "";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "No tiene una equivalencia directa en Oracle. Necesitaría modelarse mediante otras estructuras de datos.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();

                // TIMESTAMP
                dto.NO = 35;
                dto.Tipo = "TIMESTAMP";
                dto.Propiedad = "";
                dto.Equivalencia = "RAW";
                dto.EqPropiedad = "(8)";
                dto.PersoType = "ingrese valores";
                dto.PropPersoType = "ingrese valores";
                dto.Observacion = "Se utiliza para marcar versiones de filas en Oracle.";
                 ListaValores.Add(dto); dto = new DataTypeConvert();

                return ListaValores.ToList();
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
                Debug.WriteLine($"SQL Server: {mapeo.SqlServerType}, Oracle: {mapeo.OracleType}, Observación: {mapeo.Observacion}");
            }


        }
    }
}
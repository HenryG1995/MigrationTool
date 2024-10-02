using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Data;
using ToolMigration.Logic.DataModels;
using ToolMigration.Logic.Connections;
namespace ToolMigration.Logic.Transformation
{
    public class GenScriptDestino
    {
        public List<string> GenScripts { get; set; }


        public void GenScriptO(string rutaArchivo, string formato)
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
            var script_insert = "SELECT 'SELECT '''+\r\n(select 'INSERT INTO \"'+  T.TABLE_NAME + '\" ('+ STRING_AGG( '\"'+T.column_name+'\"',',') +') VALUES (''+'  from INFORMATION_SCHEMA.columns T where T.table_name = '" + tabla + "' GROUP BY T.TABLE_NAME) +''''+\r\n(\r\nSELECT\r\nSTRING_AGG(\r\n(        CASE\r\n            WHEN DATA_TYPE IN ('varchar', 'nvarchar', 'char', 'nchar', 'text', 'ntext', 'date', 'datetime', 'datetime2',\r\n                               'smalldatetime', 'time', 'timestamp') THEN CONCAT('''''''+[', COLUMN_NAME, ']+''''''')\r\n            WHEN DATA_TYPE IN ('int', 'bit', 'bigint', 'smallint', 'tinyint', 'decimal', 'numeric', 'float', 'real')\r\n                THEN COLUMN_NAME\r\n            ELSE 'Otro tipo de dato' END )\r\n,',')\r\n    AS COLUMN_NAME_WITH_QUOTES\r\nFROM INFORMATION_SCHEMA.COLUMNS T where T.table_name = '" + tabla + "')+')'' FROM " + tabla + "'";

            return script_insert;
        }

        public string GenScriptIndexes(string tabla)
        {
            string v_script = string.Empty;

            v_script = @"
                            SELECT
                                'CREATE ' +
                                CASE WHEN i.is_unique = 1 THEN 'UNIQUE' ELSE '' END COLLATE Latin1_General_CI_AS +  
                             
                                    + ' INDEX ' +
                                 (i.name) COLLATE Latin1_General_CI_AS +  
                                ' ON ' +  (t.name) COLLATE Latin1_General_CI_AS + ' (' +
                                STRING_AGG( (c.name), ', ') WITHIN GROUP (ORDER BY ic.key_ordinal)
                                COLLATE Latin1_General_CI_AS + ')' +  
                                CASE
                                    WHEN ic_included.included_columns IS NOT NULL THEN
                                        ' INCLUDE (' + ic_included.included_columns COLLATE Latin1_General_CI_AS + ')'  
                                    ELSE ''
                                END COLLATE Latin1_General_CI_AS + ';' AS create_index_script
                            FROM
                                sys.indexes i
                            JOIN
                                sys.tables t ON i.object_id = t.object_id
                            JOIN
                                sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                            JOIN
                                sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                            LEFT JOIN
                                (
                                    SELECT ic2.object_id, ic2.index_id,
                                           STRING_AGG( (c2.name), ', ') AS included_columns
                                    FROM sys.index_columns ic2
                                    JOIN sys.columns c2 ON ic2.object_id = c2.object_id AND ic2.column_id = c2.column_id
                                    WHERE ic2.is_included_column = 1
                                    GROUP BY ic2.object_id, ic2.index_id
                                ) ic_included ON i.object_id = ic_included.object_id AND i.index_id = ic_included.index_id
                            WHERE
                                t.name = '" + tabla.ToString() + @"' 
                             
                            GROUP BY
                                i.name, i.is_unique, i.type_desc, t.name, ic_included.included_columns;";

            return v_script;
        }

        public string GenScriptConstraintForeing(string tabla)
        {
            var v_script = string.Empty;

            v_script = @"
                        SELECT
                            'ALTER TABLE ' +  (parent_table.name) +
                            ' ADD CONSTRAINT ' + (fk.name) +
                            ' FOREIGN KEY ' + (parent_col.name) +
                            ' REFERENCES ' + (referenced_table.name) +
                            '(' + (referenced_col.name) + ');' AS foreign_key_script
                        FROM
                            sys.foreign_keys fk
                        JOIN
                            sys.foreign_key_columns fkc
                            ON fk.object_id = fkc.constraint_object_id
                        JOIN
                            sys.columns parent_col
                            ON fkc.parent_object_id = parent_col.object_id
                            AND fkc.parent_column_id = parent_col.column_id
                        JOIN
                            sys.columns referenced_col
                            ON fkc.referenced_object_id = referenced_col.object_id
                            AND fkc.referenced_column_id = referenced_col.column_id
                        JOIN
                            sys.tables parent_table
                            ON fk.parent_object_id = parent_table.object_id
                        JOIN
                            sys.tables referenced_table
                            ON fk.referenced_object_id = referenced_table.object_id
                        WHERE
                            parent_table.name = '" + tabla + "';";

            return v_script;
        }

        public string GenScriptPrimaryKey(string tabla)
        {

            string v_script = string.Empty;

            v_script = @"
                          SELECT
                            'ALTER TABLE ' + (t.name) +
                            ' ADD CONSTRAINT ' + (kc.name) +
                            ' PRIMARY KEY (' + STRING_AGG( (c.name), ', ') WITHIN GROUP (ORDER BY ic.key_ordinal) + ');' AS primary_key_script
                        FROM
                            sys.key_constraints kc
                        JOIN
                            sys.tables t
                            ON kc.parent_object_id = t.object_id
                        JOIN
                            sys.index_columns ic
                            ON kc.parent_object_id = ic.object_id
                            AND kc.unique_index_id = ic.index_id
                        JOIN
                            sys.columns c
                            ON ic.object_id = c.object_id
                            AND ic.column_id = c.column_id
                        WHERE
                            kc.type = 'PK' -- 'PK' indica clave primaria
                            AND t.name = '"+tabla.ToString()+@"' 
                        GROUP BY
                            kc.name, t.name;";

            return v_script;
        }

        public string GenScriptTablesDefault(string tabla, List<DataTypeConvert> tipos)
        {
            string v_script = string.Empty;

            v_script = "CREATE TABLE " + tabla.ToUpper() + " (";
            
            foreach (var item in tipos)
            {

            }
            
            
            v_script = " )";





            return v_script;
        }

        public string GenScriptTablesPerso(string tabla,List<DataTypeConvert> tipos)
        {
            string v_script = string.Empty;

            


            return v_script;
        }

        public List<DataTypeOrigenXTable> ColumTypeXTable(string tabla,string SQL_CON)
        {
        List<DataTypeOrigenXTable> v_columns = new List<DataTypeOrigenXTable>();

            Conn ExecuteQuery = new Conn();
            //   var lista = ExecuteQuery.ExecuteQuery(v_script, SQL_CON);

            v_columns = ExecuteQuery.ListaColumnas(SQL_CON,tabla);


            return v_columns;
        }


    }
}

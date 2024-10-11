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
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
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

                Debug.WriteLine("Archivo creado correctamente: " + rutaArchivo);


            });



        }

        public string GENSCRIPT(string tabla)
        {
            var script_insert = @"
    select UPPER(dt.ainsert)+dt.SCRIPT from (

        SELECT 'SELECT ''' +
       (select 'INSERT INTO ""' + T.TABLE_NAME + '"" (' + STRING_AGG('""' + T.column_name + '""', ',') + ')  VALUES ('
        from INFORMATION_SCHEMA.columns T
        where T.table_name = '"+ tabla + @"'
        GROUP BY T.TABLE_NAME) ainsert ,
       (SELECT STRING_AGG(
                       (CASE
                           WHEN DATA_TYPE IN ('date', 'datetime', 'datetime2', 'time', 'timestamp')
                            THEN CONCAT(''''''''+'+ISNULL(ISNULL(CONVERT(CHAR(19),[', COLUMN_NAME, '], 120),''''),''null'')+''''''')
                            WHEN DATA_TYPE IN
                                ('varchar', 'nvarchar', 'char', 'nchar', 'text', 'ntext',
                                  'smalldatetime',
                                 'binary', 'varbinary', 'image', 'uniqueidentifier', 'hierarchyid', 'xml')
                                THEN CONCAT(''''''''+'+ISNULL(ISNULL([', COLUMN_NAME, '],''''),''null'')+''''''')
                            WHEN DATA_TYPE IN
                                ('int', 'bit', 'bigint', 'smallint', 'tinyint', 'decimal', 'numeric', 'float', 'real',
                                 'money', 'smallmoney', 'binary', 'varbinary', 'rowversion', 'uniqueidentifier',
                                 'geometry', 'geography', 'sql_variant', 'cursor', 'table')

                                THEN ''''''''+'+isnull(isnull(CAST([' + COLUMN_NAME + '] AS VARCHAR),''''),''''''''+''null''+'''''''')+'''''''
                            ELSE 'isnull('''',null)' END)
                   , ',')
                   AS COLUMN_NAME_WITH_QUOTES

    FROM INFORMATION_SCHEMA.COLUMNS T
        where T.table_name = '"+ tabla + @"') + ' )''AS SCRIPT FROM "+ tabla + @"' as ""SCRIPT"" ) as dt";

            return script_insert;
        }

        public string GenScriptIndexes(string tabla)
        {
            string v_script = string.Empty;


            v_script = @"  SELECT
                                'CREATE ' +
                                CASE WHEN i.is_unique = 1 THEN 'UNIQUE' ELSE '' END COLLATE Latin1_General_CI_AS +

                                    + ' INDEX ' 
                                +  '""'+ UPPER(i.name) +  '""' COLLATE Latin1_General_CI_AS +
                                ' ON ' +  '""'+(UPPER(t.name))+'""' COLLATE Latin1_General_CI_AS + ' (' +
                                STRING_AGG( '""'+(UPPER(c.name))+'""', ', ') WITHIN GROUP (ORDER BY ic.key_ordinal)
                                COLLATE Latin1_General_CI_AS + ')' +
                                 ';' AS script
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

            //v_script = @"
            //                SELECT
            //                    'CREATE ' +
            //                    CASE WHEN i.is_unique = 1 THEN 'UNIQUE' ELSE '' END COLLATE Latin1_General_CI_AS +  
                             
            //                        + ' INDEX ' +
            //                     (i.name) COLLATE Latin1_General_CI_AS +  
            //                    ' ON ' +  (t.name) COLLATE Latin1_General_CI_AS + ' (' +
            //                    STRING_AGG( ("+"\""+@"c.name"+"\""+@"), ', ') WITHIN GROUP (ORDER BY ic.key_ordinal)
            //                    COLLATE Latin1_General_CI_AS + ')' +  
            //                    CASE
            //                        WHEN ic_included.included_columns IS NOT NULL THEN
            //                            ' INCLUDE (' + ic_included.included_columns COLLATE Latin1_General_CI_AS + ')'  
            //                        ELSE ''
            //                    END COLLATE Latin1_General_CI_AS + ';' AS script
            //                FROM
            //                    sys.indexes i
            //                JOIN
            //                    sys.tables t ON i.object_id = t.object_id
            //                JOIN
            //                    sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
            //                JOIN
            //                    sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
            //                LEFT JOIN
            //                    (
            //                        SELECT ic2.object_id, ic2.index_id,
            //                               STRING_AGG( (c2.name), ', ') AS included_columns
            //                        FROM sys.index_columns ic2
            //                        JOIN sys.columns c2 ON ic2.object_id = c2.object_id AND ic2.column_id = c2.column_id
            //                        WHERE ic2.is_included_column = 1
            //                        GROUP BY ic2.object_id, ic2.index_id
            //                    ) ic_included ON i.object_id = ic_included.object_id AND i.index_id = ic_included.index_id
            //                WHERE
            //                    t.name = '" + tabla.ToString() + @"' 
                             
            //                GROUP BY
            //                    i.name, i.is_unique, i.type_desc, t.name, ic_included.included_columns;";

            return v_script;
        }

        public string GenScriptConstraintForeing(string tabla)
        {
            var v_script = string.Empty;

            v_script = @"    SELECT
                            'ALTER TABLE ' +  '""'+UPPER(parent_table.name)+'""' +
                            ' ADD CONSTRAINT ' + '""'+UPPER(fk.name)+'""' +
                            ' FOREIGN KEY ' +( UPPER(  '(""'+parent_col.name+'"")') )+
                            ' REFERENCES ' + UPPER(  '""'+referenced_table.name+'""') +
                            '(' + UPPER(  '""'+referenced_col.name+'""') + ');' AS script
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

            //v_script = @"
            //            SELECT
            //                'ALTER TABLE ' +  UPPER(parent_table.name) +
            //                ' ADD CONSTRAINT ' + UPPER(fk.name) +
            //                ' FOREIGN KEY ' + UPPER(  " + "\""+@"parent_col.name"+"\""+ @") +
            //                ' REFERENCES ' + UPPER(  " + "\""+@"referenced_table.name"+"\""+ @" ) +
            //                '(' + UPPER(  " + "\""+@"referenced_col.name"+"\""+@") + ');' AS script
            //            FROM
            //                sys.foreign_keys fk
            //            JOIN
            //                sys.foreign_key_columns fkc
            //                ON fk.object_id = fkc.constraint_object_id
            //            JOIN
            //                sys.columns parent_col
            //                ON fkc.parent_object_id = parent_col.object_id
            //                AND fkc.parent_column_id = parent_col.column_id
            //            JOIN
            //                sys.columns referenced_col
            //                ON fkc.referenced_object_id = referenced_col.object_id
            //                AND fkc.referenced_column_id = referenced_col.column_id
            //            JOIN
            //                sys.tables parent_table
            //                ON fk.parent_object_id = parent_table.object_id
            //            JOIN
            //                sys.tables referenced_table
            //                ON fk.referenced_object_id = referenced_table.object_id
            //            WHERE
            //                parent_table.name = '" + tabla + "';";

            return v_script;
        }

        public string GenScriptPrimaryKey(string tabla)
        {

            string v_script = string.Empty;

            v_script = @"                                          SELECT
                            'ALTER TABLE ' + UPPER( '""'+t.name+'""') +
                            ' ADD CONSTRAINT ' +  '""'+UPPER(kc.name)+'""'+
                            ' PRIMARY KEY (' + STRING_AGG( UPPER( '""'+c.name+'""'), ', ') WITHIN GROUP (ORDER BY ic.key_ordinal) + ');' AS script
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
                            AND t.name = '"+ tabla + @"'
                        GROUP BY
                            kc.name, t.name;";

            return v_script;
        }

        public List<scriptList> GenScriptText(string script, string sqlcoonection)
        {
            List<scriptList> list = new List<scriptList>();


            var sql_command = script;

            using (SqlConnection connection = new SqlConnection(sqlcoonection))
            {
                try
                {
                    connection.Open(); // Open the connection before executing commands

                    SqlCommand command = new SqlCommand(sql_command, connection); // Specify the connection explicitly

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                    

                        while (reader.Read())
                        {
                            try // Handle potential data conversion errors within the loop
                            {
                                scriptList tab = new scriptList
                                {
                                    script = reader.GetString(reader.GetOrdinal("script"))
                                    

                                };
                                list.Add(tab);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error parsing data in row: {ex.Message}");
                                // Consider logging the error or taking appropriate action
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Debug.WriteLine($"Error opening connection or executing command: {ex.Message}");
                    // Consider logging the error or throwing a more specific exception
                }
            }

            return list;


        }


        public string GenScriptTablesDefault(string tabla, List<DataTypeConvert> tipos,string sqlconnection)
        {
            string v_script = string.Empty;

            v_script = "CREATE TABLE " + tabla.ToUpper() + " (  ";

            Conn conn = new Conn();

            List<DataTypeOrigenXTable> listaColumnas = conn.ListaColumnas(sqlconnection, tabla);

            foreach(var item in listaColumnas)
            {

                v_script= v_script + "\"" + item.COLUMN_NAME+ "\"";
                v_script = v_script + " ";
                v_script = v_script + " ";
                //asigna el tipo de dato y la propiedad
                foreach (var item2 in tipos)
                {
                    if (item.DATA_TYPE.ToUpper().Replace(" ","") == item2.Tipo.ToUpper().Replace(" ", ""))
                    {
                        if (item.DATA_LENGTH.Replace(" ", "") is not null)
                        {
                            if (item.DATA_LENGTH.Contains("MAX") && item.DATA_TYPE.ToUpper() == "VARCHAR")
                            {
                                v_script = v_script + " CLOB ";
                                break;

                            }
                            else if (item.DATA_TYPE.ToUpper() == "VARCHAR" && item.DATA_LENGTH.Contains("8000"))
                            {
                                v_script = v_script + " CLOB ";
                                break;

                            }
                            else if (item.DATA_LENGTH.Contains("MAX") && item.DATA_TYPE.ToUpper() == "VARBINARY")
                            {
                                v_script = v_script + " BLOB ";
                                break;
                            }
                            else if (item.DATA_LENGTH.Contains("MAX") && item.DATA_TYPE.ToUpper() == "NVARCHAR")
                            {
                                v_script = v_script + " NCLOB ";
                                break;
                            }
                            else if (item.DATA_TYPE.ToUpper().Contains("VARCHAR"))
                            {
                                if (Convert.ToInt32(item.DATA_LENGTH.ToString()) > 4000)
                                {
                                    v_script = v_script + " CLOB ";
                                    break;
                                }
                                else
                                {
                                    v_script = v_script + "  " + item2.Equivalencia.ToString();

                                    v_script = v_script + " (" + item.DATA_LENGTH.ToString() + " )";
                                    break;
                                }

                            }else if (item.DATA_TYPE.ToUpper() == "FLOAT")
                            {
                                v_script = v_script + " FLOAT ";
                            }
                            else
                            {
                                v_script = v_script + "  " + item2.Equivalencia;
                                if (item.DATA_LENGTH.Replace(" ","").Length > 0)
                                {
                                    v_script = v_script + " (" + item.DATA_LENGTH + ") ";
                                }
                                else
                                {
                                    v_script = v_script + " " + item2.EqPropiedad + " ";
                                }
                                break;

                            }
                        }
                        else
                        {
                            v_script = v_script + "  " + item2.Equivalencia;
                            if (item.DATA_LENGTH.Replace(" ", "").Length > 0)
                            {
                                v_script = v_script + " (" + item.DATA_LENGTH + ") ";
                            }
                            else
                            {
                                v_script = v_script + " " + item2.EqPropiedad + " ";
                            }
                            break;

                        }
                     
                         
                      
                       
                    }

                }
                // asignar si es  nullable o no

                if (item.IS_NULLABLE == true)
                {
                    v_script = v_script + " NULL ,";
                }else
                {
                    v_script = v_script + " NOT NULL ,";
                }




            }
            string resultado = v_script.Substring(0, v_script.Length - 1);

            resultado = resultado + " )";


            return resultado;
        }

        public string GenScriptTablesPerso(string tabla,List<DataTypeConvert> tipos,string SQL_CON)
        {
            string v_script = string.Empty;

            v_script = "CREATE TABLE " + tabla.ToUpper() + " (  ";

            Conn conn = new Conn();

            List<DataTypeOrigenXTable> listaColumnas = conn.ListaColumnas(SQL_CON, tabla);

            foreach (var item in listaColumnas)
            {

                v_script = v_script + "\"" + item.COLUMN_NAME + "\"";
                v_script = v_script + " ";
                v_script = v_script + " ";
                //asigna el tipo de dato y la propiedad
                foreach (var item2 in tipos)
                {
                    if (item.DATA_TYPE.ToUpper() == item2.Tipo.ToUpper())
                    {

                        if (item2.PersoType.Replace(" ", "") is not null && item2.PersoType.Contains("ingrese valores") == false)
                        {
                            v_script = v_script + "  " + item2.PersoType;

                            if (item2.PropPersoType.Replace(" ", "") is not null)
                            {
                                if (item2.PropPersoType.Contains("("))
                                {
                                    v_script = v_script + " " + item2.PropPersoType + " ";
                                }
                                else
                                {
                                    v_script = v_script + " (" + item2.PropPersoType + ") ";
                                }


                            }
                            break;

                        }
                        else
                        {// default para tipos personalizados que no coinciden con los criterios.
                            if (item.DATA_TYPE.ToUpper().Replace(" ", "") == item2.Tipo.ToUpper().Replace(" ", ""))
                            {
                                if (item.DATA_LENGTH.Replace(" ", "") is not null)
                                {
                                    if (item.DATA_LENGTH.ToUpper().Contains("MAX") && item.DATA_TYPE.ToUpper() == "VARCHAR")
                                    {
                                        v_script = v_script + " CLOB ";
                                        break;

                                    }
                                    else if (item.DATA_TYPE.ToUpper() == "VARCHAR" && item.DATA_LENGTH.Contains("8000"))
                                    {
                                        v_script = v_script + " CLOB ";
                                        break;

                                    }
                                    else if (item.DATA_LENGTH.ToUpper().Contains("MAX") && item.DATA_TYPE.ToUpper() == "VARBINARY")
                                    {
                                        v_script = v_script + " BLOB ";
                                        break;
                                    }
                                    else if (item.DATA_LENGTH.ToUpper().Contains("MAX") && item.DATA_TYPE.ToUpper() == "NVARCHAR")
                                    {
                                        v_script = v_script + " NCLOB ";
                                        break;
                                    }
                                    else if (item.DATA_TYPE.ToUpper().Contains("VARCHAR"))
                                    {
                                        if (Convert.ToInt32(item.DATA_LENGTH.ToUpper().ToString()) > 4000)
                                        {
                                            v_script = v_script + " CLOB ";
                                            break;
                                        }
                                        else
                                        {
                                            v_script = v_script + "  " + item2.Equivalencia.ToUpper().ToString();

                                            v_script = v_script + " (" + item.DATA_LENGTH.ToUpper().ToString() + " )";
                                            break;
                                        }

                                    }
                                    else if (item.DATA_TYPE.ToUpper() == "FLOAT")
                                    {
                                        v_script = v_script + " FLOAT ";
                                    }
                                    else
                                    {
                                        v_script = v_script + "  " + item2.Equivalencia;
                                        if (item.DATA_LENGTH.ToUpper().Replace(" ", "").Length > 0)
                                        {
                                            v_script = v_script + " (" + item.DATA_LENGTH + ") ";
                                        }
                                        else
                                        {
                                            v_script = v_script + " " + item2.EqPropiedad.ToUpper().ToString() + " ";
                                        }
                                        break;

                                    }
                                }
                                else
                                {
                                    v_script = v_script + "  " + item2.Equivalencia;
                                    if (item.DATA_LENGTH.Replace(" ", "").Length > 0)
                                    {
                                        v_script = v_script + " (" + item.DATA_LENGTH + ") ";
                                    }
                                    else
                                    {
                                        v_script = v_script + " " + item2.EqPropiedad + " ";
                                    }
                                    break;

                                }
                            }
                        }

                    }
                }

                // asignar si es  nullable o no

                if (item.IS_NULLABLE == true)
                {
                    v_script = v_script + " NULL ,";
                }
                else
                {
                    v_script = v_script + " NOT NULL ,";
                }


            }
            string resultado = v_script.Substring(0, v_script.Length - 1);

            resultado = resultado + " )";


            return resultado;
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

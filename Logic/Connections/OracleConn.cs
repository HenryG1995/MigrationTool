
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;
using System.IO;
using ToolMigration.Logic.DataModels;
using ToolMigration.Logic.Transformation;

namespace ToolMigration.Logic.Connections
{

    public class Conn
    {

        ConnString conections = new ConnString();

        public string SqlConection { get; set; }
        public string OraConection { get; set; }

        public bool Oratest(string UserID = "db_tienda",
                        string Pass = "Andromeda12/",
                        string Host = "192.168.0.17",
                        string Port = "1521",
                        string SID = "ORCL")
        {
            string connectionString;

            connectionString = string.Format("USER ID={0};PASSWORD={1};DATA SOURCE= (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = {3}))(CONNECT_DATA = (SID = {4}))) ;Min Pool Size=5;Max Pool Size=100;Incr Pool Size=15;Connection Timeout=0;", UserID, Pass, Host, Port, SID);

            conections.ORAString = connectionString;

            OraConection = connectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
            
                    OracleCommand command = new OracleCommand("SELECT SYS_GUID() UUID FROM DUAL", connection);
            
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Procesar los datos
                        Debug.WriteLine(reader["UUID"].ToString());
                    }

                    reader.Close();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                   // Debug.WriteLine("Error al conectar: " + ex.Message);
                    return false;
                }
            };
        }


        public void cierra_sessiones(string connectionString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OracleConnection.ClearAllPools();

                    connection.Close();
                }
                catch (OracleException ex)
                {
                    Console.WriteLine($"Error en Oracle: {ex.Message}");
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();

                        Console.WriteLine("Conexión cerrada.");
                    }
                }
            }
        }

        public bool SqlTest(string usuario, string pass, string host, string port, string database)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = host + "," + port;
            
            builder.InitialCatalog = database;
            
            builder.UserID = usuario;
            
            builder.Password = pass;
            
            builder.TrustServerCertificate = true;

            string connectionString = builder.ConnectionString;

            conections.ORAString = connectionString;

            SqlConection = connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString: connectionString))

            {
                try
                {
                    connection.Open(); 

                    Debug.WriteLine("Conexion SQL establecida correctamente");

                    SqlCommand command = new SqlCommand("SELECT NEWID() AS UUID", connection); 
                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Debug.WriteLine(reader["UUID"].ToString());
                    }

                    reader.Close();

                    connection.Close();
                    
                    return true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    
                    Debug.WriteLine("Error: " + ex.Message);
                    
                    return false;
                }
            }

        }

        public List<DataTypeOrigenXTable> ListaColumnas(string connectionString, string table_name)
        {
            List<DataTypeOrigenXTable> lista = new List<DataTypeOrigenXTable>();

            MetaDataCore metaDataCore = new MetaDataCore();

            var sql_command = metaDataCore.ALL_TYPE_COLUMNS_SQLMODEL_X_TABLE(table_name);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Open the connection before executing commands

                    SqlCommand command = new SqlCommand(sql_command, connection); // Specify the connection explicitly

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        DataTable dt = new DataTable();

                        //dt.Load(reader);

                        //lista = ConvertDataTableToList(dt);

                        while (reader.Read())
                        {
                            try // Handle potential data conversion errors within the loop
                            {
                                DataTypeOrigenXTable tab = new DataTypeOrigenXTable
                                {
                                    TABLE_NAME = reader.GetString(reader.GetOrdinal("TABLE_NAME")),
                                    COLUMN_NAME = reader.GetString(reader.GetOrdinal("COLUMN_NAME")),
                                    ORDINAL_POSITION = reader.GetInt32(reader.GetOrdinal("ORDINAL_POSITION")),
                                    COLUMN_DEFAULT = reader.GetString(reader.GetOrdinal("COLUMN_DEFAULT")),
                                    IS_NULLABLE = reader.GetBoolean(reader.GetOrdinal("IS_NULLABLE")),
                                    DATA_TYPE = reader.GetString(reader.GetOrdinal("DATA_TYPE")),
                                    DATA_TYPE_DETAIL = reader.GetString(reader.GetOrdinal("DATA_TYPE_DETAIL")),
                                    DATA_LENGTH = reader.GetString(reader.GetOrdinal("DATA_LENGTH"))

                                };
                                lista.Add(tab);

                            }
                            catch (Exception ex)
                            {
                                connection.Close();
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



            return lista;
        }

        public List<DataTypeOrigenXTable> ConvertDataTableToList(DataTable dt)
        {
            var list = new List<DataTypeOrigenXTable>();

            foreach (DataRow row in dt.Rows)
            {
                var columnInfo = new DataTypeOrigenXTable
                {
                    TABLE_NAME = row["TABLE_NAME"] != DBNull.Value ? row["TABLE_NAME"].ToString() : null,
                    COLUMN_NAME = row["COLUMN_NAME"] != DBNull.Value ? row["COLUMN_NAME"].ToString() : null,
                    ORDINAL_POSITION = row["ORDINAL_POSITION"] != DBNull.Value ? Convert.ToInt32(row["ORDINAL_POSITION"]) : 0,
                    COLUMN_DEFAULT = row["COLUMN_DEFAULT"] != DBNull.Value ? row["COLUMN_DEFAULT"].ToString() : null,
                    IS_NULLABLE = row["IS_NULLABLE"] != DBNull.Value ? Convert.ToBoolean(row["IS_NULLABLE"]) : false,
                    DATA_TYPE = row["DATA_TYPE"] != DBNull.Value ? row["DATA_TYPE"].ToString() : null,
                    DATA_TYPE_DETAIL = row["DATA_TYPE_DETAIL"] != DBNull.Value ? row["DATA_TYPE_DETAIL"].ToString() : null,
                    DATA_LENGTH = row["DATA_LENGTH"] != DBNull.Value ? row["DATA_LENGTH"].ToString() : null
                };

                list.Add(columnInfo);
            }

            return list;
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query, string connectionString)
        {
            var result = new List<Dictionary<string, object>>();

            // Crear la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ejecutar el comando y obtener el lector de datos
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Leer cada fila del resultado
                            while (reader.Read())
                            {
                                // Crear un diccionario para almacenar los datos de la fila
                                var row = new Dictionary<string, object>();

                                // Recorrer cada columna de la fila
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // Agregar al diccionario el nombre de la columna y su valor
                                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                }

                                // Agregar la fila al resultado
                                result.Add(row);
                            }
                        }
                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    connection.Close();
                    Debug.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                }
            }

            return result.ToList();
        }


        public List<TablasOrigen> TabOrigen(string sql, string str)
        {
            List<TablasOrigen> list = new List<TablasOrigen>();

            using (SqlConnection connection = new SqlConnection(str))
            {
                try
                {
                    connection.Open(); // Open the connection before executing commands

                    SqlCommand command = new SqlCommand(sql, connection); // Specify the connection explicitly

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try // Handle potential data conversion errors within the loop
                            {
                                TablasOrigen tab = new TablasOrigen
                                {
                                    NO = reader.GetInt64(reader.GetOrdinal("NO")), // Use GetOrdinal for safer column access
                                    MARCAR = reader.GetBoolean(reader.GetOrdinal("MARCAR")),
                                    TABLE_NAME = reader.GetString(reader.GetOrdinal("TABLE_NAME"))
                                };
                                list.Add(tab);
                            }
                            catch (Exception ex)
                            {
                                connection.Close();
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

        public DataTable selSQL(string sql)
        {
            DataTable dt = new DataTable();

            var str = conections.SQLString;

            using (SqlConnection connection = new SqlConnection(connectionString: str))
            {
                try
                {
                    connection.Open();
                 //   Debug.WriteLine("Conexion SQL establecida correctamente");
                    SqlCommand command = new SqlCommand(sql);
                    SqlDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();
                    connection.Dispose();
                    return dt;
                }
                catch (Exception ex)
                {
                    connection.Dispose();
                    Debug.WriteLine(ex.Message);
                    return dt;
                }

            }

        }

        public DataTable selOra(string sql)
        {
            DataTable dt = new DataTable();

            var str = conections.SQLString;

            using (OracleConnection connection = new OracleConnection(str))
            {
                try
                {
                    connection.Open();
                    Debug.WriteLine("Conexión establecida correctamente");

                    // Aquí puedes ejecutar tus consultas SQL
                    OracleCommand command = new OracleCommand(sql, connection);
                    OracleDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();

                    connection.Dispose();
                    return dt;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error al conectar: " + ex.Message);
                    return dt;
                }
            }
        }

        public DataTable selCOlsSQL(string table_name)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT\r\nORDINAL_POSITION,column_name, DATA_TYPE,\r\n   " +
                " CASE\r\n        WHEN DATA_TYPE IN ('decimal','float', 'numeric') THEN  CAST(NUMERIC_PRECISION AS VARCHAR) + ',' + CAST(NUMERIC_SCALE AS VARCHAR)\r\n      " +
                "  WHEN DATA_TYPE IN ('varchar','sql_variant','text', 'nvarchar', 'char', 'nchar') THEN  CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)\r\n        " +
                "WHEN DATA_TYPE IN ('date','time','datetime', 'datetime2', 'smalldatetime') THEN  CAST(DATETIME_PRECISION AS VARCHAR)\r\n        " +
                "WHEN DATA_TYPE IN ('int','bigint','smallint','tinyint', 'integer', 'double','money') THEN  CAST(NUMERIC_PRECISION AS VARCHAR)+ ',' + CAST(NUMERIC_SCALE AS VARCHAR)\r\n       " +
                " WHEN DATA_TYPE IN ('float') THEN  CAST(NUMERIC_PRECISION AS VARCHAR)+ ',' + CAST(NUMERIC_SCALE AS VARCHAR)\r\n       " +
                " WHEN DATA_TYPE IN ('bit','varbinary', 'boolean') THEN '1,0'\r\n       " +
                " ELSE 'other'\r\n   " +
                " END AS longitud,\r\n    " +
                "IS_NULLABLE,\r\n   " +
                " COLUMN_DEFAULT\r\nFROM\r\n    " +
                "INFORMATION_SCHEMA.COLUMNS " +
                " where table_name ='" + table_name + "'";

            var str = conections.SQLString;

            using (SqlConnection connection = new SqlConnection(connectionString: str))
            {
                try
                {
                    connection.Open();
                 //   Debug.WriteLine("Conexion SQL establecida correctamente");
                    SqlCommand command = new SqlCommand(sql);
                    SqlDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();

                    connection.Dispose();
                    return dt;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    connection.Close();
                    return dt;
                }

            }


        }
        public async Task EscribirLogAsync(string rutaArchivo, string mensaje)
        {
            try
            {
                // Abrimos el archivo con FileShare para permitir múltiples accesos
                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string logMessage = $"{DateTime.Now}: {mensaje}";
                    await sw.WriteLineAsync(logMessage); // Escribimos el mensaje de manera asíncrona
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }

        public bool executeQueryOracle(string ConnectionSQlOra, string ScriptOra, string path_log)
        {

            using (var connection = new OracleConnection(ConnectionSQlOra))
            {
               
                var str = "";
                OracleTransaction? transaction = null;
                connection.Open();
                transaction = connection.BeginTransaction();
                try
                {
               
                    using (var command = new OracleCommand(ScriptOra, connection))
                    {
                        command.Transaction = transaction;
                        // Ejecutamos el comando
                        command.ExecuteNonQuery();

                    }
                    transaction.Commit();
                    connection.Close ();
                    return true;
                }
                catch (OracleException ex)
                {
                    var rvalue = true;
                    // Manejo de errores
                    Debug.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
                    if (ex.Number == 942)
                    {
                        Debug.WriteLine("Error porque no encontro la tabla " + ScriptOra);
                        str =("Error porque no encontro la tabla " + ScriptOra);
                        rvalue = false;
                    }
                    else if (ex.Number == 54)
                    {
                        Debug.WriteLine("Error acquire with NOWAIT : " + ex.Message + ScriptOra);
                        str = ("Error acquire with NOWAIT : " + ex.Message + ScriptOra);

                    }
                    else if (ex.Number == 955)
                    {
                        Debug.WriteLine("Error al recrear porque el objeto ya existe  : " + ScriptOra + ex.Message);
                       // str = ("Error al recrear porque el objeto ya existe  : " + ScriptOra + ex.Message);
                    }
                    else if (ex.Number == 1408)
                    {
                        Debug.WriteLine("Error al recrear porque el indice porque ya existe  : " + ScriptOra + ex.Message);
                       // str = ("Error al recrear porque el indice porque ya existe  : " + ScriptOra + ex.Message);
                    }
                    else if (ex.Number == 2260)
                    {
                        Debug.WriteLine("Error al recrear la llave primaria porque ya existe :" + ScriptOra + ex.Message);
                       // str = ("Error al recrear la llave primaria porque ya existe :" + ScriptOra + ex.Message);
                    }
                    else if (ex.Number == 2275)
                    {
                        Debug.WriteLine("Error al recrear la llave foranea porque ya existe :" + ScriptOra + ex.Message);
                       // str = ("Error al recrear la llave foranea porque ya existe :" + ScriptOra + ex.Message);
                    }else   if (ex.Number == 4020)
                    {
                        Debug.WriteLine("Error al eliminar objeto por recurso utilizado, se reintenta automaticamente " + ScriptOra + ex.Message);
                      //  str = ("Error al recrear la llave foranea porque ya existe :" + ScriptOra + ex.Message);
                    }else   if (ex.Number == 2270)
                    {
                        Debug.WriteLine("se cambia a IDX automaticamente " + ScriptOra + ex.Message);
                    //    str = ("Error al recrear la llave foranea porque ya existe :" + ScriptOra + ex.Message);
                        rvalue = false;
                    }
                    else
                    {
                        Debug.WriteLine("Error desconocido :" + ScriptOra + ex.Message);
                        str = ("Error desconocido :" + ScriptOra + ex.Message);
                        rvalue = false;


                    }
                    EscribirLogAsync(path_log,str).Wait();

                    transaction.Rollback();
                    connection.Close();
                    return rvalue;
                }
                finally
                {
                    // Cerramos la conexión
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                       // Debug.WriteLine("Conexión cerrada.");
                        
                    }

                }
            }
        }

        //public async Task<bool> executeQueryOracle(string ConnectionSQlOra, string ScriptOra, string path_log)
        //{
        //    using (var connection = new OracleConnection(ConnectionSQlOra))
        //    {
        //        OracleTransaction transaction = null;

        //        await connection.OpenAsync();  // Abrir la conexión de manera asíncrona
        //        transaction = connection.BeginTransaction();
        //        try
        //        {
        //            // Abrimos la conexión
        //            Debug.WriteLine("Conexión a la base de datos exitosa.");

        //            // Comando para ejecutar el script
        //            using (var command = new OracleCommand(ScriptOra, connection))
        //            {
        //                command.Transaction = transaction;

        //                // Ejecutamos el comando de manera asíncrona
        //                await command.ExecuteNonQueryAsync();
        //            }

        //            // Commit de la transacción
        //            await transaction.CommitAsync();
        //            return true;
        //        }
        //        catch (OracleException ex)
        //        {
        //            // Manejo de errores
        //            Debug.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
        //            if (ex.Number == 942)
        //            {
        //                Debug.WriteLine("Error porque no encontró la tabla " + ScriptOra);
        //            }
        //            else if (ex.Number == 54)
        //            {
        //                Debug.WriteLine("Error acquire with NOWAIT: " + ex.Message + " " + ScriptOra);
        //            }
        //            else if  (ex.Number == 955)
        //            {
        //                Debug.WriteLine("Objeto ya existente : " + ScriptOra + " " + ex.Message);
        //            }
        //            else if (ex.Number == 2260)
        //            {
        //                Debug.WriteLine("porque la llave primaria ya existe : " + ScriptOra + " " + ex.Message);
        //            }else
        //            {
        //                Debug.WriteLine("Error desconocido : " + ScriptOra + " " + ex.Message);
        //            }

        //            // Rollback de la transacción
        //            await transaction.RollbackAsync();
        //            return false;
        //        }
        //        finally
        //        {
        //            // Cerramos la conexión
        //            if (connection.State == System.Data.ConnectionState.Open)
        //            {
        //                await connection.CloseAsync();  // Cerrar la conexión de manera asíncrona
        //                Debug.WriteLine("Conexión cerrada.");

        //                // Escribir el log de manera asíncrona
        //                await EscribirLogAsync(path_log, "Conexión Cerrada");
        //            }
        //        }
        //    }
        //}

        public string EnsureConnectionTimeout(string connectionString, int timeoutInSeconds)
        {
            // Verificar si la cadena ya contiene "Connection Timeout"
            if (!connectionString.ToLower().Contains("connection timeout"))
            {
                // Agregar el parámetro "Connection Timeout" con el valor especificado
                connectionString = $"{connectionString};Connection Timeout={timeoutInSeconds}";
                Debug.WriteLine($"Connection Timeout agregado: {timeoutInSeconds} segundos");
            }
            else
            {
                Debug.WriteLine("Connection Timeout ya presente en la cadena de conexión.");
            }

            return connectionString;
        }

        //public async Task<bool> executeQueryOracle(string ConnectionSQlOra, string ScriptOra, string path_log)
        //{
        //    // Verificar y agregar "Connection Timeout" si es necesario
        //    ConnectionSQlOra = EnsureConnectionTimeout(ConnectionSQlOra, 60);  // 60 segundos o el valor que prefieras

        //    using (var connection = new OracleConnection(ConnectionSQlOra))
        //    {
        //        OracleTransaction transaction = null;
        //        Debug.WriteLine($"Cadena de conexión utilizada: {ConnectionSQlOra}");

        //        connection.Open();  // Abrir la conexión de manera asíncrona

        //        transaction = connection.BeginTransaction();
        //        try
        //        {
        //            // Abrimos la conexión
        //            Debug.WriteLine("Conexión a la base de datos exitosa.");

        //            // Comando para ejecutar el script
        //            using (var command = new OracleCommand(ScriptOra, connection))
        //            {
        //                command.Transaction = transaction;
        //                command.CommandTimeout = 0; // Sin límite de tiempo para la ejecución del comando

        //                // Ejecutamos el comando de manera asíncrona
        //                await command.ExecuteNonQueryAsync();
        //            }

        //            // Commit de la transacción
        //            await transaction.CommitAsync();
        //            return true;
        //        }
        //        catch (OracleException ex)
        //        {
        //            // Manejo de errores
        //            Debug.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
        //            if (ex.Number == 942)
        //            {
        //                Debug.WriteLine("Error porque no encontró la tabla " + ScriptOra);
        //            }
        //            else if (ex.Number == 54)
        //            {
        //                Debug.WriteLine("Error acquire with NOWAIT: " + ex.Message + " " + ScriptOra);
        //            }
        //            else if (ex.Number == 955)
        //            {
        //                Debug.WriteLine("Objeto ya existente : " + ScriptOra + " " + ex.Message);
        //            }
        //            else if (ex.Number == 2260)
        //            {
        //                Debug.WriteLine("porque la llave primaria ya existe : " + ScriptOra + " " + ex.Message);
        //            }
        //            else
        //            {
        //                Debug.WriteLine("Error desconocido : " + ScriptOra + " " + ex.Message);
        //            }

        //            // Rollback de la transacción
        //            await transaction.RollbackAsync();
        //            return false;
        //        }
        //        finally
        //        {
        //            // Cerramos la conexión
        //            if (connection.State == System.Data.ConnectionState.Open)
        //            {
        //                await connection.CloseAsync();  // Cerrar la conexión de manera asíncrona
        //                Debug.WriteLine("Conexión cerrada.");

        //                // Escribir el log de manera asíncrona
        //                await EscribirLogAsync(path_log, "Conexión Cerrada");
        //            }
        //        }
        //    }
        //}


        public DataTable DataOrigen(string sqlConn, string Tabla)
        {
            using (var connection = new SqlConnection(sqlConn))
            {
                var dt = new DataTable();

                Conn conn = new Conn();

                List<DataTypeOrigenXTable> listaColumnas = conn.ListaColumnas(sqlConn, Tabla);
                var script = "select ";
                foreach (var item in listaColumnas)
                {
                    script = script + "\"" + item.COLUMN_NAME + "\",";

                }
                script = script.Remove(script.Length - 1);
                script = script + " from [" + Tabla + "]";

                try
                {
                    connection.Open(); // Open the connection before executing commands

                    SqlCommand command = new SqlCommand(script, connection); // Specify the connection explicitly

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Debug.WriteLine($"Error opening connection or executing command: {ex.Message}");
                    // Consider logging the error or throwing a more specific exception
                }

                return dt;

            }



        }


        //public bool BorrarTablas(string tabla, string OraConn, string path_log)
        //{
        //    var script = "drop table " + tabla + " cascade constraints purge";

        //    var ret = executeQueryOracle(OraConn, script, path_log);

        //    return ret;

        //}
        public async Task<bool> BorrarTablas(string tabla, string OraConn, string path_log)
        {
            // Generar el script de borrado de la tabla
            var script = "drop table " + tabla + " cascade constraints purge";
            // Ejecutar el script en la base de datos Oracle
            var ret = executeQueryOracle(OraConn, script, path_log);
     
            return ret;
        }

        public bool Destino(DataTable dataOrigen, string tabla, string connectionOra, List<DataTypeConvert> _listaDeConversiones)
        {
            
            try
            {
                // Procesamos el DataTable para convertir tipos de datos según la lista de conversiones
                PreprocessDataTable(dataOrigen, _listaDeConversiones);

                // Establecemos la conexión con Oracle
                using (OracleConnection connection = new OracleConnection(connectionOra))
                {
                    // Abrimos la conexión
                    OracleTransaction? transaction = null;
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    try
                    {  // Configuramos OracleBulkCopy para hacer la inserción masiva
                        using (OracleBulkCopy bulkCopy = new OracleBulkCopy(connection))
                        {
                            // Asignamos el nombre de la tabla de destino
                            bulkCopy.DestinationTableName = tabla;

                            // Mapeamos automáticamente las columnas del DataTable a la tabla en Oracle
                            foreach (DataColumn column in dataOrigen.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                            }

                            // Realizamos la inserción masiva desde el DataTable
                            bulkCopy.WriteToServer(dataOrigen);
                            transaction.Commit();
                        }
                        connection.Dispose();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }

                return true; // Si todo sale bien, retornamos true
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Debug.WriteLine($"Error en la operación de inserción masiva: {ex.Message}");
                return false; // Si ocurre un error, retornamos false
            }
        }

        private void PreprocessDataTable(DataTable dataOrigen, List<DataTypeConvert> _listaDeConversiones)
        {
            // Iteramos a través de todas las columnas del DataTable
            foreach (DataColumn column in dataOrigen.Columns)
            {
                // Buscamos el tipo de dato de la columna en la lista de conversiones
                var conversion = _listaDeConversiones.Find(c => c.Tipo == column.DataType.Name.ToUpper());

                // Si encontramos una equivalencia, aplicamos la conversión
                if (conversion != null)
                {
                    foreach (DataRow row in dataOrigen.Rows)
                    {
                        // Verificamos si la celda tiene un valor nulo
                        if (row[column.ColumnName] == DBNull.Value)
                        {
                            row[column.ColumnName] = GetDefaultValueForColumn(column);
                        }

                        // Conversión para tipos específicos
                        if (conversion.Equivalencia == "BLOB" && column.DataType == typeof(byte[]))
                        {
                            // Lógica de conversión específica para BLOB
                            row[column.ColumnName] = ConvertToBlob(row[column.ColumnName]);
                        }
                        else if (conversion.Equivalencia == "CLOB" && column.DataType == typeof(string))
                        {
                            // Lógica de conversión específica para CLOB
                            row[column.ColumnName] = ConvertToClob(row[column.ColumnName]);
                        }
                        // Puedes agregar más conversiones específicas aquí si es necesario
                    }
                }
            }
        }

        private object GetDefaultValueForColumn(DataColumn column)
        {
            // Definimos valores por defecto para cada tipo de dato común en Oracle
            if (column.DataType == typeof(string))
            {
                return ""; // Cadena vacía para columnas VARCHAR
            }
            else if (column.DataType == typeof(DateTime))
            {
                return DateTime.Now; // Fecha actual para columnas de fecha
            }
            else if (column.DataType == typeof(int) || column.DataType == typeof(long))
            {
                return 0; // Cero para columnas numéricas
            }
            else if (column.DataType == typeof(decimal) || column.DataType == typeof(double))
            {
                return 0.0m; // Cero para columnas decimales o de punto flotante
            }
            else if (column.DataType == typeof(bool))
            {
                return 0; // Falso para columnas booleanas, tratadas como enteros (0 = falso)
            }
            else if (column.DataType == typeof(Guid))
            {
                return Guid.NewGuid(); // Genera un nuevo GUID para columnas de tipo GUID
            }
            else
            {
                // En caso de que no reconozcamos el tipo de dato, devolvemos DBNull
                return DBNull.Value;
            }
        }

        private object ConvertToBlob(object data)
        {
            // Convierte el dato a BLOB si es necesario
            return data; // Lógica personalizada para convertir a BLOB
        }

        private object ConvertToClob(object data)
        {
            // Convierte el dato a CLOB si es necesario
            return data; // Lógica personalizada para convertir a CLOB
        }

    }
}

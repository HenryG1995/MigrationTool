
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToolMigration.Logic.DataModels;


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

            // var STR = Environment.GetEnvironmentVariable("STR");

            // connectionString = string.Format("USER ID={0};PASSWORD={1};DATA SOURCE= (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = {3}))(CONNECT_DATA = (SERVICE_NAME ={4}))) ;", UserID, Pass, Host, Port, ServiceName);
            connectionString = string.Format("USER ID={0};PASSWORD={1};DATA SOURCE= (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = {3}))(CONNECT_DATA = (SID = {4}))) ;", UserID, Pass, Host, Port, SID);

            // connectionString = "Data Source=tu_servidor:1521/tu_sid;User Id=tu_usuario;Password=tu_contrasena;";
          

            conections.ORAString = connectionString;

            OraConection = connectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexión establecida correctamente");

                    // Aquí puedes ejecutar tus consultas SQL
                    OracleCommand command = new OracleCommand("SELECT SYS_GUID() UUID FROM DUAL", connection);
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Procesar los datos
                        Console.WriteLine(reader["UUID"].ToString());
                    }

                    reader.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar: " + ex.Message);
                    return false;
                }
            };
        }
        public bool SqlTest(string usuario,string pass, string host,string port, string database)
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
                    connection.Open(); // Open the connection before executing commands
                    Console.WriteLine("Conexion SQL establecida correctamente");

                    SqlCommand command = new SqlCommand("SELECT NEWID() AS UUID", connection); // Specify the connection explicitly
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(reader["UUID"].ToString());
                    }

                    reader.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }

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
                                Console.WriteLine($"Error parsing data in row: {ex.Message}");
                                // Consider logging the error or taking appropriate action
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening connection or executing command: {ex.Message}");
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
                    Console.WriteLine("Conexion SQL establecida correctamente");
                    SqlCommand command = new SqlCommand(sql);
                    SqlDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();
                    return dt;
                }
                catch (Exception ex)
                {

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
                    Console.WriteLine("Conexión establecida correctamente");

                    // Aquí puedes ejecutar tus consultas SQL
                    OracleCommand command = new OracleCommand(sql, connection);
                    OracleDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();

                    return dt;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar: " + ex.Message);
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
                    Console.WriteLine("Conexion SQL establecida correctamente");
                    SqlCommand command = new SqlCommand(sql);
                    SqlDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();

                    return dt;
                }
                catch (Exception ex)
                {

                    return dt;
                }

            }

           
        }

    }
}

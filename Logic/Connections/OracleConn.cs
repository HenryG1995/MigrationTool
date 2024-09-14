
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ToolMigration.Logic.Connections
{
 
   public class Conn
    {
        public string connOra {  get; set; }
        public string connSql { get; set; }


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
            connOra = connectionString;
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

            connSql = builder.ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString: connSql))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexion SQL establecida correctamente");
                    SqlCommand command = new SqlCommand("SELECT NEWID() AS UUID");
                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["UUID"].ToString());
                    }

                    reader.Close();
                    return true;

                } catch (Exception ex)
                {

                    return false;
                }

            }
            
        }


        public DataTable selSQL(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString: connSql))
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

            using (OracleConnection connection = new OracleConnection(connOra))
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
            var sql = "SELECT\r\ndistinct\r\n    DATA_TYPE,\r\n    CASE\r\n        WHEN DATA_TYPE IN ('decimal', 'numeric') THEN  CAST(NUMERIC_PRECISION AS VARCHAR) + ',' + CAST(NUMERIC_SCALE AS VARCHAR)\r\n        WHEN DATA_TYPE IN ('varchar', 'nvarchar', 'char', 'nchar') THEN  CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR)\r\n        WHEN DATA_TYPE IN ('date','datetime', 'datetime2', 'smalldatetime') THEN  CAST(DATETIME_PRECISION AS VARCHAR)\r\n        WHEN DATA_TYPE IN ('int','tinyint', 'integer', 'double','float','money') THEN  CAST(NUMERIC_PRECISION AS VARCHAR)+ ',' + CAST(NUMERIC_SCALE AS VARCHAR)\r\n        WHEN DATA_TYPE IN ('bit','varbinary', 'boolean') THEN '1,0'\r\n        ELSE 'other'\r\n    END AS tipo\r\nFROM\r\n    INFORMATION_SCHEMA.COLUMNS where table_name ='" + table_name+"'";
            using (SqlConnection connection = new SqlConnection(connectionString: connSql))
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

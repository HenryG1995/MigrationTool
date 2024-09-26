using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using ToolMigration.Logic.Connections;

namespace ToolMigration
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public string oracon;
        public string sqlcon;
      
        public MainWindow()
        {
            InitializeComponent();
            btn_continuar.IsEnabled = false;

            #region PRUEBAS

            //SQL SERVER
            txt_sql_user.Text = "sa";
            txt_sql_catalogo.Text = "MP_CONFIG"; //USUARIO QUESE CONSULTARA
          //  txt_sql_host.Text = "192.168.71.131";
            txt_sql_host.Text = "192.168.0.18";
            txt_sql_Puerto.Text = "1433";
            txt_sql_password.Password = "Andromeda12";
    

            //ORACLE

            txt_oracle_sid.Text = "ORCL";
            txt_oracle_user.Text = "MP_CONFIG"; // basede datos destino
            txt_Oracle_Password.Password = "Andromeda12";
         // txt_Oracle_host.Text = "192.168.71.131";
            txt_Oracle_host.Text = "192.168.0.18";
            txt_Oracle_Puerto.Text = "1521";


            #endregion


        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Inicia el test", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

            var prueba = false;
            var cont = 0;
            
                var con = new ToolMigration.Logic.Connections.Conn();
                //parametros oracle
                txt_Oracle_host.IsEnabled = false;
                txt_Oracle_Password.IsEnabled = false;
                txt_Oracle_Puerto.IsEnabled = false;
                txt_oracle_sid.IsEnabled = false;
                txt_oracle_user.IsEnabled = false;
                //parametros sql
                txt_sql_catalogo.IsEnabled = false;
                txt_sql_Puerto.IsEnabled = false;
                txt_sql_host.IsEnabled = false;
                txt_sql_password.IsEnabled = false;
                txt_sql_user.IsEnabled = false;
            
                btn_continuar.IsEnabled = false;
                btn_test.IsEnabled = false;
                prueba = con.Oratest(txt_oracle_user.Text, txt_Oracle_Password.Password, txt_Oracle_host.Text, txt_Oracle_Puerto.Text, txt_oracle_sid.Text);
                if (prueba == true) { prueba = false;  cont = 1; }
                prueba = con.SqlTest(txt_sql_user.Text,txt_sql_password.Password,txt_sql_host.Text, txt_sql_Puerto.Text, txt_sql_catalogo.Text);
                if (prueba == true) { prueba = false; cont = 2; }
                if (cont == 2)
                {
                    MessageBox.Show("Test correcto ", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    btn_continuar.IsEnabled = true;
                    sqlcon = con.SqlConection;
                    oracon = con.OraConection;
                    btn_test.IsEnabled = false;
            }
                else
                {

                    MessageBox.Show("Test incorrecto ", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_Oracle_host.IsEnabled = true;
                    txt_Oracle_Password.IsEnabled = true;
                    txt_Oracle_Puerto.IsEnabled = true;
                    txt_oracle_sid.IsEnabled = true;
                    txt_oracle_user.IsEnabled = true;
                    //parametros sql
                    txt_sql_catalogo.IsEnabled = true;
                    txt_sql_Puerto.IsEnabled = true;
                    txt_sql_host.IsEnabled = true;
                    txt_sql_password.IsEnabled = true;
                    txt_sql_user.IsEnabled = true;
                    btn_test.IsEnabled = true;

            }
                // con.SqlTest(txt_sql_catalogo.Text, txt_sql_server_name.Text);    
               
           
            return;
        }

        private void txt_sql_user_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_sql_host_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_sql_server_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_sql_catalogo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_sql_Puerto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_continuar_Click(object sender, RoutedEventArgs e)
        {
            SeleccionTablas seleccionTablas = new SeleccionTablas(oracon,sqlcon);
            seleccionTablas.Show();

         
            this.Close();
        }
    }
}

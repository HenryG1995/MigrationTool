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
      
        public MainWindow()
        {
            InitializeComponent();
            btn_continuar.IsEnabled = false;

        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Inicia el test", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);


            if (1>0)
            {
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
                txt_sql_server_name.IsEnabled = false;
                btn_continuar.IsEnabled = true;
                btn_test.IsEnabled = false;
                con.Oratest(txt_oracle_user.Text, txt_Oracle_Password.Password, txt_Oracle_host.Text, txt_Oracle_Puerto.Text, txt_oracle_sid.Text);
                con.SqlTest(txt_sql_user.Text,txt_sql_password.Password,txt_sql_host.Text, txt_sql_Puerto.Text, txt_sql_catalogo.Text);
               // con.SqlTest(txt_sql_catalogo.Text, txt_sql_server_name.Text);    
                MessageBox.Show("Test correcto ", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_continuar.IsEnabled= true;
                btn_test.IsEnabled= false;
            }
            else
            {

                MessageBox.Show("test incorrecto verifique sus parametros de conexion", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                btn_continuar.IsEnabled = false;
            };
               

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
            SeleccionTablas seleccionTablas = new SeleccionTablas();
            seleccionTablas.Show();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Hide();
        }
    }
}

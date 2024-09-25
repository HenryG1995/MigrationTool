using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToolMigration.Logic.Connections;
using ToolMigration.Logic.DataModels;
using ToolMigration.Logic.Transformation;

using System;
using System.Collections.Generic;
using Binding = System.Windows.Data.Binding;


namespace ToolMigration
{
    /// <summary>
    /// Lógica de interacción para SeleccionTablas.xaml
    /// </summary>
    public partial class SeleccionTablas : Window
    {

        public List<ItemDataGrid> listaT;
        public List<TablasDestino> ListTablas = new List<TablasDestino>();

        public class ItemDataGrid
        {
            public int Id { get; set; }
            public bool Marcar { get; set; }
            public string NombreTabla { get; set; }
        }
        public SeleccionTablas()
        {
            InitializeComponent();

            dt_tabla_origen.AutoGenerateColumns = false;

            // Agregar la columna NO (texto)
            DataGridTextColumn noColumn = new DataGridTextColumn
            {
                Header = "NO",
                Binding = new Binding("NO")
            };
            dt_tabla_origen.Columns.Add(noColumn);

            DataGridCheckBoxColumn marcarColumn = new DataGridCheckBoxColumn
            {
                Header = "Marcar",
                Binding = new Binding("MARCAR")
            };
            dt_tabla_origen.Columns.Add(marcarColumn);

            DataGridTextColumn tableNameColumn = new DataGridTextColumn
            {
                Header = "Table Name",
                Binding = new Binding("TABLE_NAME")
            };
            dt_tabla_origen.Columns.Add(tableNameColumn);

            LoadDataGridView();
         //   tablas_destino();
            tablas_origen();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
  
        }


        private void tablas_origen()
        {
            // crear dt que permita la obtencion de datos en sql server.

            var tab_origen = new List<TablasOrigen>();

            MetaDataCore metaDataCore = new MetaDataCore();

            Conn conn = new Conn();

            tab_origen = conn.TabOrigen(metaDataCore.all_tables_SQL());

           
            // Asignar los datos al DataGridView
            dt_tabla_origen.ItemsSource = tab_origen;


        }

        private void tablas_destino()
        {
            // crear dt que permita la obtencion de datos en Oracle 19c.

            dt_tabla_destino.ItemsSource = ListTablas;

        }

        private void Mover()
        {
            CleanList();

            AgregateDataList();

        }

        
        private void LoadDataGridView()
        {
            // Crear una tabla para almacenar los datos
            DataTable table = new DataTable();
            table.Columns.Add("Tipo de datos de Oracle", typeof(string));
            table.Columns.Add("Tipos de datos de SQL Server", typeof(string));
            table.Columns.Add("Alternativas", typeof(string));

            // Añadir filas a la tabla
            table.Rows.Add("BFILE", "VARBINARY(MAX)", "Sí");
            table.Rows.Add("BLOB", "VARBINARY(MAX)", "Sí");
            table.Rows.Add("CHAR([1-2000])", "CHAR([1-2000])", "Sí");
            table.Rows.Add("CLOB", "VARCHAR(MAX)", "Sí");
            table.Rows.Add("FECHA", "DATETIME", "Sí");
            table.Rows.Add("FLOAT", "FLOAT", "No");
            table.Rows.Add("FLOAT([1-53])", "FLOAT([1-53])", "No");
            table.Rows.Add("FLOAT([54-126])", "FLOAT", "No");
            table.Rows.Add("INT", "NUMERIC(38)", "Sí");
            table.Rows.Add("INTERVAL", "DATETIME", "Sí");
            table.Rows.Add("LONG", "VARCHAR(MAX)", "Sí");
            table.Rows.Add("LONG RAW", "IMAGE", "Sí");
            table.Rows.Add("NCHAR([1-1000])", "NCHAR([1-1000])", "No");
            table.Rows.Add("NCLOB", "NVARCHAR(MAX)", "Sí");
            table.Rows.Add("NUMBER", "FLOAT", "Sí");
            table.Rows.Add("NUMBER([1-38])", "NUMERIC([1-38])", "No");
            table.Rows.Add("NUMBER([0-38],[1-38])", "NUMERIC([0-38],[1-38])", "Sí");
            table.Rows.Add("NVARCHAR2([1-2000])", "NVARCHAR([1-2000])", "No");
            table.Rows.Add("RAW([1-2000])", "VARBINARY([1-2000])", "No");
            table.Rows.Add("real", "FLOAT", "No");
            table.Rows.Add("ROWID", "CHAR(18)", "No");
            table.Rows.Add("TIMESTAMP", "DATETIME", "Sí");
            table.Rows.Add("MARCA DE TIEMPO(0-7)", "DATETIME", "Sí");
            table.Rows.Add("TIMESTAMP(8-9)", "DATETIME", "Sí");
            table.Rows.Add("MARCA DE TIEMPO (0-7) CON ZONA HORARIA", "VARCHAR(37)", "Sí");
            table.Rows.Add("MARCA DE TIEMPO (8-9) CON ZONA HORARIA", "VARCHAR(37)", "No");
            table.Rows.Add("MARCA DE TIEMPO (0-7) CON ZONA HORARIA LOCAL", "VARCHAR(37)", "Sí");
            table.Rows.Add("MARCA DE TIEMPO (8-9) CON ZONA HORARIA LOCAL", "VARCHAR(37)", "No");
            table.Rows.Add("UROWID", "CHAR(18)", "No");
            table.Rows.Add("VARCHAR2([1-4000])", "VARCHAR([1-4000])", "Sí");

            // Configurar DataGridView
            DataGridView dgv = new DataGridView
            {
                DataSource = table,
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = false // Habilitar la edición
            };

            this.DataContext = dgv;

            // Agregar el DataGridView al formulario
//            this.Controls.Add(dgv);
        }

        private void AgregateDataList()
        {
            //primero se lee la data que se migrara de una bd a otra 


            foreach (var item in dt_tabla_origen.Items)
            {
                if (item is TablasOrigen tabs)
                {
                    if (tabs.MARCAR is true)
                    {
                        TablasDestino dest = new TablasDestino();

                        dest.TABLE_NAME = tabs.TABLE_NAME;

                        ListTablas.Add(dest);
                    }
                }

            }

        }

        private void CleanList()

        {
           ListTablas.Clear();

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
          
        }
    }
}

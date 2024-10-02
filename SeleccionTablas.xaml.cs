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
using System.IO;
using System.Collections.Generic;
using Binding = System.Windows.Data.Binding;
using static ToolMigration.Logic.DataMove.Equivalency;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using ToolMigration.Logic.Tools;


namespace ToolMigration
{
    /// <summary>
    /// Lógica de interacción para SeleccionTablas.xaml
    /// </summary>
    public partial class SeleccionTablas : Window
    {

        public string sqlcon_data { get; set; }
        public string oracon_data { get; set; }
        public List<ItemDataGrid> listaT;
        public List<TablasDestino> ListTablas = new List<TablasDestino>();

        public class ItemDataGrid
        {
            public int Id { get; set; }
            public bool Marcar { get; set; }
            public string NombreTabla { get; set; }
        }
        public SeleccionTablas(string oracon, string sqlcon)
        {
            InitializeComponent();

            dt_tabla_origen.AutoGenerateColumns = false;

            // Agregar la columna NO (texto)
            DataConvertPerso dataConvertPerso = new DataConvertPerso();

            List<DataTypeConvert> datatypes = new List<DataTypeConvert>();

            dataConvertPerso.llenalista(datatypes);

            dt_tabla_conversiones.ItemsSource = datatypes.ToList();

            dt_tabla_conversiones.IsEnabled = false;

            dt_tabla_conversiones.Items.Refresh();

            LoadDataGridView();
            //   tablas_destino();
            tablas_origen(sqlcon);
            sqlcon_data = sqlcon;
            oracon_data = oracon;
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


        private void tablas_origen(string sqlcon)
        {
            // crear dt que permita la obtencion de datos en sql server.

            var tab_origen = new List<TablasOrigen>();

            MetaDataCore metaDataCore = new MetaDataCore();

            Conn conn = new Conn();

            tab_origen = conn.TabOrigen(metaDataCore.all_tables_SQL(), sqlcon);


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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int numeroDeElementos = dt_tabla_origen.Items.Count;

            if (numeroDeElementos > 0)
            {

                List<TablaDestinoDT> list = new List<TablaDestinoDT>();
                List<TablasOrigen> listOrigen = new List<TablasOrigen>();

                foreach (var item in dt_tabla_origen.Items)
                {
                    if (item is TablasOrigen tabs)
                    {
                        if (tabs.MARCAR is true)
                        {
                            TablaDestinoDT dto = new TablaDestinoDT();

                            dto.TABLE_NAME = tabs.TABLE_NAME;
                            dto.MARCAR = tabs.MARCAR;
                            dto.NO = tabs.NO;
                            list.Add(dto);
                        }
                        if (tabs.MARCAR is true)
                        {
                            TablasOrigen dto2 = new TablasOrigen();
                            dto2.TABLE_NAME = tabs.TABLE_NAME;
                            dto2.MARCAR = true;
                            dto2.NO = tabs.NO;
                            listOrigen.Add(dto2);

                        }
                        else
                        {
                            TablasOrigen dto2 = new TablasOrigen();
                            dto2.TABLE_NAME = tabs.TABLE_NAME;
                            dto2.MARCAR = false;
                            dto2.NO = tabs.NO;
                            listOrigen.Add(dto2);
                        }



                    }

                }

                dt_tabla_destino.ItemsSource = list;

                dt_tabla_destino.Items.Refresh();

                dt_tabla_origen.ItemsSource = listOrigen;

                dt_tabla_origen.Items.Refresh();


                //chk_marca_todo_origen.IsEnabled = true;
                //chk_marca_todos_destino.IsEnabled = true;
                //chk_sel_individual_origen.IsEnabled = true;
                //chk_sel_individual_destino.IsEnabled = true;
                //dt_tabla_origen.IsEnabled = true;





            }





        }



        private void chk_marca_todo_origen_Checked(object sender, RoutedEventArgs e)
        {

            if (chk_marca_todo_origen.IsChecked == true)
            {
                chk_marca_todos_destino.IsEnabled = false;
                chk_sel_individual_origen.IsEnabled = false;
                chk_sel_individual_destino.IsEnabled = false;

                List<TablasOrigen> list = new List<TablasOrigen>();



                foreach (var item in dt_tabla_origen.Items)
                {
                    if (item is TablasOrigen tabs)
                    {
                        TablasOrigen dto = new TablasOrigen();

                        dto.TABLE_NAME = tabs.TABLE_NAME;
                        dto.MARCAR = true;
                        dto.NO = tabs.NO;
                        list.Add(dto);

                    }

                }



                dt_tabla_origen.ItemsSource = list;

                dt_tabla_origen.Items.Refresh();



            }
            else
            {
                List<TablasOrigen> list = new List<TablasOrigen>();



                foreach (var item in dt_tabla_origen.Items)
                {
                    if (item is TablasOrigen tabs)
                    {
                        TablasOrigen dto = new TablasOrigen();

                        dto.TABLE_NAME = tabs.TABLE_NAME;
                        dto.MARCAR = false;
                        dto.NO = tabs.NO;
                        list.Add(dto);

                    }

                }


                chk_marca_todos_destino.IsEnabled = true;
                chk_sel_individual_origen.IsEnabled = true;
                chk_sel_individual_destino.IsEnabled = true;


                dt_tabla_origen.ItemsSource = list;

                dt_tabla_origen.Items.Refresh();
            }


        }

        private void chk_marca_todos_destino_Checked(object sender, RoutedEventArgs e)
        {
            if (chk_marca_todos_destino.IsChecked == true)
            {
                List<TablaDestinoDT> list = new List<TablaDestinoDT>();



                foreach (var item in dt_tabla_destino.Items)
                {
                    if (item is TablaDestinoDT tabs)
                    {
                        TablaDestinoDT dto = new TablaDestinoDT();

                        dto.TABLE_NAME = tabs.TABLE_NAME;
                        dto.MARCAR = true;
                        dto.NO = tabs.NO;
                        list.Add(dto);

                    }

                }

                dt_tabla_destino.ItemsSource = list;

                dt_tabla_destino.Items.Refresh();

                chk_marca_todo_origen.IsEnabled = false;
                chk_sel_individual_origen.IsEnabled = false;
                chk_sel_individual_destino.IsEnabled = false;

            }
            else
            {
                List<TablaDestinoDT> list = new List<TablaDestinoDT>();



                foreach (var item in dt_tabla_destino.Items)
                {
                    if (item is TablaDestinoDT tabs)
                    {
                        TablaDestinoDT dto = new TablaDestinoDT();

                        dto.TABLE_NAME = tabs.TABLE_NAME;
                        dto.MARCAR = false;
                        dto.NO = tabs.NO;
                        list.Add(dto);

                    }

                }
                chk_marca_todo_origen.IsEnabled = true;
                chk_sel_individual_origen.IsEnabled = true;
                chk_sel_individual_destino.IsEnabled = true;

                dt_tabla_destino.ItemsSource = list;

                dt_tabla_destino.Items.Refresh();
            }




        }

        private void btn_quitar_destino_Click(object sender, RoutedEventArgs e)
        {


            List<TablaDestinoDT> list = new List<TablaDestinoDT>();
            List<TablasOrigen> listori = new List<TablasOrigen>();
            List<TablaDestinoDT> lista = new List<TablaDestinoDT>();


            foreach (var item in dt_tabla_destino.Items)
            {
                if (item is TablaDestinoDT tabs)
                {
                    TablaDestinoDT dto = new TablaDestinoDT();

                    if (tabs.MARCAR == false)
                    {
                        dto.TABLE_NAME = tabs.TABLE_NAME;
                        dto.MARCAR = tabs.MARCAR;
                        dto.NO = tabs.NO;
                        list.Add(dto);
                    }



                }

            }

            foreach (var item in dt_tabla_origen.Items)
            {


                if (item is TablasOrigen tabs)
                {
                    TablasOrigen dto = new TablasOrigen();

                    dto.MARCAR = false;

                    foreach (var l in list)
                    {
                        if (tabs.TABLE_NAME == l.TABLE_NAME && tabs.NO == l.NO)
                        {
                            dto.MARCAR = true;

                        }
                    }

                    dto.TABLE_NAME = tabs.TABLE_NAME;
                    dto.NO = tabs.NO;
                    listori.Add(dto);

                }


            }



            dt_tabla_destino.ItemsSource = list;

            dt_tabla_destino.Items.Refresh();

            dt_tabla_origen.ItemsSource = listori;

            dt_tabla_origen.Items.Refresh();

            dt_tabla_origen.IsEnabled = true;


            //chk_marca_todo_origen.IsEnabled = false;
            //chk_sel_individual_origen.IsEnabled = false;
            //chk_sel_individual_destino.IsEnabled = false;



        }

        private void chk_sel_individual_origen_Checked(object sender, RoutedEventArgs e)
        {

            if (chk_sel_individual_origen.IsChecked == true)
            {

                chk_marca_todos_destino.IsEnabled = false;


                chk_marca_todo_origen.IsEnabled = false;

                dt_tabla_origen.IsEnabled = true;

                //List<TablaDestinoDT> list = new List<TablaDestinoDT>();

                //dt_tabla_destino.ItemsSource= list;

            }
            else
            {

                chk_marca_todos_destino.IsEnabled = true;


                chk_marca_todo_origen.IsEnabled = true;

                dt_tabla_origen.IsEnabled = true;


            }


        }

        private void chk_sel_individual_destino_Checked(object sender, RoutedEventArgs e)
        {

            if (chk_sel_individual_destino.IsChecked == true)
            {
                chk_marca_todos_destino.IsEnabled = false;


                chk_marca_todo_origen.IsEnabled = false;

                dt_tabla_destino.IsEnabled = true;

            }
            else
            {
                chk_marca_todos_destino.IsEnabled = true;

                chk_marca_todo_origen.IsEnabled = true;

                dt_tabla_destino.IsEnabled = true;

            }

        }



        private void chk_Convert_Manual_chked(object sender, RoutedEventArgs e)
        {
            if (chk_Convert_Manual.IsChecked == true)
            {
                dt_tabla_conversiones.IsEnabled = true;
            }
            else
            {
                dt_tabla_conversiones.IsEnabled = false;
            }



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string txt = (cmb_tipo_extencion.SelectedItem.ToString().Length <= 0) ? "sql" : cmb_tipo_extencion.SelectedItem.ToString();

            string ruta = (txt_ruta_archivo.Text.Length <= 0) ? "C://script" : txt_ruta_archivo.Text;

            ruta = ruta.Trim()+"."+ txt;



            proceso_crea_scripts(sqlcon_data, ListTablas.ToList(), ruta, chk_GenScript_tables.IsChecked.Value,chk_Convert_Manual.IsChecked.Value );

            




        }

        public void proceso_crea_scripts(string sql, List<TablasDestino> tablasDestinoList, string ruta,bool CreaTablas = false,bool perso = false)
        {

            try
            {

                try
                {
                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        List<DataTypeConvert> dataTypeConverts = new List<DataTypeConvert>();

                        foreach(var item in  dt_tabla_conversiones.Items )
                        {
                            if (item is DataTypeConvert tabs)
                            {
                                DataTypeConvert dto = new DataTypeConvert();

                                dto.NO = tabs.NO;
                                dto.Tipo = tabs.Tipo;
                                dto.Propiedad = tabs.Propiedad;
                                dto.Equivalencia = tabs.Equivalencia;
                                dto.EqPropiedad = tabs.EqPropiedad;
                                dto.PersoType = tabs.PersoType;
                                dto.PropPersoType = tabs.PropPersoType;
                                dto.Observacion = tabs.Observacion;
                                dataTypeConverts.Add(dto);

                            }

                        }


                        sw.WriteLine("+=======+INICIA la creacion de scripts para creacion de tablas +=======+");
                        foreach (var item in tablasDestinoList)
                        {
                            var script_txt = string.Empty;

                            GenScriptDestino genScriptDestino = new GenScriptDestino();
                            sw.WriteLine("Inicia tabla :" + item.TABLE_NAME);

                            if ( perso == true)
                            {
                                Tools tools = new Tools();

                               
                                script_txt = genScriptDestino.GenScriptTablesPerso(item.TABLE_NAME, dataTypeConverts);
                            }
                            else
                            {
                                script_txt = genScriptDestino.GenScriptTablesDefault(item.TABLE_NAME, dataTypeConverts);
                            }
                            
                        }
                        
                        
                        sw.WriteLine("+=======+INICIA la creacion de scripts para insercion de datos+=======+");

                        foreach (var item in tablasDestinoList)
                        {
                            var script_txt = "";

                            sw.WriteLine("Inicia tabla :" + item.TABLE_NAME);

                            GenScriptDestino genScriptDestino = new GenScriptDestino();

                            script_txt = genScriptDestino.GENSCRIPT(item.TABLE_NAME);

                            List<string> lista_scripts = new List<string>();

                            Scripting scripting = new Scripting();

                            lista_scripts = scripting.scriptListRead(sql, script_txt);

                            foreach (string script in lista_scripts)
                            {

                                sw.WriteLine(script.ToString());
                            }

                            sw.WriteLine("Finaliza tabla :" + item.TABLE_NAME);


                        }


                    }
                    Console.WriteLine("Archivo creado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear el archivo: " + ex.Message);

                }






            }
            catch (Exception ex)
            {

            }


        }





    }
}

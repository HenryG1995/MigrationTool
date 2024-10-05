using GalaSoft.MvvmLight.Command;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Diagnostics;
using System.Windows.Media.Converters;
using System.Reflection;


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

        private SeleccionTablas _mainWindow;

        // Constructor que recibe la referencia del formulario WPF
        public SeleccionTablas(SeleccionTablas mainWindow)
        {
            _mainWindow = mainWindow;
        }



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

            tab_migarcion.IsEnabled = false;
            tab_conversion.IsEnabled = false;
            tab_scripts.IsEnabled = false;
            dt_tabla_origen.IsEnabled = false;
            dt_tabla_destino.IsEnabled = false;
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
                            dto.MARCAR = false;
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

                dt_tabla_destino.IsEnabled = true;

                //List<TablaDestinoDT> list = new List<TablaDestinoDT>();

                //dt_tabla_destino.ItemsSource= list;

            }
            else
            {

                chk_marca_todos_destino.IsEnabled = true;


                chk_marca_todo_origen.IsEnabled = true;

                dt_tabla_origen.IsEnabled = true;

                dt_tabla_origen.IsEnabled = false;

                dt_tabla_destino.IsEnabled = false;


            }


        }

        private void chk_sel_individual_destino_Checked(object sender, RoutedEventArgs e)
        {

            if (chk_sel_individual_destino.IsChecked == true)
            {
                chk_marca_todos_destino.IsEnabled = false;


                chk_marca_todo_origen.IsEnabled = false;

                dt_tabla_destino.IsEnabled = true;

                dt_tabla_origen.IsEnabled = true;


            }
            else
            {
                chk_marca_todos_destino.IsEnabled = true;

                chk_marca_todo_origen.IsEnabled = true;

                dt_tabla_destino.IsEnabled = false;
                dt_tabla_origen.IsEnabled = false;


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



        private async Task proceso_crea_scripts(string sql, List<TablasDestino> tablasDestinoList, string ruta, bool CreaTablas = false, bool perso = false)
        {
            progressBar.Value = 0;
            progressText.Text = "Progreso: 0%";

            var total = tablasDestinoList.Count;
            var i = 0;
            double progresot = 0;

            try
            {

                try
                {
                    using (StreamWriter sw = new StreamWriter(ruta))
                    {
                        List<DataTypeConvert> dataTypeConverts = new List<DataTypeConvert>();



                        foreach (var item in dt_tabla_conversiones.Items)
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


                        sw.WriteLine("/*+=======+INICIA la creacion de scripts para creacion de tablas +=======+*/");

                        foreach (var (item, index) in tablasDestinoList.Select((value, index) => (value, index)))

                        //  foreach (var item in tablasDestinoList)
                        {
                            var script_txt = string.Empty;

                            GenScriptDestino genScriptDestino = new GenScriptDestino();
                            sw.WriteLine("/*Inicia creacion de tabla :" + item.TABLE_NAME + "*/");

                            if (perso == true)
                            {
                                Tools tools = new Tools();

                                script_txt = genScriptDestino.GenScriptTablesPerso(item.TABLE_NAME, dataTypeConverts, sql);
                            }
                            else
                            {
                                script_txt = genScriptDestino.GenScriptTablesDefault(item.TABLE_NAME, dataTypeConverts, sql);
                            }
                            sw.WriteLine(script_txt.ToString());

                            lstBoxScripting.Items.Add(script_txt);



                            var script_txt2 = genScriptDestino.GenScriptPrimaryKey(item.TABLE_NAME);

                            List<scriptList> script_primarykey = genScriptDestino.GenScriptText(script_txt2, sql);

                            foreach (var scriptva in script_primarykey)
                            {
                                sw.WriteLine(scriptva.script.ToString());
                                lstBoxScripting.Items.Add(scriptva.script.ToString());
                            }

                            var script_txt3 = genScriptDestino.GenScriptIndexes(item.TABLE_NAME);
                            List<scriptList> scriptsIndex = genScriptDestino.GenScriptText(script_txt3, sql);

                            foreach (var scriptva in scriptsIndex)
                            {
                                sw.WriteLine(scriptva.script.ToString() + ";");
                                lstBoxScripting.Items.Add(scriptva.script.ToString());
                            }



                            var progreso = new Progress<int>(valor =>
                            {
                                // Aquí actualizamos la barra de progreso y el texto en la UI
                                i++;

                                double percentage = ((double)(index + 1) / total * 100) / 3;
                                Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");

                                progressBar.Value = percentage;
                                progressText.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            });

                            await Task.Run(() => ActualizarBarraProgreso(progreso));


                        }


                        sw.WriteLine("/*+=======+INICIA la creacion de scripts para insercion de datos+=======+*/");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");


                        i = 0;
                        foreach (var (item, index) in tablasDestinoList.Select((value, index) => (value, index)))
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
                                List<scriptList> scriptingList = new List<scriptList>();

                                scriptingList = genScriptDestino.GenScriptText(script.ToString(), sql);

                                foreach (var itemScript in scriptingList)
                                {
                                    sw.WriteLine(itemScript.script.ToString() + ";");

                                }

                            }

                            sw.WriteLine("Finaliza tabla :" + item.TABLE_NAME);


                            var progreso = new Progress<int>(valor =>
                            {
                                // Aquí actualizamos la barra de progreso y el texto en la UI
                                i++;

                                double percentage = ((double)(index + 1) / total * 100) / 3;
                                percentage = Math.Round(percentage, 2) + 33.33;
                                Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");

                                progressBar.Value = percentage;
                                progressText.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            });

                            await Task.Run(() => ActualizarBarraProgreso(progreso));



                        }



                        sw.WriteLine("/*+=======+INICIA la creacion de scripts para LLAVES FORANEAS de datos+=======+*/");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");
                        sw.WriteLine(" ");


                        i = 0;
                        foreach (var (item, index) in tablasDestinoList.Select((value, index) => (value, index)))
                        {

                            List<scriptList> script_foreing = new List<scriptList>();

                            GenScriptDestino genScriptDestino = new GenScriptDestino();


                            string script = genScriptDestino.GenScriptConstraintForeing(item.TABLE_NAME);

                            script_foreing = genScriptDestino.GenScriptText(script, sql);


                            foreach (var scriptva in script_foreing)
                            {
                                sw.WriteLine(scriptva.script.ToString());
                                lstBoxScripting.Items.Add(scriptva.script.ToString());
                            }

                            var progreso = new Progress<int>(valor =>
                            {
                                // Aquí actualizamos la barra de progreso y el texto en la UI
                                i++;

                                double percentage = ((double)(index + 1) / total * 100) / 3;
                                percentage = Math.Round(percentage, 2) + 66.66;
                                Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");

                                progressBar.Value = percentage;
                                progressText.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            });

                            await Task.Run(() => ActualizarBarraProgreso(progreso));

                        }


                        progressBar.Value = 100;
                        progressText.Text = "Progreso: 100%";

                    }
                    System.Windows.MessageBox.Show("Revise su archivo en : " + ruta.ToString(), "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                    Debug.WriteLine("Archivo creado exitosamente.");
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                    Debug.WriteLine("Error al crear el archivo: " + ex.Message);
                    System.Windows.MessageBox.Show("Error al crear su archivo en : " + ex.Message, "Confirmación", MessageBoxButton.OK, MessageBoxImage.Error);


                }






            }
            catch (Exception ex)
            {

            }


        }
        private void chk_cancela_destino_checked(object sender, EventArgs e)
        {

            tab_migarcion.IsEnabled = false;
            tab_conversion.IsEnabled = false;
            tab_scripts.IsEnabled = false;
            dt_tabla_destino.IsEnabled = true;
            dt_tabla_origen.IsEnabled = true;
        }


        private void chk_acepta_destino_Checked(object sender, RoutedEventArgs e)
        {
            if (dt_tabla_destino.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Seleccione correctamente las columnas a agregar para poder continuar con el proceso",
                               "Error!",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
                chk_acepta_destino.IsChecked = false;
                return;
            }


            if (chk_acepta_destino.IsChecked == true)
            {
                List<TablasDestino> list = new List<TablasDestino>();

                foreach (var item in dt_tabla_destino.Items)
                {
                    if (item is TablaDestinoDT tabs)
                    {
                        TablasDestino dto = new TablasDestino();


                        dto.TABLE_NAME = tabs.TABLE_NAME;

                        list.Add(dto);

                    }

                }

                ListTablas = list.ToList();



                dt_tabla_destino.IsEnabled = false;
                dt_tabla_origen.IsEnabled = false;
                tab_scripts.IsEnabled = true;
                tab_conversion.IsEnabled = true;
                tab_migarcion.IsEnabled = true;
            }
            else
            {
                tab_migarcion.IsEnabled = false;
                tab_conversion.IsEnabled = false;
                tab_scripts.IsEnabled = false;
                dt_tabla_destino.IsEnabled = true;
                dt_tabla_origen.IsEnabled = true;
            }


        }


        private void txt_ruta_archivo_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Mostrar el diálogo de selección de carpetas
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Comprobar si el usuario seleccionó una carpeta
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Obtener la ruta de la carpeta seleccionada
                    string folderPath = folderBrowserDialog.SelectedPath;

                    // Asignar la ruta seleccionada al TextBox
                    txt_ruta_archivo.Text = folderPath;

                    // Mostrar la ruta en un MessageBox (opcional)

                }
            }
        }

        private void txt_logPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Mostrar el diálogo de selección de carpetas
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Comprobar si el usuario seleccionó una carpeta
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Obtener la ruta de la carpeta seleccionada
                    string folderPath = folderBrowserDialog.SelectedPath;

                    // Asignar la ruta seleccionada al TextBox
                    txt_logPath.Text = folderPath;

                    // Mostrar la ruta en un MessageBox (opcional)

                }
            }
        }



        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            progressBar_Migracion.Value = 0;
            string ruta = "";

            if (txt_logPath.Text.Trim().Length == 0 && (chk_gen_script_migra.IsChecked == true || chk_log_migra.IsChecked == true))
            {


                System.Windows.MessageBox.Show("Seleccione el path para poder realizar la generación de scripts/Logs",
                               "Error!",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);

                return;
            }


            ComboBoxItem selectedItem = cmb_tipo_extencion.SelectedItem as ComboBoxItem;

            Guid guid = Guid.NewGuid();
            string uniqueNumber = BitConverter.ToInt64(guid.ToByteArray(), 0).ToString();


            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();

                ruta = (txt_ruta_archivo.Text.Length <= 0) ? "C:/script" : txt_ruta_archivo.Text + "/" + uniqueNumber + "." + selectedValue;
            }
            else
            {
                ruta = (txt_ruta_archivo.Text.Length <= 0) ? "C:/script" : txt_ruta_archivo.Text + "/" + uniqueNumber + ".txt";
            }
            //Thread hilo = new Thread(new ThreadStart(Migracion(sqlcon_data, ListTablas.ToList(), true, true, true)));



            string oracleConn = "miCadenaOracle";
            string sqlConn = "miCadenaSql";
            var tablas = ListTablas.ToList();
            bool genScript = chk_gen_script_migra.IsChecked ?? false;
            bool genLog = chk_log_migra.IsChecked ?? false;
            bool genPerso = chk_Convert_Manual.IsChecked ?? false;
            bool OverRid = chk_OverRide_Migra.IsChecked ?? false;
            List<DataTypeConvert> dataTypeConverts = new List<DataTypeConvert>();



            foreach (var item in dt_tabla_conversiones.Items)
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

         

                // Instanciamos la clase que contiene el método MigraCion



                // Llamamos al método MigraCion de manera asíncrona
                await MigraCion(oracon_data, sqlcon_data, tablas, dataTypeConverts, genScript, genLog, genPerso, ruta, OverRid);


           



        }
        public void ActualizarMigracionBar(IProgress<int> progreso)
        {
            // Simula una tarea en segundo plano (ejemplo: carga de datos)

            for (int i = 0; i <= 100; i++)
            {
                // Simulamos trabajo con retraso
                Task.Delay(50).Wait();

                // Informamos del progreso
                progreso.Report(i);
            }


        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ruta = "";

            if (txt_ruta_archivo.Text.Trim().Length == 0)
            {


                System.Windows.MessageBox.Show("Seleccione el path para poder realizar la generación de scripts",
                               "Error!",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);



                return;
            }


            ComboBoxItem selectedItem = cmb_tipo_extencion.SelectedItem as ComboBoxItem;

            Guid guid = Guid.NewGuid();
            string uniqueNumber = BitConverter.ToInt64(guid.ToByteArray(), 0).ToString();


            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();

                ruta = (txt_ruta_archivo.Text.Length <= 0) ? "C:/script" : txt_ruta_archivo.Text + "/" + uniqueNumber + "." + selectedValue;
            }
            else
            {
                ruta = (txt_ruta_archivo.Text.Length <= 0) ? "C:/script" : txt_ruta_archivo.Text + "/" + uniqueNumber + ".txt";
            }




            bool genScriptTables = chk_GenScript_tables.IsChecked.Value;
            bool convertManual = chk_Convert_Manual.IsChecked.Value;
            var tablas = ListTablas.ToList();

            await proceso_crea_scripts(sqlcon_data, tablas, ruta, genScriptTables, convertManual);



        }


        public void ActualizarBarraProgreso(IProgress<int> progreso)
        {
            for (int i = 0; i <= 100; i++)
            {
                // Simulamos trabajo con retraso
                Task.Delay(50).Wait();

                // Informamos del progreso
                progreso.Report(i);
            }
        }

        private void chk_log_migra_Copiar_Checked(object sender, RoutedEventArgs e)
        {

        }

        public async Task MigraCion(string OracleConnection, string SqlConnection, List<TablasDestino> litaTablasDestino, List<DataTypeConvert> listDataTypesConvert, bool GenScript = false, bool GenLog = false, bool genPerso = false, string PathLogScript = "", bool OverrR = false)
        {
            ProgressTextMigracion.Text = "Progreso: 0%";
            int maxThreads = Environment.ProcessorCount;

            var total = litaTablasDestino.Count;
            var i = 0;
            double progresot = 0;

            if (genPerso is true)
            {

            }
            else
            {
                //Parallel.For(0, maxThreads, i =>
                //{
                Conn translateData = new Conn();
                //creacion de tablas con llave primaria sin foraneas 
                if (OverrR == true)
                {
                    //borrado de tablas 14.2857
                    //                    foreach (var tborrar in (litaTablasDestino))
                    foreach (var (tborrar, index) in litaTablasDestino.Select((value, index) => (value, index)))
                    {
                        var conn = new Conn();

                        var resul = conn.BorrarTablas(tborrar.TABLE_NAME, OracleConnection);

                        Debug.WriteLine("Tabla : " + tborrar.TABLE_NAME + " Estado de borrado de tabla : " + resul);


                        var progreso = new Progress<int>(valor =>
                        {


                            double percentage = ((double)(index + 1) / total * 100) / 4;
                            percentage = Math.Round(percentage, 2);
                          //  Debug.WriteLine($"Número: {tborrar.TABLE_NAME}, Progreso: {percentage:F2}%");
                            progresot = percentage;
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                progressBar_Migracion.Value = percentage;
                                ProgressTextMigracion.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            }));

                        });

                        await Task.Run(() => ActualizarMigracionBar(progreso));




                    }
                    //creacion completa de tablas
                    foreach (var (item, index) in litaTablasDestino.Select((value, index) => (value, index)))
                    {
                        GenScriptDestino genScript = new GenScriptDestino();
                        var conn = new Conn();

                        var resul = false;
                        //creacion de  tabla
                        var script = genScript.GenScriptTablesDefault(item.TABLE_NAME, listDataTypesConvert.ToList(), SqlConnection);

                        var result = translateData.executeQueryOracle(OracleConnection, script);

                        Debug.WriteLine("Tabla : " + item.TABLE_NAME + " Estado de creacion de tabla : " + result);
                        //creacion de llave primaria
                        var script_primaryk = genScript.GenScriptPrimaryKey(item.TABLE_NAME);

                        var listScriptPrimaryky = genScript.GenScriptText(script_primaryk, SqlConnection);
                        foreach (var item2 in listScriptPrimaryky)
                        {
                            resul = translateData.executeQueryOracle(OracleConnection, item2.script.Replace(";", ""));
                            Debug.WriteLine("Tabla : " + item.TABLE_NAME + " Estado de creacion de llave primaria : " + result);
                        }
                        //creacion de indices
                        var scritp_index = genScript.GenScriptIndexes(item.TABLE_NAME);

                        var scriptindexlist = genScript.GenScriptText(scritp_index, SqlConnection);

                        foreach (var item2 in scriptindexlist)
                        {
                            result = translateData.executeQueryOracle(OracleConnection, item2.script.Replace(";", ""));
                            Debug.WriteLine("Tabla : " + item.TABLE_NAME + " Estado de creacion de indices: " + result);
                        }

                        var progreso = new Progress<int>(valor =>
                        {
                            double percentage = ((double)(index + 1) / total * 100) / 4;
                            percentage = Math.Round(percentage, 2) + 25.00;
                          //  Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                progressBar_Migracion.Value = percentage;
                                ProgressTextMigracion.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            }));
                            progresot = percentage;

                        });

                        await Task.Run(() => ActualizarMigracionBar(progreso));

                    }

                    // migracion de datos con bulk
                    foreach (var (item, index) in litaTablasDestino.Select((value, index) => (value, index)))
                    {
                        Conn conn = new Conn();

                        var dtorigen = conn.DataOrigen(SqlConnection, item.TABLE_NAME);

                        var result = conn.Destino(dtorigen, item.TABLE_NAME, OracleConnection, listDataTypesConvert.ToList());

                        Debug.WriteLine(item.TABLE_NAME + " , insercion realizada = " + result);

                        var progreso = new Progress<int>(valor =>
                        {
                            double percentage = ((double)(index + 1) / total * 100) / 4;
                            percentage = Math.Round(percentage, 2) + 50.00;
                           // Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                progressBar_Migracion.Value = percentage;
                                ProgressTextMigracion.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            }));
                            progresot = percentage;
                        });

                        await Task.Run(() => ActualizarMigracionBar(progreso));


                    }

                    // creacion de foraneas 
                    foreach (var (item, index) in litaTablasDestino.Select((value, index) => (value, index)))
                    {
                        GenScriptDestino genScriptDestino = new GenScriptDestino();

                        var script_foraneas = genScriptDestino.GenScriptConstraintForeing(item.TABLE_NAME);

                        List<scriptList> lista_script = genScriptDestino.GenScriptText(script_foraneas, SqlConnection);

                        foreach (var item2 in lista_script)
                        {
                            Conn conn = new Conn();

                            var result = conn.executeQueryOracle(OracleConnection, item2.script.Replace(";", ""));

                        }
                        var progreso = new Progress<int>(valor =>
                        {
                            double percentage = ((double)(index + 1) / total * 100) / 4;
                            percentage = Math.Round(percentage, 2) + 75.00;
                        //    Debug.WriteLine($"Número: {item.TABLE_NAME}, Progreso: {percentage:F2}%");
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                progressBar_Migracion.Value = percentage;
                                ProgressTextMigracion.Text = "Progreso: " + Math.Round(percentage, 2) + "%";
                            }));
                            progresot = percentage;
                        });

                        await Task.Run(() => ActualizarMigracionBar(progreso));

                    }

                }
                else
                {
                    // migracion de datos con bulk
                    foreach (var item in litaTablasDestino)
                    {
                        Conn conn = new Conn();

                        var dtorigen = conn.DataOrigen(SqlConnection, item.TABLE_NAME);

                        var result = conn.Destino(dtorigen, item.TABLE_NAME, OracleConnection, listDataTypesConvert.ToList());

                        Debug.WriteLine(item.TABLE_NAME + " , insercion realizada = " + result);

                    }

                }



            }

        }




    }






}

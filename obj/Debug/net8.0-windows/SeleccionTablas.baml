   M S B A M L   `   `   `  ����H  DToolMigration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ToolMigrationToolMigration  5   	   #    ToolMigration.SeleccionTablas   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ToolMigration     5   	   -   5          �Name$  MigrationTool��5   	    � Title$ MigrationTool��5   	   $	��545��6   $
��1221��6,    � 
ResizeMode$ NoResize=�69   $��Center=�6O   $��Center=�6l    � WindowStyle$ 
ToolWindow=�6�    � WindowStartupLocation$ CenterScreen=�6�    � Cursor$ Arrow��6�   ��6�   Z� .��5	      � $��	0,0,28,43q�6    � ColumnDefinitions 5
   
   .+�5   
   �� $��20,10,0,-10q�6   $
��Left=�6*   $
��1164��6E   ���	#FFB7B7B7.@�5      �� -   5         ��Name$ tab_selector��5      $��Selector de tablas��6,   .��5      � ���	#FFE5E5E5$	��429��6.   $��Right=�6;   $
��1133��6W   $��0,0,15,0q�6d   .+�5      � $��	0,-21,0,0q�6    5      �� 5      $
��276* �6/   �� 5      $
��857* �6/   5      �� 5      $��Origen ( SQL Server )��6   $
��Left=�6<   $��	194,5,0,0q�6W   $	��Top=�6j   $	��123��6�   �� 5      $��Destino ( Oracle 19c )��6   $
��Left=�6=   $��
597,10,0,0q�6X   $	��Top=�6l   $	��123��6�   �� 5      -   5         ��Name$ chk_marca_todo_origen��5      $��Marcar Todos��6>   $
��Left=�6U   $��Bottom=�6p   $��477,0,0,303q�6�   �� 5      -   5        $ chk_sel_individual_origen��5      $��Seleccionar Individual��6B   $
��Left=�6c   	 d� 	IsChecked$	 FalseY�6�   $��477,90,0,323q�6�   �� 5      -   5        
 ��Name$
 btn_quitar_destino��5      $��<-------��69   $��Right=�6O   $	��Top=�6k   $	��126��6�    J� RenderTransformOrigin$ 0.374,0.686&�6�   $��0,289,533,0q�6�   �� 5      -   5        $
 btn_mov_a_destino��5      $��
--------->��68   $
��Left=�6P   $	��Top=�6k   $	��126��6�   $��477,132,0,0q�6�   �� 5      -   5        $ chk_marca_todos_destino��5      $��Marcar Todos��6@   $
��Left=�6W   $	��Top=�6r   $��477,242,0,0q�6�   �� 5      -   5        $  chk_sel_individual_destino��5      $��Seleccionar Individual��6C   $
��Left=�6d   $��477,262,0,0q�6   $	��Top=�6�   $��22��6�   $	 FalseY�6  � 5      $��Right=�6   $��
0,36,99,15q�67   $
��1024��6K   .+�5       &   System.Windows.Controls.DataGrid  -	   5          Name$ dt_tabla_origen��5    #      AutoGenerateColumns$ False��6=   $
��Left=�6Y   $	��425��6t   $��	10,3,0,-3q�6�      Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"   "   ,  &System.Windows.Controls.DataGridColumn   Header$ NO��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *NO+4  .System.Windows.Controls.DataGridCheckBoxColumn  5#   "   $ Marcar��69    6I   �� *MARCAR+  5$   "   $ Table Name           ��65    6T   �� *
TABLE_NAME+5&        5'      -
   5        $ dt_tabla_destino��5'   #   $��
607,0,10,0q�6=   $ False��6Q    5(        5)   "   $ NO��65    6A   �� *NO+  5*   "   $ Marcar��69    6I   �� *MARCAR+  5+   "   $ Table Name           ��65    6T   �� *
TABLE_NAME+5.      �� 5/      -   5        $ chk_acepta_destino��5/   #   $��Aceptar��6?   $
��Left=�6Q   $��466,147,0,0q�6l   $	��Top=�6�   $ 0.5,0.5&�6�   6�   50      �� 51      $��Origen��6   $
��Left=�6-   $��
467,46,0,0q�6H   $	��Top=�6\   52      53      �� 54      -   5        $ tab_conversion��54      $��Conversiones��55   	   .��56      � 
������� 57      �� 58      $	��32* �6+   �� 59      $
��547* �6+   .+�5;      �� -   5        $ chk_Convert_Manual��5;      $��Conversion Manual��6;   $
��Left=�6W   $��	10,33,0,0q�6r   $	��Top=�6�   $��2��6�   �� 5<      $��SQL <----> ORACLE��6   $��0,67,0,0q�6@   $��2��6R   .��5=        -   5        $ dt_tabla_conversiones��5=   #   $ False��6p    5>        5?   "   $ NO��65      
IsReadOnly$
 True��6B    6T   �� *NO+  5@   "   $ Tipo de dato Origen��65   $
 True��6R    6d   �� *Tipo+  5A   "   $ 	Propiedad��65   $
 True��6H    6Z   �� *	Propiedad+  5B   "   $ Equivalencia��65   $
 True��6K    6]   �� *Equivalencia+  5C   "   $ Propiedad Equivalente��65   $
 True��6T    6f   �� *EqPropiedad+4  .System.Windows.Controls.DataGridTemplateColumn  5D   "   $ Tipo Personalizado��69      CellTemplate 5E   &   �� 5F   *   .R�5G   .   �� ��68   �� *	PersoType+5H   +      CellEditingTemplate 5J   &   �� 5K   *   .R�5L   .   �� ��66   �� *	PersoType+ �� UpdateSourceTrigger$ PropertyChanged=�5M   +   5O   #     5Q   "   $% Propiedad de Tipo Personalizado��69    5R   &   �� 5S   *   .R�5T   .   �� ��68   �� *PropPersoType+5U   +    5W   &   �� 5X   *   .R�5Y   .   �� ��66   �� *PropPersoType+$ PropertyChanged=�5Z   +   5\   #     5]   "   $ Observacion��65   $
 True��6J    6\   �� *Observacion+5`      5a      5b      5c      �� 5d      -   5        $ tab_scripts��5d      $��Scripts��6+   .��5e      � ���	#FFE5E5E5$	��460��6.    5f      �� 5g      $
��277* �6+   �� 5h      $
��881* �6+   .+�5l      � -   5         �Name$ progressBar��5l   "   $��
10,401,8,7q�67    � Minimum$ 0Q�6K    � Maximum$	 100Q�6W    � LargeChange$ 10Q�6e   $��2��6v   �� 5m      -   5        $��Generar Script��5m      $
��Left=�66   $��
47,255,0,0q�6Q   $	��Top=�6e   $	��157��6}   $��38��6�   6�   �� 5n      -   5         ��Name$ cmb_tipo_extencion��5n      $
��Left=�6;   $��	47,69,0,0q�6V   $	��Top=�6i   $	��157��6�   .@�5p      �� $	��txt��6'   �� 5q      $	��sql��6'   5t      �� 5u      $ ��Seleccionar Tipo Extencion��6   $
��Left=�6A   $��	47,43,0,0q�6\   $	��Top=�6o   $ 0.271,0.437&�6�   $	��157��6�   �� 5v      $��Donde Guardar el archivo��6   $
��Left=�6?   $��
47,172,0,0q�6Z   $	��Top=�6n   $ 0.271,0.437&�6�   $	��157��6�   �� 5y      -   5         ��Name$ txt_ruta_archivo��5y      $
��Left=�68   $��
47,203,0,0q�6S    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��157��6�   6�   �� 5~      -   5        $ chk_GenScript_tables��5~      $
��Left=�6=   $��
47,113,0,0q�6X   $	��Top=�6l   $	��157��6�   $��Generar Tablas��6�   6�   �� 5      $��Generar Insert ��6   $
��Left=�69   $	��Top=�6T   $	��157��6l   $��
47,138,0,0q�6x   �� 5�      -   5         ��Name$ lstBoxScripting��5�      $��66,69,37,64q�6b   $ ScrollN��6w   $��1��6�   �� 5�      -   5         ��Name$ progressText��5�       $��Progreso: 0%��66   $ 0.318,-0.093&�6J   $��170,401,510,17q�6o   $��1��6�   5�      5�      �� 5�      -   5        $ tab_migarcion��5�      $��	Migracion��6-   .��5�      � ���	#FFE5E5E5 5�      �� 5�      .+�5�      � -   5        $ progressBar_Migracion��5�   "   $��35��6A   $ 0Q�6M   $	 100Q�6Y   $	��Top=�6g   $��-1,422,21,0q�6   $ 35Q�6�   �� 5�      -   5        $ txt_logPath��5�      $
��Left=�63   $��
114,66,0,0q�6N   $
 Wrap=�6b   $	��Top=�6v   $	��147��6�   $��18��6�   �� 5�      $��Ubicacion de log��6   $
��Left=�67   $��	10,62,0,0q�6R   $	��Top=�6e   $��26��6}   $��99��6�   �� 5�      -   5        $ chk_log_migra��5�      $��Habilitar Log��66   $
��Left=�6N   $��	10,42,0,0q�6i   $	��Top=�6|   $��15��6�   $��88��6�   �� 5�      -   5        $ LstResumenLogMigra��5�      $��0,193,21,57q�6:   �� 5�      $��Resumen de migracion��6   $
��Left=�6;   $��	0,167,0,0q�6V   $	��Top=�6i   $��26��6�   $	��131��6�   �� 5�      -   5        $
 
btn_migrar��5�      $��Iniciar Migracion��61   $
��Left=�6M   $��
10,110,0,0q�6h   $	��Top=�6|   $��31��6�   $	��224��6�   6�   �� 5�      -   5        $ chk_gen_script_migra��5�      $��Generar Scripts��6=   $
��Left=�6W   $��	10,22,0,0q�6r   $	��Top=�6�   $��15��6�   $	��101��6�   �� 5�      -   5        $ chk_OverRide_Migra��5�      $��Sobrescribir destino��6;   $
��Left=�6Z   $��
122,22,0,0q�6u   $	��Top=�6�   $��15��6�   $	��125��6�   �� 5�      -   5        $ ProgressTextMigracion��5�       $ 0.318,-0.093&�6?   $
��Left=�6d   $��461,422,0,0q�6   $
��Wrap=�6�   $��Progreso: 0%��6�   $	��Top=�6�   $	��193��6�   $��36��6�   5�      5�      5�      � 5�   
   $
��Left=�6   $��15��6*   $��Bottom=�66   $��59��6Q   $��
505,0,0,86q�6\   5�      5�      
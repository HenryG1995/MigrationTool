﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Diagnostics;
namespace ToolMigration.Logic.DataMove
{
    internal class SelectorD
    {


        public SelectorD() { }

        public List<string> ListTables { get; set; }

        public DataTable data {  get; set; }

        public List<string> tablas(string connstr,bool or)
        {

               switch (or)
            {
                case true:
                    {
                        //Oracle

                        Debug.WriteLine("Inicia la obtencion de tablas para oracle");



                        break;
                    }
                case false:
                    {
                        //SQL

                        Debug.WriteLine("Inicia la obtencion de tablas para SQL");


                        break;
                    }
            }

 
        return ListTables.ToList();
        }

        public bool migration(string constr)
        {





            return true;
        }

       

     

    }
}

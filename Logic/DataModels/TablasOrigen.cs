using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.DataModels
{
    public class TablasOrigen
    {
        public Int64 NO { get; set; }
        public bool MARCAR { get; set; }
        public string ? TABLE_NAME { get; set; }

    }

    public class TablasDestino
    {
        public string ? TABLE_NAME { get; set; }

    }

    public class TablaDestinoDT
    {
        public Int64 NO { get; set; }
        public bool MARCAR { get; set; }
        public string ? TABLE_NAME { get; set; }

    }

    public class DataTypeConvert
    {
        public Int64 NO { get; set; }
        public string ? Tipo { get; set; }
        public string ? Propiedad { get; set; }
        public string ? Equivalencia { get; set; }
        public string ? EqPropiedad { get; set; }
        public string ? PersoType { get; set; }
        public string ? PropPersoType { get; set; }
        public string ? Observacion { get; set; }
    }

    public class DataTypeOrigenXTable
    {
        public string ? TABLE_NAME { get; set; }
        public string ? COLUMN_NAME { get; set; }
        public int ? ORDINAL_POSITION { get; set; }
        public string ? COLUMN_DEFAULT { get; set; }
        public bool ? IS_NULLABLE { get; set; }
        public string ? DATA_TYPE { get; set; }
        public string ? DATA_TYPE_DETAIL { get; set; }        
        public string ? DATA_LENGTH { get; set; }

    }
    public class scriptList 
    {
        public string script { get; set; }
    }


}

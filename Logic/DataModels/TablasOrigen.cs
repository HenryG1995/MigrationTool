using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolMigration.Logic.DataModels
{
    public class TablasOrigen
    {
        public string NO { get; set; }
        public bool MARCAR { get; set; }
        public string TABLE_NAME { get; set; }

     }

    public class TablasDestino
    {
        public string TABLE_NAME { get; set; }

    }
            
    
}

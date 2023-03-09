using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBookProject.Database.Tables
{
    public partial class USER
    {
        public int userid { get; set; }

        public string firstname { get; set; }

        public string? lastname { get; set; } 

        public string email { get; set; }

        public string password { get; set; }

        public string? mobile { get; set; }        

    }
}

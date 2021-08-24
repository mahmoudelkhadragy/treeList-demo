using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreeListDemo.Model
{
    
        [Table("ViewField", Schema = "dbo")]
        public class ViewField
        {
            [Key, Column(Order = 0)]
            public int ViewID { get; set; }

            [Key, Column(Order = 1)]
            public int FieldID { get; set; }

            public virtual View view { set; get; }
            public virtual Fields field { set; get; }
        }

    
}

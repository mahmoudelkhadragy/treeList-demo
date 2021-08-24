using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TreeListDemo.Model
{
    public enum FieldTypeLib : byte
    {
        NormalEnglish = 0,
        NormalArabic = 1,      
        JoinNormalEnglish = 2, 
        JoinNormalArabic = 3,
     
    }
    [Table("Fields", Schema = "dbo")]
    public class Fields
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { set; get; }

        [Required]
        [Display(Name = "TableName")]
        public string TableName { set; get; }

        [Required]
        [Display(Name = "FieldsPrefix")]
        public string FieldsPrefix { set; get; }
        [Required]
        [Display(Name = "FieldType")]
        public FieldTypeLib FieldType { set; get; }

        [Display(Name = "DisplayFieldprefix")]
        public string DisplayFieldprefix { set; get; }

        [Display(Name = "LinkingFieldPrefix")]
        public string LinkingFieldPrefix { set; get; }


        [Display(Name = "SecondaryTable")]
        public string SecondaryTable { set; get; }


        [Display(Name = "SecondaryLink")]
        public string SecondaryLink { set; get; }
       

    }
}

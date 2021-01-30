using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Shopping
{
    public class CartDepartment
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Department_ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Index("Department_Index", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(80)]
        public string Department_Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        public string Description { get; set; }      
        
        public virtual ICollection<Category> Categories { get; set; }
    }

    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Index("Category_Index", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(80)]
        public string Name { get; set; }

     
        public int Department_ID { get; set; }
        public virtual CartDepartment cartDepartment { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

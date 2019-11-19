using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppWebApi.ENTITIES.EntityClass
{
    public class Task
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsComleted { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DueDate{ get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual User Owner { get; set; }
        

    }
}

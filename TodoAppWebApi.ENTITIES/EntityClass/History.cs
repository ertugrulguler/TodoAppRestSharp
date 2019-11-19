using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppWebApi.ENTITIES.EntityClass
{
    public class History
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "250 karakterden fazla giriş yapılamaz")]
        public string Text { get; set; }

        public virtual User Owner { get; set; }

    }
}

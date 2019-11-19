
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAppWebApi.ENTITIES.EntityClass
{
    public class User 
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(15,ErrorMessage ="Kullanıcı adı 15 karakterden fazla olamaz")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }


        public virtual List<History> Histories { get; set; }
        public virtual List<Task> Tasks { get; set; }


    }
}

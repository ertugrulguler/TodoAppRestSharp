using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppWebApi.ENTITIES.ViewModels
{
    public class TaskResponseViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Username { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

    }
}

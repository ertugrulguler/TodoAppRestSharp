using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppWebApi.DAL.Context;

namespace TodoAppWebApi.BLL
{
    public class Test
    {
        public Test()
        {
        TodoContext db = new TodoContext();
            db.Users.ToList();
        }
        
    }
}

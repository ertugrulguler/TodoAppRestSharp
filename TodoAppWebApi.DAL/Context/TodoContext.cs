using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppWebApi.DAL.Initializer;
using TodoAppWebApi.ENTITIES.EntityClass;

namespace TodoAppWebApi.DAL.Context
{
    public class TodoContext : DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    Database.Connection.ConnectionString =/* ConfigurationManager.ConnectionStrings["TodoAppApi"].ConnectionString;*/@"Server=DESKTOP-M9NVTPB\SQLEXPRESS;Database=TodoAppWebApi;Integrated Security=True;";
        //    Database.SetInitializer<TodoContext>(null);

        //    base.OnModelCreating(modelBuilder);
        //}
        public TodoContext()
        {
            Database.Connection.ConnectionString = @"Server=DESKTOP-M9NVTPB\SQLEXPRESS;Database=TodoAppWebApi;Integrated Security=True;";

            Database.SetInitializer(new TodoInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<TodoAppWebApi.ENTITIES.EntityClass.Task> Tasks { get; set; }

    }
}

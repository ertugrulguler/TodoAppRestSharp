namespace TodoAppWebApi.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Text = c.String(nullable: false, maxLength: 250),
                        Owner_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Username = c.String(nullable: false, maxLength: 15),
                        Email = c.String(),
                        Password = c.String(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        IsComleted = c.Boolean(nullable: false),
                        DueDate = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Owner_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Owner_ID", "dbo.Users");
            DropForeignKey("dbo.Histories", "Owner_ID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Owner_ID" });
            DropIndex("dbo.Histories", new[] { "Owner_ID" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Users");
            DropTable("dbo.Histories");
        }
    }
}

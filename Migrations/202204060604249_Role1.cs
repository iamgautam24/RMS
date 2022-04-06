namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Checked = c.Boolean(nullable: false),
                        Registration_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Registrations", t => t.Registration_Id)
                .Index(t => t.Registration_Id);
            
            AddColumn("dbo.Registrations", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "Registration_Id", "dbo.Registrations");
            DropIndex("dbo.Roles", new[] { "Registration_Id" });
            DropColumn("dbo.Registrations", "RoleName");
            DropTable("dbo.Roles");
        }
    }
}

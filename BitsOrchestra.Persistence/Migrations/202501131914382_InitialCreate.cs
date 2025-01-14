namespace BitsOrchestra.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Married = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 13),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Phone, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contacts", new[] { "Phone" });
            DropTable("dbo.Contacts");
        }
    }
}

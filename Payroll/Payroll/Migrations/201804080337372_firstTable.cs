namespace Payroll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Payroll",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Role = c.String(nullable: false, maxLength: 30),
                        Section = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Hours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_Payroll");
        }
    }
}

namespace DoAn_LTWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Brand_Id = c.Int(nullable: false),
                        Brand_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id1)
                .Index(t => t.Brand_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Brand_Id1", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_Id1" });
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}

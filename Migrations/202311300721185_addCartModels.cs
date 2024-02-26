namespace DoAn_LTWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Product_Id", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "Product_Id" });
            DropTable("dbo.Carts");
        }
    }
}

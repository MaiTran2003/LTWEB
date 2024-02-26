namespace DoAn_LTWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIdRow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Product_Id", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Carts", name: "Product_Id", newName: "Id");
            AlterColumn("dbo.Carts", "Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "Id");
            AddForeignKey("dbo.Carts", "Id", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "ProId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ProId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "Id", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "Id" });
            AlterColumn("dbo.Carts", "Id", c => c.Int());
            RenameColumn(table: "dbo.Carts", name: "Id", newName: "Product_Id");
            CreateIndex("dbo.Carts", "Product_Id");
            AddForeignKey("dbo.Carts", "Product_Id", "dbo.Products", "Id");
        }
    }
}

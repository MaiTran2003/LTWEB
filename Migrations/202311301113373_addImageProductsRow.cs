namespace DoAn_LTWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageProductsRow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageProducts", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageProducts");
        }
    }
}

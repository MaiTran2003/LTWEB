namespace DoAn_LTWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeQuantity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Long());
        }
    }
}

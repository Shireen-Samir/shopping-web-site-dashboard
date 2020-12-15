namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "categorypicture", c => c.String());
            AddColumn("dbo.Products", "productpicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "productpicture");
            DropColumn("dbo.Categories", "categorypicture");
        }
    }
}

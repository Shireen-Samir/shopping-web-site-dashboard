namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prod_cat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryid = c.Int(nullable: false, identity: true),
                        categoryname = c.String(nullable: false),
                        categorydescription = c.String(),
                    })
                .PrimaryKey(t => t.categoryid);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productid = c.Int(nullable: false, identity: true),
                        productname = c.String(nullable: false),
                        productdescription = c.String(),
                        price = c.Int(nullable: false),
                        categoryid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.productid)
                .ForeignKey("dbo.Categories", t => t.categoryid, cascadeDelete: true)
                .Index(t => t.categoryid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "categoryid", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "categoryid" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}

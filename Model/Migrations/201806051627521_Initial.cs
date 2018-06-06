namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomePages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstImgUrl = c.String(unicode: false),
                        SecondImgUrl = c.String(unicode: false),
                        ThirdImgUrl = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HomePages");
        }
    }
}

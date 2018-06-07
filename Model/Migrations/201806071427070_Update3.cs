namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TouristInformationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InformationId = c.Int(nullable: false),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TouristInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        ImgUrl = c.String(unicode: false),
                        Distance = c.String(unicode: false),
                        Price = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TouristRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(unicode: false),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TouristRoutes");
            DropTable("dbo.TouristInformations");
            DropTable("dbo.TouristInformationDetails");
        }
    }
}

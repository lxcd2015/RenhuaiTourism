namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WisdomGuideMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WisdomGuideId = c.Int(nullable: false),
                        ImgUrl = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WisdomGuides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WisdomGuideViewSpots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WisdomGuideId = c.Int(nullable: false),
                        ViewSpotName = c.String(unicode: false),
                        ViewSpotDescribe = c.String(unicode: false),
                        ImgUrl = c.String(unicode: false),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WisdomGuideViewSpotVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WisdomGuideViewSpotId = c.Int(nullable: false),
                        VideoName = c.String(unicode: false),
                        ImgUrl = c.String(unicode: false),
                        VideoUrl = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WisdomGuideViewSpotVideos");
            DropTable("dbo.WisdomGuideViewSpots");
            DropTable("dbo.WisdomGuides");
            DropTable("dbo.WisdomGuideMaps");
        }
    }
}

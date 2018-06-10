namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Introduces", "VideoUrl", c => c.String(unicode: false));
            AddColumn("dbo.TouristRoutes", "RouteName", c => c.String(unicode: false));
            AddColumn("dbo.TouristRoutes", "NeedDays", c => c.Int(nullable: false));
            AddColumn("dbo.WisdomGuideViewSpotVideos", "VoiceName", c => c.String(unicode: false));
            AddColumn("dbo.WisdomGuideViewSpotVideos", "VoiceUrl", c => c.String(unicode: false));
            DropColumn("dbo.TouristRoutes", "Content");
            DropColumn("dbo.WisdomGuideViewSpotVideos", "VideoName");
            DropColumn("dbo.WisdomGuideViewSpotVideos", "VideoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WisdomGuideViewSpotVideos", "VideoUrl", c => c.String(unicode: false));
            AddColumn("dbo.WisdomGuideViewSpotVideos", "VideoName", c => c.String(unicode: false));
            AddColumn("dbo.TouristRoutes", "Content", c => c.String(unicode: false));
            DropColumn("dbo.WisdomGuideViewSpotVideos", "VoiceUrl");
            DropColumn("dbo.WisdomGuideViewSpotVideos", "VoiceName");
            DropColumn("dbo.TouristRoutes", "NeedDays");
            DropColumn("dbo.TouristRoutes", "RouteName");
            DropColumn("dbo.Introduces", "VideoUrl");
        }
    }
}

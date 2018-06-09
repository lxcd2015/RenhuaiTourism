namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WisdomGuideViewSpots", "Position", c => c.String(unicode: false));
            AddColumn("dbo.WisdomGuideViewSpots", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.WisdomGuideViewSpots", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.WisdomGuideViewSpots", "ViewSpotDescribe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WisdomGuideViewSpots", "ViewSpotDescribe", c => c.String(unicode: false));
            DropColumn("dbo.WisdomGuideViewSpots", "Latitude");
            DropColumn("dbo.WisdomGuideViewSpots", "Longitude");
            DropColumn("dbo.WisdomGuideViewSpots", "Position");
        }
    }
}

namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TouristInformations", "Position", c => c.String(unicode: false));
            AddColumn("dbo.TouristInformations", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.TouristInformations", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.TouristInformations", "Distance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TouristInformations", "Distance", c => c.String(unicode: false));
            DropColumn("dbo.TouristInformations", "Latitude");
            DropColumn("dbo.TouristInformations", "Longitude");
            DropColumn("dbo.TouristInformations", "Position");
        }
    }
}

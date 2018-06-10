namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WisdomGuideViewSpots", "Phone", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WisdomGuideViewSpots", "Phone");
        }
    }
}

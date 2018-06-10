namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailParagraphs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailId = c.Int(nullable: false),
                        ParagraphIndex = c.Int(nullable: false),
                        ParagraphContent = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModularType = c.Int(nullable: false),
                        Classify = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ImgUrl = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Introduces", "ImgUrl");
            DropColumn("dbo.Introduces", "Content");
            DropColumn("dbo.WisdomGuideViewSpots", "Content");
            DropTable("dbo.TouristInformationDetails");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.WisdomGuideViewSpots", "Content", c => c.String(unicode: false));
            AddColumn("dbo.Introduces", "Content", c => c.String(unicode: false));
            AddColumn("dbo.Introduces", "ImgUrl", c => c.String(unicode: false));
            DropTable("dbo.Details");
            DropTable("dbo.DetailParagraphs");
        }
    }
}

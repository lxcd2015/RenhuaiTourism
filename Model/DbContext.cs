using Model.Data;
using System.Data.Entity;


namespace Model
{
    public class RTDbContext : DbContext
    {

        public RTDbContext()
            : base("name=ConnectionString")
        {
        }

        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<Introduce> Introduces { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<WisdomGuide> WisdomGuides { get; set; }
        public DbSet<WisdomGuideMap> WisdomGuideMaps { get; set; }
        public DbSet<WisdomGuideViewSpot> WisdomGuideViewSpots { get; set; }
        public DbSet<WisdomGuideViewSpotVideo> WisdomGuideViewSpotVideos { get; set; }
    }
}
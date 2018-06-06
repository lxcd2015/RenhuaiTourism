namespace ViewModel.WisdomGuide
{
    public class TouristSpotInfo
    {

        /// <summary>
        /// 景点Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoUrl { get; set; }
        
        /// <summary>
        /// 景点名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 景点描述（距我100米）
        /// </summary>
        public string Describe { get; set; }

        public TouristSpotType Type { get; set; }
    }
}
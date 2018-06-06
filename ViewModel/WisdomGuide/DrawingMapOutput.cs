namespace ViewModel.WisdomGuide
{
    public class DrawingMapOutput
    {
        /// <summary>
        /// 地图图片地址
        /// </summary>
        public string MapImgUrl { get; set; }

        /// <summary>
        /// 是否有更多
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// 景点信息
        /// </summary>
        public TouristSpotInfo SpotInfo { get; set; }
    }
}
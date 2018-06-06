using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ViewModel.WisdomGuide;

namespace WebApi.Controllers
{
    /// <summary>
    /// 智慧导览服务
    /// </summary>
    public class WisdomGuideController : ApiController
    {
        /// <summary>
        /// 获取地图页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DrawingMapOutput GetDrawingMap(DrawingMapInput input)
        {
            input = new DrawingMapInput();
            input.type = TouristSpotType.大坝镇;

            var data = TouristData.GetInstance();
            var list = data.SpotList == null ? null : data.SpotList.Where(p => p.Type == input.type);
            if (list != null && list.Count() != 0)
            {
                return new DrawingMapOutput
                {
                    MapImgUrl = data.MapImgUrl,
                    HasMore = list.Count() > 1,
                    SpotInfo = list.FirstOrDefault()
                };
            }
            return null;
        }

        /// <summary>
        /// 获取音视频详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TouristSpotList GetSpotList(DrawingMapInput input)
        {
            var data = TouristData.GetInstance();
            var list = data.SpotList == null ? null : data.SpotList.Where(p=>p.Type==input.type);
            if (list != null && list.Count() > 1)
            {
                var result = new TouristSpotList();
                result.SpotInfoList = new List<TouristSpotInfo>();
                foreach(var item in list)
                {
                    result.SpotInfoList.Add(item);
                };
                return result;
            }
            return null;
        }

        public class TouristData
        {
            private static readonly object _obj = new object();
            private static TouristData _instance;

            private TouristData(){

            }

            public static TouristData GetInstance()
            {
              
                    if (_instance == null)
                    {
                        lock (_obj)
                        {
                            if (_instance == null)
                            {
                                _instance = new TouristData
                                {
                                    MapImgUrl = "/Resource/Image",
                                    SpotList = new List<TouristSpotInfo>{
                                         new TouristSpotInfo{
                                             Id=1,
                                             Name = "九寨沟",
                                             Describe="距我10米",
                                             Type=TouristSpotType.苍龙街道
                                         },
                                         new TouristSpotInfo{
                                             Id=2,
                                             Name = "毕棚沟",
                                             Describe="距我51米",
                                             Type=TouristSpotType.苍龙街道
                                         },
                                         new TouristSpotInfo{
                                             Id=3,
                                             Name = "黄龙溪",
                                             Describe="距我129米",
                                             Type=TouristSpotType.大坝镇
                                         },
                                         new TouristSpotInfo{
                                             Id=4,
                                             Name = "熊猫基地",
                                             Describe="距我10米",
                                             Type=TouristSpotType.二合镇
                                         },
                                         new TouristSpotInfo{
                                             Id=5,
                                             Name = "花溪",
                                             Describe="距我91米",
                                             Type=TouristSpotType.高大坪乡
                                         },
                                         new TouristSpotInfo{
                                             Id=6,
                                             Name = "杜甫草堂",
                                             Describe="距我10米",
                                             Type=TouristSpotType.合马镇
                                         },
                                         new TouristSpotInfo{
                                             Id=7,
                                             Name = "锦里",
                                             Describe="距我3112米",
                                             Type=TouristSpotType.后山苗族布依族自治乡
                                         },
                                         new TouristSpotInfo{
                                             Id=8,
                                             Name = "月牙湖",
                                             Describe="距我531米",
                                             Type=TouristSpotType.火石岗乡
                                         },
                                         new TouristSpotInfo{
                                             Id=9,
                                             Name = "欢乐谷",
                                             Describe="距我10131米",
                                             Type=TouristSpotType.火石岗乡
                                         },
                                         new TouristSpotInfo{
                                             Id=10,
                                             Name = "宽窄巷子",
                                             Describe="距我710米",
                                             Type=TouristSpotType.火石岗乡
                                         },
                                         new TouristSpotInfo{
                                             Id=11,
                                             Name = "金沙遗址",
                                             Describe="距我110米",
                                             Type=TouristSpotType.九仓镇
                                         },
                                         new TouristSpotInfo{
                                             Id=12,
                                             Name = "动物园",
                                             Describe="距我10米",
                                             Type=TouristSpotType.龙井乡
                                         },
                                         new TouristSpotInfo{
                                             Id=13,
                                             Name = "科技馆",
                                             Describe="距我9米",
                                             Type=TouristSpotType.鲁班镇
                                         }


                                     }
                                };
                            }
                        }
                    }
                    return _instance;
                
            }

            public string MapImgUrl { get; set; }

            public List<TouristSpotInfo> SpotList { get; set; }
        }
    }

}

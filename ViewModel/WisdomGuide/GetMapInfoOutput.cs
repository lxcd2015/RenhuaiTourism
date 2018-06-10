using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WisdomGuide
{
    public class GetMapInfoOutput
    {
        /// <summary>
        /// 地图地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 景点列表
        /// </summary>
        public List<ViewSpotSimpleInfo> ViewSpotList { get; set; }
    }
}

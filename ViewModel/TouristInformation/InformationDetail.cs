using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristInformation
{
    public class InformationDetail
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// 距离描述
        /// </summary>
        public string DistanceDescription { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 价格描述
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Content { get; set; }
    }
}

using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristInformation
{
    public class AddOrEditTouristInformation
    {
        public int Id { get; set; }
        /// <summary>
        /// 旅游信息类型（1：酒店；2：餐饮；3：特产；4：旅行社；5：车辆服务；6：旅游购物）
        /// </summary>
        public TouristInformationType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 距离描述
        /// </summary>
        //public string Distance { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }

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

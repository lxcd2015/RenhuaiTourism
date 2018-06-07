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
        /// 旅游信息类型
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
        public string Distance { get; set; }

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

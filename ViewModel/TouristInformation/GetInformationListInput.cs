using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristInformation
{
    public class GetInformationListInput
    {
        /// <summary>
        /// 旅游信息类型（1：酒店；2：餐饮；3：特产；4：旅行社；5：车辆服务；6：旅游购物；7：酒庄）
        /// </summary>
        public TouristInformationType Type { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
    }
}

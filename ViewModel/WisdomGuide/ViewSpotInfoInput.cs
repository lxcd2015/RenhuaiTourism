using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WisdomGuide
{
    public class ViewSpotInfoInput
    {
        /// <summary>
        /// 景点名称
        /// </summary>
        public string ViewSpotName { get; set; }

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

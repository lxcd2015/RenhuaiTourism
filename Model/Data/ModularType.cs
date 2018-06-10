using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    /// <summary>
    /// 模块类型(1：酒都简介；2：智慧导览；3：旅游信息；4：精品线路；5：痛客行)
    /// </summary>
    public enum ModularType
    {
        /// <summary>
        /// 酒都简介
        /// </summary>
        Introduce=1,
        /// <summary>
        /// 智慧导览
        /// </summary>
        WisdomGuide=2,
        /// <summary>
        /// 旅游信息
        /// </summary>
        TouristInformation=3,
        /// <summary>
        /// 精品线路
        /// </summary>
        TouristRoute=4,
        /// <summary>
        /// 痛客行
        /// </summary>
        Message=5,
    }
}

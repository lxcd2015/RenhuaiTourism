using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Introduce
{
    /// <summary>
    /// 仁怀简介
    /// </summary>
    public class IntroduceInput
    {
        /// <summary>
        /// 标题（如【酒都简介】）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string BigImgUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<string> Contents { get; set; }
    }
}

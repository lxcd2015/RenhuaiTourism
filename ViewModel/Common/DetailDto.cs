using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Common
{
    public class DetailDto
    {
        /// <summary>
        /// 详情图片路径
        /// </summary>
        public string ImgUrl { get; set; }


        /// <summary>
        /// 段落内容
        /// </summary>
        public List<string> Paragraphs { get; set; }
    }
}

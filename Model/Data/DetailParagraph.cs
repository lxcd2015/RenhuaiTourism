using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    /// <summary>
    /// 详情段落表
    /// </summary>
    public class DetailParagraph
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 详情表
        /// </summary>
        public int DetailId { get; set; }
        
        /// <summary>
        /// 段落编号
        /// </summary>
        public int ParagraphIndex { get; set; }
       
        /// <summary>
        /// 段落内容
        /// </summary>
        public string ParagraphContent { get; set; }
    }
}

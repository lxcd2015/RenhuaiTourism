using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// 留言类型（投诉、建议）
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}

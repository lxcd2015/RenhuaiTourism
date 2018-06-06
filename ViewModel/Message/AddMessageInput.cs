using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;

namespace ViewModel.Message
{
    public class AddMessageInput
    {
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 留言类型（1:投诉,2:建议）
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}

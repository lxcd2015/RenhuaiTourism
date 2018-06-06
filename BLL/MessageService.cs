using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Message;

namespace BLL
{
    public class MessageService
    {
        /// <summary>
        /// 添加投诉或建议
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddMessageInput input)
        {
            var model = new Message
            {
                MessageType=input.MessageType,
                Content=input.Content,
                MessageTime=DateTime.Now
            };

            using (var db = new RTDbContext())
            {
                db.Messages.Add(model);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <returns></returns>
        public List<MessageForView> GetComplaintList()
        {
            var result = new List<MessageForView>();
            using (var db = new RTDbContext())
            {
                var list= db.Messages.Where(p => p.MessageType == MessageType.Complaint);
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        result.Add(new MessageForView
                        {
                            Content = item.Content,
                            MessageTime = item.MessageTime
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取建议列表
        /// </summary>
        /// <returns></returns>
        public List<MessageForView> GetAdviseList()
        {
            var result = new List<MessageForView>();
            using (var db = new RTDbContext())
            {
                var list = db.Messages.Where(p => p.MessageType == MessageType.Advise);
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        result.Add(new MessageForView
                        {
                            Content = item.Content,
                            MessageTime = item.MessageTime
                        });
                    }
                }
            }
            return result;
        }
    }
}

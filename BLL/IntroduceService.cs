using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Introduce;

namespace BLL
{
    public class IntroduceService
    {

        /// <summary>
        /// 编辑简介仁怀
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Edit(IntroduceInput input)
        {
            var imgPath = ResourcePath.Introduce;
            var model = new Introduce
            {
                Title = input.Title,
                ImgUrl = Path.Combine(imgPath,input.ImgUrl),
                Content = input.Content
            };

            //try
            //{
            using (var db = new RTDbContext())
            {
                var oldModel = db.Introduces.FirstOrDefault();
                if (oldModel == null)
                {
                    db.Introduces.Add(model);
                }
                else
                {
                    oldModel.Title = model.Title;
                    oldModel.ImgUrl = model.ImgUrl;
                    oldModel.Content = model.Content;
                    db.Entry(oldModel).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            //}
            //catch(Exception e)
            //{
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 获取仁怀简介
        /// </summary>
        /// <returns></returns>
        public IntroduceInput Detail()
        {
            var result = new IntroduceInput();
            using (var db = new RTDbContext())
            {
                var data = db.Introduces.FirstOrDefault();
                if (data == null) return null;
                result.Title = data.Title;
                result.ImgUrl = data.ImgUrl;
                result.Content = data.Content;
            }
            return result;
        }
    }
}

using BLL.Common;
using Common;
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
    public class IntroduceService : ServiceBase
    {
        private readonly DetailManager _detail;
        private readonly string _imgPath;

        public IntroduceService()
        {
            _imgPath = ResourcePath.Introduce;
            _detail = new DetailManager(ModularType.Introduce);
        }

        /// <summary>
        /// 编辑简介仁怀
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Edit(IntroduceInput input)
        {
            var introduceId = 0;
            var model = new Introduce
            {
                Title = input.Title,
                //ImgUrl = Path.Combine(imgPath,input.ImgUrl),
                //Content = input.Content
            };

            //try
            //{
            using (var db = new RTDbContext())
            {
                var oldModel = db.Introduces.FirstOrDefault();
                if (oldModel == null)
                {
                    db.Introduces.Add(model);
                    db.SaveChanges();
                    introduceId = model.Id;
                }
                else
                {
                    oldModel.Title = model.Title;
                    //oldModel.ImgUrl = model.ImgUrl;
                    //oldModel.Content = model.Content;
                    db.Entry(oldModel).State = EntityState.Modified;
                    db.SaveChanges();
                    introduceId = oldModel.Id;
                }
                
                _detail.AddOrEdit(new AddOrEditDetailInput
                {
                    ProjectId = introduceId,
                    ImgUrl = PathCombine(_imgPath, input.BigImgUrl),
                    Paragraphs = input.Contents
                }, db);

                db.SaveChanges();
            }
            //}
            //catch(Exception e)
            //{
            //    return false;
            //}
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
                if (data == null)
                    throw new RTException("数据不存在，请联系管理员");
                result.Title = data.Title;
                //result.ImgUrl = data.ImgUrl;
                //result.Content = data.Content;

                var detail = _detail.GetDetail(new GetDetailInput
                {
                    ProjectId = data.Id
                }, db);
                if (detail == null) return result;

                result.BigImgUrl = detail.ImgUrl;
                result.Contents = detail.Paragraphs;
            }
            return result;
        }
    }
}

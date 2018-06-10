using BLL.Common;
using Common;
using Model;
using Model.Data;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using ViewModel.Common;
using ViewModel.HomePage;

namespace BLL
{
    public class HomePageService:ServiceBase
    {
        public void Edit(HomePageInput input)
        {
            var imgPath = ResourcePath.HomePage;
            var model = new HomePage
            {
                FirstImgUrl = PathCombine(imgPath,input.FirstImgUrl),
                ThirdImgUrl = PathCombine(imgPath, input.ThirdImgUrl)
            };
            if (input.SecondImgUrlList != null && input.SecondImgUrlList.Count != 0)
            {
                input.SecondImgUrlList= input.SecondImgUrlList.Select(p => PathCombine(imgPath, p)).ToList();
                model.SecondImgUrl = string.Join("\n", input.SecondImgUrlList);
            }
            //try
            //{
            using (var db = new RTDbContext())
            {
                var oldModel = db.HomePages.FirstOrDefault();
                if (oldModel == null)
                {
                    db.HomePages.Add(model);
                }
                else
                {
                    oldModel.FirstImgUrl = model.FirstImgUrl;
                    oldModel.SecondImgUrl = model.SecondImgUrl;
                    oldModel.ThirdImgUrl = model.ThirdImgUrl;
                    db.Entry(oldModel).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            //}
            //catch(Exception e)
            //{
            //    return false;
            //}
        }

        public HomePageInput Detail()
        {

            var result = new HomePageInput();
            using (var db = new RTDbContext())
            {
                var data = db.HomePages.FirstOrDefault();
                if (data == null)
                    throw new RTException("数据不存在，请联系管理员");
                result.FirstImgUrl = data.FirstImgUrl;
                result.SecondImgUrlList = data.SecondImgUrl.Split('\n').ToList();
                result.ThirdImgUrl = data.ThirdImgUrl;
            }
            return result;
        }
    }
}

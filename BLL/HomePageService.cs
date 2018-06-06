using Model;
using Model.Data;
using System;
using System.Data.Entity;
using System.Linq;
using ViewModel.HomePage;

namespace BLL
{
    public class HomePageService
    {
        public bool Edit(HomePageInput input)
        {
            var model = new HomePage
            {
                FirstImgUrl = input.FirstImgUrl,
                ThirdImgUrl = input.ThirdImgUrl
            };
            if (input.SecondImgUrlList != null && input.SecondImgUrlList.Count != 0)
            {
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
            return true;
        }

        public HomePageInput Detail()
        {
            var result = new HomePageInput();
            using (var db = new RTDbContext())
            {
                var data = db.HomePages.FirstOrDefault();
                if (data == null) return null;
                result.FirstImgUrl = data.FirstImgUrl;
                result.SecondImgUrlList = data.SecondImgUrl.Split('\n').ToList();
                result.ThirdImgUrl = data.ThirdImgUrl;
            }
            return result;
        }
    }
}

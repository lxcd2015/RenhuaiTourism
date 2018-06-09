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
using ViewModel.TouristInformation;

namespace BLL
{
    public class TouristInformationService
    {
        private string imgPath = ResourcePath.TouristInformation;

        /// <summary>
        /// 添加旅游信息
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddOrEditTouristInformation input)
        {
            using (var db = new RTDbContext())
            {
                var information = new TouristInformation
                {
                    //Distance = input.Distance,
                    ImgUrl =Path.Combine(imgPath,input.ImgUrl),
                    Name = input.Name,
                    Phone = input.Phone,
                    Price = input.Price,
                    Type = input.Type
                };
                db.TouristInformations.Add(information);
                db.SaveChanges();
                if (information.Type == TouristInformationType.Hotel)
                {
                    var informationId = information.Id;
                    var detail = new TouristInformationDetail
                    {
                        InformationId = informationId,
                        Content = input.Content
                    };
                    db.TouristInformationDetails.Add(detail);
                    db.SaveChanges();
                }

            }
        }

        /// <summary>
        /// 编辑旅游信息
        /// </summary>
        /// <param name="input"></param>
        public void Edit(AddOrEditTouristInformation input)
        {
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault(p => p.Id == input.Id);
                if (information == null)
                    return;
                information.ImgUrl = Path.Combine(imgPath,input.ImgUrl);
                information.Name = input.Name;
                information.Phone = input.Phone;
                information.Price = input.Price;
                //information.Distance = input.Distance;
                information.Type = input.Type;
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                if (information.Type == TouristInformationType.Hotel)
                {
                    var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == information.Id);
                    if (detail == null)
                    {
                        db.TouristInformationDetails.Add(new TouristInformationDetail
                        {
                            InformationId = information.Id,
                            Content = input.Content
                        });
                    }
                    else
                    {
                        detail.Content = input.Content;
                        db.Entry(detail).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
        }

        public void Delete(RTEntity<int> input)
        {
            using (var db = new RTDbContext())
            {
               var information= db.TouristInformations.FirstOrDefault();
                if (information == null) return;
                var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == information.Id);
                db.TouristInformationDetails.Remove(detail);
                db.TouristInformations.Remove(information);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取旅游信息用于编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AddOrEditTouristInformation GetInformationForEdit(RTEntity<int> input)
        {
            var result = new AddOrEditTouristInformation();
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault(p => p.Id == input.Parameter);
                if (information == null) return null;
                //result.Distance = information.Distance;
                result.Id = information.Id;
                result.ImgUrl = information.ImgUrl;
                result.Name = information.Name;
                result.Phone = information.Phone;
                result.Price = information.Price;
                result.Type = information.Type;
                if (result.Type == TouristInformationType.Hotel)
                {
                    var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == information.Id);
                    if (detail == null) return result;
                    result.Content = detail.Content;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取旅游信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<InformationForView> GetList(RTEntity<TouristInformationType> input)
        {
            var result = new List<InformationForView>();
            using (var db = new RTDbContext())
            {
                var list = db.TouristInformations.Where(p => p.Type == input.Parameter);
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        result.Add(new InformationForView
                        {
                            Id = item.Id,
                            //Distance = item.Distance,
                            ImgUrl = item.ImgUrl,
                            Name = item.Name,
                            Phone = item.Phone,
                            Price = item.Price,
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取酒店详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InformationDetail GetDetail(RTEntity<int> input)
        {
            var result = new InformationDetail();
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault(p => p.Id == input.Parameter);
                if (information == null) return null;
                if (information.Type != TouristInformationType.Hotel) return null;
                result.ImgUrl = information.ImgUrl;
                //result.Distance = information.Distance;
                result.Name = information.Name;
                result.Phone = information.Phone;
                result.Price = information.Price;
                var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == input.Parameter);
                if (detail == null) return null;
                result.Content = detail.Content;
            }
            return result;
        }
    }
}

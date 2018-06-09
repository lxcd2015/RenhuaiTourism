using BLL.Common;
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
                    Position=input.Position,
                    Latitude=input.Latitude,
                    Longitude=input.Longitude,
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
                information.Position = input.Position;
                information.Longitude = input.Longitude;
                information.Latitude = input.Latitude;
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
                result.Position = information.Position;
                result.Longitude = information.Longitude;
                result.Latitude = information.Latitude;
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
        public List<InformationForView> GetList(GetInformationListInput input)
        {
            var result = new List<InformationForView>();
            using (var db = new RTDbContext())
            {
                var list = db.TouristInformations.Where(p => p.Type == input.Type);
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        double distance = 0;
                        string distanceDescription = "";
                        if (item.Type == TouristInformationType.Hotel)
                        {
                            LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, item.Longitude, item.Latitude);
                            if (distance > 1000)
                            {
                                distanceDescription = string.Format("距我{0}千米", (distance / 1000).ToString("f2"));
                            }
                            else
                            {
                                distanceDescription = string.Format("距我{0}米", distance.ToString("f0"));
                            }
                        }

                        result.Add(new InformationForView
                        {
                            Id = item.Id,
                            //Distance = item.Distance,
                            ImgUrl = item.ImgUrl,
                            Name = item.Name,
                            Phone = item.Phone,
                            Price = item.Price,
                            Distance = distance,
                            DistanceDescription = distanceDescription,
                            Position = item.Position,
                        });
                    }
                    result = result.OrderBy(p=>p.Distance).ToList();
                }
                
            }
            return result;
        }

        /// <summary>
        /// 获取酒店详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InformationDetail GetDetail(GetInformationDetail input)
        {
            var result = new InformationDetail();
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault(p => p.Id == input.Id);
                if (information == null) return null;
                if (information.Type != TouristInformationType.Hotel) return null;

                double distance = 0;
                string distanceDescription = "";
                if (information.Type == TouristInformationType.Hotel)
                {
                    distance = LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, information.Longitude, information.Latitude);
                    if (distance > 1000)
                    {
                        distanceDescription = string.Format("距我{0}千米", (distance / 1000).ToString("f2"));
                    }
                    else
                    {
                        distanceDescription = string.Format("距我{0}米", distance.ToString("f0"));
                    }
                }
                result.ImgUrl = information.ImgUrl;
                //result.Distance = information.Distance;
                result.Position = information.Position;
                result.Distance = distance;
                result.DistanceDescription = distanceDescription;
                result.Name = information.Name;
                result.Phone = information.Phone;
                result.Price = information.Price;

                var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == input.Id);
                if (detail == null) return null;
                result.Content = detail.Content;
            }
            return result;
        }

    }
}

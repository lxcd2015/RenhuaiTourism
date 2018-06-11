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
using ViewModel.TouristInformation;

namespace BLL
{
    public class TouristInformationService : ServiceBase
    {
        private readonly string _imgPath;
        private readonly DetailManager _detail;

        public TouristInformationService()
        {
            _imgPath = ResourcePath.TouristInformation;
            _detail = new DetailManager(ModularType.TouristInformation);
        }

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
                    ImgUrl =HttpPathCombine(_imgPath, input.SmallImgUrl),
                    Position = input.Position,
                    Latitude = input.Latitude,
                    Longitude = input.Longitude,
                    Name = input.Name,
                    Phone = input.Phone,
                    Price = input.Price,
                    Type = input.Type
                };
                db.TouristInformations.Add(information);
                db.SaveChanges();
                if (information.Type == TouristInformationType.Hotel|| information.Type == TouristInformationType.Winery)
                {
                    //var informationId = information.Id;
                    //var detail = new TouristInformationDetail
                    //{
                    //    InformationId = informationId,
                    //    Content = input.Content
                    //};
                    //db.TouristInformationDetails.Add(detail);
                    _detail.AddOrEdit(new AddOrEditDetailInput
                    {
                        Classify = (int)information.Type,
                        ProjectId = information.Id,
                        ImgUrl = HttpPathCombine(_imgPath, input.BigImgUrl),
                        Paragraphs = input.Contents
                    }, db);
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
                    throw new RTException("所选信息不存在");
                information.ImgUrl = HttpPathCombine(_imgPath, input.SmallImgUrl);
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
                if (information.Type == TouristInformationType.Hotel|| information.Type == TouristInformationType.Winery)
                {
                    _detail.AddOrEdit(new AddOrEditDetailInput
                    {
                        Classify = (int)information.Type,
                        ProjectId = information.Id,
                        ImgUrl = HttpPathCombine(_imgPath, input.BigImgUrl),
                        Paragraphs = input.Contents
                    }, db);
                    db.SaveChanges();
                }
            }
        }

        public void Delete(RTEntity<int> input)
        {
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault();
                if (information == null)
                    throw new RTException("所删数据不存在");
                //var detail = db.TouristInformationDetails.FirstOrDefault(p => p.InformationId == information.Id);
                //db.TouristInformationDetails.Remove(detail);

                //删除详情
                _detail.Delete(new GetDetailInput
                {
                    Classify = (int)information.Type,
                    ProjectId = information.Id
                }, db);

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
                if (information == null)
                    throw new RTException("所选数据不存在");
                //result.Distance = information.Distance;
                result.Id = information.Id;
                result.SmallImgUrl = information.ImgUrl;
                result.Position = information.Position;
                result.Longitude = information.Longitude;
                result.Latitude = information.Latitude;
                result.Name = information.Name;
                result.Phone = information.Phone;
                result.Price = information.Price;
                result.Type = information.Type;
                if (result.Type == TouristInformationType.Hotel|| information.Type == TouristInformationType.Winery)
                {
                    var detail = _detail.GetDetail(new GetDetailInput
                    {
                        Classify = (int)result.Type,
                        ProjectId = information.Id
                    }, db);
                    if (detail == null) return result;

                    result.BigImgUrl = detail.ImgUrl;
                    result.Contents = detail.Paragraphs;
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
                        if (item.Type == TouristInformationType.Hotel|| item.Type == TouristInformationType.Winery)
                        {
                            distance = LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, item.Longitude, item.Latitude);
                            if (distance > 1000)
                            {
                                distanceDescription = string.Format("距我{0}km", (distance / 1000).ToString("f2"));
                            }
                            else
                            {
                                distanceDescription = string.Format("距我{0}m", distance.ToString("f0"));
                            }
                        }

                        result.Add(new InformationForView
                        {
                            Id = item.Id,
                            //Distance = item.Distance,
                            SmallImgUrl = item.ImgUrl,
                            Name = item.Name,
                            Phone = item.Phone,
                            Price = item.Price,
                            Distance = distance,
                            DistanceDescription = distanceDescription,
                            Position = item.Position,
                        });
                    }
                    if (input.Type == TouristInformationType.Hotel)
                    {
                        result = result.OrderBy(p => p.Distance).ToList();
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
        public InformationDetail GetDetail(GetInformationDetail input)
        {
            var result = new InformationDetail();
            using (var db = new RTDbContext())
            {
                var information = db.TouristInformations.FirstOrDefault(p => p.Id == input.Id);
                if (information == null)
                    throw new RTException("所选数据不存在");
                if (information.Type != TouristInformationType.Hotel&& information.Type != TouristInformationType.Winery)
                    throw new RTException("酒店数据，才有详细信息");

                double distance = 0;
                string distanceDescription = "";
                if (information.Type == TouristInformationType.Hotel|| information.Type == TouristInformationType.Winery)
                {
                    distance = LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, information.Longitude, information.Latitude);
                    if (distance > 1000)
                    {
                        distanceDescription = string.Format("距我{0}km", (distance / 1000).ToString("f2"));
                    }
                    else
                    {
                        distanceDescription = string.Format("距我{0}m", distance.ToString("f0"));
                    }
                }
                //result.Distance = information.Distance;
                result.Position = information.Position;
                result.Distance = distance;
                result.DistanceDescription = distanceDescription;
                result.Name = information.Name;
                result.Phone = information.Phone;
                result.Price = information.Price;

                var detail = _detail.GetDetail(new GetDetailInput
                {
                    Classify = (int)information.Type,
                    ProjectId = information.Id
                }, db);
                if (detail == null) return result;

                result.BigImgUrl = detail.ImgUrl;
                result.Contents = detail.Paragraphs;

            }
            return result;
        }

    }
}

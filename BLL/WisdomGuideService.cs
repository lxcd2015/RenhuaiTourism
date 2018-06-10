using BLL.Common;
using Model;
using Model.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using ViewModel;
using ViewModel.Common;
using ViewModel.WisdomGuide;

namespace BLL
{
    public class WisdomGuideService : ServiceBase
    {
        private readonly string _resourcePath;

       private readonly DetailManager _detail;

       public WisdomGuideService()
       {
            _resourcePath = ResourcePath.WisdomGuide;
           _detail = new DetailManager(ModularType.WisdomGuide);
       }

        /// <summary>
        /// 获取智慧导览Id
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private int GetWisdomGuideId(RTDbContext db)
        {
            var wisdomGuide = db.WisdomGuides.FirstOrDefault();
            if (wisdomGuide != null)
            {
                return wisdomGuide.Id;
            }
            db.WisdomGuides.Add(new WisdomGuide());
            db.SaveChanges();
            return db.WisdomGuides.FirstOrDefault().Id;
        }

        /// <summary>
        /// 修改地图地址
        /// </summary>
        public void ChangeMap(ChangeMapInput input)
        {
            if (input.ImgUrl == null || input.ImgUrl == "") return;
            input.ImgUrl = PathCombine(_resourcePath, input.ImgUrl);
            //using (TransactionScope tran = new TransactionScope())
            //{
                using (var db = new RTDbContext())
                {
                    int wisdomGuideId = GetWisdomGuideId(db);

                    var map = db.WisdomGuideMaps.FirstOrDefault();
                    if (map != null)
                    {
                        map.WisdomGuideId = wisdomGuideId;
                        map.ImgUrl =input.ImgUrl;
                        db.Entry(map).State = EntityState.Modified;
                    }
                    else
                    {
                        db.WisdomGuideMaps.Add(new WisdomGuideMap
                        {
                            WisdomGuideId = wisdomGuideId,
                            ImgUrl = input.ImgUrl
                        });
                    }
                    db.SaveChanges();
                }
            //    tran.Complete();
            //}
        }

        /// <summary>
        /// 添加景点
        /// </summary>
        public void AddViewSpot(AddOrEditViewSpotDto input)
        {
            //using (TransactionScope tran = new TransactionScope())
            //{
                using (var db = new RTDbContext())
                {
                    int wisdomGuideId = GetWisdomGuideId(db);
                    var model = new WisdomGuideViewSpot
                    {
                        //Content = input.Content,
                        ImgUrl = PathCombine(_resourcePath, input.SmallImgUrl),
                        //ViewSpotDescribe = input.ViewSpotDescribe,
                        Position=input.Position,
                        Phone=input.Phone,
                        Longitude =input.Longitude,
                        Latitude=input.Latitude,
                        ViewSpotName = input.ViewSpotName,
                        WisdomGuideId = wisdomGuideId
                    };
                    db.WisdomGuideViewSpots.Add(model);
                    db.SaveChanges();


                    _detail.AddOrEdit(new AddOrEditDetailInput
                    {
                        ProjectId = model.Id,
                        ImgUrl = PathCombine(_resourcePath, input.BigImgUrl),
                        Paragraphs = input.Contents
                    }, db);

                    int ViewSpotId = model.Id;
                    if (input.VideoList != null && input.VideoList.Count != 0)
                    {
                        input.VideoList.ForEach(item =>
                        {
                            db.WisdomGuideViewSpotVideos.Add(new WisdomGuideViewSpotVideo
                            {
                                ImgUrl =PathCombine(_resourcePath, item.ImgUrl),
                                VideoName = item.VideoName,
                                VideoUrl = PathCombine(_resourcePath, item.VideoUrl),
                                WisdomGuideViewSpotId = ViewSpotId
                            });
                        });
                    }
                    db.SaveChanges();
                }
                //tran.Complete();
            //}
        }

        /// <summary>
        /// 编辑景点
        /// </summary>
        public void EditViewSpot(AddOrEditViewSpotDto input)
        {
            //using (TransactionScope tran = new TransactionScope())
            //{
            using (var db = new RTDbContext())
            {
                int wisdomGuideId = GetWisdomGuideId(db);
                var model = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Id);
                if (model == null) return;

                model.ImgUrl = PathCombine(_resourcePath, input.SmallImgUrl);
                //model.ViewSpotDescribe = input.ViewSpotDescribe;
                model.Position = input.Position;
                model.Phone = input.Phone;
                model.Longitude = input.Longitude;
                model.Latitude = input.Latitude;
                model.ViewSpotName = input.ViewSpotName;
                model.WisdomGuideId = wisdomGuideId;
                db.Entry(model).State = EntityState.Modified;

                int ViewSpotId = model.Id;

                _detail.AddOrEdit(new AddOrEditDetailInput
                {
                    ProjectId = model.Id,
                    ImgUrl = PathCombine(_resourcePath, input.BigImgUrl),
                    Paragraphs = input.Contents
                }, db);

                var list = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == ViewSpotId).ToList();
                if (list != null && list.Count() != 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        db.WisdomGuideViewSpotVideos.Remove(list[i]);
                    }
                }

                if (input.VideoList != null && input.VideoList.Count != 0)
                {
                    input.VideoList.ForEach(item =>
                    {
                        db.WisdomGuideViewSpotVideos.Add(new WisdomGuideViewSpotVideo
                        {
                            ImgUrl = PathCombine(_resourcePath, item.ImgUrl),
                            VideoName = item.VideoName,
                            VideoUrl = PathCombine(_resourcePath, item.VideoUrl),
                            WisdomGuideViewSpotId = ViewSpotId
                        });
                    });
                }

                db.SaveChanges();
            }
            //    tran.Complete();
            //}
        }

        /// <summary>
        /// 获取用于编辑景点的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AddOrEditViewSpotDto GetViewSpotForEdit(RTEntity<int> input)
        {
            if (input == null) return null;
            var result = new AddOrEditViewSpotDto();
            using (var db = new RTDbContext())
            {
                var viewSpot = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewSpot == null) return null;
                result.Id = viewSpot.Id;
                result.SmallImgUrl = viewSpot.ImgUrl;
                //result.Content = viewSpot.Content;
                //result.ImgUrl = viewSpot.ImgUrl;
                //result.ViewSpotDescribe = viewSpot.ViewSpotDescribe;
                result.Position = viewSpot.Position;
                result.Phone = viewSpot.Phone;
                result.Longitude = viewSpot.Longitude;
                result.Latitude = viewSpot.Latitude;
                result.ViewSpotName = viewSpot.ViewSpotName;
                result.WisdomGuideId = viewSpot.WisdomGuideId;

                var detail = _detail.GetDetail(new GetDetailInput
                {
                    ProjectId = viewSpot.Id
                }, db);
                if (detail == null) return result;

                result.BigImgUrl = detail.ImgUrl;
                result.Contents = detail.Paragraphs;

                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewSpot.Id).ToList();
                if (videoList != null && videoList.Count != 0)
                {
                    result.VideoList = new List<ViewSpotVideoDto>();
                    videoList.ForEach(item =>
                    {
                        result.VideoList.Add(new ViewSpotVideoDto
                        {
                            ImgUrl = item.ImgUrl,
                            VideoName = item.VideoName,
                            VideoUrl = item.VideoUrl,
                            WisdomGuideViewSpotId = item.WisdomGuideViewSpotId
                        });
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 获取地图景点简介信息
        /// </summary>
        public ViewSpotInfoOutput ViewSpotInfo(ViewSpotInfoInput input)
        {
            if (input == null) return null;
            var result = new ViewSpotInfoOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Id);
                if (viewPort == null) return null;

                #region 根据经纬度计算距离
                double distance = 0;
                string distanceDescription = "";

                distance = LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, viewPort.Longitude, viewPort.Latitude);
                if (distance > 1000)
                {
                    distanceDescription = string.Format("距我{0}千米", (distance / 1000).ToString("f2"));
                }
                else
                {
                    distanceDescription = string.Format("距我{0}米", distance.ToString("f0"));
                }
                #endregion

                result.ViewSpotName = viewPort.ViewSpotName;
                //result.ViewSpotDescribe = viewPort.ViewSpotDescribe;
                result.Position = viewPort.Position;
                result.Phone = viewPort.Phone;
                result.Distance = distance;
                result.ImgUrl = viewPort.ImgUrl;
                result.DistanceDescription = distanceDescription;

                var map = db.WisdomGuideMaps.FirstOrDefault(p => p.WisdomGuideId == viewPort.WisdomGuideId);
                if (map != null) result.MapImgUrl = map.ImgUrl;

                result.HasMoreView = false;
                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id).ToList();
                if (videoList != null && videoList.Count != 0)
                {
                    var video = videoList.FirstOrDefault();
                    result.FirstVideo = new ViewSpotVideoDto();
                    result.FirstVideo.ImgUrl = video.ImgUrl;
                    result.FirstVideo.VideoUrl = video.VideoUrl;
                    result.FirstVideo.VideoName = video.VideoName;
                    if (videoList.Count > 1) result.HasMoreView = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取景点详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ViewSpotDetailOutput ViewSpotDetail(RTEntity<int> input)
        {
            if (input == null) return null;
            var result = new ViewSpotDetailOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewPort == null) return null;

                var detail = _detail.GetDetail(new GetDetailInput
                {
                    ProjectId = viewPort.Id
                }, db);
                if (detail == null) return result;

                result.BigImgUrl = detail.ImgUrl;
                result.Contents = detail.Paragraphs;
            }
            return result;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        public List<ViewSpotVideoDto> GetVideoList(RTEntity<int> input)
        {
            if (input == null) return null;
            var result = new List<ViewSpotVideoDto>();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewPort == null) return result;

                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id).ToList();
                if (videoList != null && videoList.Count != 0)
                {
                    videoList.ForEach(item =>
                    {
                        result.Add(new ViewSpotVideoDto
                        {
                            ImgUrl = item.ImgUrl,
                            VideoName = item.VideoName,
                            VideoUrl = item.VideoUrl,
                        });
                    });
                }
            }
            return result;
        }

    }
}

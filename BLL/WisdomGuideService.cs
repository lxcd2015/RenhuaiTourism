using BLL.Common;
using Common;
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
            if (input.ImgUrl == null || input.ImgUrl == "")
                throw new RTException("请填写图片地址");
            input.ImgUrl = HttpPathCombine(_resourcePath, input.ImgUrl);
            //using (TransactionScope tran = new TransactionScope())
            //{
            using (var db = new RTDbContext())
            {
                int wisdomGuideId = GetWisdomGuideId(db);

                var map = db.WisdomGuideMaps.FirstOrDefault();
                if (map != null)
                {
                    map.WisdomGuideId = wisdomGuideId;
                    map.ImgUrl = input.ImgUrl;
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
                    ImgUrl = HttpPathCombine(_resourcePath, input.SmallImgUrl),
                    //ViewSpotDescribe = input.ViewSpotDescribe,
                    Position = input.Position,
                    Phone = input.Phone,
                    Longitude = input.Longitude,
                    Latitude = input.Latitude,
                    ViewSpotName = input.ViewSpotName,
                    WisdomGuideId = wisdomGuideId
                };
                db.WisdomGuideViewSpots.Add(model);
                db.SaveChanges();


                _detail.AddOrEdit(new AddOrEditDetailInput
                {
                    ProjectId = model.Id,
                    ImgUrl = HttpPathCombine(_resourcePath, input.BigImgUrl),
                    Paragraphs = input.Contents
                }, db);

                int ViewSpotId = model.Id;
                if (input.VoiceList != null && input.VoiceList.Count != 0)
                {
                    input.VoiceList.ForEach(item =>
                    {
                        db.WisdomGuideViewSpotVideos.Add(new WisdomGuideViewSpotVideo
                        {
                            ImgUrl = HttpPathCombine(_resourcePath, item.ImgUrl),
                            VoiceName = item.VoiceName,
                            VoiceUrl = HttpPathCombine(_resourcePath, item.VoiceUrl),
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
                if (model == null)
                    throw new RTException("所选数据不存在");

                model.ImgUrl = HttpPathCombine(_resourcePath, input.SmallImgUrl);
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
                    ImgUrl = HttpPathCombine(_resourcePath, input.BigImgUrl),
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

                if (input.VoiceList != null && input.VoiceList.Count != 0)
                {
                    input.VoiceList.ForEach(item =>
                    {
                        db.WisdomGuideViewSpotVideos.Add(new WisdomGuideViewSpotVideo
                        {
                            ImgUrl = HttpPathCombine(_resourcePath, item.ImgUrl),
                            VoiceName = item.VoiceName,
                            VoiceUrl = HttpPathCombine(_resourcePath, item.VoiceUrl),
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
            if (input == null)
                throw new RTException("输入参数不能为空");
            var result = new AddOrEditViewSpotDto();
            using (var db = new RTDbContext())
            {
                var viewSpot = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewSpot == null)
                    throw new RTException("所选数据不存在");
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
                    result.VoiceList = new List<ViewSpotVideoDto>();
                    videoList.ForEach(item =>
                    {
                        result.VoiceList.Add(new ViewSpotVideoDto
                        {
                            ImgUrl = item.ImgUrl,
                            VoiceName = item.VoiceName,
                            VoiceUrl = item.VoiceUrl,
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
            if (input == null)
                throw new RTException("输入参数不能为空");
            var result = new ViewSpotInfoOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Id);
                if (viewPort == null)
                    throw new RTException("所选数据不存在");

                #region 根据经纬度计算距离
                double distance = 0;
                string distanceDescription = "";

                distance = LongitudeAndLatitudeToDistance.GetDistance(input.Longitude, input.Latitude, viewPort.Longitude, viewPort.Latitude);
                if (distance > 1000)
                {
                    distanceDescription = string.Format("距我{0}km", (distance / 1000).ToString("f2"));
                }
                else
                {
                    distanceDescription = string.Format("距我{0}m", distance.ToString("f0"));
                }
                #endregion

                result.ViewSpotName = viewPort.ViewSpotName;
                //result.ViewSpotDescribe = viewPort.ViewSpotDescribe;
                result.Position = viewPort.Position;
                result.Phone = viewPort.Phone;
                result.Distance = distance;
                result.ImgUrl = viewPort.ImgUrl;
                result.Longitude = viewPort.Longitude;
                result.Latitude = viewPort.Latitude;
                result.DistanceDescription = distanceDescription;

                var map = db.WisdomGuideMaps.FirstOrDefault(p => p.WisdomGuideId == viewPort.WisdomGuideId);
                if (map != null) result.MapImgUrl = map.ImgUrl;

                #region 详情
                var detail = _detail.GetDetail(new GetDetailInput
                {
                    ProjectId = viewPort.Id
                }, db);
                if (detail == null) return result;

                result.BigImgUrl = detail.ImgUrl;
                result.Contents = detail.Paragraphs;
                #endregion

                result.HasMoreView = false;
                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id).ToList();
                if (videoList != null && videoList.Count != 0)
                {
                    var video = videoList.FirstOrDefault();
                    result.FirstVoice = new ViewSpotVideoDto();
                    result.FirstVoice.ImgUrl = video.ImgUrl;
                    result.FirstVoice.VoiceUrl = video.VoiceUrl;
                    result.FirstVoice.VoiceName = video.VoiceName;
                    result.FirstVoice.WisdomGuideViewSpotId = video.WisdomGuideViewSpotId;
                    
                    if (videoList.Count > 1)
                    {
                        result.HasMoreView = true;
                        result.VoiceList = new List<ViewSpotVideoDto>();
                        videoList.ForEach(item => {
                            result.VoiceList.Add(new ViewSpotVideoDto {
                                ImgUrl=item.ImgUrl,
                                VoiceName = item.VoiceName,
                                VoiceUrl = item.VoiceUrl,
                                WisdomGuideViewSpotId=item.WisdomGuideViewSpotId
                            });
                        });
                    }
                }
            }
            return result;
        }

        public GetMapInfoOutput GetMapInofo()
        {
            var result = new GetMapInfoOutput();
            using (var db = new RTDbContext())
            {
                var wisdom = db.WisdomGuides.FirstOrDefault();
                if (wisdom == null)
                    throw new RTException("所选数据不存在");
                var map = db.WisdomGuideMaps.FirstOrDefault(p => p.WisdomGuideId == wisdom.Id);
                if (map != null)
                {
                    result.ImgUrl = map.ImgUrl;
                }
                var spotList = db.WisdomGuideViewSpots.Where(p => p.WisdomGuideId == wisdom.Id);
                if (spotList != null && spotList.Count() != 0)
                {
                    result.ViewSpotList = new List<ViewSpotSimpleInfo>();
                    spotList.ToList().ForEach(item => {
                        result.ViewSpotList.Add(new ViewSpotSimpleInfo {
                            Id=item.Id,
                            ViewSpotName=item.ViewSpotName
                        });
                    });
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
            if (input == null)
                throw new RTException("输入参数不能为空");
            var result = new ViewSpotDetailOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewPort == null)
                    throw new RTException("所选数据不存在");

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
            if (input == null)
                throw new RTException("输入参数不能为空");
            var result = new List<ViewSpotVideoDto>();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.Id == input.Parameter);
                if (viewPort == null)
                    throw new RTException("所选数据不存在");

                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id).ToList();
                if (videoList != null && videoList.Count != 0)
                {
                    videoList.ForEach(item =>
                    {
                        result.Add(new ViewSpotVideoDto
                        {
                            ImgUrl = item.ImgUrl,
                            VoiceName = item.VoiceName,
                            VoiceUrl = item.VoiceUrl,
                        });
                    });
                }
            }
            return result;
        }

    }
}

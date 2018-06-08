using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ViewModel;
using ViewModel.Common;
using ViewModel.WisdomGuide;

namespace BLL
{
    public class WisdomGuideService
    {
        private string imgPath = ResourcePath.WisdomGuide;
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
            input.ImgUrl = Path.Combine(imgPath, input.ImgUrl);
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
                        Content = input.Content,
                        ImgUrl = Path.Combine(imgPath,input.ImgUrl),
                        ViewSpotDescribe = input.ViewSpotDescribe,
                        ViewSpotName = input.ViewSpotName,
                        WisdomGuideId = wisdomGuideId
                    };
                    db.WisdomGuideViewSpots.Add(model);
                    db.SaveChanges();
                    int ViewSpotId = model.Id;
                    if (input.VideoList != null && input.VideoList.Count != 0)
                    {
                        input.VideoList.ForEach(item =>
                        {
                            db.WisdomGuideViewSpotVideos.Add(new WisdomGuideViewSpotVideo
                            {
                                ImgUrl =Path.Combine(imgPath,item.ImgUrl),
                                VideoName = item.VideoName,
                                VideoUrl = item.VideoUrl,
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

                    model.Content = input.Content;
                    model.ImgUrl = Path.Combine(imgPath,input.ImgUrl);
                    model.ViewSpotDescribe = input.ViewSpotDescribe;
                    model.ViewSpotName = input.ViewSpotName;
                    model.WisdomGuideId = wisdomGuideId;
                    db.Entry(model).State = EntityState.Modified;

                    int ViewSpotId = model.Id;

                    var list = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == ViewSpotId)?.ToList();
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
                                ImgUrl =Path.Combine(imgPath,item.ImgUrl),
                                VideoName = item.VideoName,
                                VideoUrl = item.VideoUrl,
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
        public AddOrEditViewSpotDto GetViewSpotForEdit(RTEntity<string> input)
        {
            if (input == null) return null;
            var result = new AddOrEditViewSpotDto();
            using (var db = new RTDbContext())
            {
                var viewSpot = db.WisdomGuideViewSpots.FirstOrDefault(p => p.ViewSpotName == input.Parameter);
                if (viewSpot == null) return null;
                result.Id = viewSpot.Id;
                result.Content = viewSpot.Content;
                result.ImgUrl = viewSpot.ImgUrl;
                result.ViewSpotDescribe = viewSpot.ViewSpotDescribe;
                result.ViewSpotName = viewSpot.ViewSpotName;
                result.WisdomGuideId = viewSpot.WisdomGuideId;
                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewSpot.Id)?.ToList();
                if (videoList != null && videoList.Count != 0)
                {
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
        public ViewSpotInfoOutput ViewSpotInfo(RTEntity<string> input)
        {
            if (input == null) return null;
            var result = new ViewSpotInfoOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.ViewSpotName == input.Parameter);
                if (viewPort == null) return null;
                result.ViewSpotName = viewPort.ViewSpotName;
                result.ViewSpotDescribe = viewPort.ViewSpotDescribe;
                var map = db.WisdomGuideMaps.FirstOrDefault(p => p.WisdomGuideId == viewPort.WisdomGuideId);
                if (map != null) result.MapImgUrl = map.ImgUrl;

                result.HasMoreView = false;
                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id)?.ToList();
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
        public ViewSpotDetailOutput ViewSpotDetail(RTEntity<string> input)
        {
            if (input == null) return null;
            var result = new ViewSpotDetailOutput();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.ViewSpotName == input.Parameter);
                if (viewPort == null) return null;
                result.Content = viewPort.Content;
                result.ImgUrl = viewPort.ImgUrl;
            }
            return result;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        public List<ViewSpotVideoDto> GetVideoList(RTEntity<string> input)
        {
            if (input == null) return null;
            var result = new List<ViewSpotVideoDto>();
            using (var db = new RTDbContext())
            {
                var viewPort = db.WisdomGuideViewSpots.FirstOrDefault(p => p.ViewSpotName == input.Parameter);
                if (viewPort == null) return result;

                var videoList = db.WisdomGuideViewSpotVideos.Where(p => p.WisdomGuideViewSpotId == viewPort.Id)?.ToList();
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

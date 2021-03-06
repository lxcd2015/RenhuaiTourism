﻿using BLL.Common;
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
using ViewModel.TouristRoute;

namespace BLL
{
    public class TouristRouteService : ServiceBase
    {
        private readonly string _imgPath;
        private readonly DetailManager _detail;

        public TouristRouteService()
        {
            _imgPath = ResourcePath.TouristRoute;
            _detail = new DetailManager(ModularType.TouristRoute);
        }

        /// <summary>
        /// 添加旅游线路信息
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddOrEditTouristRouteInput input)
        {
            var route = new TouristRoute
            {
                NeedDays=input.NeedDays,
                RouteName=input.RouteName,
                ImgUrl = HttpPathCombine(_imgPath, input.ImgUrl)
            };

            using (var db = new RTDbContext())
            {
                db.TouristRoutes.Add(route);
                db.SaveChanges();

                _detail.AddOrEdit(new AddOrEditDetailInput
                {
                    ProjectId = route.Id,
                    ImgUrl = HttpPathCombine(_imgPath, input.ImgUrl),
                    Paragraphs = input.Contents
                }, db);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑旅游线路信息
        /// </summary>
        /// <param name="input"></param>
        public void Edit(AddOrEditTouristRouteInput input)
        {
            using (var db = new RTDbContext())
            {
                var routes = db.TouristRoutes.FirstOrDefault(p => p.Id == input.Id);
                if (routes == null)
                    throw new RTException("所选数据不存在");
                routes.ImgUrl = HttpPathCombine(_imgPath, input.ImgUrl);
                routes.NeedDays = input.NeedDays;
                routes.RouteName = input.RouteName;
                //routes.Content = input.Content;
                db.Entry(routes).State = EntityState.Modified;

                _detail.AddOrEdit(new AddOrEditDetailInput
                {
                    ProjectId = routes.Id,
                    ImgUrl = HttpPathCombine(_imgPath, input.ImgUrl),
                    Paragraphs = input.Contents
                }, db);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取旅游线路信息列表
        /// </summary>
        /// <returns></returns>
        public List<TouristRouteForView> GetList()
        {
            var result = new List<TouristRouteForView>();
            using (var db = new RTDbContext())
            {
                var list = db.TouristRoutes.Where(p => true);
                if (list != null && list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        result.Add(new TouristRouteForView
                        {
                            Id = item.Id,
                            NeedDays = item.NeedDays,
                            RouteName = item.RouteName,
                            ImgUrl = item.ImgUrl
                        });
                    }
                    result = result.OrderBy(p => p.NeedDays).ToList();
                }
            }
            return result;
        }

        /// <summary>
        /// 根据Id删除旅游线路
        /// </summary>
        /// <param name="input"></param>
        public void Delete(RTEntity<int> input)
        {
            using (var db = new RTDbContext())
            {
                var route = db.TouristRoutes.FirstOrDefault(p => p.Id == input.Parameter);
                if (route == null)
                    throw new RTException("所删数据不存在");
                db.TouristRoutes.Remove(route);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据Id获取单个旅游线路信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TouristRouteForView GetTouristRoute(RTEntity<int> input)
        {
            var result = new TouristRouteForView();
            using (var db = new RTDbContext())
            {
                var route = db.TouristRoutes.FirstOrDefault(p => p.Id == input.Parameter);
                if (route == null)
                    throw new RTException("所选数据不存在");
                result.Id = route.Id;
                result.ImgUrl = route.ImgUrl;
                result.NeedDays = route.NeedDays;
                result.RouteName = route.RouteName;

                var detail = _detail.GetDetail(new GetDetailInput
                {
                    ProjectId = route.Id
                }, db);
                if (detail == null) return result;
                result.Contents = detail.Paragraphs;
            }
            return result;
        }
    }
}

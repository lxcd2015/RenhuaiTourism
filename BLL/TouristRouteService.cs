﻿using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.TouristRoute;

namespace BLL
{
    public class TouristRouteService
    {
        /// <summary>
        /// 添加旅游线路信息
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddOrEditTouristRouteInput input)
        {
            var route = new TouristRoute
            {
                Content = input.Content,
                ImgUrl = input.ImgUrl
            };

            using (var db = new RTDbContext())
            {
                db.TouristRoutes.Add(route);     
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
                var routes= db.TouristRoutes.FirstOrDefault(p=>p.Id==input.Id);
                if (routes != null)
                {
                    routes.ImgUrl = input.ImgUrl;
                    routes.Content = input.Content;
                    db.Entry(routes).State = EntityState.Modified;
                    db.SaveChanges();
                }
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
                        result.Add(new TouristRouteForView {
                            Id = item.Id,
                            Content=item.Content,
                            ImgUrl=item.ImgUrl
                        });
                    }
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
                var route = db.TouristRoutes.FirstOrDefault(p => p.Id == input.Id);
                if (route != null)
                {
                    db.TouristRoutes.Remove(route);
                }
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
                var route = db.TouristRoutes.FirstOrDefault(p=>p.Id==input.Id);
                if (route != null)
                {
                    result.Id = route.Id;
                    result.ImgUrl = route.ImgUrl;
                    result.Content = route.Content;
                }
            }
            return result;
        }
    }
}

using Common;
using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;

namespace BLL.Common
{
    public class DetailManager
    {
        private readonly ModularType _modularType;

        public DetailManager(ModularType modularType)
        {
            _modularType = modularType;
        }

        /// <summary>
        /// 添加或编辑详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="db"></param>
        public void AddOrEdit(AddOrEditDetailInput input, RTDbContext db)
        {
            var detail = db.Details.FirstOrDefault(p => p.ModularType == _modularType && p.Classify == input.Classify && p.ProjectId == input.ProjectId);
            var detailId = 0;
            if (detail == null)
            {
                var model = new Detail
                {
                    ModularType = _modularType,
                    Classify = input.Classify,
                    ProjectId = input.ProjectId,
                    ImgUrl = input.ImgUrl
                };
                db.Details.Add(model);
                db.SaveChanges();
                detailId = model.Id;
            }
            else
            {
                detail.ModularType = _modularType;
                detail.Classify = input.Classify;
                detail.ProjectId = input.ProjectId;
                detail.ImgUrl = input.ImgUrl;
                db.Entry(detail).State = EntityState.Modified;
                detailId = detail.Id;
            }

            //删除原有文章信息
            var list = db.DetailParagraphs.Where(p => p.DetailId == detailId).ToList();
            if (list != null && list.Count() != 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    db.DetailParagraphs.Remove(list[i]);
                }
            }
            if (input.Paragraphs != null && input.Paragraphs.Count != 0)
            {
                int i = 0;
                input.Paragraphs.ForEach(item =>
                {
                    i += 1;
                    db.DetailParagraphs.Add(new DetailParagraph
                    {
                        DetailId = detailId,
                        ParagraphIndex = i,
                        ParagraphContent = item
                    });
                });
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 删除详情
        /// </summary>
        /// <param name="input"></param>
        /// <param name="db"></param>
        public void Delete(GetDetailInput input, RTDbContext db)
        {
            var detail = db.Details.FirstOrDefault(p => p.ModularType == _modularType && p.Classify == input.Classify && p.ProjectId == input.ProjectId);
            if (detail == null)
                throw new RTException("所选数据不存在");
            var list = db.DetailParagraphs.Where(p => p.DetailId == detail.Id).ToList();
            if (list != null && list.Count() != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.DetailParagraphs.Remove(list[i]);
                }
            }
            db.Details.Remove(detail);
            //删除多余的段落
            DeleteSurplusParagraphs(db);
        }

        /// <summary>
        /// 删除多余的段落
        /// </summary>
        private void DeleteSurplusParagraphs(RTDbContext db)
        {
            var deleteParagraphs = new List<DetailParagraph>();
            var detailIds= db.Details.Select(p => p.Id);
            if (detailIds == null || detailIds.Count() == 0)
            {
                deleteParagraphs = db.DetailParagraphs.ToList();
            }
            else
            {
                deleteParagraphs = db.DetailParagraphs.Where(p=>!detailIds.Contains(p.DetailId)).ToList();
            }
            if (deleteParagraphs != null && deleteParagraphs.Count != 0)
            {
                for (int i = 0; i < deleteParagraphs.Count; i++)
                {
                    db.DetailParagraphs.Remove(deleteParagraphs[i]);
                }
            }
        }

        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public DetailDto GetDetail(GetDetailInput input, RTDbContext db)
        {
            var result = new DetailDto();
            var detail = db.Details.FirstOrDefault(p => p.ModularType == _modularType && p.Classify == input.Classify && p.ProjectId == input.ProjectId);
            if (detail == null) return null;
            result.ImgUrl = detail.ImgUrl;
            var paragraphs = db.DetailParagraphs.Where(p => p.DetailId == detail.Id).OrderBy(p => p.ParagraphIndex).Select(p => p.ParagraphContent);
            if (paragraphs != null && paragraphs.Count() != 0)
            {
                result.Paragraphs = paragraphs.ToList();
            }
            return result;
        }
    }
}

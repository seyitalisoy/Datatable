using Business.Abstract;
using Business.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LinkManager : ILinkService
    {
        private readonly ILinkDal _linkDal;

        public LinkManager(ILinkDal linkDal)
        {
            _linkDal = linkDal;
        }

        [ValidationAspect(typeof(LinkValidator))]
        public IResult Add(Link link)
        {
            var newLink = link;
            newLink.CreatedAt = DateTime.Now;
            _linkDal.Add(newLink);
            return new SuccessResult("Link başarıyla eklendi.");
        }

        public IResult Delete(Link link)
        {
            _linkDal.Delete(link);
            return new SuccessResult("Link silindi!");
        }

        public IDataResult<Link> Get(int id)
        {
            var result = _linkDal.Get(l=>l.Id==id);
            if (result!=null)
            {
                return new SuccessDataResult<Link>(result);
            }
            return new ErrorDataResult<Link>("Bu Id'ye ait link bulunmamaktadır.");
        }

        public IDataResult<List<Link>> Getall()
        {
            var result = _linkDal.GetAll();
            if (result.Any())
            {
                return new SuccessDataResult<List<Link>>(result);
            }
            return new ErrorDataResult<List<Link>>("Link bulunmamaktadır.");
        }

        public IDataResult<SpecifiedLinksDto> GetSpecifiedLinks(PageDto pageDto)
        {
            var linksQuery = Getall().Data.AsQueryable();

            if (!string.IsNullOrEmpty(pageDto.filterText))
            {
                string keyword = pageDto.filterText.ToLower();

                linksQuery = linksQuery.Where(i =>
                    (i.Url ?? string.Empty).ToLower().Contains(keyword) ||
                    (i.Name ?? string.Empty).ToLower().Contains(keyword) ||
                    (i.Description ?? string.Empty).ToLower().Contains(keyword)
                );
            }

            var linkCount = linksQuery.Count();

            var pagedLinks = linksQuery
                .OrderBy(p => p.Id)
                .Skip((pageDto.PageNumber - 1) * pageDto.PageSize)
                .Take(pageDto.PageSize)
                .ToList();

            var result = new SpecifiedLinksDto
            {
                Links = pagedLinks,
                LinkCount = linkCount
            };

            return new SuccessDataResult<SpecifiedLinksDto>(result);
        }


        [ValidationAspect(typeof(LinkValidator))]
        public IResult Update(Link link)
        {
            var newLink = link;
            newLink.UpdatedAt = DateTime.Now;
            _linkDal.Update(link);
            return new SuccessResult("Link başarıyla güncellendi.");
        }
    }
}

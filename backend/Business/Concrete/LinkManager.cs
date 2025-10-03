using Business.Abstract;
using Business.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
            _linkDal.Add(link);
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

        [ValidationAspect(typeof(LinkValidator))]
        public IResult Update(Link link)
        {
            _linkDal.Update(link);
            return new SuccessResult("Link güncellendi.");
        }
    }
}

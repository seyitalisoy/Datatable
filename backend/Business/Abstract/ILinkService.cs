using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILinkService
    {
        IDataResult<List<Link>> Getall();

        IDataResult<SpecifiedLinksDto> GetSpecifiedLinks(PageDto pageDto);

        IDataResult<Link> Get(int id);

        IResult Add(Link link);
        IResult Delete(Link link);
        IResult Update(Link link);
    }
}

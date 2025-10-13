using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ValidationErrorDetails : ErrorDetails
    {
        //fluent validation return type -> IEnumerable<ValidationFailure>
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}

using JWTAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Common.ServiceBase
{
    public interface IServiceBase<T>
    where T : BaseEntity
    {
        ValidationResult DoSubmit(T entity);
        ValidationResult SubmitValidation(T entity);
    }
}

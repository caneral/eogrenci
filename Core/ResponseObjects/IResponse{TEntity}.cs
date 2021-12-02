using System;
using System.Collections.Generic;

namespace Core.ResponseObjects
{
    public interface IResponse<TEntity> : IResponse
    {
        TEntity Data { get; set; }
        List<CustomValidationError> ValidationErrors { get; set; }


    }
}

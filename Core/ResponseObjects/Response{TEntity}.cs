using System;
using System.Collections.Generic;

namespace Core.ResponseObjects
{
    public class Response<TEntity> : Response, IResponse<TEntity>
    {
        public TEntity Data { get; set; }


        public Response(ResponseType responseType, TEntity data) : base(responseType)
        {
            Data = data;
        }

        public List<CustomValidationError> ValidationErrors { get; set; }

        public Response(ResponseType responseType, TEntity data, List<CustomValidationError> errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }

        public Response(ResponseType responseType, string message) : base(responseType, message)
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Common.CommonObjects
{
    public class ApiResponse<T> : ApiResponse, IApiResponse<T>
    {
        public T Data { get; set; }


        public ApiResponse(Status status, string resultMessage) : base(status, resultMessage)
        {

        }

        public ApiResponse(Status status, T data) : base(status)
        {
            Data = data;
        }


        public ApiResponse(T data, string resultMessage, string errorCode): base(Status.Failed, errorCode)
        {
            Data = data;
            ErrorCode = errorCode;
        }
    }
}

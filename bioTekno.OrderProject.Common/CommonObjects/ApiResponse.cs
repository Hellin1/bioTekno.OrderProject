using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Common.CommonObjects
{
    public class ApiResponse :  IApiResponse
    {
        // Status(Success, Failed enum) , ResultMessage,ErrorCode,Data(GenericType)


        public ApiResponse(Status status)
        {
            Status = status;
        }


        public ApiResponse(Status status, string resultMessage)
        {
            Status = status;
            ResultMessage = resultMessage;
        }

        public ApiResponse(Status status, string resultMessage, string errorCode)
        {
            Status = status;
            ResultMessage = resultMessage;
            ErrorCode = errorCode;
        }

        public string ResultMessage { get; set; }

        public Status Status { get; set; }

        public string ErrorCode { get; set; }
    }

    public enum Status
    {
        Success,
        Failed
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Common.CommonObjects
{
    public interface IApiResponse<T> : IApiResponse
    {
        T Data { get; set; }

        //List<CustomValidationErrors> ValidationErrors { get; set; }
    }
}

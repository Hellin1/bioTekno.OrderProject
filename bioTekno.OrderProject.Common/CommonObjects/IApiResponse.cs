using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Common.CommonObjects
{
    public interface IApiResponse
    {
        string ResultMessage { get; set; }

        Status Status { get; set; }
    }
}

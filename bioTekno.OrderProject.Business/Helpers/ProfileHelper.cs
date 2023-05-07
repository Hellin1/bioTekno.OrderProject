using AutoMapper;
using bioTekno.OrderProject.Business.Mappings.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>
            {
                new ProductProfile(),
                new OrderDetailProfile(),
            };
        }
    }
}

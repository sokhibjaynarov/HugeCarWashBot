using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeCarWashBot.Domain.Entities.Configurations
{
    public class AppConfigurations
    {
        public static string Position { get; } = "AppConfigurations";
        public string ApiHostDomain { get; set; }
    }
}

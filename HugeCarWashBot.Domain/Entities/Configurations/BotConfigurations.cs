using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeCarWashBot.Domain.Entities.Configurations
{
    public class BotConfigurations
    {
        public static string Position { get; } = "BotConfigurations";
        public string AuthToken { get; set; }
    }
}

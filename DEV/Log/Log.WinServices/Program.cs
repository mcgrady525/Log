using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Tracy.Frameworks.Common.Helpers;

namespace Log.WinServices
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                //加上时间戳
                var timeStamp = Guid.NewGuid().ToString("N");
                var environment = ConfigHelper.GetAppSetting("Environment");
                config.SetServiceName(string.Format("LogWinServices_{0}_{1}", environment, timeStamp));
                config.SetDisplayName(string.Format("LogWinServices_{0}_{1}", environment, timeStamp));
                config.SetDescription(string.Format("Log系统Windows服务_{0}_{1}", environment, timeStamp));

                config.Service<MainService>(ser =>
                {
                    ser.ConstructUsing(name => new MainService());
                    ser.WhenStarted((service, control) => service.Start());
                    ser.WhenStopped((service, control) => service.Stop());
                });
            });
        }
    }
}

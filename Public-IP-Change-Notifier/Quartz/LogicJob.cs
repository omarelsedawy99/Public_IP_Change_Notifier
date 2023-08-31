using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_IP_Change_Notifier.Quartz
{
    [DisallowConcurrentExecution]
    public class LogicJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                new Logic().Process();
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync($"An error happen when executing notifier => {ex}");
            }
        }
    }
}

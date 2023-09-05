using Quartz;
using CouponManagementServiceV2.Core.Business.Interfaces;
namespace CouponManagementServiceV2.Core.Schedules
{
    [DisallowConcurrentExecution]
    public class CheckExpire : IJob
    {
        private readonly ICommandService _commandService;

        public CheckExpire(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            /*
            _commandService.CheckCouponRequest();
            */
            Console.Write(DateTime.Now.ToString());
            return Task.FromResult(true);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace SwapIt.Mapper.Models.Infrastructure
{
    public class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TReturn RunSync<TReturn>(Func<Task<TReturn>> task)
        {
            return _taskFactory.StartNew(task).Unwrap().GetAwaiter().GetResult();
        }
    }
}
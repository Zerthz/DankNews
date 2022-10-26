using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace Functions
{
    [DurableTask(nameof(SayHelloActivity))]
    public class SayHelloActivity : TaskActivityBase<string, string>
    {
        readonly ILogger? logger;
        public SayHelloActivity(ILoggerFactory? loggerFactory)
        {
            this.logger = loggerFactory?.CreateLogger<SayHelloActivity>();
        }
        protected override string OnRun(TaskActivityContext context, string? name)
        {
            logger?.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}
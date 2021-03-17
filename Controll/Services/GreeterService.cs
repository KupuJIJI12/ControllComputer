using System.IO;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Controll.Services
{
    public class ControllService : Controll.ControllBase
    {
        private readonly ILogger<ControllService> _logger;
        public ControllService(ILogger<ControllService> logger)
        {
            _logger = logger;
        }

        public override Task<Empty> SetCommand(CommandRequest request, ServerCallContext context)
        {
            var task = new RunTask(new FileInfo(request.Path));
            if(request.Command == Commands.Open) task.OpenFile();
            else task.CloseFile();
            return Task.FromResult(new Empty());
        }
    }
}

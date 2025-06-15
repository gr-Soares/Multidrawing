using Grpc.Core;


namespace MultidrawingService
{
    public class Service: DrawGreeter.DrawGreeterBase
    {
        private static List<Draw> draws = new List<Draw>();

        public override Task<SendDrawResponse> SendDraw(SendDrawRequest request, ServerCallContext context)
        {
            draws.Add(request.DrawPath);

            var response = new SendDrawResponse();
            return Task.FromResult(response);
        }

        public override Task<ReceiveDrawResponse> ReceiveDraw(ReceiveDrawRequest request, ServerCallContext context)
        {
            var response = new ReceiveDrawResponse();
            response.DrawPath.AddRange(draws);
            return Task.FromResult(response);
        }

        public override Task<RemoveDrawResponse> RemoveDraw(RemoveDrawRequest request, ServerCallContext context)
        {
            
            for(int i = 0; i < draws.Count; i++)
            {
                if (draws[i].Id == request.Id)
                {
                    draws.RemoveAt(i);
                    break;
                }
            }

            var response = new RemoveDrawResponse();
            return Task.FromResult(response);
        }
    }
}

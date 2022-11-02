using Discount.Grpc.Protos;
using Grpc.Net.Client;
using MassTransit.Futures.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient
            _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient 
            discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);

            //// This switch must be set before creating the GrpcChannel/HttpClient.
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            //// The port number(5000) must match the port of the gRPC server.
            //var channel = GrpcChannel.ForAddress("http://localhost:8003");
            //var client = new DiscountProtoService.DiscountProtoServiceClient(channel);
            //return await _discountProtoService.GetDiscountAsync(discountRequest);
        }

    }
}

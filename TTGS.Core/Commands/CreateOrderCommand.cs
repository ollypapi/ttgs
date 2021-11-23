using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Entity;
using TTGS.Shared.Enums;
using TTGS.Shared.Request;

namespace TTGS.Core.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public CreateOrderRequest Order { get; set; }
        public bool Commit { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Commodity = request.Order.Commodity,
                OwnerId = request.Order.OwnerId,
                Quantity = request.Order.Quantity,
                Exporter = request.Order.Exporter,
                Importer = request.Order.Importer,
                NumberOfTruck = request.Order.NumberOfTruck,
                PickupPoints = request.Order.PickupPoints,
                DropOffPoints = request.Order.DropOffPoints,
                TransitBorders = request.Order.TransitBorders,
                ImportBorder = request.Order.ImportBorder,
                DateCreated = DateTime.Now,
                OrderStatus = OrderStatusEnum.InProgress.ToString()
            };

            order.Addresses.Add(new OrderAddress
            {
                Address = request.Order.ExportAgentDetails.Address,
                EmailAddress = request.Order.ExportAgentDetails.EmailAddress,
                ContactPerson = request.Order.ExportAgentDetails.ContactPerson,
                PhoneNumbers = request.Order.ExportAgentDetails.PhoneNumbers,
                AddressType = OrderAddressTypeEnum.ExportAgentAddress.ToString(),
            });

            order.Addresses.Add(new OrderAddress
            {
                Address = request.Order.ImportAgentDetails.Address,
                EmailAddress = request.Order.ImportAgentDetails.EmailAddress,
                ContactPerson = request.Order.ImportAgentDetails.ContactPerson,
                PhoneNumbers = request.Order.ImportAgentDetails.PhoneNumbers,
                AddressType = OrderAddressTypeEnum.ImportAgentAddress.ToString(),
            });

            await _unitOfWork.Orders.InsertAsync(order);

            if(request.Commit) await _unitOfWork.SaveAsync();

            return true;
        }
    }
}

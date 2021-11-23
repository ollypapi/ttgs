using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Shared.Request;

namespace TTGS.Core.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public UpdateOrderStatusRequest UpdateOrderStatus { get; set; }
        public bool Commit { get; set; }
    }
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly ITTGSUnitOfWork _unitOfWork;
        public UpdateOrderStatusCommandHandler(ITTGSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.Orders.AsQueryable().FirstOrDefault(x => x.Id == request.UpdateOrderStatus.Id);
            if (order != null)
            {
                order.OrderStatus = request.UpdateOrderStatus.OrderStatus.ToString();
            }
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveAsync();
            return true;
            
        }
    }
}

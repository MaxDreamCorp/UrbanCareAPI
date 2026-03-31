using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UrbanCare.Application.DTOs.Responses;

namespace UrbanCare.Application.Features.OrderOperations.Queries
{
    public record GetOrderStatusesQuery() : IRequest<List<OrderStatusResponseDTO>>;
}

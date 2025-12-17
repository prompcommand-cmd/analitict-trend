using AnaliticTrend.Application.Abstracts.UseCases.Production.Trend;
using AnaliticTrend.Application.Models.Dto;
using AnaliticTrend.Application.Models.Production.Trend.Request;
using AnaliticTrend.Application.Utilities;
using Application.Abstracts.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnaliticTrend.Application.UseCase.Production.Trend.OrderTrend
{
    public class OrderTrendUseCase : IOrderTrendUseCase
    {
        private readonly IAppDbRepository<OrderRecord> _orderRecordRepository;

        public OrderTrendUseCase(IAppDbRepository<OrderRecord> orderRecordRepository)
        {
            _orderRecordRepository = orderRecordRepository;
        }

        public async Task<List<OrderRecordDto>> GetListOrderTrend(OrderTrendRequest request)
        {
            return await _orderRecordRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(request.orderDateYear),
                        w => string.Compare(w.OrderDate, request.orderDateYear + "-01-01") >= 0 &&
                            string.Compare(w.OrderDate, (int.Parse(request.orderDateYear) + 1) + "-01-01") < 0)
                .Select(orderRecord => new OrderRecordDto
                {
                    Id = orderRecord.Id,
                    RowId = orderRecord.RowId,
                    OrderId = orderRecord.OrderId,
                    OrderDate = DateTimeOffset.Parse(orderRecord.OrderDate),
                    ShipMode = orderRecord.ShipMode,
                    CustomerId = orderRecord.CustomerId,
                    CustomerName = orderRecord.CustomerName,
                    Segment = orderRecord.Segment,
                    City = orderRecord.City,
                    State = orderRecord.State,
                    Country = orderRecord.Country,
                    PostalCode = orderRecord.PostalCode,
                    Market = orderRecord.Market,
                    Region = orderRecord.Region,
                    ProductId = orderRecord.ProductId,
                    Category = orderRecord.Category,
                    SubCategory = orderRecord.SubCategory,
                    ProductName = orderRecord.ProductName,
                    Sales = orderRecord.Sales,
                    Quantity = orderRecord.Quantity,
                    Discount = orderRecord.Discount,
                    Profit = orderRecord.Profit,
                    ShippingCost = orderRecord.ShippingCost,
                    OrderPriority = orderRecord.OrderPriority
                }).ToListAsync();
        }
    }
}

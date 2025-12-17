using AnaliticTrend.Application.Models.Dto;
using AnaliticTrend.Application.Models.Production.Trend.Request;
using Application.Abstracts.UseCases;

namespace AnaliticTrend.Application.Abstracts.UseCases.Production.Trend
{
    public interface IOrderTrendUseCase : IGenericUseCase
    {
        Task<List<OrderRecordDto>> GetListOrderTrend(OrderTrendRequest request);
    }
}

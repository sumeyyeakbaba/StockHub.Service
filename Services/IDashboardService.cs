using StockHub.Services.DTO;

namespace StockHub.Services.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardStatisticsAsync();
    }
}
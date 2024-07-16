using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JSSATSProject.Service.Service.BackgroundService;

public class PromotionStatusUpdateService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<PromotionStatusUpdateService> _logger;

    public PromotionStatusUpdateService(IServiceScopeFactory scopeFactory, ILogger<PromotionStatusUpdateService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var promotionService = scope.ServiceProvider.GetRequiredService<IPromotionService>();
                    await promotionService.UpdatePromotionStatusesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating promotion statuses.");
            }

            //delay for a day
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}

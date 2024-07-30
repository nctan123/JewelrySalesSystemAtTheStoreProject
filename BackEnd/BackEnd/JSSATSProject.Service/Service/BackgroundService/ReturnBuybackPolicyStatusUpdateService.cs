using JSSATSProject.Service.Service.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JSSATSProject.Service.Service.BackgroundService;

public class ReturnBuybackPolicyStatusUpdateService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ReturnBuybackPolicyStatusUpdateService> _logger;

    public ReturnBuybackPolicyStatusUpdateService(IServiceScopeFactory scopeFactory, ILogger<ReturnBuybackPolicyStatusUpdateService> logger)
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
                    var returnBuyBackPolicyService = scope.ServiceProvider.GetRequiredService<IReturnBuyBackPolicyService>();
                    await returnBuyBackPolicyService.UpdateStatusesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating return buyback policy statuses.");
            }

            //delay for a day
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}
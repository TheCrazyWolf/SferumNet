using Microsoft.EntityFrameworkCore;
using SferumNet.Database;
using SferumNet.DbModels.Common;
using SferumNet.DbModels.Scenarios;
using SferumNet.DbModels.Vk;
using SferumNet.Scenarios;
using SferumNet.Services.Common;
using WelcomeJob = SferumNet.Scenarios.WelcomeJob;

namespace SferumNet.Services;

public class ScenarioConfigurator : IScenarioConfigurator
{
    public DateTime? DateTimeStarted { get; set; }
    private CancellationTokenSource _cancelTokenSource = new();
    private readonly IServiceScopeFactory _scopeFactory;

    public ScenarioConfigurator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _ = RunAsync();
    }

    public async Task RunAsync()
    {
        _cancelTokenSource = new();

        using var scope = _scopeFactory.CreateScope();
        var ef = scope.ServiceProvider.GetRequiredService<SferumNetContext>();

        var profiles = await GetProfilesAsync(ef);

        if (profiles is null)
            return;

        DateTimeStarted = DateTime.Now;

        foreach (var profile in profiles)
        {
            var scenarios = await GetJobsByProfileAsync(ef, profile.Id);

            if (scenarios is null)
                continue;

            foreach (var scenario in scenarios)
            {
                if (scenario is WelcomeJob)
                    await Task.Run(() =>
                        new WelcomeJob(scope.ServiceProvider.GetRequiredService<SferumNetContext>(),
                                scope.ServiceProvider.GetRequiredService<DbLogger>(), scenario.Id)
                            .ExecuteAsync(_cancelTokenSource.Token));
                
                if (scenario is FactJob)
                    await Task.Run(() =>
                        new FactsJob(scope.ServiceProvider.GetRequiredService<SferumNetContext>(),
                                scope.ServiceProvider.GetRequiredService<DbLogger>(), scenario.Id)
                            .ExecuteAsync(_cancelTokenSource.Token));
                
                if (scenario is ScheduleJob)
                    await Task.Run(() =>
                        new SchedulesJob(scope.ServiceProvider.GetRequiredService<SferumNetContext>(),
                                scope.ServiceProvider.GetRequiredService<DbLogger>(), scenario.Id)
                            .ExecuteAsync(_cancelTokenSource.Token));
                
                
                // ETC ...
            }
        }
    }

    public async Task StopAsync()
    {
        DateTimeStarted = null;

        await _cancelTokenSource.CancelAsync();
        _cancelTokenSource.Dispose();
    }

    public async Task RestartAsync()
    {
        await StopAsync();
        await RunAsync();
    }

    private async Task<ICollection<VkProfile>?> GetProfilesAsync(SferumNetContext ef)
    {
        return await ef.VkProfiles
            .ToListAsync(cancellationToken: _cancelTokenSource.Token);
    }

    private async Task<ICollection<Job>?> GetJobsByProfileAsync(SferumNetContext ef, long idProfile)
    {
        return await ef.Scenarios
            .Where(sc => sc.IdProfile == idProfile)
            .Where(sc => sc.IsActive)
            .ToListAsync(cancellationToken: _cancelTokenSource.Token);
    }
}
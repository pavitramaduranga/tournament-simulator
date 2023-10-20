using TournamentBracketGenerator.Application.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TournamentBracketGenerator.Application.Interfaces;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.AddSingleton<IApplication, Application>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ISingleEliminationStageService, SingleEliminationStageService>();
        services.AddScoped<IGroupStageService, GroupStageService>();
    })
    .Build();

var app = _host.Services.GetRequiredService<IApplication>();
app.Main();

using NBomber.Contracts;
using NBomber.CSharp;

namespace WIS.PerformanceTests.Scenarios;

public static class LoadSimulationContext
{
    public static LoadSimulation Default =>
        Simulation.Inject(
            rate: 10,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30));
    
    public static LoadSimulation Rate50 =>
        Simulation.Inject(
            rate: 50,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30));
    
    public static LoadSimulation Rate100 =>
        Simulation.Inject(
            rate: 100,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30));
}
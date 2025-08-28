using API.Interview.Models;
using API.Interview.Models.Common.Constants;
using API.Interview.Models.Common.Mapping;

namespace API.Interview.Services;

public interface IInterviewRepository
{
    Task<Client?> GetClientByIdentifier(string identifier);
    Task<TargetSystem?> GetTargetSystem(TargetSystemType type);
    Task<bool> IsClientEnabledForTargetSystem(Guid clientId, Guid targetSystemId);
    Task<bool> IsActivityEnabledForClientTargetSystem(Guid clientId, Guid targetSystemId, ActivityType activityType);
}

public class InMemoryInterviewRepository : IInterviewRepository
{
    private readonly List<Client> _clients =
    [
        new() { ID = new Guid("92197389-2915-444a-8339-530581cd1234"), EIPIdentifier = "ClientA", DisplayText = "Client A", IsActive = true },
        new() { ID = new Guid("92197389-2915-444a-8339-530581cd5678"), EIPIdentifier = "ClientB", DisplayText = "Client B", IsActive = false }
    ];

    private readonly List<TargetSystem> _targetSystems =
    [
        new() { ID = new Guid("92197389-2915-444a-8339-530581cd1d33"), DisplayText = "Meridianlink Core", EIPIdentifier = "Meridianlink" }
    ];

    private readonly HashSet<(Guid clientId, Guid targetSystemId)> _clientTargetSystems =
    [
        (new Guid("92197389-2915-444a-8339-530581cd1234"), new Guid("92197389-2915-444a-8339-530581cd1d33")),
        (new Guid("92197389-2915-444a-8339-530581cd5678"), new Guid("92197389-2915-444a-8339-530581cd1d33")),
    ];

    private readonly HashSet<(Guid clientId, Guid targetSystemId, ActivityType activity)> _clientActivities =
    [
        (new Guid("92197389-2915-444a-8339-530581cd1234"), new Guid("92197389-2915-444a-8339-530581cd1d33"), ActivityType.BookLoan),
        (new Guid("92197389-2915-444a-8339-530581cd5678"), new Guid("92197389-2915-444a-8339-530581cd1d33"), ActivityType.BookLoan)

    ];

    public Task<Client?> GetClientByIdentifier(string identifier)
        => Task.FromResult(_clients.FirstOrDefault(x => x.EIPIdentifier.Equals(identifier, StringComparison.OrdinalIgnoreCase)));

    public Task<TargetSystem?> GetTargetSystem(TargetSystemType type)
        => Task.FromResult(_targetSystems.FirstOrDefault(x => x.EIPIdentifier == type.ToString()));

    public Task<bool> IsClientEnabledForTargetSystem(Guid clientId, Guid targetSystemId)
        => Task.FromResult(_clientTargetSystems.Contains((clientId, targetSystemId)));

    public Task<bool> IsActivityEnabledForClientTargetSystem(Guid clientId, Guid targetSystemId, ActivityType activityType)
        => Task.FromResult(_clientActivities.Contains((clientId, targetSystemId, activityType)));
}

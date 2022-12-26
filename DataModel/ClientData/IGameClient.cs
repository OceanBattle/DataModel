using OceanBattle.DataModel.DTOs;

namespace OceanBattle.DataModel.ClientData
{
    public interface IGameClient
    {
        Task UpdateActiveUsersAsync(IEnumerable<UserDto> users);
        Task InviteAsync(UserDto sender);
        Task FinishDeploymentAsync();
        Task StartDeploymentAsync();
        Task EndGameAsync();
        Task StartGameAsync();
        Task GotHitAsync();
    }
}

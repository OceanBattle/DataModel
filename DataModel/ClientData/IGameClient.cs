using OceanBattle.DataModel.DTOs;

namespace OceanBattle.DataModel.ClientData
{
    public interface IGameClient
    {
        Task UpdateActiveUsersAsync(IEnumerable<UserDto> users);
        Task InviteAsync(UserDto sender);
        Task FinishDeploymentAsync();
        Task StartDeploymentAsync(LevelDto level);
        Task EndGameAsync();
        Task StartGameAsync(BattlefieldDto oponentBattlefield);
        Task GotHitAsync(BattlefieldDto battlefield, int x, int y);
    }
}

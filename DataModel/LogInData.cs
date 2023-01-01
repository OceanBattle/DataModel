using OceanBattle.DataModel;

namespace OceanBattle.Client.DataStore
{
    public class LogInData : BaseModel
    {
        public string? BearerToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

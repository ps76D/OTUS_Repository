using PlayerProfileSystem;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        void SaveGame(PlayerProfile playerProfile, IGameRepository gameRepository);
        void LoadGame(PlayerProfile playerProfile, IGameRepository gameRepository);
    }
}
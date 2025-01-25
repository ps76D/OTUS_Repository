using PlayerProfileSystem;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        void SaveGame(PlayerProfile playerProfile);
        void LoadGame(PlayerProfile playerProfile);
    }
}
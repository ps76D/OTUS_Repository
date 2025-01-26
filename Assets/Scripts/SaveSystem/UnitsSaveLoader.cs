using PlayerProfileSystem;
using UnityEngine;

namespace SaveSystem
{
    public class UnitsSaveLoader : MonoBehaviour, ISaveLoader
    {

        public void SaveGame(PlayerProfile playerProfile, IGameRepository gameRepository)
        { 
            Debug.Log("Game Saved");
        }

        public void LoadGame(PlayerProfile playerProfile, IGameRepository gameRepository)
        {
            Debug.Log("Game Loaded");
        }
    }
}

using System;
using PlayerProfileSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;


namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private PlayerProfile _playerProfile;
        
        [ShowInInspector]
        private ISaveLoader[] _saveLoaders;

        [Inject]
        public void Construct(PlayerProfile playerProfile, ISaveLoader[] saveLoaders)
        {
            _saveLoaders = saveLoaders;
            _playerProfile = playerProfile;
            
            _playerProfile.Initialize();
        }

        public void SaveGame(PlayerProfile playerProfile)
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(playerProfile);
            }
        }

        public void LoadGame(PlayerProfile playerProfile)
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame(playerProfile);
            }
        }
    }
}
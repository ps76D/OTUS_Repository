using PlayerProfileSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;


namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        private PlayerProfile _playerProfile;
        
        private GameRepository _gameRepository;
        
        [ShowInInspector]
        private ISaveLoader[] _saveLoaders;

        [Inject]
        public void Construct(PlayerProfile playerProfile, GameRepository gameRepository, ISaveLoader[] saveLoaders)
        {
            _saveLoaders = saveLoaders;
            _playerProfile = playerProfile;
            _gameRepository = gameRepository;
            
            _playerProfile.Initialize();
        }

        public void SaveGame()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(_playerProfile, _gameRepository);
            }
            
            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();
            
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame(_playerProfile, _gameRepository);
            }
        }
    }
}
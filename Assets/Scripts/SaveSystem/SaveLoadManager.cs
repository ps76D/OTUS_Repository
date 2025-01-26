﻿using System;
using System.Collections.Generic;
using PlayerProfileSystem;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        private PlayerProfile _playerProfile;
        
        private GameRepository _gameRepository;
        
        private ISaveLoader[] _saveLoaders;
        
        private readonly List<object> _services = new ();

        [Inject]
        public void Construct(PlayerProfile playerProfile, GameRepository gameRepository, ISaveLoader[] saveLoaders)
        {
            _saveLoaders = saveLoaders;
            _playerProfile = playerProfile;
            _gameRepository = gameRepository;
            
            _playerProfile.Initialize();
            
            _services.Add(playerProfile.ResourceService);
            _services.Add(playerProfile.UnitManager);
        }

        public void SaveGame()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(this, _gameRepository);
            }
            
            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();
            
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame(this, _gameRepository);
            }
        }
        
        public T GetService<T>()
        {
            for (int i = 0, count = _services.Count; i < count; i++)
            {
                if (_services[i] is T result)
                {
                    return result;
                }
            }
            
            throw new Exception($"Service {typeof(T).Name} is not found!");
        }
    }
}
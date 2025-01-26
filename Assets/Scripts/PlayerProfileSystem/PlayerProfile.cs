using System;
using System.Collections.Generic;
using GameEngine;
using UnityEngine;
using Zenject;

namespace PlayerProfileSystem
{
    [Serializable]
    public class PlayerProfile
    {
        [Inject]
        [SerializeField] private ResourceService _resourceService;
        
        [Inject]
        [SerializeField] private UnitManager _unitManager;
        
        [Inject]
        private IEnumerable<Resource> _resources;
        
        [Inject]
        private IEnumerable<Unit> _units;
        
        public ResourceService ResourceService => _resourceService;
        public UnitManager UnitManager => _unitManager;
        
        private readonly List<object> _services = new ();
        
        public void Initialize()
        {
            _resourceService.SetResources(_resources);
            _unitManager.SetupUnits(_units);
            
            _services.Add(_resourceService);
            _services.Add(_unitManager);
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
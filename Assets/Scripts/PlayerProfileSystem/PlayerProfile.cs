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

        public void Initialize()
        {
            _resourceService.SetResources(_resources);
            _unitManager.SetupUnits(_units);
        }
    }
}
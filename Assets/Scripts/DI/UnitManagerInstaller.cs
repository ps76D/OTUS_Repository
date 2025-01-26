using GameEngine;
using UnityEngine;
using Zenject;

namespace DI
{
    public class UnitManagerInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _unitsContainer;
        
        private UnitManager _unitManager;
        
        [Inject]
        public void Construct(UnitManager unitManager)
        {
            _unitManager = unitManager;
            _unitManager.SetContainer(_unitsContainer);
        }
    }
}
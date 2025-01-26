using System.Collections.Generic;
using System.Linq;
using GameEngine;
using PlayerProfileSystem;
using SaveSystem;
using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SaveLoadManager _saveLoadManager;
        [SerializeField] private UIManager _uiManager;

        private IEnumerable<Resource> _resources;
        private IEnumerable<Unit> _units;
        
        public override void InstallBindings()
        {
            _resources = FindObjectsOfType<MonoBehaviour>(true).OfType<Resource>().ToList();
            Container.Bind<IEnumerable<Resource>>().FromInstance(_resources).AsCached().NonLazy();
            Container.Bind<ResourceService>().FromNew().AsCached().NonLazy();

            _units = FindObjectsOfType<MonoBehaviour>(true).OfType<Unit>().ToList();
            Container.Bind<IEnumerable<Unit>>().FromInstance(_units).AsCached().NonLazy();
            Container.Bind<UnitManager>().FromNew().AsCached().NonLazy();

            Container.Bind<ISaveLoader>().To<ResourcesSaveLoader>().AsCached().NonLazy();

            Container.Bind<PlayerProfile>().ToSelf().AsSingle().NonLazy();

            Container.Bind<ProfileService>().ToSelf().AsSingle().NonLazy();

            Container.Bind<GameRepository>().FromNew().AsSingle().NonLazy();

            Container.Bind<SaveLoadManager>().FromInstance(_saveLoadManager).AsCached().NonLazy();
            
            Container.Bind<UIManager>().FromInstance(_uiManager).AsCached().NonLazy();
        }
    }
}

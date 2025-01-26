using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        
        [Inject]
        [SerializeField] private SaveLoadManager _saveLoadManager;

        /*[Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }*/
        
        private void Start()
        {
            _saveButton.onClick.AddListener(SaveGame);
            _loadButton.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            _saveLoadManager.LoadGame();
        }

        private void SaveGame()
        {
            _saveLoadManager.SaveGame();
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SaveGame);
            _loadButton.onClick.RemoveListener(LoadGame);
        }


    }
}
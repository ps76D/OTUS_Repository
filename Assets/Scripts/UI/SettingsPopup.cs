using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        private void Start()
        {
            _saveButton.onClick.AddListener(SaveGame);
            _loadButton.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            
        }

        private void SaveGame()
        {
            
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SaveGame);
            _loadButton.onClick.RemoveListener(LoadGame);
        }


    }
}
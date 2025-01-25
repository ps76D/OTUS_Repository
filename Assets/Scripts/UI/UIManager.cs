using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SettingsPopup _settingsPopup;
        
        public SettingsPopup SettingsPopup => _settingsPopup;
        
        
    }
}
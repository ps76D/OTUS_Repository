using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class GameRepository : IGameRepository
    {
        private const string GAME_STATE_KEY = "GameStateKey"; 
        
        private Dictionary<string, string> _gameState = new ();

        public void SetData<T>(T data)
        {
            var key = typeof(T).ToString();
            
            var jsonData = JsonConvert.SerializeObject(data);
            _gameState[key] = jsonData;
        }

        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();

            if (_gameState.TryGetValue(key, out var jsonData))
            {
                data = JsonConvert.DeserializeObject<T>(jsonData);
                return true;
            }
            
            data = default;
            return false;
        }

        public void SaveState()
        {
            var gameStateJson = JsonConvert.SerializeObject(_gameState);
            
            /*var saveName = DateTime.Now.ToString("yy.MM.dd  HH_mm");
                
            var savePath = Path.Combine(Application.persistentDataPath, saveName + ".json");
            
            //Способ сохранения
            try
            {
                File.WriteAllText(savePath, contents: gameStateJson);
                Debug.Log(message: "Successfully Saved");
            }
            catch (Exception ex)
            {
                Debug.Log(message: "Save Failed"+ex);
            }*/
            
            PlayerPrefs.SetString(GAME_STATE_KEY, gameStateJson);
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
                Debug.Log("Game State Loaded");
            }
            else
            {
                Debug.Log("No Game State Loaded");
            }
        }
    }
}
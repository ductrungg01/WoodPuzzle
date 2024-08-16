using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class LevelManager : MonoBehaviour, IGameFlow
    {
        public static LevelManager Instance { get; private set; }

        [HideInInspector] public Level currentLevel;

        private int levelIndexToLoad = 0;
        public GameObject levelParentHierachy;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }

            PlayerPrefs.DeleteAll();
        }

        public void OnFinishGame(EndGameType type) 
        {
            currentLevel.OnFinishGame(type);    
        }

        public void OnPrepareGame() 
        {
            if (currentLevel != null)
            {
                GameObject go = currentLevel.gameObject;
                go.SetActive(false);
                Destroy(go, 0.25f);
            }

            var cachedMap = Resources.Load($"Level {levelIndexToLoad}") as GameObject;
            
            if (cachedMap == null)
            {
                Debug.LogError($"Cannot find level {levelIndexToLoad}");
                return;
            }

            GameObject map = Instantiate(cachedMap);
            map.transform.SetParent(levelParentHierachy.transform);
            currentLevel = map.GetComponent<Level>();
            Debug.Log($"Load level {levelIndexToLoad} successfully");

            currentLevel.OnPrepareGame();
        }

        public void OnPauseGame() {}

        
        public void OnResumeGame() {}


        public void OnStartGame() {}

        public void GetNextLevelIndex()
        {
            levelIndexToLoad = GameBase.CurrentLevel;
            if (levelIndexToLoad > ConfigManager.GameConfig.levelIndexBeforeLoop)
            {
                levelIndexToLoad = Random.Range(1, ConfigManager.GameConfig.levelIndexBeforeLoop + 1);
            }
        }
    }
}



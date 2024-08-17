using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class ConfigManager : MonoBehaviour
    {
        public GameConfig gameConfig;

        public static GameConfig GameConfig
        {
            get => Instance.gameConfig;
        }

        public static ConfigManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }
    }
}



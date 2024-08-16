using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class GameBase
    {
        private const string CURRENT_LEVEL = "";

        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
            set => PlayerPrefs.SetInt(CURRENT_LEVEL, value);
        }
    }

    public enum GameState
    {
        PREPARE,
        PLAYING,
        PAUSING,
        FINISH
    }

    public enum EndGameType
    {
        WIN,
        LOSE,
        DRAW
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Tools/Create new game config")]
    public class GameConfig : ScriptableObject
    {
        public int levelIndexBeforeLoop = 2;
    }
}



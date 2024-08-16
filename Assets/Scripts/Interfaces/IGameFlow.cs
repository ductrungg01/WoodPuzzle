using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public interface IGameFlow
    {
        public void OnPrepareGame();
        public void OnStartGame();
        public void OnPauseGame();
        public void OnResumeGame();
        public void OnFinishGame(EndGameType type);
    }
}


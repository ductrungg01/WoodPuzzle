using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class Level : MonoBehaviour, IGameFlow
    {
        [SerializeField] private float levelTime = 60f;
        private float remainTime = 0;

        public Transform woodParentInHierachy;
        public Transform boltParentInHierachy;
        public Transform holdParentInHierachy;

        private int numberOfWood = 0;

        public void AddWood() { numberOfWood++; }
        public void RemoveWood() { 
            numberOfWood--; 
            if (numberOfWood <= 0)
            {
                GameManager.Instance.FinishGame(EndGameType.WIN);
            }
        }

        private void Start()
        {
            remainTime = levelTime;
        }

        private void Update()
        {
            UiManager.Instance.GetPopupInGame().UpdateRemainTime(remainTime);
            remainTime -= Time.deltaTime;

            if (remainTime <= 0 )
            {
                GameManager.Instance.FinishGame(EndGameType.LOSE);
            }
        }

        public void OnFinishGame(EndGameType type)
        {
            Destroy(gameObject);
        }

        public void OnPauseGame()
        {
            
        }

        public void OnPrepareGame()
        {
            Wood.ResetWoodUsedPosition();
        }

        public void OnResumeGame()
        {
            
        }

        public void OnStartGame()
        {
            
        }
    }
}



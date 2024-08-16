using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WoodPuzzle.UI
{
    public class PopupSplash : PopupBase
    {
        public Image loadingProgress;
        public CanvasGroup canvasGroup;
        public TMP_Text progressText;

        private float remainTimeToLoading = 2.0f;
        private float currentRemainLoadingTime = 0.0f;
        private bool allowCountdownTime = false;

        protected override void OnShowing()
        {
            loadingProgress.fillAmount = 0f;
            currentRemainLoadingTime = remainTimeToLoading;
            allowCountdownTime = true;
        }

        protected override void OnShown()
        {
            loadingProgress.fillAmount = 0f;
        }

        private void Update()
        {
            if (allowCountdownTime == false) return;

            currentRemainLoadingTime -= Time.deltaTime;
            currentRemainLoadingTime = Mathf.Max(0, currentRemainLoadingTime);
            loadingProgress.fillAmount = 1f - currentRemainLoadingTime / remainTimeToLoading;
            progressText.text = $"LOADING... {Mathf.FloorToInt(100f * loadingProgress.fillAmount)}%";

            if (currentRemainLoadingTime > 0) return;

            allowCountdownTime = false;
            canvasGroup.DOFade(0f, .25f).OnComplete(delegate
            {
                Hide();
            });
        }
    }
}



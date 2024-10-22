//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UI_PassTurnBanner : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        [SerializeField]
        TMP_Text text;

        [SerializeField]
        float speed = 1.0f;

        Coroutine coroutine;

        void Awake()
        {
            TurnSystem.OnPassTurn += ShowBanner;

            BoardState.OnGameOver += (x) => HideBanner();
        }

        public void ShowBanner(int index, Player player)
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(TimeBanner(player));
        }

        public void HideBanner()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            canvasGroup.alpha = 0.0f;
            text.text = "";
        }

        IEnumerator TimeBanner(Player player)
        {
            canvasGroup.alpha = 0.0f;

            text.text = "Player " + player.Name + " has the turn";

            while(canvasGroup.alpha < 1.0f)
            {
                canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }

            while (canvasGroup.alpha > 0.0f)
            {
                canvasGroup.alpha -= Time.deltaTime * speed;
                yield return null;
            }
        }
    }
}


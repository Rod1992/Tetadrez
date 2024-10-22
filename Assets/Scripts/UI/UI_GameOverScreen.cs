//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UI_GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        TMP_Text text;

        [SerializeField]
        CanvasGroup canvasGroup;

        [SerializeField]
        GameObject panel;

        [SerializeField]
        float speed = 0.25f;

        void Awake()
        {
            BoardState.OnGameOver += OnGameOver;
        }

        // Start is called before the first frame update
        void Start()
        {
            panel.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnGameOver(Player player)
        {
            panel.gameObject.SetActive(true);

            text.text = "Game won by " + player.Name;

            StartCoroutine(TimeBanner());
        }

        IEnumerator TimeBanner()
        {
            canvasGroup.alpha = 0.0f;

            while (canvasGroup.alpha < 1.0f)
            {
                canvasGroup.alpha += Time.deltaTime * speed;
                yield return null;
            }
        }

        public void PlayAgain()
        {
            if (ServiceLocator.GetGameService<Game>(out Game game))
            {
                panel.gameObject.SetActive(false);
                game.ForcePhase(EGamePhase.ChooseStartingPlayer);
            }
        }
    }
}

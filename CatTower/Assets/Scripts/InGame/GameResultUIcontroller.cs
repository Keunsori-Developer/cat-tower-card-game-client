using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace CatTower
{

    public class GameResultUIcontroller : SingletonGameObject<GameResultUIcontroller>
    {
        [SerializeField] private GameObject ResultScoreLayout;
        private GameObject ResultScoreprefab;
        public GameObject ResultPopup;
        public Button Exitbutton;
        private PlayerOrder[] rank = new PlayerOrder[6];

        void Awake()
        {
            ResultScoreprefab = Resources.Load("Ingame/ResultScore") as GameObject;
        }

        void Start()
        {
            Exitbutton.onClick.AddListener(ChangeTitle);
        }

        public void ShowGameResult(IngameResult result)
        {
            ResultPopup.SetActive(true);
            ShowPlayerList(result.player);
        }

        private void ShowPlayerList(List<PlayerOrder> players)
        {
            int N = players.Count;
            List<PlayerOrder> sortedPlayer = new List<PlayerOrder>();

            for (int l = 0; l < N; l++)
            {
                rank[l] = players[l];
            }

            for (int k = 0; k < N - 1; k++)
            {
                for (int j = k + 1; j < N; j++)
                {
                    if (players[k].score < players[j].score)
                    {
                        var temp = rank[k];
                        rank[k] = rank[j];
                        rank[j] = temp;
                    }
                }
            }
            for (int i = 0; i < N; i++)
            {
                GameObject playerObject = Instantiate(ResultScoreprefab, ResultScoreLayout.transform);
                var nickname = playerObject.transform.Find("name").GetComponent<Text>();
                nickname.text = rank[i].userInfo.nickname;
                var ranking = playerObject.transform.Find("rank").GetComponent<Text>();
                ranking.text = (i != 0 && rank[i - 1].score == rank[i].score) ? i + "등" : (i + 1) + "등";
                if (rank[i].userInfo.mid == UserData.mid) nickname.color = Color.yellow;
                var score = playerObject.transform.Find("score").GetComponent<Text>();
                score.text = rank[i].score.ToString();
            }
        }

        public void ChangeTitle()
        {
            SceneManager.LoadScene("Title");
        }
    }
}

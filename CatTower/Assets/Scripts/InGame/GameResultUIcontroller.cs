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
        public int[] rank = new int[6];

        void Awake()
        {
            ResultScoreprefab = Resources.Load("Ingame/ResultScore") as GameObject;
        }

        void Start()
        {
            Exitbutton.onClick.AddListener(ChangeTitle);
        }
        
        public void ShowPlayerList(List<PlayerOrder> players)
        {
            int N = players.Count;
            for (int l = 0; l < players.Count; l++)
            {
                rank[l] = l + 1;
            }

            for (int k = 0; k < N - 1; k++)
            {
                for (int j = k + 1; j < N; j++)
                {
                    if (players[k].score > players[j].score)
                    {
                        int temp = rank[k];
                        rank[k] = rank[j];
                        rank[j] = temp;
                    }
                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                string rankstr = Convert.ToString(rank[i]);
                GameObject playerObject = Instantiate(ResultScoreprefab, ResultScoreLayout.transform);
                var nickname = playerObject.transform.Find("name").GetComponent<Text>();
                nickname.text = players[i].userInfo.nickname;
                var ranking = playerObject.transform.Find("rank").GetComponent<Text>();
                ranking.text = rankstr;
                if (players[i].userInfo.mid == UserData.mid) nickname.color = Color.yellow;
                var score = playerObject.transform.Find("score").GetComponent<Text>();
                score.text = players[i].score.ToString();
            }
        }

        public void ShowPlayersInfo(IngamePlayerOrder response)
        {
            ShowPlayerList(response.player);
        }

        public void ChangeTitle()
        {
            SceneManager.LoadScene("Title");
        }
    }
}

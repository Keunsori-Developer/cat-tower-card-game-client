using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class CardDB : MonoBehaviour
    {
        private List<Card> cardDBs;
        public GameObject layoutGroup;
        public GameObject cardPrefab;

        public void GetCard(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                GameObject go = Instantiate(cardPrefab, layoutGroup.transform);
                if(list[i] == "A")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[0]);
                }
                else if(list[i] == "B")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[1]);
                }
                else if (list[i] == "C")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[2]);
                }
                else if (list[i] == "D")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[3]);
                }
                else if (list[i] == "E")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[4]);
                }
                else if (list[i] == "S0")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[5]);
                }
                else if (list[i] == "S1")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[6]);
                }
                else if (list[i] == "S2")
                {
                    go.GetComponent<MyCard>().SetCard(cardDBs[7]);
                }
            }
        }

        void Awake()
        {
            cardDBs = new List<Card>()
            {
                new Card(Breed.Mackerel, Resources.Load<Sprite>("Ingame/Cat/mack"), "A", false),
                new Card(Breed.Siamese, Resources.Load<Sprite>("Ingame/Cat/siam"), "B" , false),
                new Card(Breed.Persian, Resources.Load<Sprite>("Ingame/Cat/persian"), "C",  false),
                new Card(Breed.Ragdoll, Resources.Load<Sprite>("Ingame/Cat/rag"), "D", false),
                new Card(Breed.RussianBlue, Resources.Load<Sprite>("Ingame/Cat/russian"), "E", false),
                new Card(Breed.Savanna, Resources.Load<Sprite>("Ingame/Cat/savanna"), "S0", true),
                new Card(Breed.ThreeColor, Resources.Load<Sprite>("Ingame/Cat/three"), "S1", true),
                new Card(Breed.Odd, Resources.Load<Sprite>("Ingame/Cat/odd"), "S2", true)
            };
        }

        void Start()
        {
            // TODO: 서버로부터 카드 정보 받았을 때 아래 코드 진행되도록 수정해야 함
            if (cardPrefab == null) cardPrefab = Resources.Load("Ingame/MyCard") as GameObject;
            //GetCard();
        }
    }
}
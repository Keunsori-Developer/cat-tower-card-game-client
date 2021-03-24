using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class MyCardDeck : MonoBehaviour
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
                new Card(Breed.Mackerel, Resources.Load<Sprite>("Ingame/Cat/mack1"), "A", false),
                new Card(Breed.Siamese, Resources.Load<Sprite>("Ingame/Cat/siam1"), "B" , false),
                new Card(Breed.Persian, Resources.Load<Sprite>("Ingame/Cat/persian1"), "C",  false),
                new Card(Breed.Ragdoll, Resources.Load<Sprite>("Ingame/Cat/rag1"), "D", false),
                new Card(Breed.RussianBlue, Resources.Load<Sprite>("Ingame/Cat/russian1"), "E", false),
                new Card(Breed.Savanna, Resources.Load<Sprite>("Ingame/Cat/savanna1"), "S0", true),
                new Card(Breed.ThreeColor, Resources.Load<Sprite>("Ingame/Cat/three1"), "S1", true),
                new Card(Breed.Odd, Resources.Load<Sprite>("Ingame/Cat/odd1"), "S2", true)
            };
        }

        void Start()
        {
            if (cardPrefab == null) cardPrefab = Resources.Load("Ingame/MyCard") as GameObject;
        }
    }
}
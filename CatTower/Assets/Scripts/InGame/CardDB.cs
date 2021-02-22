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

        void Awake()
        {
            cardDBs = new List<Card>()
            {
                new Card(Breed.Mackerel, Resources.Load<Sprite>("Ingame/Cat/mack"), false),
                new Card(Breed.Siamese, Resources.Load<Sprite>("Ingame/Cat/siam"), false),
                new Card(Breed.Persian, Resources.Load<Sprite>("Ingame/Cat/persian"), false),
                new Card(Breed.Ragdoll, Resources.Load<Sprite>("Ingame/Cat/rag"), false),
                new Card(Breed.Savanna, Resources.Load<Sprite>("Ingame/Cat/savanna"), true),
                new Card(Breed.ThreeColor, Resources.Load<Sprite>("Ingame/Cat/three"), true),
                new Card(Breed.Odd, Resources.Load<Sprite>("Ingame/Cat/odd"), true)
            };
        }

        void Start()
        {
            // TODO: 서버로부터 카드 정보 받았을 때 아래 코드 진행되도록 수정해야 함
            if (cardPrefab == null) cardPrefab = Resources.Load("Ingame/MyCard") as GameObject;
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("!!!asdfasfs");
                GameObject go = Instantiate(cardPrefab, layoutGroup.transform);
                go.GetComponent<MyCard>().SetCard(cardDBs[Random.Range(0, 7)]);
                //go.transform.SetParent(parentObject.transform);
            }
        }
    }
}
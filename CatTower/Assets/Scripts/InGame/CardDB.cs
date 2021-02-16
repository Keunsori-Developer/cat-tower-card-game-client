using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower {
    public class CardDB : MonoBehaviour
    {
        public static CardDB instance;
        public GameObject parentObject;
        private void Awake()
        {
            instance = this;
        }
        public List<Card> cardDBs = new List<Card>();

        public GameObject MyCardPrefab;
        public Vector2[] pos;
        private void Start()
        {
            for(int i = 0; i < 5; i++)
            {
                GameObject go = Instantiate(MyCardPrefab, pos[i], Quaternion.identity);
                go.GetComponent<MyCard>().SetCard(cardDBs[Random.Range(0, 8)]);
                go.transform.SetParent(parentObject.transform);
            }
        }
    }
}
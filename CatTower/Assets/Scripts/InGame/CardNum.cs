using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower {
    public class CardNum : MonoBehaviour
    {
        public MyCard myCard;
        public GameObject gameObj;
        public void FindObj()
        {
            gameObj = GameObject.Find("MyCards");
        }

        public void IncreaseCard() //본인카드의 갯수
        {
            if(myCard.card.br == Breed.Mackerel)
            {
                gameObj.GetComponent<CheckUsable>().mackNum++;
            }
            if(myCard.card.br == Breed.Odd)
            {
                gameObj.GetComponent<CheckUsable>().oddNum++;
            }
            if (myCard.card.br == Breed.Persian)
            {
                gameObj.GetComponent<CheckUsable>().persNum++;
            }
            if (myCard.card.br == Breed.Ragdoll)
            {
                gameObj.GetComponent<CheckUsable>().ragNum++;
            }
            if (myCard.card.br == Breed.RussianBlue)
            {
                gameObj.GetComponent<CheckUsable>().russiaNum++;
            }
            if (myCard.card.br == Breed.Siamese)
            {
                gameObj.GetComponent<CheckUsable>().siamNum++;
            }
            if (myCard.card.br == Breed.Savanna)
            {
                gameObj.GetComponent<CheckUsable>().savaNum++;
            }
            if (myCard.card.br == Breed.ThreeColor)
            {
                gameObj.GetComponent<CheckUsable>().thrNum++;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            FindObj();
            IncreaseCard(); //게임 시작 할 때 한번만
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
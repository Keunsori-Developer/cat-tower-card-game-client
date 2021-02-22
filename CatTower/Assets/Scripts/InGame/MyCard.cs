using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class MyCard : MonoBehaviour
    {
        public Card card;
        
        public void SetCard(Card _card)
        {
            card = new Card(_card.br, _card.catImage, _card.special);
            this.gameObject.GetComponent<Image>().sprite = card.catImage;
            IncreaseCard();
        }
        public void IncreaseCard() //본인카드의 갯수
        {
            GameObject gameObjectForCardData = GameObject.Find("MyCards");

            if(card.br == Breed.Mackerel)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().mackNum++;
            }
            if(card.br == Breed.Odd)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().oddNum++;
            }
            if (card.br == Breed.Persian)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().persNum++;
            }
            if (card.br == Breed.Ragdoll)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().ragNum++;
            }
            if (card.br == Breed.RussianBlue)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().russiaNum++;
            }
            if (card.br == Breed.Siamese)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().siamNum++;
            }
            if (card.br == Breed.Savanna)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().savaNum++;
            }
            if (card.br == Breed.ThreeColor)
            {
                gameObjectForCardData.GetComponent<CheckUsable>().thrNum++;
            }
        }
        void Start()
        {

        }
        void Update()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class MyCard : MonoBehaviour
    {
        public Card card;
        public Image MyImage;
        
        public void SetCard(Card _card)
        {
            card.catImage = _card.catImage;
            card.br = _card.br;
            card.special = _card.special;
            MyImage.sprite = _card.catImage;
        }
        void Start()
        {

        }
        void Update()
        {

        }
    }
}

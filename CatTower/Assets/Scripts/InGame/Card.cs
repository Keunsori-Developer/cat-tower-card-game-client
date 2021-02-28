using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public enum Breed
    {
        none,
        Siamese,
        RussianBlue,
        Mackerel,
        Persian,
        Ragdoll,
        Savanna,
        ThreeColor,
        Odd
    }

    public class Card
    {
        public Breed br;
        public string brS;
        public Sprite catImage;
        public bool special;

        public Card(Breed breed, Sprite image, string breedString, bool isSpecial)
        {
            this.br = breed;
            this.brS = breedString;
            this.catImage = image;
            this.special = isSpecial;
        }
    }
}

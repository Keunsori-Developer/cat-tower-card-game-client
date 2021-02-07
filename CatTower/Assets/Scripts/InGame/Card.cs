using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower {
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

    [System.Serializable]
    public class Card
    {
        public Breed br;
        public Sprite catImage;
        public bool special;
    }
}

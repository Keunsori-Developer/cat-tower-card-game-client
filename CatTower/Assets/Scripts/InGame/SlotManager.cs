using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class SlotManager : MonoBehaviour
    {
        public int[] arrSlotIndex = new int[58];
        public Breed[] arrSlotBreed = new Breed[58];

        void Start()
        {
            for (int i = 0; i < 58; i++)
            {
                arrSlotIndex[i] = 0;
                arrSlotBreed[i] = Breed.none;
            }
        }

        void Update()
        {

        }
    }
}

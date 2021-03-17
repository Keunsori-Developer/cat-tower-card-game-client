using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class SlotManager : MonoBehaviour
    {
        public int[] arrSlotIndex = new int[58];
        public Breed[] arrSlotBreed = new Breed[58];
        public Sprite backGround;

        public void ResetSlot()
        {
            for(int i = 0; i < 58; i++)
            {
                arrSlotIndex[i] = 0;
                arrSlotBreed[i] = Breed.none;
            }
        }

        public void ResetSprite()
        {
            for (int i = 0; i < 8; i++) {
                this.transform.GetChild(7).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(7).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 7; i++)
            {
                this.transform.GetChild(6).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(6).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 6; i++)
            {
                this.transform.GetChild(5).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(5).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 5; i++)
            {
                this.transform.GetChild(4).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(4).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 4; i++)
            {
                this.transform.GetChild(3).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(3).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 3; i++)
            {
                this.transform.GetChild(2).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(2).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            for (int i = 0; i < 2; i++)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = backGround;
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Slot>().myBr = Breed.none;
            }
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = backGround;
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<Slot>().myBr = Breed.none;

        }

        void Start()
        {
            ResetSlot();
        }
    }
}

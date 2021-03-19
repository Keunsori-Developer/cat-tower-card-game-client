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
        private Dictionary<Breed, Sprite> catImageDic;


        void Start()
        {
            catImageDic = new Dictionary<Breed, Sprite>
            {
                {Breed.Mackerel, Resources.Load<Sprite>("Ingame/Cat/mack")},
                {Breed.Siamese, Resources.Load<Sprite>("Ingame/Cat/siam")},
                {Breed.Persian, Resources.Load<Sprite>("Ingame/Cat/persian")},
                {Breed.Ragdoll, Resources.Load<Sprite>("Ingame/Cat/rag")},
                {Breed.RussianBlue, Resources.Load<Sprite>("Ingame/Cat/russian")},
                {Breed.Savanna, Resources.Load<Sprite>("Ingame/Cat/savanna")},
                {Breed.ThreeColor, Resources.Load<Sprite>("Ingame/Cat/three")},
                {Breed.Odd, Resources.Load<Sprite>("Ingame/Cat/odd")}
            };
            ResetSlot();
        }

        public void ResetSlot()
        {
            for (int i = 0; i < 58; i++)
            {
                arrSlotIndex[i] = 0;
                arrSlotBreed[i] = Breed.none;
            }
        }

        public void ResetSprite()
        {
            for (int i = 0; i < 8; i++)
            {
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

        public void SetSprite(int index, Breed breed)
        {
            Transform slotToChange = null;
            if (index - 7 <= 0)
            {
                slotToChange = this.transform.GetChild(7).transform.GetChild(index);
            }
            else if (index - 8 <= 0)
            {
                slotToChange = this.transform.GetChild(6).transform.GetChild(index - 8);
            }
            else if (index - 16 <= 0)
            {
                slotToChange = this.transform.GetChild(5).transform.GetChild(index - 16);
            }
            else if (index - 24 <= 0)
            {
                slotToChange = this.transform.GetChild(4).transform.GetChild(index - 24);
            }
            else if (index - 32 <= 0)
            {
                slotToChange = this.transform.GetChild(3).transform.GetChild(index - 32);
            }
            else if (index - 40 <= 0)
            {
                slotToChange = this.transform.GetChild(2).transform.GetChild(index - 40);
            }
            else if (index - 48 <= 0)
            {
                slotToChange = this.transform.GetChild(1).transform.GetChild(index - 48);
            }
            else if (index == 56)
            {
                slotToChange = this.transform.GetChild(0).transform.GetChild(0);
            }
            Debug.Log(slotToChange.name + " 의 이미지를 변경!!!!!!!!!!!!!!!!");
            if(slotToChange != null) slotToChange.GetComponent<Image>().sprite = catImageDic[breed];
        }
    }
}

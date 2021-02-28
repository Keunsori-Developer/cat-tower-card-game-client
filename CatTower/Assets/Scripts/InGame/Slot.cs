using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{

    public class Slot : MonoBehaviour
    {
        public int index;
        public Breed myBr;
        private GameObject parentSlot;
        private GameObject myCards;
        private bool canUse;

        private void Awake() {
            parentSlot = GameObject.Find("SlotManager");
            myCards = GameObject.Find("MyCards");
            canUse = true;
        }

        public bool CheckSlot(Card target)
        {
            if (target.br != Breed.none)
            {
                if (target.special == false)
                {

                    if (index < 8)
                    {
                        if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                            return false;
                        SetSlot(target);
                    }
                    else
                    {
                        if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1
                            && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1
                            && (parentSlot.GetComponent<SlotManager>().arrSlotBreed[index - 8] == target.br || parentSlot.GetComponent<SlotManager>().arrSlotBreed[index - 7] == target.br))
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                return false;
                            SetSlot(target);
                        }
                        else return false;
                    }
                }
                else
                {
                    if (target.br == Breed.ThreeColor)
                    {
                        if (index < 8)
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                return false;
                            SetSlot(target);
                        }
                        else
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1)
                            {
                                if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return false;
                                SetSlot(target);
                            }
                            else return false;
                        }
                    }
                    else if (target.br == Breed.Odd)
                    {
                        if (index == 0)
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[1] == 0)
                            {
                                SetSlot(target);
                            }
                        }
                        else if (index == 7)
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[6] == 0)
                            {
                                SetSlot(target);
                            }
                        }
                        else if (index < 7 && index > 0)
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 1] == 0 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index + 1] == 0)
                            {
                                SetSlot(target);
                            }
                        }
                        else
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 1] == 0 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index + 1] == 0)
                            {
                                if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return false;
                                SetSlot(target);
                            }
                        }
                    }
                    else
                    {
                        if(GetComponent<CheckUsable>().savaBool == true)
                        {
                            if (index < 8)
                            {
                                if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return false;
                                GetComponent<CheckUsable>().myScore -= 5;
                                SetSlot(target);
                            }
                            
                        }
                        else
                        {
                            if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && parentSlot.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1)
                            {
                                if (parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return false;
                                GetComponent<CheckUsable>().myScore -= 5;
                                SetSlot(target);
                            }
                            else return false;
                        }
                    }
                }
                return true;
            }
            else return false;

        }
        public void SetSlot(Card card)
        {
            this.gameObject.GetComponent<Image>().sprite = card.catImage;
            parentSlot.GetComponent<SlotManager>().arrSlotBreed[index] = card.br;
            parentSlot.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
            //del = true;
            myCards.GetComponent<CheckUsable>().ResetBr();
            myCards.GetComponent<CheckUsable>().CheckBr();
            canUse = false;
        }
    }
}

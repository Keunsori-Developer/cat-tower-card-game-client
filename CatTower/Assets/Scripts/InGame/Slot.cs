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

                    SetBoard();
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
                    SetBoard();


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

        public void SetBoard()
        {
            // TODO: 정체 확인 필요

            // for (int i = 0; i < 57; i++)
            // {
            //     if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.none)
            //     {
            //         listBoard.Add("X");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Mackerel)
            //     {
            //         listBoard.Add("A");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Siamese)
            //     {
            //         listBoard.Add("B");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Persian)
            //     {
            //         listBoard.Add("C");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Ragdoll)
            //     {
            //         listBoard.Add("D");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.RussianBlue)
            //     {
            //         listBoard.Add("E");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Savanna)
            //     {
            //         listBoard.Add("S0");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.ThreeColor)
            //     {
            //         listBoard.Add("S1");
            //     }
            //     else if (parentSlot.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Odd)
            //     {
            //         listBoard.Add("S2");
            //     }
            // }
        }
    }
}

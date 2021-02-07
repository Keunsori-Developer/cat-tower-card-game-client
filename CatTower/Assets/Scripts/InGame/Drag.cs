using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CatTower
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {

        public static Vector2 defaultposition;
        public static Sprite mySprite;
        public static bool del = false;

        public Image myImage;
        public int index;
        public static Breed br;
        public static bool special;

        public Slot mySlot;
        public MyCard myCard;
        public GameObject gameObj;
        public GameObject checkObj;
        public bool dragAble;

        public void DecreaseCard() //사용할때마다 카드 수 감소시킴
        {
            if (myCard.card.br == Breed.Mackerel)
            {
                checkObj.GetComponent<CheckUsable>().mackNum--;
            }
            if (myCard.card.br == Breed.Odd)
            {
                checkObj.GetComponent<CheckUsable>().oddNum--;
            }
            if (myCard.card.br == Breed.Persian)
            {
                checkObj.GetComponent<CheckUsable>().persNum--;
            }
            if (myCard.card.br == Breed.Ragdoll)
            {
                checkObj.GetComponent<CheckUsable>().ragNum--;
            }
            if (myCard.card.br == Breed.RussianBlue)
            {
                checkObj.GetComponent<CheckUsable>().russiaNum--;
            }
            if (myCard.card.br == Breed.Siamese)
            {
                checkObj.GetComponent<CheckUsable>().siamNum--;
            }
            if (myCard.card.br == Breed.ThreeColor)
            {
                checkObj.GetComponent<CheckUsable>().thrNum--;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dragAble == false)
            {
                return;
            }
            else
            {
                defaultposition = this.transform.position;
                mySprite = this.GetComponent<MyCard>().card.catImage;
                br = this.GetComponent<MyCard>().card.br;
                special = this.GetComponent<MyCard>().card.special;
            }
        }

      
        public void OnDrag(PointerEventData eventData)
        {
            if (dragAble == false)
            {
                return;
            }
            else
            {
                Vector2 currentPos = Input.mousePosition;
                this.transform.position = currentPos;
            }
        }
        

        public void OnDrop(PointerEventData eventData)
        {
            if (br != Breed.none)
            {
                if (special == false)
                {
                    if (dragAble == false)
                    {
                        if (index < 8)
                        {
                            if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                return;
                            mySlot.myBr = br;
                            myImage.sprite = mySprite;
                            gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                            gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                            del = true;
                            checkObj.GetComponent<CheckUsable>().ResetBr();
                            checkObj.GetComponent<CheckUsable>().CheckBr();
                        }
                        else
                        {
                            if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1 && (gameObj.GetComponent<SlotManager>().arrSlotBreed[index - 8] == br || gameObj.GetComponent<SlotManager>().arrSlotBreed[index - 7] == br))
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return;
                                mySlot.myBr = br;
                                myImage.sprite = mySprite;
                                gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                del = true;
                                checkObj.GetComponent<CheckUsable>().ResetBr();
                                checkObj.GetComponent<CheckUsable>().CheckBr();
                            }
                            else return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (dragAble == false)
                    {
                        if (br == Breed.ThreeColor)
                        {
                            if (index < 8)
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                    return;
                                mySlot.myBr = br;
                                myImage.sprite = mySprite;
                                gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                del = true;
                                checkObj.GetComponent<CheckUsable>().ResetBr();
                                checkObj.GetComponent<CheckUsable>().CheckBr();
                            }
                            else
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1)
                                {
                                    if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                        return;
                                    mySlot.myBr = br;
                                    myImage.sprite = mySprite;
                                    gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                    gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                    del = true;
                                    checkObj.GetComponent<CheckUsable>().ResetBr();
                                    checkObj.GetComponent<CheckUsable>().CheckBr();
                                }
                                else return;
                            }
                        }
                        else if (br == Breed.Odd)
                        {
                            if (index == 0)
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[1] == 0)
                                {
                                    mySlot.myBr = br;
                                    myImage.sprite = mySprite;
                                    gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                    gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                    del = true;
                                    checkObj.GetComponent<CheckUsable>().ResetBr();
                                    checkObj.GetComponent<CheckUsable>().CheckBr();
                                }
                            }
                            else if (index == 7)
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[6] == 0)
                                {
                                    mySlot.myBr = br;
                                    myImage.sprite = mySprite;
                                    gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                    gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                    del = true;
                                    checkObj.GetComponent<CheckUsable>().ResetBr();
                                    checkObj.GetComponent<CheckUsable>().CheckBr();
                                }
                            }
                            else if (index < 7 && index > 0)
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index + 1] == 0)
                                {
                                    mySlot.myBr = br;
                                    myImage.sprite = mySprite;
                                    gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                    gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                    del = true;
                                    checkObj.GetComponent<CheckUsable>().ResetBr();
                                    checkObj.GetComponent<CheckUsable>().CheckBr();
                                }
                            }
                            else
                            {
                                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 8] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 7] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[index + 1] == 0)
                                {
                                    if (gameObj.GetComponent<SlotManager>().arrSlotIndex[index] != 0)
                                        return;
                                    mySlot.myBr = br;
                                    myImage.sprite = mySprite;
                                    gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = mySlot.myBr;
                                    gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
                                    del = true;
                                    checkObj.GetComponent<CheckUsable>().ResetBr();
                                    checkObj.GetComponent<CheckUsable>().CheckBr();
                                }
                            }
                        }
                    }
                    else
                        return;
                }
            }
            else return;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragAble == false)
            {
                return;
            }
            else
            {   
                this.transform.position = defaultposition;
                if (del == true)
                {
                    checkObj = GameObject.Find("MyCards");
                    DecreaseCard();
                    checkObj.GetComponent<CheckUsable>().CheckCard();
                    Destroy(gameObj);
                    del = false;
                }
                br = Breed.none;
            }                    
        }
    }
}

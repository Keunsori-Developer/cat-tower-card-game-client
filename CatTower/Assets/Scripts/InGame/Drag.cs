using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CatTower
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static Vector2 defaultposition;
        public static Sprite mySprite;
        public static bool del = true;

        public Image myImage;
        public int index;
        public static Breed br;
        public static string brS;
        public static bool special;

        public MyCard myCard;
        public GameObject gameObj;
        public GameObject checkObj;
        public bool dragAble;

        public void SetSlot()//필요??
        {
            myImage.sprite = mySprite;
            gameObj.GetComponent<SlotManager>().arrSlotBreed[index] = br;
            gameObj.GetComponent<SlotManager>().arrSlotIndex[index] = 1;
            del = true;
            checkObj.GetComponent<CheckUsable>().ResetBr();
            checkObj.GetComponent<CheckUsable>().CheckBr();
        }
  
        public void SetSprite()
        {
            for (int i = 0; i < 57; i++)
            {
                if (i == index)
                {
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Mackerel)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/mack");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Siamese)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/siam");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Persian)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/persian");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Ragdoll)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/rag");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.RussianBlue)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/russian");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.ThreeColor)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/three");
                    }
                    else if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i] == Breed.Odd)
                    {
                        myImage.sprite = Resources.Load<Sprite>("Ingame/Cat/odd");
                    }
                }
            }
        }

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
            if (myCard.card.br == Breed.Savanna)
            {
                checkObj.GetComponent<CheckUsable>().savaNum--;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dragAble == false || GameController.Instance.controllAble == false)
            {
                return;
            }
            else
            {
                defaultposition = this.transform.position;
                mySprite = this.GetComponent<MyCard>().card.catImage;
                br = this.GetComponent<MyCard>().card.br;
                brS = this.GetComponent<MyCard>().card.brS;
                special = this.GetComponent<MyCard>().card.special;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragAble == false || GameController.Instance.controllAble == false)
            {
                return;
            }
            else
            {
                Vector2 currentPos = Input.mousePosition;
                this.transform.position = currentPos;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragAble == false || GameController.Instance.controllAble == false)
            {
                return;
            }
            else
            {
                var gr = GameObject.Find("CardSlotCanvas").GetComponent<GraphicRaycaster>();
                var ped = new PointerEventData(null);
                ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                gr.Raycast(ped, results);
                GameObject slotObject = null;
                foreach (var h in results)
                {
                    Debug.Log(h.gameObject.name);
                    slotObject = h.gameObject;
                    break;
                }
                Slot slot;
                if (slotObject == null || !slotObject.TryGetComponent<Slot>(out slot))
                {
                    this.transform.position = defaultposition;
                    return;
                }
                if (!slot.CheckSlot(this.gameObject.GetComponent<MyCard>().card))
                {
                    Debug.Log("여기엔 둘 수 없음");
                    del = false;
                    this.transform.position = defaultposition;
                    return;
                }
                
                if (del == true)
                {
                    checkObj = GameObject.Find("MyCards");
                    DecreaseCard();
                    checkObj.GetComponent<CheckUsable>().CheckCard();
                    checkObj.GetComponent<CheckUsable>().SumScore();
                    Destroy(this.gameObject);
                    //del = false;
                }
                GameController.Instance.throwInfo(brS, index);
                GameController.Instance.MyTurnEnd();
                br = Breed.none;                              
            }                    
        }
    }
}

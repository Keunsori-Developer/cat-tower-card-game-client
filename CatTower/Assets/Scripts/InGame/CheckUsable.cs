using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class CheckUsable : MonoBehaviour
    {
        public bool usableCard = true;
        public GameObject gameObj;
        public bool siamBool = false;
        public bool russianBool = false;
        public bool mackBool = false;
        public bool persBool = false;
        public bool ragBool = false;
        public bool thrBool = false;
        public bool oddBool = false;
        public bool savaBool = false;
        public int siamNum = 0;
        public int russiaNum = 0;
        public int mackNum = 0;
        public int ragNum = 0;
        public int persNum = 0;
        public int thrNum = 0;
        public int oddNum = 0;
        public int savaNum = 0;
        public int myScore = 0;
        public int roundScore;

        public void CheckCard() //사용가능 카드를 확인
        {
            if (!((siamBool == true && siamNum > 0) || (russianBool == true && russiaNum > 0) || (mackBool == true && mackNum > 0) || (persBool == true && persNum > 0) || (ragBool == true && ragNum > 0) || (thrBool == true && thrNum > 0) || (oddBool == true && oddNum > 0) || (savaBool == true && savaNum > 0)))
            {
                usableCard = false;
            }
            else usableCard = true;
        }

        public void SetBreedBool(int i)
        {
            if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i] == 0)
            {
                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 8] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 7] == 1)
                {
                    thrBool = true;
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 8] == Breed.Siamese || gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 7] == Breed.Siamese)
                    {
                        siamBool = true;
                        if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                            oddBool = true;
                    }
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 8] == Breed.RussianBlue || gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 7] == Breed.RussianBlue)
                    {
                        russianBool = true;
                        if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                            oddBool = true;
                    }
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 8] == Breed.Ragdoll || gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 7] == Breed.Ragdoll)
                    {
                        ragBool = true;
                        if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                            oddBool = true;
                    }
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 8] == Breed.Persian || gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 7] == Breed.Persian)
                    {
                        persBool = true;
                        if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                            oddBool = true;
                    }
                    if (gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 8] == Breed.Mackerel || gameObj.GetComponent<SlotManager>().arrSlotBreed[i - 7] == Breed.Mackerel)
                    {
                        mackBool = true;
                        if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                            oddBool = true;
                    }
                }
            }
        }

        public void CheckBr() //누군가 카드를 놓을때 호출, 사용가능한 고양이를 true로 바꿔줌
        {
            if(myScore == 1 && savaNum == 1)
            {
                savaBool = true;
            }
            for (int i = 0; i < 9; i++)
            {
                if (gameObj.GetComponent<SlotManager>().arrSlotIndex[i] == 0)
                {
                    siamBool = true;
                    russianBool = true;
                    mackBool = true;
                    persBool = true;
                    ragBool = true;
                    thrBool = true;
                    if ((i == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[1] == 0) || (i == 7 && gameObj.GetComponent<SlotManager>().arrSlotIndex[6] == 0))
                    {
                        oddBool = true;
                    }
                    if(i > 0 && i < 7 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i-1] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i + 1] == 0)
                    {
                        oddBool = true;
                    }
                    if(i == 8 && gameObj.GetComponent<SlotManager>().arrSlotIndex[9] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 8] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[i - 7] == 1)
                    {
                        oddBool = true;
                    }
                }
            }
            for (int i = 9; i < 15; i++)
            {
                SetBreedBool(i);
            }
            for(int i = 16; i < 22; i++)
            {
                SetBreedBool(i);
            }
            for (int i = 24; i < 29; i++)
            {
                SetBreedBool(i);
            }
            for (int i = 32; i < 36; i++)
            {
                SetBreedBool(i);
            }
            for (int i = 40; i < 43; i++)
            {
                SetBreedBool(i);
            }
            for (int i = 48; i < 50; i++)
            {
                SetBreedBool(i);
            }
            if (gameObj.GetComponent<SlotManager>().arrSlotIndex[56] == 0 && gameObj.GetComponent<SlotManager>().arrSlotIndex[48] == 1 && gameObj.GetComponent<SlotManager>().arrSlotIndex[49] == 1)
            {
                thrBool = true;
                oddBool = true;
                if (gameObj.GetComponent<SlotManager>().arrSlotBreed[48] == Breed.Siamese || gameObj.GetComponent<SlotManager>().arrSlotBreed[49] == Breed.Siamese)
                {
                    siamBool = true;
                }
                if (gameObj.GetComponent<SlotManager>().arrSlotBreed[48] == Breed.RussianBlue || gameObj.GetComponent<SlotManager>().arrSlotBreed[49] == Breed.RussianBlue)
                {
                    russianBool = true;
                }
                if (gameObj.GetComponent<SlotManager>().arrSlotBreed[48] == Breed.Ragdoll || gameObj.GetComponent<SlotManager>().arrSlotBreed[49] == Breed.Ragdoll)
                {
                    ragBool = true;
                }
                if (gameObj.GetComponent<SlotManager>().arrSlotBreed[48] == Breed.Persian || gameObj.GetComponent<SlotManager>().arrSlotBreed[49] == Breed.Persian)
                {
                    persBool = true;
                }
                if (gameObj.GetComponent<SlotManager>().arrSlotBreed[48] == Breed.Mackerel || gameObj.GetComponent<SlotManager>().arrSlotBreed[49] == Breed.Mackerel)
                {
                    mackBool = true;
                }
            }
        }

        public void SumScore()
        {
            myScore = siamNum + russiaNum + mackNum + ragNum + persNum + thrNum + oddNum + savaNum;
        }
                
        public void ResetBr()
        {
            siamBool = false;
            russianBool = false;
            mackBool = false;
            persBool = false;
            ragBool = false;
            thrBool = false;
            oddBool = false;
        }
  
        void Start()
        {
            SumScore();
            CheckBr(); //본인 차례 시작할 때  한번 호출
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace CatTower
{
    [Serializable]
    public class IngameStart
    {
        public string roomId;
        public int round;
        public Userinfo user;
    }

    [Serializable]
    public class IngameCardGive
    {
        public List<string> cards;
    }

    [Serializable]
    public class IngamePlayerOrder
    {
        public List<PlayerOrder> playerOrder;
    }

    [Serializable]
    public class PlayerOrder
    {
        public Userinfo userInfo;
        public int order;
        public int score;
        public bool giveup;
    }

    [Serializable]
    public class IngameThrow
    {
        public Userinfo user;
        public CardInfo card;
        public string roomId;
    }

    [Serializable]
    public class CardInfo
    {
        public string breed;
        public int index;
    }

    [Serializable]
    public class IngameGiveUp
    {
        public Userinfo userInfo;
        public string roomId;
    }

    [Serializable]
    public class IngameStatus
    {
        public Userinfo user;
        public List<string> board; //TODO: string 이 맞는지 확인 필요
        public int order;
        public List<PlayerOrder> player;
    }

    [Serializable]
    public class IngameFinish
    {
        public string roomId;
        public Userinfo user;
        public int round;
        public int leftCard;
    }

    [Serializable]
    public class IngameResult
    {
        public int round;
        public List<PlayerOrder> player;
    }
}
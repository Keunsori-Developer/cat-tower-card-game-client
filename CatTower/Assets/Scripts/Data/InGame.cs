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
        public UserInfo user;
    }

    [Serializable]
    public class IngameCardGive
    {
        public List<string> cards;
    }

    [Serializable]
    public class IngamePlayerOrder
    {
        public List<PlayerOrder> player;
    }

    [Serializable]
    public class PlayerOrder
    {
        public UserInfo userInfo;
        public int order;
        public int score;
        public bool giveup;
    }

    [Serializable]
    public class IngameThrow
    {
        public UserInfo user;
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
        public UserInfo userInfo;
        public string roomId;
    }

    [Serializable]
    public class IngameStatus
    {
        public UserInfo user;
        public List<string> board; //TODO: string 이 맞는지 확인 필요
        public int order;
        public List<PlayerOrder> player;
        public bool giveup;
    }

    [Serializable]
    public class IngameEndRound
    {
        public UserInfo user;
        public List<string> board; //TODO: string 이 맞는지 확인 필요
        public int order;
        public List<PlayerOrder> player;
    }

    [Serializable]
    public class IngameFinish
    {
        public string roomId;
        public UserInfo user;
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
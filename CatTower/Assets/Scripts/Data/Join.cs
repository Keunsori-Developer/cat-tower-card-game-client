using System;

namespace CatTower
{
    [Serializable]
    public class JoinRequest
    {
        public string roomId;
        public class userInfo
        {
            public string nickname;
            public string mid;
        }
    }

    [Serializable]
    public class JoinResponse
    {
            //userlist미구현
    }
}
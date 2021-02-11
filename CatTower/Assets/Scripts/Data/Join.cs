using System;

namespace CatTower
{
    [Serializable]
    public class JoinRequest
    {
        public string roomId;
        public Userinfo userInfo;
    }

    [Serializable]
    public class JoinResponse
    {
            //userlist미구현
    }

    [Serializable]
    public struct Userinfo //UserInfo로 동일한 클래스가 이미 있는데 여기서 닉네임이랑 미드가 static이라 일단 걍 하나더 만듬
    {
        public string nickname;
        public string mid;
    }

}
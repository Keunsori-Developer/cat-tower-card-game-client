using System;

namespace CatTower
{
    [Serializable]
    public class RoomInfoRequest
    {
        public string hostId;
        public string name;
        public int capacity;//string으로 되어있길래 바꿈
        public int mode;
    }

    [Serializable]
    public class RoomInfoResponse
    {
        
    }
}
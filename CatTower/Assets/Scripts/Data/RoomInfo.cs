using System;

namespace CatTower
{
    [Serializable]
    public class RoomInfoRequest
    {
        public UserInfo hostInfo;
        public string name;
        public int capacity;
        public int mode;
    }
}
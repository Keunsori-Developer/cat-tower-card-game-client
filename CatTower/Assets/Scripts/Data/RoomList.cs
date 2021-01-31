using System;

namespace CatTower
{
    [Serializable]
    public class RoomListRequest
    {

    }

    [Serializable]
    public class RoomListResponse
    {
        public string id;
        public string name;
        public int capacity;
        public int joined;
    }
}
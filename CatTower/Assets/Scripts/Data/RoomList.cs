using System;
using System.Collections;
using System.Collections.Generic;

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
        public class hostinfo
        {
            public string nickname;
            public string mid;
        }
    }
    
}
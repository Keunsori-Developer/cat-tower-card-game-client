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
    public class RoomListResponse : BaseResponse
    {
        public List<RoomInfo> rooms;
    }
    [Serializable]
    public class Hostinfo
    {
        public string nickname;
        public string mid;
    }
    [Serializable]
    public class RoomInfo
    {
        public string id;
        public string name;
        public int capacity;
        public int joined;
        public int mode;
        public Hostinfo hostInfo;
    }
}


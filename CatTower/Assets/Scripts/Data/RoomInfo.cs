using System;

namespace CatTower
{
    [Serializable]
    public class RoomInfoRequest
    {
        public string hostId;
        public string name;
        public int capacity;//string���� �Ǿ��ֱ淡 �ٲ�
        public int mode;
    }

    [Serializable]
    public class RoomInfoResponse
    {
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace CatTower
{
    [Serializable]
    public class IngameStartRequest
    {
        public string roomId;
        public int round;
        public Userinfo a;
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;

namespace CatTower
{
   public static class JoinedRoom
    {
        private static string _roomId = null;

        public static string roomId
        {
            get => PlayerPrefs.GetString(_roomId, "");
            set
            {
                PlayerPrefs.SetString(_roomId, value);
            }
        }

        public static string roomName;
        public static List<UserInfo> joinedUserList = new List<UserInfo>();
        public static UserInfo host;
    }
    
}
using System;
using UnityEngine;

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
    }
    
}
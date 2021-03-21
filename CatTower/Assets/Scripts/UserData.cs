using System;
using System.Linq;
using UnityEngine;

namespace CatTower
{
    public static class UserData
    {
        private static string _midKey = "userInfo_mid";
        private static string _nickNameKey = "userInfo_nickName";

        /// <summary>
        /// 유저의 고유 번호
        /// </summary>
        public static string mid
        {
            get => PlayerPrefs.GetString(_midKey, "");
        }

        /// <summary>
        /// 유저가 설정한 닉네임
        /// </summary>
        public static string nickName
        {
            get => PlayerPrefs.GetString(_nickNameKey, "");
            set
            {
                PlayerPrefs.SetString(_nickNameKey, value);
                if (string.IsNullOrEmpty(mid)) PlayerPrefs.SetString(_midKey, SetRandomMid());
            }
        }

        public static UserInfo GetUserInfo()
        {
            return new UserInfo()
            {
                mid = mid,
                nickname = nickName
            };
        }

        private static string SetRandomMid()
        {
            System.Random rand = new System.Random();
            string input_alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string input_number = "1234567890";

            var chars = Enumerable.Range(0, 5)
                                   .Select(x => input_alphabet[rand.Next(0, input_alphabet.Length)]);
            var nums = Enumerable.Range(0, 4)
                                   .Select(x => input_number[rand.Next(0, input_number.Length)]);
            return new string(chars.ToArray()) + new string(nums.ToArray());
        }
    }
}

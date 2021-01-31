using UnityEngine;

namespace CatTower
{
    public class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).Name;
                    }
                }

                return _instance;
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
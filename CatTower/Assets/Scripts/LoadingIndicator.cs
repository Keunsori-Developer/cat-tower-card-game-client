using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class LoadingIndicator : SingletonGameObject<LoadingIndicator>
    {
        private static GameObject indicatorPrefab;
        private static GameObject loadingGameObject;

        public static void Show()
        {
            if (indicatorPrefab == null) indicatorPrefab = Resources.Load("LoadingIndicator") as GameObject;
            if (loadingGameObject == null)
                loadingGameObject = Instantiate(indicatorPrefab);
        }

        public static void Hide()
        {
            if (loadingGameObject != null)
                Destroy(loadingGameObject);
        }
    }
}
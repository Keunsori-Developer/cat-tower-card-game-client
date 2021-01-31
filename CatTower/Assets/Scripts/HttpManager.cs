using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace CatTower
{
    public class HttpManager : SingletonGameObject<HttpManager>
    {
        private string _url = "http://localhost:3000";

         public void Get<TResponse>(string path, Action<TResponse> responseHandler)
        {
            StartCoroutine(CoroutineGet<TResponse>(path, responseHandler));
        }

        public void Post<TRequest, TResponse>(string path, TRequest request, Action<TResponse> responseHandler)
        {
            StartCoroutine(CoroutinePost<TRequest, TResponse>(path, request, responseHandler));
        }

        private IEnumerator CoroutineGet<TResponse>(string path, Action<TResponse> responseHandler)
        {
            
            UnityWebRequest webRequest = new UnityWebRequest();
            webRequest.url = _url + path;
            webRequest.method = UnityWebRequest.kHttpVerbGET;
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("network error");
                yield break;
            }
            if (webRequest.responseCode != 200)
            {
                Debug.Log("responseCode not 200");
                yield break;
            }
            var responseBody = webRequest.downloadHandler.text;
            if (responseHandler != null) responseHandler(JsonUtility.FromJson<TResponse>(responseBody));
            yield return null;
        }
        
        private IEnumerator CoroutinePost<TRequest, TResponse>(string path, TRequest request, Action<TResponse> responseHandler)
        {
            var json = JsonUtility.ToJson(request);
            Debug.Log("request:\n" + json);
            UnityWebRequest webRequest = new UnityWebRequest();
            webRequest.url = _url + path;
            webRequest.method = UnityWebRequest.kHttpVerbPOST;
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            webRequest.uploadHandler.contentType = "application/json";
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("network error");
                yield break;
            }
            if (webRequest.responseCode != 200)
            {
                Debug.Log("responseCode not 200");
                yield break;
            }
            var responseBody = webRequest.downloadHandler.text;
            Debug.Log("response:\n" + responseBody);
            if (responseHandler != null) responseHandler(JsonUtility.FromJson<TResponse>(responseBody));
            yield return null;
        }
    }
}
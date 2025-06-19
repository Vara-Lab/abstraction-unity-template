using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

namespace AbstractionServer
{
    public static class AbstractionApiClient
    {
        public static string BaseUrl = "http://localhost:8000";
        public static string Token = null;

        public static IEnumerator Get<T>(string path, Action<T> onSuccess, Action<string> onError)
        {
            string url = $"{BaseUrl}{path}";
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(Token))
                request.SetRequestHeader("Authorization", $"Bearer {Token}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (typeof(T) == typeof(string))
                {
                    onSuccess?.Invoke((T)(object)request.downloadHandler.text);
                }
                else
                {
                    T result = JsonUtility.FromJson<T>(request.downloadHandler.text);
                    onSuccess?.Invoke(result);
                }
            }
            else
            {
                onError?.Invoke(request.downloadHandler.text);
            }
        }


       public static IEnumerator Post<TReq, TRes>(string path, TReq body, Action<TRes> onSuccess, Action<string> onError)
{
    string url = $"{BaseUrl}{path}";
    string json = JsonUtility.ToJson(body);

    UnityWebRequest request = new UnityWebRequest(url, "POST");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");

    if (!string.IsNullOrEmpty(Token))
        request.SetRequestHeader("Authorization", $"Bearer {Token}");

    yield return request.SendWebRequest();

    string rawJson = request.downloadHandler.text;
    Debug.Log("RESPONSE: " + rawJson);

    if (request.result == UnityWebRequest.Result.Success)
    {
        if (typeof(TRes) == typeof(string))
        {
            onSuccess?.Invoke((TRes)(object)rawJson);
        }
        else
        {
            try
            {
                TRes result = JsonUtility.FromJson<TRes>(rawJson);
                onSuccess?.Invoke(result);
            }
            catch (Exception ex)
            {
                Debug.LogError("JSON parse error: " + ex.Message);
                Debug.LogError("Raw JSON: " + rawJson);
                onError?.Invoke("Failed to parse response.");
            }
        }
    }
    else
    {
        onError?.Invoke(rawJson);
    }
}


        public static IEnumerator Post(string path, Action<string> onSuccess, Action<string> onError)
        {
            return Post<object, string>(path, new object(), onSuccess, onError);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class WebGet : MonoBehaviour
{
    // Start is called before the first frame update
    [Serializable]
    public class HR
    {
        public float hr;
    }
    public static float latestHR = 130;
// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetRequest("https://test-watch-jbusa.herokuapp.com/updates/recent"));

        // A non-existing page.
        // StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        while(GameControl.started) {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                        success(webRequest.downloadHandler.text);
                        break;
                }
            }
            yield return new WaitForSeconds(10);
            yield return null;
        }
    }

    public void success(string text) {
        HR hr = JsonUtility.FromJson<HR>(text);
        latestHR = hr.hr;
    }
}

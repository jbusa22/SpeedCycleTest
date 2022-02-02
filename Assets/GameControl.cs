using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [Serializable]
    public class MyClass
    {
        public int time;
        public float line;
    }
    [Serializable]
    public class MyWrapper
    {
        public List<MyClass> song;
    }
    public static bool started = false;
    public AudioSource audioSource;
    public LineRenderer lineRenderer;
    public AnimationCurve animationCurve;
    public List<Vector3> points;

    public GameObject ballPrefab;
    public Image UICam;
    // Start is called before the first frame update
    void Start()
    {
        started = true;
        audioSource.Play();
        string str = "{\"song\": [{\"time\":29,\"line\": 0.2},{\"time\":42,\"line\": 0},{\"time\":56,\"line\":0.2},{\"time\":70,\"line\":0.4}, {\"time\":95,\"line\":0}, {\"time\":140,\"line\":0.2}, {\"time\":160,\"line\":0}, {\"time\":175,\"line\":0.1}]}";
        MyWrapper myObject = JsonUtility.FromJson<MyWrapper>(str);
        List<Vector3> myList = new List<Vector3>();
        myList.Add(Vector3.zero);
        float inc = 0;
        float tot = 0;
        int prev = 0;
        foreach (var item in myObject.song)
        {
            float temp = (float)(item.time - prev) * inc;
            tot += temp;
            myList.Add(new Vector3(item.time, tot, 0));
            inc = item.line;
            prev = item.time;
            // Debug.Log(new Vector3(item.time, tot, 0));
        }
        points = myList;
        lineRenderer.positionCount = myList.Count;
        lineRenderer.SetPositions(myList.ToArray());
        spawnBalls();

    }

    void spawnBalls() {
        Vector3 prevItem = Vector3.up;
        foreach (var item in points)
        {
            if (prevItem == Vector3.up) {
                prevItem = item;
            } else {
                float dist = Vector3.Distance(prevItem, item);
                float startDist = 0;
                float incDist = 1;
                while(dist > startDist) {
                    // Vector3 spawnPoint = Vector3.Lerp(prevItem, item, UnityEngine.Random.Range(0, 1f));
                    Vector3 spawnPoint = Vector3.MoveTowards(prevItem, item, startDist);
                    Vector3 crossPoint = spawnPoint + new Vector3(0, 0, -1);
                    var dir = Vector3.Cross(item - prevItem, crossPoint  - prevItem);
                    var norm = Vector3.Normalize(dir) + new Vector3(0, UnityEngine.Random.Range(-.1f, .1f), 0);
                    Instantiate(ballPrefab, spawnPoint + norm, Quaternion.identity, lineRenderer.gameObject.transform);
                    
                    startDist += incDist;
                }
                prevItem = item;
            }
            // Debug.Log(new Vector3(item.time, tot, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        float val = animationCurve.Evaluate(WebGet.latestHR);
        // Color.Lerp(Color.red, Color.green, )
        Color overlay = Color.Lerp(Color.red, Color.green, val);
        overlay.a = UICam.color.a;
        UICam.color = overlay;
    }
}

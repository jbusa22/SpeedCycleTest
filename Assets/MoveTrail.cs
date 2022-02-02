using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    Vector3 startingPos;
    public float speed = 4;
    public bool zooming = false;
    float elapsed = 0;
    Vector3 endingPos;
    TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiting());
        trailRenderer = GetComponent<TrailRenderer>();
    }

    IEnumerator waiting() {
        float randY = Random.Range(-1f,1f);
        float randX = Random.Range(-1f,1f);
        startingPos = new Vector3(randY,randX, -10);
        endingPos = new Vector3(randY,randX, 4);
        transform.localPosition = startingPos;
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        zooming = true;
        trailRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming) {
            elapsed += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(startingPos, endingPos, elapsed);
            if ((transform.localPosition - endingPos).magnitude < 0.1f) {
                zooming = false;
                elapsed = 0;
                trailRenderer.enabled = false;
                StartCoroutine(waiting());
            }
        }
    }
}

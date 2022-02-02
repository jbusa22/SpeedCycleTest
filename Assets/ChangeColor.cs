using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    Image image;
    float elapsed = 0;
    float amount = .4f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed >= amount) {
            
            Color col = Random.ColorHSV();
            col.a = image.color.a;
            image.color = col;
            elapsed = 0;
        }
        elapsed += Time.deltaTime;
    }
}

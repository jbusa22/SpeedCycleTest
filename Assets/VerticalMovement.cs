using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float xSpeed = 1f;

    public float yRotSpeed = .6f;
    // Start is called before the first frame update
    Rigidbody rb;

    // Update is called once per frame
     public Vector3 destEuler;

     // Use this for initialization
     void Start () {

        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(xSpeed, 0, 0);
        destEuler = transform.rotation.eulerAngles;
     }

     // Update is called once per frame
     void Update () {
        float vertical_input = Input.GetAxis("Vertical");
        float yCmapl = Mathf.Clamp(destEuler.x + vertical_input * yRotSpeed, -90f, 90f);
        destEuler.x = yCmapl;
        rb.velocity = new Vector3(xSpeed, yCmapl / 45f, 0);
        // Debug.Log(vertical_input);
        transform.eulerAngles = destEuler;
    }
}

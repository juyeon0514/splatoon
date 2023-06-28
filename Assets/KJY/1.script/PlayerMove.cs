using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 2f;
    public float jumpSpeed;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3 (h, 0, v); 
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
    }
}

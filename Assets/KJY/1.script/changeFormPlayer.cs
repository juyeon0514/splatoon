using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFormPlayer : MonoBehaviour
{
    public static changeFormPlayer instance;
    public bool isHuman;
    public GameObject body;
    public Transform Floor;
    public Transform player;
    Rigidbody rb;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isHuman = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHuman();
        ChangeSquid();
        StopBody();
    }

    void ChangeHuman()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && isHuman == false)
        {
            //body.GetComponent<Collider>().enabled = true;
            isHuman = true;
            Vector3 tmp = transform.position;
            tmp.y = Mathf.Lerp(-2f, 0f, 0.5f);
            transform.position = tmp;
        }
    }

    void ChangeSquid()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isHuman == true)
        {
            body.GetComponent<Collider>().enabled = false;
            isHuman = false;
        }
    }

    void StopBody()
    {
        if (isHuman == false)
        {
            if (transform.position.y <= -2)
            {
                body.GetComponent<Collider>().enabled = true;
                //transform.position.y = -2;
                Vector3 currentPosition = transform.position;
                currentPosition.y = -2f;
                transform.position = currentPosition;
            }
        }
    }
}

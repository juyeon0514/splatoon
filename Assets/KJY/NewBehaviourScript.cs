using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        transform.position += dir * 5 * Time.deltaTime;
        
        if(h != 0 || v != 0)
        {
            float angle = Vector3.Angle(body.right, dir);
            //���࿡ body �� ���������� ���ƾ� �Ѵٸ�
            if(angle < 90)
            {
                transform.Rotate(0, 500 * Time.deltaTime, 0);
            }

            //�׷��� ������
            else
            {
                transform.Rotate(0, -500 * Time.deltaTime, 0);
            }
        }
    }
}
 
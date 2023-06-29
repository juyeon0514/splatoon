using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Combine_test : MonoBehaviour
{
    public static Combine_test instance;
    public Transform cameraArm;
    public GameObject characterBody;
    public GameObject cam;
    public MeshRenderer mesh;
    public Rigidbody rb;

    public int jumpCount;
    public bool isGround;
    public bool jumping;
    public float jumpSpeed;

    private float camera_dist = 0f;
    public float camera_width = -10f;
    public float camera_height = 4f;
    Vector3 dir;

    public float rotateSpeed;
    Material material;
    // Start is called before the first frame update

    private void Awake()
    {
       instance = this;
    }
    void Start()
    {
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);
        dir = new Vector3(0, camera_height, camera_width).normalized;
        //material = characterBody.GetComponent<Material>();
        jumpCount = 1;
        //renderer = characterBody.GetComponent<Renderer>();
        material = mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
        LongJump();
        if (changeFormPlayer.instance.isHuman == true)
        {
            LimitGroubd();
        }
        //Turn();
    }

    private void LookAround()
    {
        Vector2 mousedelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 이거 왜 였는지 기억이 잘 안남
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 요건 이해가 가는데
        float x = camAngle.x - mousedelta.y;//왜 이렇게 했더라 다시 봐야지

        if (changeFormPlayer.instance.isHuman == true)
        {
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
                //만약에 x가 70에 점점 가까워지면
                //이미지에 알파값음 점점 내리고싶
                if (x > 50)
                {
                    float alpha = Mathf.InverseLerp(100f, 50f, x);
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
                else
                {
                    Color color = material.color;
                    color.a = 1f;
                    material.color = color;
                }
            }
            else
            {
                x = Mathf.Clamp(x, 325f, 361f);
                if (x < 335)
                {
                   float alpha = Mathf.InverseLerp(310f, 361f, x);
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
                else
                {
                    Color color = material.color;
                    color.a = 1f;
                    material.color = color;
                }
            }
        }
        else
        {
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 350f, 361f);
            }
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mousedelta.x, camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            characterBody.transform.forward = Vector3.Slerp(characterBody.transform.forward, moveDir, 0.05f);
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    private void Turn()
    {

        Quaternion newRotation = Quaternion.LookRotation(transform.forward);
        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGround == true && jumpCount >= 1)
            {
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
                jumpCount--;
                jumping = true;
            }
        }
    }

    private void LongJump()
    {
        if (Input.GetButton("Jump") && transform.position.y <= 1)
        {
            if (isGround == true)
            {
                rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Acceleration);
            }
        }
    }

    private void LimitGroubd()
    {
        Vector3 ray_target = cameraArm.up * camera_height + cameraArm.forward * camera_width;
        RaycastHit hitinfo;
        Physics.Raycast(cameraArm.position, ray_target, out hitinfo, camera_dist);
        if (hitinfo.point != Vector3.zero)
        {
            cam.transform.position = hitinfo.point;
        }
        else
        {
            cam.transform.localPosition = Vector3.zero;
            cam.transform.Translate(dir * camera_dist);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;   
            jumpCount = 1;
            if (changeFormPlayer.instance.isHuman == true)
            {
                Vector3 tmp = transform.position;
                tmp.y = 0;
                transform.position = tmp;
            }
        }
        
    }
}

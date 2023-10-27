using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Joystick js;
    public GameObject CrossHair;
    public GameObject Flash;
    public GameObject gameover;
    private Transform cameraTransform;
    private Animator anim;
    public BoxOpen box;
    private UI ui;
    public float p_Hp = 100f;
    public float speed = 8f;
    public float turnspeed = 3f;
    public GameObject Flashlight;
    public float p_power;
/*    public float statime;*/
    private float xRotate = 0.0f;
    public bool iswall;
    float dist = 1f;
    public int flashstate;
    private void Awake()
    {
        gameover.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        anim = this.GetComponent<Animator>();
        CrossHair=Instantiate(CrossHair);
        ui=GetComponent<UI>();
        gameover=GetComponent<GameObject>();
        p_power = 0f;
        /*        p_stamina = 100f;*/
        js.gameObject.SetActive(false);//Ű���� �̵��϶� ���̽�ƽ false
        flashstate = 0;
    
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false; // ���콺Ŀ�� �Ⱥ��̰�
        Cursor.lockState = CursorLockMode.Locked; // 
/*        MouseRotation();*/
        if (p_Hp >= 100)
        {
            p_Hp = 100;
        }

        if (p_power>=100f)
        {
            p_power = 100f;
        }
        /*        statime += Time.deltaTime;  */

/*           FlashControl();*/
    }


    private void FixedUpdate()
    {
        Move();
        //StopToWall();
        /*        joyMove();*/
    }

    void Move() //Ű���� �̵�
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(Horizontal, 0f, Vertical); 
        movement = transform.TransformVector(movement); //���� ��ǥ ����(world space)���� ���� ��ǥ �������� ���͸� ��ȯ�ϴ� �� ���.
        movement *= speed * Time.deltaTime;
        
        


        bool isMoving = movement.magnitude > 0f; // �÷��̾ �����̰� �ִ��� Ȯ��.

        if(iswall)
        {
            movement = Vector3.zero;
        }

        if (!iswall)
        {
            speed = 8f;
            transform.position += movement;
        }

        if(Input.GetKey(KeyCode.S))
        {
            speed=4f;
        }

        /*        if (Input.GetKey(KeyCode.LeftShift) && p_stamina > 1f && isMoving && statime >= 1f) //���� �޸���
                {
                    isrun = true;
                    if (isrun)
                    {
                        runspeed = speed * 2f;
                        nowspeed = runspeed;
                        p_stamina -= 50f * Time.deltaTime;
                    }

                    if (p_stamina <= 1f)
                    {
                        statime = 0f;
                        isrun = false;
                    }
                }
                else
                {
                    nowspeed = speed;
                    p_stamina += 30f * Time.deltaTime;
                }
                if (p_stamina >= 100f)
                {
                    p_stamina = ui.MaxSta;
                }*/
    }

    void FlashControl() // �ķ��� ����
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            flashstate++;
            if(flashstate%2==0)
            {
                Flashlight.SetActive(false);
            }
            else
                Flashlight.SetActive(true);
        }
    }
    void StopToWall()
    {
        iswall = Physics.Raycast(transform.position, transform.forward, 0.6f, LayerMask.GetMask("Wall"));
    }

    void MouseRotation()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnspeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;


        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnspeed;
        // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)

        // Clamp �� ���� ������ �����ϴ� �Լ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

    }

    void joyMove()
    {
        // ��ƽ�� �����ִ� ������ �������ش�.
        Vector3 dir = new Vector3(js.Horizontal, 0, js.Vertical);

        // ĳ������ ���� ȸ�� ���� �����Ͽ� ���̽�ƽ �Է��� ȸ����Ų��.
        dir = Quaternion.Euler(0, transform.eulerAngles.y, 0) * dir;

        // Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
        dir.Normalize();

        // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
        transform.position += dir * speed * Time.deltaTime;
    }


    public void TakeDamage(float damageAmount)
    {
        p_Hp -= damageAmount;
        if(p_Hp <= 0)
            ui.HandleHP();
        else
            StartCoroutine(ui.ShowBloodScreen());
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Potion"))
        {
            Potion item=other.gameObject.GetComponent<Potion>();
            switch(item.type)
            {
                case "Hppotion":
                    p_Hp += 20;
                    Destroy(other.gameObject);
                    break;
                case "Powerpotion":
                    p_power += 100;
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}
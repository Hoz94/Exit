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
    public GameObject TutorialPanel;
    public Text TutorialDoorText;
    public Text TutorialPowerText;
    public Text TutorialPortalText;
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
    public float TutorialTime = 0f;
    bool FirstTrigger;
    bool SecondTrigger;
    bool ThirdTrigger;

    int FirstTriggerCount = 0;
    int SecondTriggerCount = 0;

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
        js.gameObject.SetActive(false);//키보드 이동일때 조이스틱 false
        flashstate = 0;
    
    }

    // Update is called once per frame
    void Update()
    {
        TutorialTime += Time.deltaTime;
        Cursor.visible = false; // 마우스커서 안보이게
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

        /*           FlashControl();*/

        HandleTutoTrigger();

        if (TutorialTime >= 2f)
        {
            TutorialPanel.SetActive(false);
            TutorialDoorText.gameObject.SetActive(false);
            TutorialPowerText.gameObject.SetActive(false);
            TutorialPortalText.gameObject.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        Move();
        //StopToWall();
        /*        joyMove();*/
    }

    void HandleTutoTrigger()
    {
        if(FirstTrigger&&FirstTriggerCount==1) 
        {
            TutorialTime = 0f;
            TutorialPanel.SetActive(true);
            TutorialDoorText.gameObject.SetActive(true);
        }

        if(SecondTrigger&&SecondTriggerCount==1)
        {
            TutorialTime = 0f;
            TutorialPanel.SetActive(true);
            TutorialPowerText.gameObject.SetActive(true);
        }

        if(ThirdTrigger&& Tuto_Mon.instance.isdead==true)
        {
            TutorialTime = 0f;
            TutorialPanel.SetActive(true);
            TutorialPortalText.gameObject.SetActive(true);
        }
    }

    void Move() //키보드 이동
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(Horizontal, 0f, Vertical); 
        movement = transform.TransformVector(movement); //월드 좌표 공간(world space)에서 로컬 좌표 공간으로 벡터를 변환하는 데 사용.
        movement *= speed * Time.deltaTime;
        
        


        bool isMoving = movement.magnitude > 0f; // 플레이어가 움직이고 있는지 확인.

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

        /*        if (Input.GetKey(KeyCode.LeftShift) && p_stamina > 1f && isMoving && statime >= 1f) //빨리 달리기
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

    void FlashControl() // 후레쉬 조작
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
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnspeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;


        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnspeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)

        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

    }

    void joyMove()
    {
        // 스틱이 향해있는 방향을 저장해준다.
        Vector3 dir = new Vector3(js.Horizontal, 0, js.Vertical);

        // 캐릭터의 현재 회전 값을 고려하여 조이스틱 입력을 회전시킨다.
        dir = Quaternion.Euler(0, transform.eulerAngles.y, 0) * dir;

        // Vector의 방향은 유지하지만 크기를 1로 줄인다. 길이가 정규화 되지 않을시 0으로 설정.
        dir.Normalize();

        // 오브젝트의 위치를 dir 방향으로 이동시킨다.
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

        if(other.gameObject.CompareTag("FirstTrigger"))
        {
            FirstTrigger = true;
            FirstTriggerCount++;

        }

        if (other.gameObject.CompareTag("SecondTrigger"))
        {
            SecondTrigger = true;
            SecondTriggerCount++;
        }

        if (other.gameObject.CompareTag("ThirdTrigger"))
        {
            ThirdTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FirstTrigger=false;
        SecondTrigger=false;
        ThirdTrigger=false;
    }
}
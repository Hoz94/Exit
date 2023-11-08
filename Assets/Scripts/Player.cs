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
    public GameObject CrossHair;
    public GameObject Flash;
    public GameObject gameover;
    public GameObject Flashlight;

    public Light light;


    private Transform cameraTransform;

    private Animator anim;

    public BoxOpen box;

    private UI ui;

    private float xRotate = 0.0f;
    public float p_Hp = 100f;
    public float speed = 5.5f;
    public float turnspeed = 3f;
    public float p_power;
    float dist = 1f;
    /*    public float statime;*/

    public int flashstate;
    public bool iswall;

    private void Awake()
    {
        gameover.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        anim = this.GetComponent<Animator>();
        CrossHair = Instantiate(CrossHair);
        ui = GetComponent<UI>();
        gameover = GetComponent<GameObject>();

        p_power = 0f;

        flashstate = 0;

        /*        p_stamina = 100f;*/
        //js.gameObject.SetActive(true);//키보드 이동일때 조이스틱 false
    }

    // Update is called once per frame
    void Update()
    {

        if (p_Hp >= 100)
        {
            p_Hp = 100;
        }

        if (p_power >= 100f)
        {
            p_power = 100f;
        }

        if (light.gameObject.activeSelf == true)
        {
            FlashControl();
        }
    }


    private void FixedUpdate()
    {
        Move();
        //joyMove();
    }


    void Move() //키보드 이동
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(Horizontal, 0f, Vertical);
        movement = transform.TransformVector(movement); //월드 좌표 공간(world space)에서 로컬 좌표 공간으로 벡터를 변환하는 데 사용.
        movement *= speed * Time.deltaTime;


        transform.position += movement;

        //bool isMoving = movement.magnitude > 0f; // 플레이어가 움직이고 있는지 확인.

    }

    void FlashControl() // 후레쉬 조작
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            flashstate++;
            if (flashstate % 2 == 0)
            {
                light.spotAngle = 135f;
                light.range = 35f;
            }
            else
            {
                light.spotAngle = 20f;
                light.range = 70f;
            }
        }
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
        if (p_Hp <= 0)
            ui.HandleHP();
        else
            StartCoroutine(ui.ShowBloodScreen());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            Potion item = other.gameObject.GetComponent<Potion>();
            switch (item.type)
            {
                case "Hppotion":
                    p_Hp += 20;
                    Destroy(other.gameObject);
                    break;
                case "Powerpotion":
                    p_power += 50;
                    Destroy(other.gameObject);
                    break;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cameracontrol : MonoBehaviour
{
    public Camera cam;
    private Vector3 offset = new Vector3(0, 0, 0.1f);
    public float turnSpeed=50f;
    public float xRotation;

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void LateUpdate()
    {
        CamControl();
    }

    public void CamControl()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //마우스 x,y 좌표 값 저장

        xRotation -= (mouseInput.y * Time.fixedDeltaTime) * turnSpeed; // 
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(0f, mouseInput.x * Time.fixedDeltaTime * turnSpeed, 0f);        
    }
}
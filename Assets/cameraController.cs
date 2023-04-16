using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Vector3 previousMousePosition;
    Vector3 newMousePosition;

    RaycastHit cameraCentre;
    float panAmount;
    float previousScroll;
    float minPanAngle = -90f;
    float maxPanAngle = 90f;
    float scrollSpeed = 4;
    float cameraPanSpeed = 70f;
    GameObject playerHand;
    GameObject cameraDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerHand = GameObject.Find("PlayerHand");
        cameraDirection = GameObject.Find("MainCameraDirection");
        panAmount = this.transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            previousMousePosition = playerHand.transform.position;
            cameraCentre = CameraCentrePoint();
        }
        if(Input.GetMouseButton(0)){
            newMousePosition = playerHand.transform.position;
            if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)){
                float rotateAngle = Input.GetAxis("Mouse X") * cameraPanSpeed * Mathf.Deg2Rad;
                Rotate(rotateAngle);
            }
            else if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)){
                panAmount += Input.GetAxis("Mouse Y") * cameraPanSpeed * Mathf.Deg2Rad;
                panAmount = Mathf.Clamp(panAmount, minPanAngle, maxPanAngle);
                Quaternion rotation = Quaternion.Euler(panAmount, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                Pan(rotation);
            }
            else if(playerHand.GetComponent<playerControlls>().onTerrain){
                DragCamera();
            }
            previousMousePosition = newMousePosition;
        }
        if(Input.GetAxis("Mouse ScrollWheel") != previousScroll){
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void DragCamera(){
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");
        cameraDirection.transform.Translate(Vector3.back * moveY, Space.Self);
        cameraDirection.transform.Translate(Vector3.left * moveX, Space.Self);
    }

    private RaycastHit CameraCentrePoint(){
        RaycastHit rayHitInfo;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out rayHitInfo)){
            return rayHitInfo;
        }
        return rayHitInfo;
    }

    private void Zoom(float scrollAmount){
        this.transform.Translate(Vector3.forward * scrollAmount * scrollSpeed);
        previousScroll = Input.GetAxis("Mouse ScrollWheel");
    }

    private void Rotate(float angle){
        if(cameraCentre.point == new Vector3(0,0,0)){
            cameraDirection.transform.Rotate(Vector3.up, angle, Space.World);
        }
        else{
            cameraDirection.transform.RotateAround(cameraCentre.point, Vector3.up, angle);
        }   
    }

    private void Pan(Quaternion rotation){
        this.transform.rotation = rotation;
    }
}
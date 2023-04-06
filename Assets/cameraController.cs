using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    Vector2 previousMousePosition;
    Vector2 newMousePosition;

    RaycastHit? cameraCentre;
    float previousScroll;
    float minPanAngle = 90f;
    float maxPanAngle = 180f;
    float scrollSpeed = 4;
    float cameraDragResistance = 50f;
    float cameraPanSpeed = 70f;
    GameObject playerHand;
    // Start is called before the first frame update
    void Start()
    {
        playerHand = GameObject.Find("PlayerHand");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            previousMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            cameraCentre = CameraCentrePoint();
        }
        if(Input.GetMouseButton(0)){
            newMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)){
                //Rotate();
            }
            else if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)){
                float panAmount = Input.GetAxis("Mouse Y") * cameraPanSpeed * Mathf.Deg2Rad;
                Debug.Log("neg "+-minPanAngle);
                Debug.Log("pan "+panAmount);
                Debug.Log("rot "+this.transform.rotation.eulerAngles.x );
                /*if(this.transform.rotation.eulerAngles.x + panAmount > minPanAngle){
                    panAmount = this.transform.rotation.eulerAngles.x - minPanAngle;
                }
                else if(this.transform.rotation.eulerAngles.x + panAmount < -maxPanAngle){
                    panAmount = -(this.transform.rotation.eulerAngles.x - maxPanAngle);
                }*/
                Pan(panAmount);
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
        Vector2 moveDistance = (previousMousePosition - newMousePosition) / cameraDragResistance;
        this.transform.position = new Vector3(this.transform.position.x + moveDistance.x, this.transform.position.y, this.transform.position.z + moveDistance.y);
        
    }
    private RaycastHit? CameraCentrePoint(){
        RaycastHit rayHitInfo;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out rayHitInfo, default, 3)){
            return rayHitInfo;
        }
        return null;
    }
    private void Zoom(float scrollAmount){
        this.transform.Translate(Vector3.forward * scrollAmount * scrollSpeed);
        previousScroll = Input.GetAxis("Mouse ScrollWheel");
    }
    private void Rotate(float angle, Vector3 rotationPoint){
        if(cameraCentre != null){
            
        }
    }
    private void Pan(float angle){
        this.transform.Rotate(angle,0f,0f);
    }
}
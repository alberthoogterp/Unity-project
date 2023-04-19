using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlls : MonoBehaviour
{
    public bool onTerrain;
    private GameObject mouseOverObject; 
    private float maxRange = 30.0f;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        onTerrain = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || !onTerrain){
            FollowMouse();
        }
        if(Input.GetMouseButtonDown(1)){
            Interract();
        }
    }

    private void FollowMouse(){
        Vector3 nextPosition = this.gameObject.transform.position;
        RaycastHit rayHitInfo;
        Vector3 mouseOverLocation;

        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHitInfo) && rayHitInfo.distance < maxRange){
            onTerrain = true;
            mouseOverObject = rayHitInfo.collider.gameObject;
            mouseOverLocation = rayHitInfo.point;
            nextPosition = mouseOverLocation;
        }
        else{
            onTerrain = false;
            nextPosition = mainCamera.ScreenPointToRay(Input.mousePosition).GetPoint(maxRange);
            mouseOverObject = null;
        }   
        transform.position = nextPosition;
    }

    private void Interract(){
        if(mouseOverObject != null ){
            if(mouseOverObject.GetComponent<physicsObject>().Grabbable){
                Debug.Log(mouseOverObject.name);
            }
        }
    }
}

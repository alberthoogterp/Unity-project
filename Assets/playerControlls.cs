using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlls : MonoBehaviour
{
    public bool onTerrain;
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
        if(!Input.GetMouseButton(0) || !onTerrain){
            FollowMouse();
        }
    }

    private void FollowMouse(){
        Vector3 nextPosition = this.gameObject.transform.position;
        RaycastHit midRayHitInfo;
        GameObject mouseOverObject;
        Vector3 mouseOverLocation;

        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out midRayHitInfo) && midRayHitInfo.distance < maxRange){
            onTerrain = true;
            mouseOverObject = midRayHitInfo.collider.gameObject;
            mouseOverLocation = midRayHitInfo.point;
            nextPosition = mouseOverLocation;
        }
        else{
            onTerrain = false;
            nextPosition = mainCamera.ScreenPointToRay(Input.mousePosition).GetPoint(maxRange);
        }   
        transform.position = nextPosition;
    }
}

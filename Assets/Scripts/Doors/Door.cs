using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool Open;    
    private Transform LeftDoor;
    private Transform RightDoor;
    private Vector3 LeftTarget;
    private Vector3 RightTarget;
    void Start()
    {
        Open = false;
        LeftDoor = transform.GetChild(0);
        RightDoor = transform.GetChild(1);
        LeftTarget = LeftDoor.position - new Vector3(0,0,2.5f);
        RightTarget = RightDoor.position + new Vector3(0,0,2.5f);
    }

    void Update()
    {
        if(Open){
            LeftDoor.position = Vector3.Lerp(LeftDoor.position, LeftTarget, 0.8f * Time.deltaTime);
            RightDoor.position = Vector3.Lerp(RightDoor.position, RightTarget, 0.8f * Time.deltaTime);
        }
    }

    public void SetOpen(bool open){
        Open = open;
    }
}

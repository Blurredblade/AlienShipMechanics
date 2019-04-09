using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgicalLaser : MonoBehaviour
{   
    public float baseRotationSpeed = 50f;
    public float extensionSpeed = 30f;

    private float baseRotZ;
    private float baseRotY;
    private float LowerRot;
    private float UpperRot;
    private LineRenderer laser;
    private Light laserDot;
    private ParticleSystem laserSparks;
    private bool LaserOn;
    private bool canToggle;
    
    void Start()
    {
        baseRotY = transform.rotation.eulerAngles.y;
        LowerRot = UpperRot = 0;
        laser = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<LineRenderer>();
        laserDot = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).GetComponent<Light>();
        laserSparks = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(3).GetComponent<ParticleSystem>();
        LaserOn = false;
        canToggle = true;
    }

    void Update()
    {
        CalcLaser();
    }

    void CalcLaser(){
        RaycastHit laserHit;
        if(LaserOn){
            laserDot.intensity = 30;
            
            if(Physics.Raycast(laser.transform.position, laser.transform.TransformDirection(Vector3.forward), out laserHit, Mathf.Infinity)){
                float dist = Vector3.Distance(laser.transform.position, laserHit.point);
                laser.SetPosition(1, new Vector3(0,0,dist + 0.2f));
                laserDot.transform.position = laserHit.point + (laserHit.normal * 0.005f);
                laserSparks.transform.position = laserHit.point + (laserHit.normal * 0.02f);
                laserSparks.transform.rotation = Quaternion.FromToRotation(Vector3.up, laserHit.normal);
                if(laserHit.collider.gameObject.tag == "doorpanel"){
                    laserHit.collider.gameObject.GetComponent<DoorControlPanel>().SetHit(true);
                }
            }
            
        }else{
            laser.SetPosition(1, new Vector3(0,0,0));
            laserDot.intensity = 0;
            
        }
    }

    public void ToggleLaser(){
        if(canToggle){
            if(!LaserOn)laserSparks.Play(); else laserSparks.Stop();
            LaserOn = !LaserOn;
            StartCoroutine(WaitToToggle());
        }
    }

    IEnumerator WaitToToggle(){
        canToggle = false;
        yield return new WaitForSeconds(0.5f);
        canToggle = true;
    }

    public void AdjustLaserAngles(Direction dir){
        switch(dir){
            case Direction.Left:
                baseRotY -= baseRotationSpeed * Time.deltaTime;
                break;
            case Direction.Right:
                baseRotY += baseRotationSpeed * Time.deltaTime;
                break;
            case Direction.Down:
                LowerRot -= extensionSpeed * Time.deltaTime;
                UpperRot += extensionSpeed * Time.deltaTime;
                break;
            case Direction.Up:
                LowerRot += extensionSpeed * Time.deltaTime;
                UpperRot -= extensionSpeed * Time.deltaTime;
                break;
        }

        LowerRot = Mathf.Clamp(LowerRot, -30f, 56f);
        UpperRot = Mathf.Clamp(UpperRot, -56f, 30f);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, baseRotY, transform.rotation.eulerAngles.z);
        transform.GetChild(1).localRotation = transform.GetChild(2).localRotation = Quaternion.Euler(0, 0, LowerRot);
        transform.GetChild(1).GetChild(1).localRotation = Quaternion.Euler(0, 0, UpperRot - LowerRot);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    public GameObject receiver;
    public GameObject emitter;
    public LineRenderer laser;
    private Light laserDot;
    private ParticleSystem laserSparks;
    private int _layermask = 1 << 0 | 1 << 11;
    // Start is called before the first frame update
    void Start()
    {
        laser.transform.position = emitter.transform.position;
        // laser.enabled = false;
        laser.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CalcLaser(ParticleSystem laserSparks, Light laserDot)
    {
        Debug.Log("LaserReflector Hit");
        if (this.laserSparks == null) this.laserSparks = laserSparks;
        if (this.laserDot == null) this.laserDot = laserDot;

        RaycastHit laserHit;
        // if (LaserOn)
        // {
        laserDot.intensity = 30;
        Vector3 dir = laser.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(laser.transform.position, dir, out laserHit, Mathf.Infinity, _layermask))
        {
            float dist = Vector3.Distance(laser.transform.position, laserHit.point);
            laser.SetPosition(1, new Vector3(0, 0, dist));

            if (laserHit.collider.gameObject.tag == "doorpanel")
            {
                laserHit.collider.gameObject.GetComponent<DoorControlPanel>().SetHit(true);
            }
            else if (laserHit.collider.gameObject.layer == 11)
            {
                Reflect(laserHit, dir, 1);
            }
            else
            {
                laser.positionCount = 2;
                laserDot.transform.position = laserHit.point + (laserHit.normal * 0.005f);
                laserSparks.transform.position = laserHit.point + (laserHit.normal * 0.02f);
                laserSparks.transform.rotation = Quaternion.FromToRotation(Vector3.up, laserHit.normal);
            }
        }
        // }
        // else
        // {
        //     laser.SetPosition(1, new Vector3(0, 0, 0));
        //     laserDot.intensity = 0;
        //     laser.positionCount = 2;

        // }
    }

    void Reflect(RaycastHit laserHit, Vector3 dir, int num)
    {
        laser.positionCount = num + 2;
        RaycastHit reflectHit;
        Vector3 direction = Vector3.Reflect(dir, laserHit.normal);

        if (Physics.Raycast(laserHit.point, direction, out reflectHit, Mathf.Infinity, _layermask))
        {
            laser.SetPosition(laser.positionCount - 1, laser.transform.InverseTransformPoint(reflectHit.point));
            if (reflectHit.collider.gameObject.layer == 11)
            {
                Reflect(reflectHit, direction, num + 1);

            }
            else if (reflectHit.collider.gameObject.tag == "doorpanel")
            {
                reflectHit.collider.gameObject.GetComponent<DoorControlPanel>().SetHit(true);
            }
            else if (reflectHit.collider.gameObject.GetComponent<LaserReflector>())
            {
                reflectHit.collider.gameObject.GetComponent<LaserReflector>().CalcLaser(laserSparks, laserDot);
            }
            else
            {
                laserDot.transform.position = reflectHit.point + (reflectHit.normal * 0.005f);
                laserSparks.transform.position = reflectHit.point + (reflectHit.normal * 0.02f);
                laserSparks.transform.rotation = Quaternion.FromToRotation(Vector3.up, reflectHit.normal);
            }
        }
    }
}

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
    private int reflectCount;
    private int _layermask;

    void Start()
    {
        reflectCount = 0;
        baseRotY = transform.rotation.eulerAngles.y;
        LowerRot = UpperRot = 0;
        laser = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<LineRenderer>();
        laserDot = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(2).GetComponent<Light>();
        laserSparks = transform.GetChild(1).GetChild(1).GetChild(1).GetChild(3).GetComponent<ParticleSystem>();
        LaserOn = false;
        canToggle = true;
        _layermask = 1 << 0 | 1 << 11;
    }

    void Update()
    {
        CalcLaser();
    }

    void CalcLaser()
    {
        RaycastHit laserHit;
        if (LaserOn)
        {
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

        }
        else
        {
            laser.SetPosition(1, new Vector3(0, 0, 0));
            laserDot.intensity = 0;
            laser.positionCount = 2;

        }
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
            else
            {
                laserDot.transform.position = reflectHit.point + (reflectHit.normal * 0.005f);
                laserSparks.transform.position = reflectHit.point + (reflectHit.normal * 0.02f);
                laserSparks.transform.rotation = Quaternion.FromToRotation(Vector3.up, reflectHit.normal);
            }
        }
    }

    public void ToggleLaser()
    {
        if (canToggle)
        {
            if (!LaserOn) laserSparks.Play(); else laserSparks.Stop();
            LaserOn = !LaserOn;
            StartCoroutine(WaitToToggle());
        }
    }

    IEnumerator WaitToToggle()
    {
        canToggle = false;
        yield return new WaitForSeconds(0.5f);
        canToggle = true;
    }

    public void AdjustLaserAngles(Direction dir)
    {
        switch (dir)
        {
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

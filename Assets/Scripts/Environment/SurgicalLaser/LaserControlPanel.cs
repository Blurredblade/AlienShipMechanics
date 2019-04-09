using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControlPanel : MonoBehaviour
{
    public SurgicalLaser _surgicalLaser;
    private bool usable;

    void Start(){
        usable = false;
    }
    public void MoveLaser(Direction dir){
        if(usable)_surgicalLaser.AdjustLaserAngles(dir);
    }

    public void ToggleLaser(){
        if(usable)_surgicalLaser.ToggleLaser();
    }

    public void SetUsable(bool u){
        usable = u;
    }
}

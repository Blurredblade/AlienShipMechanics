using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour, Interactable
{
    public int requiredItem;
    private bool Open;
    private Transform Door1;
    private Transform Door2;
    private Quaternion startRot;
    private Quaternion targetRot1;
    private Quaternion targetRot2;


    

    void Start()
    {  
        Door1 = transform.GetChild(0);
        Door2 = transform.GetChild(1);
        startRot = Door1.rotation;
        targetRot1 = targetRot2 = startRot;
        targetRot1.y -= 60;
        targetRot2.y += 60;
        Open = false;
    }

    void Update()
    {
        if(Open){
            Door1.rotation = Quaternion.Lerp(Door1.rotation, targetRot1, 0.001f);
            Door2.rotation = Quaternion.Lerp(Door2.rotation, targetRot2, 0.001f);
        }
    }

    public void Activate(Player p){
        if(p.GetInventory().CheckForItem(requiredItem)){
            Open = true;
            GetComponent<BoxCollider>().enabled = false;
        }else{
            Debug.Log("You need the key!");
        }
    }


}

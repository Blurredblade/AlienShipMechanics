using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour, Interactable
{
    public int requiredItem;
    public LaserControlPanel panel;
    private GameObject _fuse;
    void Start()
    {
        _fuse = transform.GetChild(0).gameObject;
        _fuse.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Activate(Player p){
        if(p.GetInventory().CheckForItem(requiredItem)){
            _fuse.SetActive(true);
            panel.SetUsable(true);
            gameObject.layer = 0;
        }
    }
}

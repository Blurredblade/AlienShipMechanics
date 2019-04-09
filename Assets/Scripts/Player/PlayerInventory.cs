using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<InventoryItem> inventory;

    void Start(){
        inventory = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem i){
        inventory.Add(i);
    }
    
    public bool CheckForItem(int id){
        return inventory.Exists(i => i.itemId == id);
    }
}

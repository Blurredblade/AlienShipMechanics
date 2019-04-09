using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int itemId;
    public string itemName;

    public void PickUpItem(){
        Destroy(gameObject);
    }
}

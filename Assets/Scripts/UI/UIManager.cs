using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image _reticle;
    public Image _inventoryPanel;
    public Image[] _inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        _inventoryPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            ToggleInventory();
        }
    }

    void ToggleInventory(){
        _reticle.gameObject.SetActive(!_reticle.gameObject.activeSelf);
        _inventoryPanel.gameObject.SetActive(!_inventoryPanel.gameObject.activeSelf);
    }

}

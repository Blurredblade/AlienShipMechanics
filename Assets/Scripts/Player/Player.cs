using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Image _reticle = null;
    private PlayerInventory _inventory;
    private Camera _camera;
    private int _layermask;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _reticle.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _inventory = GetComponent<PlayerInventory>();
        _layermask = 1 << 9 | 1 << 10;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward), out hit, 3f, _layermask))
        {
            _reticle.GetComponent<RectTransform>().localScale = Vector3.Lerp(_reticle.GetComponent<RectTransform>().localScale, new Vector3(1, 1, 1), 0.15f);
            if (Input.GetKey(KeyCode.E))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    Interactable i = (Interactable)hit.collider.gameObject.GetComponent(typeof(Interactable));
                    i.Activate(this);
                }
                else if (hit.collider.gameObject.layer == 10)
                {
                    InventoryItem i = hit.collider.gameObject.GetComponent<InventoryItem>();
                    _inventory.AddItem(i);
                    i.PickUpItem();
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    if (hit.collider.gameObject.GetComponent<mirror>())
                    {
                        mirror mirror = hit.collider.gameObject.GetComponent<mirror>();
                        mirror.pull(this);
                    }
                }
            }
        }
        else
        {
            _reticle.GetComponent<RectTransform>().localScale = Vector3.Lerp(_reticle.GetComponent<RectTransform>().localScale, new Vector3(0.5f, 0.5f, 0.5f), 0.15f);
        }
    }

    public PlayerInventory GetInventory()
    {
        return _inventory;
    }
}

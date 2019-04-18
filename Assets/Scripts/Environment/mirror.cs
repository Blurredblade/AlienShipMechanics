using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour, Interactable
{
    private Player player;
    public void Activate(Player p)
    {
        if (!player)
            player = p;
        GetComponent<Rigidbody>().AddForce(new Vector3(p.transform.forward.normalized.x, 0, p.transform.forward.normalized.z), ForceMode.Impulse);
    }

    public void pull(Player p)
    {
        if (!player)
            player = p;
        GetComponent<Rigidbody>().AddForce(-new Vector3(p.transform.forward.normalized.x, 0, p.transform.forward.normalized.z), ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}

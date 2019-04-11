using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlPanel : MonoBehaviour
{
    public Door door;
    private bool isHit;
    private Material mat;
    private Color initialColor;
    
    void Start(){
        isHit = false;
        mat = GetComponent<MeshRenderer>().material;
        initialColor = mat.color;
    }
    void Update()
    {
        if(isHit){
            mat.color = Color.Lerp(mat.color, Color.black, 3f * Time.deltaTime);
        }
        isHit = false;
    }

    public void SetHit(bool hit){
        isHit = hit;
        if(hit){
            StartCoroutine(BurnTimer());
        }
    }

    IEnumerator BurnTimer(){
        yield return new WaitForSeconds(3f);
        if(isHit){
          door.SetOpen(true);
          Destroy(gameObject);  
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour, Interactable
{
    public enum ButtonType{
        Up,Down,Left,Right,Power
    }

    public ButtonType type;

    private Vector3 pressedPos;

    private Vector3 normalPos;
    private bool pressed;

    void Start(){
        normalPos = transform.localPosition;
        pressedPos = transform.localPosition - new Vector3(0,0.2f,0);
        pressed = false;
    }

    public void Activate(Player p){
        LaserControlPanel panel = transform.parent.GetComponent<LaserControlPanel>();
        pressed = true;
        switch(type){
            case ButtonType.Up:
                panel.MoveLaser(Direction.Up);
                break;
            case ButtonType.Down:
                panel.MoveLaser(Direction.Down);
                break;
            case ButtonType.Left:
                panel.MoveLaser(Direction.Left);
                break;
            case ButtonType.Right:
                panel.MoveLaser(Direction.Right);
                break;
            case ButtonType.Power:
                panel.ToggleLaser();
                break;
        }
    }

    void Update(){
        if(pressed){
            transform.localPosition = Vector3.Lerp(transform.localPosition, pressedPos, 0.2f);
        }else{
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPos, 0.2f);
        }
        pressed = false;
    }
}

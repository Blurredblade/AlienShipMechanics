using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    private int countDownMinute = 1;
    private float countDownSecond = 59;
    private bool countDownStart = false;
    private bool initialStart = true;
    public Text TimerDisplay;
    private bool lose = false;
    // Start is called before the first frame update
    void Start(){
        countDownStart = true;
    }

    // Update is called once per frame
    void Update()
    {
      
        

        if(countDownStart && !lose) {
            countDownSecond = Mathf.Clamp(countDownSecond-Time.deltaTime, 0, Mathf.Infinity);
            if(initialStart) {
                if(countDownMinute > 0 && countDownSecond == 0) {
               countDownMinute-=1;
               countDownSecond= 59;
            }
            
            if(countDownMinute != 0 && countDownSecond == 0) {
                countDownSecond = 59;
            }

            if(countDownMinute == 0 && countDownSecond == 0) {
                initialStart = false;
                countDownMinute=45;
                countDownSecond=59;
            }
            TimerDisplay.text = "You've been abducted. You have 45 minutes to get out. " + string.Format("{0}:{1}", countDownMinute, countDownSecond.ToString("00"));
            }
            else {
            if(countDownMinute > 0 && countDownSecond == 0) {
               countDownMinute-=1;
               countDownSecond=59;
            }
            
            if(countDownMinute != 0 && countDownSecond == 0) {
                countDownSecond = 59;
            }

            if(countDownMinute == 0 && countDownSecond == 0) {
                lose = true;
            }
            TimerDisplay.text = string.Format("{0}:{1}", countDownMinute, countDownSecond.ToString("00"));
            }
        }

        if(lose && !initialStart) {
           TimerDisplay.text = "You lose";
        }
    
    }
    
}

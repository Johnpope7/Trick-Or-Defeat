using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Variables
    private float timeDuration = 0f * 60f;
    private float timer;
    [SerializeField]
    private Text FirstMinute;
    [SerializeField]
    private Text SecondMinute;
    [SerializeField]
    private Text separator;
    [SerializeField]
    private Text FirstSecond;
    [SerializeField]
    private Text SecondSecond;
    #endregion

    #region BuiltIn Functions
    void Start()
    {
        ResetTimer();
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay(timer);
        
    }
    #endregion

    #region Custom Functions
    private void ResetTimer() 
    {
        timer = timeDuration;
    }
    private void UpdateTimerDisplay(float time) 
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        FirstMinute.text = currentTime[0].ToString();
        SecondMinute.text = currentTime[1].ToString();
        FirstSecond.text = currentTime[2].ToString();
        SecondSecond.text = currentTime[3].ToString();
    }
    #endregion
}

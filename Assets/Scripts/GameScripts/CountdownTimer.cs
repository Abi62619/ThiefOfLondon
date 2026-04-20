using UnityEngine;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 300;//change in dialog script too if change
    public TextMeshProUGUI timeText;

 void Update()
{
    
    timeRemaining -= Time.deltaTime;

    TimeSpan time = TimeSpan.FromSeconds(Mathf.Abs(timeRemaining));
    string negativeSign = timeRemaining < 0 ? "~" : "";

    timeText.text = negativeSign + time.ToString(@"mm\:ss");
    PlayerPrefs.SetFloat("TimeRemaining", timeRemaining);
    PlayerPrefs.Save();
}
}

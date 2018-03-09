using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    public float seconds;
    public float hours;

    private bool isPaused;



    private void Update()
    {
        if (isPaused) return;
        seconds += Time.deltaTime;
    }


    public void StartClock() {

    }

    public void PauseClock() {

    }

    public void ResetClock()
    {

    }
    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    public void OnGUI()
    {
        GUILayout.TextArea(FormatTime(Time.realtimeSinceStartup), 200);
    }
}

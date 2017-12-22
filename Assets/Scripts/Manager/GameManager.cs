using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public int GameFrameRate = 300;
    public float TimeScale { get; private set; }
    public bool Paused { get; set; }

    private float savedTimeScale = 1f;

    protected void Start()
    {
        Application.targetFrameRate = GameFrameRate;
    }

    public void Reset()
    {
        TimeScale = 1f;
        Paused = false;
    }

    public void SetTimeScale(float newTimeScale)
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = newTimeScale;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = savedTimeScale;
    }
}

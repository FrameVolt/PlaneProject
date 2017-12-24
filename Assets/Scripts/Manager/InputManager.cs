using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{ 
	
	void Update () {
        if (Input.GetButtonDown("Esc"))
        {
            Esc();
        }

        if (GameManager.Instance.Paused)
        {
            return;
        }

        SetMovement();
    }
    public virtual void SetMovement()
    {
  
        if (GameManager.Instance.Player == null) { return; }
        GameManager.Instance.Player.SetHorizontalMove(Input.GetAxis("Horizontal"));
        GameManager.Instance.Player.SetVerticalMove(Input.GetAxis("Vertical"));

    }
    public virtual void Esc()
    {
        //GameManager.Instance.Pause();
        if(UIManager.Instance.PauseManager)
        UIManager.Instance.PauseManager.Esc();
    }
}

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
        if (GameManager.Instance.Player == null)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            GameManager.Instance.Player.FireOnce();
        }
        if (Input.GetButton("Fire1"))
        {
            GameManager.Instance.Player.FireStart();
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
        if(UIManager.Instance.PauseManager)
        UIManager.Instance.PauseManager.Esc();
    }
}

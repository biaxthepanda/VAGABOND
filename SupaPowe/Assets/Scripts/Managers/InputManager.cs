using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Menu) return;


        if (Input.GetButtonDown("Cancel"))
        {
            // GameManager.Instance.ChangeState(GameManager.GameState.Menu);
        }
    }
}

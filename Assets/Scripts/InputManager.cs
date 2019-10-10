using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector2 dpad;
    public int inputCode;
    PlayerControls controls;

    

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        PlayerControls.GameplayActions player2 = controls.Gameplay;
        
        controls.Gameplay.move.performed += ctx => dpad = ctx.ReadValue<Vector2>();
        controls.Gameplay.move.canceled += ctx => dpad = Vector2.zero;

        controls.Gameplay.a.performed += ctx => inputCode = inputCode + 1000;
        controls.Gameplay.b.performed += ctx => inputCode = inputCode + 100;
        controls.Gameplay.x.performed += ctx => inputCode = inputCode + 10;
        controls.Gameplay.y.performed += ctx => inputCode = inputCode + 1;
        controls.Gameplay.Ltrigger.performed += ctx => inputCode = inputCode + 10000;
        controls.Gameplay.Rtrigger.performed += ctx => inputCode = inputCode + 100000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

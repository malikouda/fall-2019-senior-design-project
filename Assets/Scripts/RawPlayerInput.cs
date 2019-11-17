using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class RawPlayerInput : MonoBehaviour
{
    public class controllerInput
    {
        public Vector2 move;
        public bool x, y, a, b,start,lTrigger,rTrigger;
        public void resetinput()
        {
            x = false;
            y = false;
            a = false;
            b = false;
            start = false;
            lTrigger = false;
            rTrigger = false;
        }

    }

    //The unique color related to this player
    public PLAYERID playerId;

    public controllerInput controller;
    private PlayerControls controls;
    private PlayerInput inputComp;
    Vector2 move;
    public void Awake()
    {
        controller = new controllerInput();
        controls = new PlayerControls();
        controls.devices = GetComponent<PlayerInput>().user.pairedDevices;
        controls.Gameplay.move.performed += ctx => controller.move = ctx.ReadValue<Vector2>();
        controls.Gameplay.move.canceled += ctx => controller.move = Vector2.zero;
        controls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make this player persistant
        DontDestroyOnLoad(gameObject);


  
    }

    //Assign a color to the player
    public void assignId(PLAYERID newID)
    {
        playerId = newID;
    }

    //Assign control over the menu
    public void assignMenuControls(InputSystemUIInputModule module)
    {
        GetComponent<PlayerInput>().uiInputModule = module;
    }

    public void LateUpdate()
    {
        controller.resetinput();
    }

    //BELOW: Called from player input component 
    public void OnX()
    {
        Debug.Log("X");
        controller.x = true;
    }

    public void OnY()
    {
        Debug.Log("Y");
        controller.y = true;
    }

    public void OnA()
    {
        Debug.Log("A");
        controller.a = true;
    }

    public void OnB()
    {
        Debug.Log("B");
        controller.b = true;
    }

    public void OnStart()
    {
        controller.start = true;
    }

    public void OnLtrigger()
    {
        Debug.Log("Left trigger");
        controller.lTrigger = true;
    }

    public void OnRtrigger()
    {
        controller.rTrigger = true;
    }

}

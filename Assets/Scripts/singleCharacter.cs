using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class singleCharacter : MonoBehaviour
{
    public enum BUTTONS { X, Y, A, B };
    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;
    Rigidbody rb;
    Vector3 moveDirection;
    float inputAmount;
    bool touching = false;
    GameObject interactable;
    Vector2 move;
    minigame currntGame;
    controllerInput controller;

    PlayerControls controls;
    class controllerInput
    {
        public Vector2 move;
        public bool x;
        public bool y;
        public bool a;
        public bool b;
        public void resetinput()
        {
            move = Vector2.zero;
            x = false;
            y = false;
            a = false;
            b = false;
        }

    }

    private void Awake()
    {
        controller = new controllerInput();

        controls = new PlayerControls();
        controls.devices = GetComponent<PlayerInput>().user.pairedDevices;
        controls.Gameplay.move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.move.canceled += ctx => move = Vector2.zero;
        controller = new controllerInput();
        controls.Enable();
    }

    private void Start()
    {

    }

    private void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        Vector2 keyMove = new Vector2(horiz, vert);
        Vector2 m = (move + keyMove) * Time.deltaTime;
        Vector3 combinedInput = new Vector3(m.x, 0, m.y);

        moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);
        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        if (controller.x)
        {
            Debug.Log("X");
            if (currntGame != null)
            {
                currntGame.playerInput((int)BUTTONS.X);
            }
        }

        if (controller.y)
        {
            Debug.Log("y");
            if (currntGame != null)
            {
                currntGame.playerInput((int)BUTTONS.Y);
            }
        }

        if (controller.a)
        {
            if (currntGame != null)
            {
                currntGame.playerInput((int)BUTTONS.A);
            }
        }

        if (controller.b)
        {
            if (currntGame != null)
            {
                currntGame.playerInput((int)BUTTONS.B);
            }
        }

        controller.resetinput();
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            touching = true;
            interactable = other.gameObject;
        }
        if (other.tag == "minigame")
        {
            Debug.Log("enter game");
            currntGame = other.gameObject.GetComponent<minigame>();
            currntGame.startGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            touching = false;
            interactable = null;
        }
        if (other.tag == "minigame")
        {
            currntGame = null;
        }
    }

    public void OnX()
    {
        controller.x = true;
    }

    public void OnY()
    {
        controller.y = true;
    }

    public void OnB()
    {
        controller.b = true;
    }

    public void OnA()
    {
        controller.a = true;
    }
}

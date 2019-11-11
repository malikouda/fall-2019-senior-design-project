using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public enum BUTTONS {X,Y,A,B};
    public enum ITEMS {TABLET};
    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;
    [HideInInspector]
    public MazeRoom currentRoom;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public bool isActivated;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float inputAmount;
    private bool touching = false;
    private GameObject interactable;
    private Vector2 move;
    private minigame currentGame;
    private controllerInput controller;
    private MazeCell currentCell;
    private List<Character> players;
    private PlayerControls controls;

    class controllerInput
    {
        public Vector2 move;
        public bool x , y, a, b;
        public void resetinput()
        {
            move = Vector2.zero;
            x = false;
            y = false;
            a = false;
            b = false;
        }

    }

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = new Vector3(cell.transform.localPosition.x * 3f, 0.5f, cell.transform.localPosition.z * 3f);
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

    private void Start() {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Spawn(this);
        isActivated = true;
        players = new List<Character>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            Character c = g.GetComponent<Character>();
            if (c != null)
                players.Add(c);
        }
    }

    private void Update()
    {
        if (!isActivated)
        {
            rb.velocity = Vector3.zero;
            foreach (Character p in players)
            {
                if (p.isActivated && Vector3.Distance(transform.position,p.transform.position) < .01f)
                {
                    isActivated = true;
                    tag = "Player";
                    break;
                }
            }
            return;
        }

        Vector2 m = move * Time.deltaTime;
        Vector3 combinedInput = new Vector3(m.x, 0, m.y);

        moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);
        if (moveDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * rotateSpeed);
            transform.rotation = targetRotation;
        }

        if (controller.x)
        {
            Debug.Log("X");
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.X);
            }
        }

        if (controller.y)
        {
            Debug.Log("y");
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.Y);
            }
        }

        if (controller.a)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.A);
            }
        }

        if (controller.b)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.B);
            }
        }

        controller.resetinput();
    }

    private void FixedUpdate() {
        if (isActivated)
            rb.velocity = (moveDirection * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            touching = true;
            interactable = other.gameObject;
        }
        else if (other.tag == "minigame")
        {
            Debug.Log("enter game");
            currentGame = other.gameObject.GetComponent<minigame>();
            currentGame.startGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "minigame")
        {
            currentGame = null;
        }
        else if (other.tag == "roomChange" && gameManager.generateCeilings) {
            MazeRoom otherRoom = other.gameObject.GetComponentInParent<MazeCell>().room;
            if (otherRoom != currentRoom) {
                gameManager.activeRooms.Add(otherRoom);
                gameManager.activeRooms.Remove(currentRoom);
                if (!gameManager.activeRooms.Contains(currentRoom)) {
                    currentRoom.ceiling.GetComponent<Ceiling>().fadeIn = true;
                    currentRoom.ceiling.GetComponent<Ceiling>().fadeOut = false;
                    otherRoom.ceiling.GetComponent<Ceiling>().fadeOut = true;
                    otherRoom.ceiling.GetComponent<Ceiling>().fadeIn = false;

                }
                else {
                    otherRoom.ceiling.GetComponent<Ceiling>().fadeOut = true;
                    otherRoom.ceiling.GetComponent<Ceiling>().fadeIn = false;
                }
                currentRoom = otherRoom;
            }
        }
    }

    //the player can't move or interact with the world until another player unties them
    public void immobilize ()
    {
        rb.velocity = Vector3.zero;

        isActivated = false;
    }

    //These Functions are for the controller, if you need input use the controller class

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
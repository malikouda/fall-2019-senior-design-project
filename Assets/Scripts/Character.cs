using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ITEMS {TABLET};
    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;
    [HideInInspector]
    public MazeRoom currentRoom;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public bool isActivated;


    private RawPlayerInput.controllerInput inputDevice;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float inputAmount;
    private bool touching = false;
    private GameObject interactable;
    private Vector2 move;
    private minigame currentGame;
    private Animator anim;
    private MazeCell currentCell;
    private List<Character> players;




    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = new Vector3(cell.transform.localPosition.x * 3f, 0.5f, cell.transform.localPosition.z * 3f);
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponentInChildren<Animator>();
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
                float d = Vector3.Distance(transform.position, p.transform.position);
                if (p.isActivated &&  d < 1.5f)
                {
                    
                    isActivated = true;
                    tag = "Player";
                    GameManager.instance.releasePlayer();
                    break;
                }
            }
            return;
        }

        if (rb.velocity.magnitude >= .01f)
        {
            anim.SetBool("Walking", true);
        }else
        {
            anim.SetBool("Walking", false);
        }


        Vector2 m = inputDevice.move * Time.deltaTime;
        Vector3 combinedInput = new Vector3(m.x, 0, m.y);
        
        moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);
        if (moveDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * rotateSpeed);
            transform.rotation = targetRotation;
        }

        

        if (inputDevice.x)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.X);
            }
        }

        if (inputDevice.y)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.Y);
            }
        }

        if (inputDevice.a)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.A);
            }
        }

        if (inputDevice.b)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.B);
            }
        }

        if (inputDevice.lTrigger)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.LTRIGGER);
            }
        }

        if (inputDevice.rTrigger)
        {
            if (currentGame != null)
            {
                currentGame.playerInput((int)BUTTONS.RTRIGGER);
            }
        }
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
            currentGame.endGame();
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

    public void assignController(RawPlayerInput input)
    {
        inputDevice = input.controller;
        switch (input.playerId)
        {
            case PLAYERID.BLUE:
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
                    break;
                }
            case PLAYERID.RED:
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                    break;
                }
            case PLAYERID.GREEN:
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
                    break;
                }
            case PLAYERID.YELLOW:
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
                    break;
                }
        }
    }


}
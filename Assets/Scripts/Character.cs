using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public enum BUTTONS {X,Y,A,B};
    public enum ITEMS {TABLET};
    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;
    public MazeRoom currentRoom;
    public GameManager gameManager;


    Rigidbody rb;
    Vector3 moveDirection;
    float inputAmount;
    bool touching = false;
    GameObject interactable;
    Vector2 move;
    minigame currntGame;
    controllerInput controller;
    MazeCell currentCell;

    PlayerControls controls;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = new Vector3(cell.transform.localPosition.x * 3f, 0.5f, cell.transform.localPosition.z * 3f);
    }
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

    private void Update()
    {
        
        Vector2 m = move * Time.deltaTime;
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
        if (other.tag == "roomChange") {
            MazeRoom otherRoom = other.gameObject.GetComponentInParent<MazeCell>().room;
            if (otherRoom != currentRoom) {
                gameManager.activeRooms.Add(otherRoom);
                gameManager.activeRooms.Remove(currentRoom);
                if (!gameManager.activeRooms.Contains(currentRoom)) {
                    currentRoom.ceiling.gameObject.SetActive(true);
                    otherRoom.ceiling.gameObject.SetActive(false);
                }
                else {
                    otherRoom.ceiling.gameObject.SetActive(false);
                }
                currentRoom = otherRoom;
            }
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
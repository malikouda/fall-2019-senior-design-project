using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour {

    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;


    PlayerControls controls;
    Rigidbody rb;
    Vector3 moveDirection;
    float inputAmount;
    bool touching = false;
    GameObject interactable;
    Vector2 move;
    bool x;
    bool y;
    bool a;
    bool b;
    bool ltrigger;
    bool rtrigger;
    bool pressedButton;
    minigame currntGame;
    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.a.performed += ctx => pressButton(2);
        controls.Gameplay.b.performed += ctx => pressButton(3);
        controls.Gameplay.x.performed += ctx => pressButton(0);
        controls.Gameplay.y.performed += ctx => pressButton(1);
        controls.Gameplay.Ltrigger.performed += ctx => pressButton(4);
        controls.Gameplay.Rtrigger.performed += ctx => pressButton(5);
    }

    private void Start() {
    }
   
    void pressButton(int button)
    {
        pressedButton = true;
        switch (button)
        {
            
            case 0:
            {
                x = true;
                break;
            }
            case 1:
            {
                y = true;
                break;
            }
            case 2:
            {
                a = true;
                break;
            }
            case 3:
            {
                b = true;
                break;
            }
            case 4:
            {
                ltrigger = true;
                break;
            }
            case 5:
            {
                rtrigger = true;
                break;
            }
            default:
            {
                pressedButton = false;
                break;
            }

        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Update() {

        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        Vector3 combinedInput = new Vector3(m.x, 0, m.y);

        moveDirection = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);
        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        if (a)
        {
            if (currntGame != null)
            {
                currntGame.playerInput(2);
            }
            //Debug.Log("pressed a");
        }

        if (b)
        {
            if (currntGame != null)
            {
                currntGame.playerInput(3);
            }
            //Debug.Log("pressed b");
        }

        if (x)
        {
            if (currntGame != null)
            {
                currntGame.playerInput(0);
            }
            //Debug.Log("pressed x");
        }

        if (y)
        {
            if (currntGame != null)
            {
                currntGame.playerInput(1);
            }
            //Debug.Log("pressed y");
        }

        if (ltrigger)
        {
            //Debug.Log("pressed left trigger");
        }

        if (rtrigger)
        {
            //Debug.Log("pressed right trigger");
        }


        a = false;
        b = false;
        x = false;
        y = false;
        ltrigger = false;
        rtrigger = false;
        pressedButton = false;
        return;

        float inputMagnitude = Mathf.Abs(m.x) + Mathf.Abs(m.y);
        inputAmount = Mathf.Clamp01(inputMagnitude);

        if (moveDirection != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotateSpeed);
            transform.rotation = targetRotation;
        }

        if (touching && Input.GetKeyDown(KeyCode.E) && interactable) {
            interactable.GetComponent<Interactable>().Activate(this.gameObject);
            interactable.GetComponent<Interactable>().Activate(this.gameObject);
        }



    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Interactable") {
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

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Interactable") {
            touching = false;
            interactable = null;
        }
        if (other.tag == "minigame")
        {
            currntGame = null;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Maze mazePrefab;
    public Character playerPrefab;
    [HideInInspector]
    public List<Character> players = new List<Character>();
    [HideInInspector]
    public List<MazeRoom> activeRooms = new List<MazeRoom>();
    public bool generateCeilings = false;
    public bool restart = false;
    public int numObjectives;
    [Tooltip("The objective text")]
    public Text objText;
    [Tooltip("The Objective animator")]
    public Animator objanim;
    public Animator winScreen;
    public Animator loseScreen;
    public InputSystemUIInputModule inputModule;
    
    private int maxObjectives;
    private int numPlayers;
    private Maze mazeInstance;
    private int numAlertedGuards;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start() {
        BeginGame();
        maxObjectives = numObjectives;
        displayObjectives();
    }

    void Update() {
        if (restart) {
            RestartGame();
        }
    }

    private void BeginGame() {
        restart = false;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.Generate();
        mazeInstance.gameObject.transform.localScale *= 3;
        foreach(RawPlayerInput p in GameObject.FindObjectsOfType<RawPlayerInput>())
        {
            GameObject playerOBJ = Instantiate(playerPrefab.gameObject);
            Character player = playerOBJ.GetComponent<Character>();
            player.assignController(p);
            Spawn(player);
        }
        Invoke("paths",1);
    }


    private void paths()
    {
        patrolCreation.createPatrols();
    }

    public void Spawn(Character playerInstance) {
        players.Add(playerInstance);
        MazeCell startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
        startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
        playerInstance.SetLocation(startingCell);
        playerInstance.currentRoom = startingCell.room;
        if (generateCeilings) {
            activeRooms.Add(playerInstance.currentRoom);
            playerInstance.currentRoom.ceiling.GetComponent<Ceiling>().fadeOut = true;
        }
        ++numPlayers;
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void catchPlayer()
    {
        --numPlayers;
        if (numPlayers <= 0)
        {
            foreach(GameObject p in GameObject.FindGameObjectsWithTag("playerInput"))
            {
                PlayerInput i = p.GetComponent<PlayerInput>();
                i.uiInputModule = inputModule;
            }
            loseScreen.gameObject.SetActive(true);
            loseScreen.SetTrigger("Lose");
        }
    }

    //When a player is released
    public void releasePlayer()
    {
        ++numPlayers;
    }

    //When a minigame is completed
    public void completedMinigame()
    {
        --numObjectives;
        displayObjectives();
    }

    public void wonGame()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("playerInput"))
        {
            PlayerInput i = p.GetComponent<PlayerInput>();
            i.uiInputModule = inputModule;
        }
        winScreen.gameObject.SetActive(true);
        winScreen.SetTrigger("Win");
    }

    //Shows how many objectives are left
    public void displayObjectives()
    {
        if (numObjectives == 0)
            objText.text = "Diamond is open!";
        else
            objText.text = numObjectives + " security measures left";
        objanim.SetTrigger("display");
    }

    public void returnToMenu()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("playerInput"))
        {
            Destroy(player);
            SceneManager.LoadScene(0);
        }
    }

    //When the players alert the guards
    public void alertGuard()
    {
        //If there are currently no alerted guards, this is new, play alert
        if (numAlertedGuards == 0)
        {
            GetComponent<AudioFade>().suddenChange();
        }
        ++numAlertedGuards;
        Debug.Log(numAlertedGuards);
    }

    //When the players escape the guards
    public void evadeGuard()
    {
        --numAlertedGuards;
        //If there are currently no more alerted guards, go back to normal
        if (numAlertedGuards == 0)
        {
            GetComponent<AudioFade>().fadeSounds();
        }
    }
}

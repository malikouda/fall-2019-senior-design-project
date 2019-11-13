using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public Text objText;
    public Animator winScreen;
    public Animator loseScreen;

    private int maxObjectives;
    private int numPlayers;
    private Maze mazeInstance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start() {
        BeginGame();
        maxObjectives = numObjectives;
        objText.text = "Objectives: " + numObjectives + "/" + maxObjectives;

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

    private void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void catchPlayer()
    {
        --numPlayers;
        if (numPlayers <= 0)
        {
            //This is where the game over screen goes
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
        objText.text = "Objectives: " + numObjectives + "/" + maxObjectives;
    }

    public void wonGame()
    {
        winScreen.SetTrigger("Win");
    }
}

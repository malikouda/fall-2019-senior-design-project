using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Character playerPrefab;

    public static GameManager instance;

    [HideInInspector]
    public List<Character> players = new List<Character>();
    [HideInInspector]
    public List<MazeRoom> activeRooms = new List<MazeRoom>();

    public bool generateCeilings = false;

    public bool restart = false;

    public int numObjectives;

    private int numPlayers;

    void Start() {
        BeginGame();
        if (instance == null)
            instance = this;
    }

    void Update() {
        if (restart) {
            RestartGame();
        }
        if (numObjectives <= 0) {
            RestartGame();
        }
    }

    private void BeginGame() {
        restart = false;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.Generate();
        mazeInstance.gameObject.transform.localScale *= 3;
        numObjectives = mazeInstance.numObjectives;
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
            Debug.Log("Game Over");
        }
    }

    public void releasePlayer()
    {
        ++numPlayers;
    }
}

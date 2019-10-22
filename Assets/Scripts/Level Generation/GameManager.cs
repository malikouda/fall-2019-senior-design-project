using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Character playerPrefab;

    [HideInInspector]
    public List<Character> players = new List<Character>();
    [HideInInspector]
    public List<MazeRoom> activeRooms = new List<MazeRoom>();

    public bool generateCeilings = false;

    public bool restart = false;

    public int numObjectives;

    void Start() {
        BeginGame();
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
    }

    private void RestartGame() {
        SceneManager.LoadScene(0);
    }
}

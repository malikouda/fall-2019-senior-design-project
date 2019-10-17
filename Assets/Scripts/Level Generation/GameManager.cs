using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Character playerPrefab;

    [HideInInspector]
    public List<Character> players = new List<Character>();
    [HideInInspector]
    public List<MazeRoom> activeRooms = new List<MazeRoom>();

    void Start() {
        BeginGame();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            Spawn();
        }

    }

    private void BeginGame() {
        
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.Generate();
        mazeInstance.gameObject.transform.localScale *= 3;
        patrolCreation.instance.makePatrols();
    }

    public void Spawn() {
        MazeCell startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
        Character playerInstance = Instantiate(playerPrefab) as Character;
        startingCell = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
        playerInstance.SetLocation(startingCell);
        playerInstance.gameManager = this;
        playerInstance.currentRoom = startingCell.room;
        if (activeRooms.Contains(playerInstance.currentRoom) != true) {
            activeRooms.Add(playerInstance.currentRoom);
            playerInstance.currentRoom.ceiling.gameObject.SetActive(false);
        }
    }

    private void RestartGame() {
        Destroy(mazeInstance.gameObject);
        foreach(Character player in players) {
            if (player != null) {
                Destroy(player.gameObject);
            }
        }
        BeginGame();
    }

}

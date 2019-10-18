using UnityEngine;
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

    void Start() {
        BeginGame();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RestartGame();
        }
    }

    private void BeginGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.Generate();
        mazeInstance.gameObject.transform.localScale *= 3;
        //patrolCreation.instance.makePatrols();
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
        Destroy(mazeInstance.gameObject);
        foreach (Character player in players) {
            if (player != null) {
                Destroy(player.gameObject);
            }
        }
        BeginGame();
    }

    public void SetLayerRecursively(GameObject obj, int newLayer) {
        if (null == obj) {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform) {
            if (null == child) {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}

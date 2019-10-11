using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Character playerPrefab;

    private Character playerInstance;

    void Start() {
        BeginGame();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }    
    }

    private void BeginGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.Generate();
        mazeInstance.gameObject.transform.localScale *= 3;
        playerInstance = Instantiate(playerPrefab) as Character;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    private void RestartGame() {
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null) {
            Destroy(playerInstance.gameObject);
        }
        BeginGame();
    }

}

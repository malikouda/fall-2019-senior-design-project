using UnityEngine;

public class MazeDoor : MazePassage {

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction) {
        base.Initialize(primary, other, direction);
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.name != "Trigger") {
                child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
            }
        }
    }
}

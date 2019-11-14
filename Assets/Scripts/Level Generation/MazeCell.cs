using UnityEngine;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    private int initializedEdgeCount;

    public MazeRoom room;

    public bool occupied = false;

    public void Initialize(MazeRoom room) {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    public bool IsFullyInitialized {
        get {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public MazeCellEdge GetEdge(MazeDirection direction) {
        return edges[(int)direction];
    }

    public MazeDirection RandomInitializedDirection {
        get {
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] != null) {
                    return (MazeDirection)i;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no initialized directions.");
        }
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    public MazeDirection RandomUninitializedDirection {
        get {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }
}

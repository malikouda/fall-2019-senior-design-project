using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour {

    public IntVector2 size;
    
    public MazeCell cellPrefab;
    private MazeCell[,] cells;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public MazeDoor doorPrefab;
    public GameObject ceilingPrefab;
    public GameObject ceilingCellPrefab;
    public GameObject mazeRoomPrefab;
    public MazeArt artPrefab;
    public int numObjectives;
    [Tooltip("The final objective prefab")]
    public GameObject finalObjectivePrefab;
    [Tooltip("All the objective prefabs")]
    public GameObject[] objectivePrefabs;
    [Range(0f, 1f)]
    public float doorProbability;
    [Range(0f, 0.2f)]
    public float artProbability;
    

    public MazeRoomSettings[] roomSettings;

    public List<MazeRoom> rooms = new List<MazeRoom>();
    [HideInInspector]
    public GameManager gameManager;

    //percentage more likely art is to spawn against a wall
    private const float wallIncentive = .3f;

    //percentage less likely art is to spawn in open cells
    private const float middleDetterant = .5f;

    //precentage more likely art is to spawn in corners or dead ends
    private const float cornerOrDeadEndIncentive = 1.0f;

    public void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }

    public void Generate() {
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            DoNextGenerationStep(activeCells);
        }

        List<MazeRoom> objRooms = new List<MazeRoom>();

        for (int i = 0; i < rooms.Count; i++) {
            GameObject mazeRoom = Instantiate(mazeRoomPrefab) as GameObject;
            mazeRoom.name = "Maze Room " + (i + 1);
            mazeRoom.transform.parent = transform;
            rooms[i].roomName = mazeRoom.name;
            rooms[i].parent = mazeRoom;
            if (gameManager.generateCeilings) {
                GameObject ceiling = Instantiate(ceilingPrefab) as GameObject;
                ceiling.transform.localPosition = mazeRoom.transform.localPosition;
                ceiling.transform.parent = mazeRoom.transform;
                ceiling.name = "Ceiling";
                rooms[i].ceiling = ceiling;
                foreach (MazeCell cell in rooms[i].cells) {
                    cell.transform.parent = mazeRoom.transform;
                    GameObject ceilingCell = Instantiate(ceilingCellPrefab) as GameObject;
                    ceilingCell.name = "Ceiling Cell " + cell.coordinates.x + ", " + cell.coordinates.z;
                    ceilingCell.transform.localPosition = new Vector3(cell.coordinates.x - size.x * 0.5f + 0.5f, 1.1f, cell.coordinates.z - size.z * 0.5f + 0.5f);
                    ceilingCell.transform.parent = ceiling.transform;

                   /*
                    if (Random.value < artProbability && !CellNextToDoor(cell) && !cell.occupied && !CellNextToObject(cell)) {
                        cell.occupied = true;
                        MazeArt prefab = Instantiate(artPrefab) as MazeArt;
                        prefab.transform.position = new Vector3(cell.transform.position.x, 0.258f, cell.transform.position.z);
                        prefab.transform.parent = cell.transform.parent;
                    }
                    */
                    
                    //generateArt(mazeRoom, i);
                    
                }

                MeshFilter[] meshFilters = rooms[i].ceiling.GetComponentsInChildren<MeshFilter>();
                CombineInstance[] combine = new CombineInstance[meshFilters.Length];

                int j = 0;
                while (j < meshFilters.Length) {
                    combine[j].mesh = meshFilters[j].sharedMesh;
                    combine[j].transform = meshFilters[j].transform.localToWorldMatrix;
                    meshFilters[j].gameObject.SetActive(false);
                    if (meshFilters[j].gameObject.tag == "ceilingCell") {
                        Destroy(meshFilters[j].gameObject);
                    }

                    j++;
                }
                rooms[i].ceiling.transform.GetComponent<MeshFilter>().mesh = new Mesh();
                rooms[i].ceiling.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                rooms[i].ceiling.transform.gameObject.SetActive(true);
            }
            generateArt(mazeRoom, i);

            if (rooms[i].size > 1) {
                if (RoomHasValidPlacement(rooms[i])) {
                    findEligibleCells(rooms[i]);
                    if (rooms[i].eligibleCells.Count > 0) {
                        objRooms.Add(rooms[i]);
                    }
                }
            }
        }


        //Spawn the objectives
        spawnObjective(finalObjectivePrefab, objRooms);
        numObjectives = gameManager.numObjectives;
        for (int i = 0; i < numObjectives; i++) {
            spawnObjective(objectivePrefabs[Random.Range(0,objectivePrefabs.Length)], objRooms);
        }
    }

    private void generateArt(GameObject mazeRoom, int i)
    {
        foreach (MazeCell cell in rooms[i].cells)
        {
            float randomValueWeighted = Random.value;
            MazeDirection wallDirection = MazeDirection.Null;
            //weight for walls next to wall and against those in the middle
            randomValueWeighted -= CellNextToWall(cell, out wallDirection) ? wallIncentive : -middleDetterant;
            //weight for corners and dead ends
            if (CellinDeadEndOrCorner(cell))
                randomValueWeighted -= cornerOrDeadEndIncentive;

            if (randomValueWeighted < artProbability && !CellNextToDoor(cell) && !cell.occupied && !CellNextToObject(cell) && (!CellTooNarrow(cell) || CellinDeadEndOrCorner(cell)))
            {
                cell.occupied = true;
                MazeArt prefab = Instantiate(artPrefab) as MazeArt;
                prefab.transform.position = new Vector3(cell.transform.position.x, 0.258f, cell.transform.position.z);
                prefab.transform.parent = cell.transform.parent;
                if (wallDirection != MazeDirection.Null)
                {
                    switch (wallDirection)
                    {
                        case MazeDirection.North:
                            {
                                //prefab.transform.Translate(new Vector3(0, 0, .1f));
                                break;
                            }
                        case MazeDirection.South:
                            {
                                prefab.transform.Rotate(new Vector3(prefab.transform.rotation.x, 90, prefab.transform.rotation.z));
                                break;
                            }
                        case MazeDirection.East:
                            {
                                prefab.transform.Rotate(new Vector3(prefab.transform.rotation.x, 180, prefab.transform.rotation.z));
                                break;
                            }
                        case MazeDirection.West:
                            {
                                prefab.transform.Rotate(new Vector3(prefab.transform.rotation.x, 90, prefab.transform.rotation.z));
                                break;
                            }
                    }
                }
            }
        }
    }

    private void findEligibleCells(MazeRoom room) {
        for (int i = 0; i < room.cells.Count; i++) {
            MazeCell cell = room.cells[i];
            if (!CellNextToDoor(cell) && !CellNextToObject(cell) && !cell.occupied) {
                room.eligibleCells.Add(cell);
            }
        }
    }

    private void spawnObjective(GameObject objectivePrefabInst, List<MazeRoom> objRooms) {
        int randomRoomIndex = Random.Range(0, objRooms.Count);
        MazeRoom randomRoom = objRooms[randomRoomIndex];

        int randomCellIndex = Random.Range(0, randomRoom.eligibleCells.Count);
        MazeCell randomCell = randomRoom.eligibleCells[randomCellIndex];

        randomCell.occupied = true;

        Vector3 objPosition = randomRoom.eligibleCells[randomCellIndex].transform.position;
        GameObject objective = Instantiate(objectivePrefabInst) as GameObject;
        objective.transform.position = objPosition * 3;
        objRooms.Remove(randomRoom);
        randomRoom.eligibleCells.Remove(randomCell);
    }

    private bool CellNextToDoor(MazeCell cell) {
        bool nextToDoor = false;
        MazeDirection[] directions = new MazeDirection[4] { MazeDirection.North, MazeDirection.East, MazeDirection.South, MazeDirection.West };
        for (int i = 0; i < 4; i++) {
            if (cell.GetEdge(directions[i]) is MazeDoor) {
                nextToDoor = true;
                break;
            }
        }
        return nextToDoor;
    }

    private bool CellNextToWall(MazeCell cell, out MazeDirection wall)
    {
        MazeDirection[] directions = new MazeDirection[4] { MazeDirection.North, MazeDirection.East, MazeDirection.South, MazeDirection.West };
        for (int i = 0; i < 4; i++)
        {
            if (cell.GetEdge(directions[i]) is MazeWall)
            {
                wall = directions[i];
                return true;
            }
        }
        
        wall = MazeDirection.Null;
        return false;
    }

    //Checks if the cell is in a dead end or a corner
    private bool CellinDeadEndOrCorner(MazeCell cell)
    {
        //check the numbe of walls surrounding the cell
        int numWalls = 0;
        MazeDirection[] directions = new MazeDirection[4] { MazeDirection.North, MazeDirection.East, MazeDirection.South, MazeDirection.West };
        for (int i = 0; i < 4; i++)
        {
            if (cell.GetEdge(directions[i]) is MazeWall)
            {
                ++numWalls;
            }
        }

        //If the cell has 3 walls, it's a dead end
        if (numWalls == 3)
            return true;
        
        //If the cell has 2 walls, but they aren't opposite each other, it's in a corner
        if (numWalls == 2 && !CellTooNarrow(cell))
            return true;
        else
            return false;

    }

    //checks if the cell is in a 1x1 corridor
    private bool CellTooNarrow(MazeCell cell)
    {
        bool NS = cell.GetEdge(MazeDirection.North) is MazeWall && cell.GetEdge(MazeDirection.South) is MazeWall;
        bool EW = cell.GetEdge(MazeDirection.East) is MazeWall && cell.GetEdge(MazeDirection.West) is MazeWall;
        return NS || EW;
    }


    private bool CellNextToObject(MazeCell cell) {
        bool nextToObject = false;
        MazeDirection[] directions = new MazeDirection[4] { MazeDirection.North, MazeDirection.East, MazeDirection.South, MazeDirection.West };
        for (int i = 0; i < 4; i++) {
            if (cell.GetEdge(directions[i]).otherCell != null) {
                if (cell.GetEdge(directions[i]).otherCell.occupied) {
                    nextToObject = true;
                    break;
                }
            }
        }
        return nextToObject;
    }

    private bool RoomHasValidPlacement(MazeRoom room) {
        int numValidCells = room.cells.Count;
        for (int i = 0; i < room.cells.Count; i++) {
            if (CellNextToDoor(room.cells[i])) {
                numValidCells--;
                if (numValidCells < 1) {
                    return false;
                }
            }
        }
        return true;
    }

    private MazeCell CreateCell(IntVector2 coordinates) {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(Random.Range(1, size.x), Random.Range(1, size.z));
        }
    }

    private bool ContainsCoordinates(IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z > 0 && coordinate.z < size.z;
    }

    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.z];
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells) {
        MazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells) {
        int currentIndex = Random.Range(0, activeCells.Count - 1);
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates)) {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null) {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else {
                CreateWall(currentCell, neighbor, direction);
            }
        } else {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        if (passage is MazeDoor) {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room) {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private MazeRoom CreateRoom(int indexToExclude) {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
        if (newRoom.settingsIndex == indexToExclude) {
            newRoom.settingsIndex = ((newRoom.settingsIndex + 1) % roomSettings.Length);
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }
}

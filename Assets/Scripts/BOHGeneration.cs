using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BOHGeneration : MonoBehaviour
{
    [Tooltip("The number of 'units' on the x axis")]
    public int xGrid;
    [Tooltip("The number of 'units' on the z axis")]
    public int zGrid;
    [Tooltip("The side length of each grid unit")]
    public float GridSize;

    [Tooltip("Draw the grid with gizmos")]
    public bool drawGrid;

    public Text debugText;
    public room[] roomPrefabs;
    public GameObject oneByOnePrefab;

    private GameObject BoHObject;
    [System.Serializable]
    public class room
    {
        public GameObject prefab;
        public int xLen;
        public int zLen;

        public void createRoom(int WorldX, int WorldZ)
        {
            Instantiate(prefab, new Vector3(WorldX, 0, WorldZ),Quaternion.identity);
        }
    }

    //the number assosciated with the last room created
    int currentRoomNumber = 0;
    Dictionary<Vector2, nSizeRoom> grid;
    // Start is called before the first frame update
    void Start()
    {
        BoHObject = new GameObject();
        BoHObject.name = "BackOfHouse";
        grid = new Dictionary<Vector2, nSizeRoom>();
        List<nSizeRoom> rooms = new List<nSizeRoom>();
        //for all the defined room prefabs
        foreach (room prefab in roomPrefabs)
        {
            //randomly add rooms until no possible spot exists
            int failsafe = 0;
            Vector2 nextPosition = pickRandomEmptySpot(prefab.xLen, prefab.zLen);
            while (nextPosition != new Vector2(-1,-1) && failsafe < 100)
            {
                failsafe++;
                rooms.Add(createAtGrid((int)nextPosition.x, (int)nextPosition.y, prefab.xLen, prefab.zLen,prefab.prefab));
                nextPosition = pickRandomEmptySpot(prefab.xLen, prefab.zLen);
            }
        }

        //fill the remaining empty spaces

        for (int x = 0; x <= xGrid; x++)
        {
            for (int z = 0; z <= zGrid; z++)
            {
                if (!grid.ContainsKey(new Vector2(x, z)))
                {
                    rooms.Add(createAtGrid(x, z, 1, 1, oneByOnePrefab));
                }
            }
        }
        //merge duplicate rooms
        foreach(nSizeRoom room in rooms)
        {
            mergeRooms(room);
        }

        //set room ids to reflect merged rooms
        int newID = 0;
        foreach (Transform child in BoHObject.transform)
        {
            nSizeRoom oldRoom = child.GetComponent<nSizeRoom>();
            if (oldRoom == null)
            {
                nSizeRoom[] childRooms = child.GetComponentsInChildren<nSizeRoom>();
                foreach(nSizeRoom childroom in childRooms)
                {
                    childroom.roomId = newID;
                }
            }else
            {
                oldRoom.roomId = newID;
            }
            newID++;
        }
        
        //make doorways
        for (int x = 0; x <= xGrid; x++)
        {
            for (int z = 0; z <= zGrid; z++)
            {
                if (grid[new Vector2(x,z)] != null)
                {
                    Vector2 roomsDir = SurroundingRooms(x, z);
                    if ( roomsDir != Vector2.zero)
                    {
                        nSizeRoom origRoom = grid[new Vector2(x, z)];
                        origRoom.makeDoorwayTo(roomsDir);
                        grid[roomsDir].makeDoorwayTo(new Vector2(x, z));
                        changeRoomIds(grid[roomsDir].roomId, origRoom.roomId);
                    }
                }

            }
        }
    }


    private void changeRoomIds(int oldId, int newID)
    {
        foreach (nSizeRoom room in BoHObject.GetComponentsInChildren<nSizeRoom>())
        {
            if(room.roomId == oldId)
            {
                room.roomId = newID;
            }
        }
    }


    private void mergeRooms(nSizeRoom room)
    {
        int x = room.xCoord;
        int z = room.zCoord;
        bool hasSurrounding = false;
        //Check West
        Vector2 comp = new Vector2(x - 1, z);
        //if there's something in that spot
        if (grid.ContainsKey(comp))
        {
            //and it's not the same room
            if (room.roomId != grid[comp].roomId)
            {
                //and it's the same size as the compared room
                if (room.xSize == grid[comp].xSize && room.zSize == grid[comp].zSize)
                {
                    //and it is directly next to the other room
                    if (x == grid[comp].xCoord || z == grid[comp].zCoord)
                    {
                        //delete the walls and make one big room
                        hasSurrounding = true;
                        room.deleteWest();
                        grid[comp].deleteEast();
                        if (room.gameObject.transform.parent == BoHObject.transform)
                        {
                            GameObject parent = new GameObject();
                            parent.name = "Room " + room.roomId;
                            parent.tag = "room";
                            room.gameObject.transform.parent = parent.transform;
                            parent.transform.parent = BoHObject.transform;
                            grid[comp].transform.parent = parent.transform;
                        }else
                        {
                            grid[comp].transform.parent = room.transform.parent;
                        }
                    }
                }
            }
        }
        //Check East
        comp = new Vector2(x + 1, z);
        if (grid.ContainsKey(comp))
        {
            if (room.roomId != grid[comp].roomId)
            {
                if (room.xSize == grid[comp].xSize && room.zSize == grid[comp].zSize)
                {
                    if (x == grid[comp].xCoord || z == grid[comp].zCoord)
                    {
                        hasSurrounding = true;
                        room.deleteEast();
                        grid[comp].deleteWest();
                        if (room.gameObject.transform.parent == BoHObject.transform)
                        {
                            GameObject parent = new GameObject();
                            parent.name = "Room " + room.roomId;
                            parent.tag = "room";
                            room.gameObject.transform.parent = parent.transform;
                            parent.transform.parent = BoHObject.transform;
                            grid[comp].transform.parent = parent.transform;
                        }
                        else
                        {
                            grid[comp].transform.parent = room.transform.parent;
                        }
                    }
                }
            }
        }
        //Check North
        comp = new Vector2(x, z+1);
        if (grid.ContainsKey(comp))
        {
            if (room.roomId != grid[comp].roomId)
            {
                if (room.xSize == grid[comp].xSize && room.zSize == grid[comp].zSize)
                {
                    if (room.xCoord == grid[comp].xCoord || room.zCoord == grid[comp].zCoord)
                    {
                        hasSurrounding = true;
                        room.deleteNorth();
                        grid[comp].deleteSouth();
                        if (room.gameObject.transform.parent == BoHObject.transform)
                        {
                            GameObject parent = new GameObject();
                            parent.name = "Room " + room.roomId;
                            parent.tag = "room";
                            room.gameObject.transform.parent = parent.transform;
                            parent.transform.parent = BoHObject.transform;
                            grid[comp].transform.parent = parent.transform;
                        }
                        else
                        {
                            grid[comp].transform.parent = room.transform.parent;
                        }
                    }
                }
            }
        }
        //Check South
        comp = new Vector2(x, z-1);
        if (grid.ContainsKey(comp))
        {
            if (room.roomId != grid[comp].roomId)
            {
                if (room.xSize == grid[comp].xSize && room.zSize == grid[comp].zSize)
                {
                    if (room.xCoord == grid[comp].xCoord || room.zCoord == grid[comp].zCoord)
                    {
                        hasSurrounding = true;
                        room.deleteSouth();
                        grid[comp].deleteNorth();
                        if (room.gameObject.transform.parent == BoHObject.transform)
                        {
                            GameObject parent = new GameObject();
                            parent.name = "Room " + room.roomId;
                            parent.tag = "room";
                            room.gameObject.transform.parent = parent.transform;
                            parent.transform.parent = BoHObject.transform;
                            grid[comp].transform.parent = parent.transform;
                        }
                        else
                        {
                            grid[comp].transform.parent = room.transform.parent;
                        }
                    }
                }
            }
        }

        //if it has no surroundong rooms, make it it's own room
        if (!hasSurrounding)
        {
            room.gameObject.name = "Room " + room.roomId;
            room.tag = "room";
            room.transform.parent = BoHObject.transform;
        }

    }

    private Vector2 SurroundingRooms(int x, int z)
    {
        nSizeRoom currentRoom = grid[new Vector2(x, z)];
        //Check West
        Vector2 comp = new Vector2(x - 1, z);
        if (grid.ContainsKey(comp))
        {
            if (grid[comp] != null)
            {
                if (grid[comp].roomId != currentRoom.roomId)
                    return comp;
            }    
        }
        //Check East
        comp = new Vector2(x + 1, z);
        if (grid.ContainsKey(comp))
        {
            if (grid[comp] != null)
            {
                if (grid[comp].roomId != currentRoom.roomId)
                    return comp;
            }
        }
        //Check North
        comp = new Vector2(x, z + 1);
        if (grid.ContainsKey(comp))
        {
            if (grid[comp] != null)
            {
                if (grid[comp].roomId != currentRoom.roomId)
                    return comp;
            }
        }
        //Check South
        comp = new Vector2(x, z - 1);
        if (grid.ContainsKey(comp))
        {
            if (grid[comp] != null)
            {
                if (grid[comp].roomId != currentRoom.roomId)
                    return comp;
            }
        }

        return Vector2.zero;
    }
    private nSizeRoom createAtGrid(int x, int z, int sizeX , int sizeZ, GameObject prefab)
    {

        GameObject g =  Instantiate(prefab, new Vector3(x * GridSize, 0, z * GridSize), Quaternion.identity,BoHObject.transform);
        g.transform.localScale = new Vector3(GridSize, GridSize, GridSize);
        nSizeRoom roomScript = g.GetComponent<nSizeRoom>();
        roomScript.xCoord = x;
        roomScript.zCoord = z;
        roomScript.roomId = currentRoomNumber;
        roomScript.xSize = sizeX;
        roomScript.zSize = sizeZ;
        for(int xInd = 0; xInd < sizeX;xInd++)
        {
            for (int zInd = 0; zInd < sizeZ; zInd++)
            {
                grid.Add(new Vector2((int)x + xInd, (int)z - zInd), roomScript);
            }
        }
        currentRoomNumber++;
        return roomScript;
    }

    private Vector2 pickRandomEmptySpot(int xLength, int zLength)
    {
        //gets all empty squares that the room could fit in and doesn't have something there alredy
        List<Vector2> possible = new List<Vector2>();
        for (int x = 0; x <= xGrid - xLength; x++)
        {
            for (int z = zLength; z <= zGrid; z++)
            {
                if (!grid.ContainsKey(new Vector2(x, z)))
                {
                    possible.Add(new Vector2(x, z));
                }
            }
        }

        Vector2[] possibleArray;
        possibleArray = possible.ToArray();
        //for all the possible squares
        foreach (Vector2 v in possibleArray)
        {
            //if this isn't a valid location, remove it from possible
            for (int x = 0; x < xLength; x++)
            {
                for (int z = 0; z < zLength; z++)
                {
                    //Debug.Log("Starting Point: " + v);
                    //Debug.Log(new Vector2((int)(v.x + x), (int)(v.y - z)));

                    if (grid.ContainsKey(new Vector2((int)(v.x + x), (int)(v.y - z))))
                    {
                        //Debug.Log(x + " , " + z);
                        possible.Remove(v);
                    }
                }
            }


        }

        if (possible.Count > 0)
        {
            return possible[Random.Range(0, possible.Count)];
        }
        return new Vector2(-1, -1);

    }

    //
    private void OnDrawGizmos()
    {
        if (!drawGrid)
        {
            return;
        }
        Gizmos.color = Color.black;
        Vector3 origin = new Vector3((xGrid / 2 * GridSize) + GridSize / 2, 0, (zGrid / 2 * GridSize) + GridSize / 2);
        Gizmos.DrawWireCube(origin, new Vector3(GridSize * xGrid , 0, zGrid * GridSize));
        for (int x = 0; x < xGrid;x++)
        {
            Gizmos.DrawWireCube(new Vector3(x*GridSize,0,origin.z), new Vector3(0, 0, zGrid * GridSize));
        }

        for (int z = 0; z < zGrid; z++)
        {
            Gizmos.DrawWireCube(new Vector3(origin.x, 0, z * GridSize), new Vector3(xGrid*GridSize, 0, 0));
        }

    }

}

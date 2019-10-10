using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : ScriptableObject {

    public int settingsIndex;

    public MazeRoomSettings settings;

    public List<MazeCell> cells = new List<MazeCell>();

    public int size = 0;

    public void Add(MazeCell cell) {
        cell.room = this;
        cells.Add(cell);
        size++;
    }

    public void Assimilate(MazeRoom room) {
        for (int i = 0; i < room.cells.Count; i++) {
            Add(room.cells[i]);
        }
    }
    
}

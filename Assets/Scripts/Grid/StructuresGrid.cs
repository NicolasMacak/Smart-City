using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresGrid
{
    private GenericGrid<GameObject> structuresGrid { get; set; }

    public StructuresGrid(int width, int height, float cellSize)
    {
        structuresGrid = new GenericGrid<GameObject>(width, height, cellSize);
    }

    public void setOccupation()
    {

    }
}

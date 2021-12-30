using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtilities
{
    public static Position GetPosition(Vector3 worldPosition, float cellSize)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / cellSize);
        return new Position(x, z);
    }
}

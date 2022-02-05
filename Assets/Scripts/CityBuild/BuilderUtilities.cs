using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SimulationObject;

public class BuilderUtilities
{
   public static Quaternion GetQuarterionFromOrientation(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.DOWN: return Quaternion.Euler(0f, 180f, 0f);
            case Orientation.RIGHT: return Quaternion.Euler(0f, 90f, 0f);
            case Orientation.LEFT: return Quaternion.Euler(0f, 270f, 0f);
            default: return Quaternion.identity;
        }
    }

    public static Vector3 GetRotatedCoordinates(Vector3 coordinates, Orientation orientation)
    {
        float gridOffset = CONSTANTS.SIMULATION.CELL_SIZE;
        switch (orientation)
        {
            case Orientation.DOWN: return coordinates + new Vector3(gridOffset, 0, gridOffset);
            case Orientation.RIGHT: return coordinates + new Vector3(0, 0, gridOffset);
            case Orientation.LEFT: return coordinates + new Vector3(gridOffset, 0, 0);
            default: return coordinates;
        }
    }

    public static int RoundByCellsize(float n)
    {
        var intN = Mathf.FloorToInt(n);

        return ((intN / (int)CONSTANTS.SIMULATION.CELL_SIZE) * (int)CONSTANTS.SIMULATION.CELL_SIZE);
    }
}

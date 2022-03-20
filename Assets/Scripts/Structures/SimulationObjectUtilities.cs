using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SimulationObject;
public static class SimulationObjectUtilities
{
    //public static Position GetPosition(Vector3 worldPosition)
    //{
    //    Debug.Log(worldPosition.x + " " + worldPosition.z);
    //    int x = Mathf.FloorToInt(worldPosition.x / (CONSTANTS.SIMULATION.CELL_SIZE + 0.1f)); // 0.1 ensures that 
    //    int z = Mathf.FloorToInt(worldPosition.z / (CONSTANTS.SIMULATION.CELL_SIZE + 0.1f));

    //    Debug.Log(x + " T " + z);
    //    return new Position(x, z);
    //}

    //public static Orientation GetOrientationFromAngle(float yRotation)
    //{
    //    switch (yRotation)
    //    {
    //        case 0: return Orientation.UP;
    //        case 90f: return Orientation.RIGHT;
    //        case 180f: return Orientation.UP;
    //        case 270f: return Orientation.LEFT;
    //        default: return Orientation.UP;
    //    }
    //}

    //public static float GetAngleFromOrientation(Orientation orientation)
    //{
    //    switch (orientation)
    //    {
    //        case Orientation.UP: return 0;
    //        case Orientation.RIGHT: return 90;
    //        case Orientation.DOWN: return 180;
    //        case Orientation.LEFT: return 270;
    //        default: return 0;
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CityBuilder;

public class RaycastCollision
{
    public GameObject collidedObject { get; }
    public Vector3 gridPoint { get; }


    public RaycastCollision() {
        collidedObject = null;
        gridPoint = Vector3.up;
    }

    public RaycastCollision(GameObject collidedObject, Vector3 gridPoint)
    {
        this.collidedObject = collidedObject;
        this.gridPoint = gridPoint;
    }

    public bool ShouldInteractionStop(BuilderAction builderAction)
    {
        return IsOffMapClick() || !IsBuilderActionValid(builderAction);
    }

    private bool IsBuilderActionValid(BuilderAction builderAction)
    {
        switch (builderAction)
        {
            case BuilderAction.Build: return collidedObject.name == CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND;
            case BuilderAction.Destroy: return collidedObject.name != CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND; // TODO cekovat ci ma budova tag structure ci co
            default: return false;
        }
    }

    private bool IsOffMapClick()
    {
        return collidedObject == null;
    }

    public string ToString()
    {
        return "gameObject: " + collidedObject.name + "point: " + gridPoint.ToString();
    }
}

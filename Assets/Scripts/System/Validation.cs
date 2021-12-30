using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CityBuilder;

public static class Validation
{
    private static bool IsBuilderActionValid(BuilderAction builderAction, GameObject collidedObject)
    {
        switch (builderAction)
        {
            case BuilderAction.Build : return collidedObject.name == CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND;
            case BuilderAction.Destroy: return collidedObject.name != CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND; // TODO cekovat ci ma budova tag structure ci co
            default: return false;
        }
    }
}

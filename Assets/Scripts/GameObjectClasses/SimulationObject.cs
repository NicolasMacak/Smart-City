using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SimulationObjectXml;

public class SimulationObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Orientation orientation { get; set; }
    public Model model { get; set; }

    public int width { get; set; }
    public enum Orientation
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public enum Model
    {
        House,
        RoadStraigth,
        RoadTurn
    }

    public Position GetPosition()
    {
        Vector3 worldPosition = gameObject.transform.position;

        int x = Mathf.FloorToInt(worldPosition.x / (CONSTANTS.SIMULATION.CELL_SIZE)); // 0.1 ensures that 
        int z = Mathf.FloorToInt(worldPosition.z / (CONSTANTS.SIMULATION.CELL_SIZE));

        if (orientation == Orientation.RIGHT) { z -= 1; } // pivot je mimo k
        else if (orientation == Orientation.LEFT) { x -= 1; }
        else if(orientation == Orientation.DOWN) { z -= 1; x -= 1; }

        return new Position(x, z);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

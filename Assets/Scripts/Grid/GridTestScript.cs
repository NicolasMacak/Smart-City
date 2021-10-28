using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Xml.Serialization;
using System.IO;

public class GridTestScript : MonoBehaviour
{
    [SerializeField]
    public Transform consumerBuilding;

    [SerializeField]
    public Transform road;

    private GridStructure<int> grid;
    // Start is called before the first frame update
    void Start()
    {
        var fileStream = File.Open("Assets/simulation.xml", FileMode.Open); // XML parsing
        XmlSerializer serializer = new XmlSerializer(typeof(Simulation));
        var simulation = (Simulation)serializer.Deserialize(fileStream);

        grid = new GridStructure<int>(5, 5, 10f);

        Debug.Log(simulation.id);
        Debug.Log("fcking Velkost "+simulation.consumerStructures.Count);
        foreach (var consumerStructure in simulation.consumerStructures)
        {
            int x = consumerStructure.position.x;
            int z = consumerStructure.position.z;
            Debug.Log(consumerStructure.ToString());

            Instantiate(consumerBuilding, grid.GetWorldPosition(x, z), Quaternion.identity);
        }


        
        //grid.SetValue(new Vector3(12f, 0, 5f), 40);
        //Debug.Log(grid.GetValue(new Vector3(12f, 0, 5f)));

        //Instantiate(consumerBuilding, grid.GetWorldPosition(1, 1), Quaternion.identity);
        //Instantiate(consumerBuilding, grid.GetWorldPosition(4, 0), Quaternion.identity);
        //Instantiate(consumerBuilding, grid.GetWorldPosition(4, 2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 40);
        }
    }
}

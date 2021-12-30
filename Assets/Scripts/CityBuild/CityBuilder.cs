using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
//using CONSTANTS;

using static Validation;

public class CityBuilder : MonoBehaviour
{
    //const string Grid_GROUND_ID = "GridGround";

    private Transform selectedStructure;

    private List<Transform> structureOptions = new List<Transform>();


    public enum BuilderAction
    {
        Build,
        Destroy
    }

    private GridStructure<int> grid;

    void Start()
    {
        InitGridGround();
        LoadModels();

        //SimulationSerializer serializer = new SimulationSerializer();
        //serializer.GenerateXML();

        Simulation simulation = ParseSimulationFromXML();
        grid = new GridStructure<int>(10, 20, 10f);
        //grid = new GridStructure<int>(40, 40, 10f);


        //Debug.Log("Velkost "+simulation.consumerStructures.Count);
        foreach (var consumerStructure in simulation.consumerStructures)
        {
            int x = consumerStructure.position.x;
            int z = consumerStructure.position.z;
            //Debug.Log(consumerStructure.ToString());

            PlaceBulding(x, z);// Instantiate(consumerBuilding, grid.GetWorldPosition(x, z), Quaternion.identity);
        }
        //GameObject housesObj = GameObject.Find("houses");
        SimulationXmlGenerator.GenerateXML(GameObject.Find("Structures").transform);
    }

    private void LoadModels()
    {
        structureOptions.Add(Resources.Load<Transform>("Models/Accomodation/House"));
        structureOptions.Add(Resources.Load<Transform>("Models/Accomodation/FlatBlock"));
        structureOptions.Add(Resources.Load<Transform>("Models/Accomodation/TraffoStation"));

        selectedStructure = structureOptions[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceStructure();
        }
        else if (Input.GetMouseButton(1))
        {
            DestroyStructure();
        }

        if(Input.GetKey("1"))
        {
            selectedStructure = structureOptions[0];
        }
        else if (Input.GetKey("2"))
        {
            selectedStructure = structureOptions[1];
        }
        else if (Input.GetKey("3"))
        {
            selectedStructure = structureOptions[2];
        }
        //Debug.Log(Mouse3D.GetMouseWorldPosition());
    }

    //private bool isBuilderActionValid(BuilderAction builderAction, GameObject collidedObject)
    //{
    //    switch (builderAction)
    //    {
    //        case BuilderAction.Build: return true;
    //    }
    //}

    private void PlaceStructure()
    {
        var raycastCollision = Mouse3D.GetMouseWorldPosition();
        if (raycastCollision.ShouldInteractionStop(BuilderAction.Build)) { return; }
        
        InstintiateStructure(raycastCollision.gridPoint);
    }

    private void DestroyStructure()
    {
        var raycastCollision = Mouse3D.GetMouseWorldPosition();
        Debug.Log(raycastCollision.collidedObject.name);
        if (raycastCollision.ShouldInteractionStop(BuilderAction.Destroy)) { return; }
        Destroy(raycastCollision.collidedObject);
    }



    private void InitGridGround()
    {
        GameObject gridGround = GameObject.Find(CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND);

        Vector3 rescale = gridGround.transform.localScale;

        rescale.z *= 20;
        rescale.x *= 10;

        gridGround.transform.localScale = rescale;
        //gridGround.transform.localScale.z = 200;
        //gridGround.transform.localScale.x = 100;
        gridGround.transform.position = new Vector3(50, 0, 100); //housesObj.transform.position;
    }

    private Simulation ParseSimulationFromXML() 
    {
        var fileStream = File.Open("Assets/simulation.xml", FileMode.Open); // XML parsing
        //var fileStream = File.Open("manualConstructedSimulation.xml", FileMode.Open); // XML parsing
        XmlSerializer serializer = new XmlSerializer(typeof(Simulation));
        return (Simulation)serializer.Deserialize(fileStream);
    }

    // Start is called before the first frame update


    public void PlaceBulding(int x, int z)
    {
        var ggg = Instantiate(selectedStructure, grid.GetWorldPosition(x, z), Quaternion.identity);
        ggg.SetParent(GameObject.Find("houses").transform);
    }

    public void InstintiateStructure(Vector3 clickedPosition)
    {
        clickedPosition.x = RoundByCellsize(clickedPosition.x);
        clickedPosition.z = RoundByCellsize(clickedPosition.z);

        var instintiatedStructure = Instantiate(selectedStructure, clickedPosition, Quaternion.identity);
        instintiatedStructure.SetParent(GameObject.Find("houses").transform);
    }

    private int RoundByCellsize(float n)
    {
        var intN = Mathf.FloorToInt(n);

        return (intN / 10) * 10;
    }

}

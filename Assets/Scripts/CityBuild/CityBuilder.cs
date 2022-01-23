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

    /**
     Pridat separe classu v ktorej bude grid pre objekty. Neskor mozno bude treba ine gridy, preto si ponecham abstraktnu deklaraciu
     */ 
    private enum Structure
    {
        House,
        StraightRoad,
        RoadTurn
    }

    private BuildOption selectedBuildOption;
    private Dictionary<Structure, BuildOption> buildOptions = new Dictionary<Structure, BuildOption>();

    private GenericGrid<int> grid;

    public enum BuilderAction
    {
        Build,
        Destroy
    }

    void Start()
    {
        InitGridGround();
        LoadModels();

        //SimulationSerializer serializer = new SimulationSerializer();
        //serializer.GenerateXML();

        Simulation simulation = ParseSimulationFromXML();
        grid = new GenericGrid<int>(10, 20, 5f);

        InsintiateConsumerStructures(simulation.consumerStructures);
        InsintiateTrafficStructures(simulation.trafficStructures);
        //Debug.Log("Velkost "+simulation.consumerStructures.Count);

        //GameObject housesObj = GameObject.Find("houses");
    }

    private void InsintiateConsumerStructures(List<ConsumerStructure> consumerStructures)
    {
        selectedBuildOption = buildOptions[Structure.House];

        foreach (var consumerStructure in consumerStructures)
        {

            //Debug.Log(consumerStructure.ToString());

            PlaceStructureFromXml(consumerStructure);
        }
    }

    private void InsintiateTrafficStructures(List<Road> roads)
    {
        selectedBuildOption = buildOptions[Structure.StraightRoad];

        foreach (var road in roads)
        {

            //Debug.Log(consumerStructure.ToString());

            PlaceStructureFromXml(road);
        }
    }



    private void LoadModels()
    {
        buildOptions.Add(Structure.House, new BuildOption(Resources.Load<Transform>("Models/Accomodation/House"), "Houses", CONSTANTS.StructureName.HOUSE));
        buildOptions.Add(Structure.StraightRoad, new BuildOption(Resources.Load<Transform>("Models/Traffic/StraightRoad"), "Roads", CONSTANTS.StructureName.ROAD_STRAIGTH));
        buildOptions.Add(Structure.RoadTurn, new BuildOption(Resources.Load<Transform>("Models/Traffic/RoadTurn"), "Roads", CONSTANTS.StructureName.ROAD_TURN));

        selectedBuildOption = buildOptions[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceStructureByClick();
        }
        else if (Input.GetMouseButton(1))
        {
            DestroyStructure();
        }

        if(Input.GetKey("1"))
        {
            selectedBuildOption = buildOptions[Structure.House];
        }
        else if (Input.GetKey("2"))
        {
            selectedBuildOption = buildOptions[Structure.StraightRoad];
        }
        else if (Input.GetKey("3"))
        {
            selectedBuildOption = buildOptions[Structure.RoadTurn];
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

    private void PlaceStructureByClick()
    {
        var raycastCollision = Mouse3D.GetMouseWorldPosition();
        if (raycastCollision.ShouldInteractionStop(BuilderAction.Build)) { return; }

        Vector3 clickedPosition = raycastCollision.gridPoint;
        clickedPosition.x = RoundByCellsize(clickedPosition.x);
        clickedPosition.z = RoundByCellsize(clickedPosition.z);

        InstintiateStructure(clickedPosition);
    }

    private void PlaceStructureFromXml(SimulationObject simulationObjects)
    {
        Vector3 coordinates = new Vector3();
        coordinates.x = simulationObjects.position.x * grid.cellSize;
        coordinates.z = simulationObjects.position.z * grid.cellSize;

        InstintiateStructure(coordinates);
    }

    private void DestroyStructure()
    {
        var raycastCollision = Mouse3D.GetMouseWorldPosition();
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
        var fileStream = File.Open("Assets/manualConstructedSimulation.xml", FileMode.Open); // XML parsing
        //var fileStream = File.Open("manualConstructedSimulation.xml", FileMode.Open); // XML parsing
        XmlSerializer serializer = new XmlSerializer(typeof(Simulation));
        return (Simulation)serializer.Deserialize(fileStream);
    }

    // Start is called before the first frame update


    //public void PlaceBulding(int x, int z)
    //{
    //    var ggg = Instantiate(selectedBuildOption.model, grid.GetWorldPosition(x, z), Quaternion.identity);
    //    ggg.SetParent(GameObject.Find("houses").transform);
    //}

    public void InstintiateStructure(Vector3 coordinates)
    {
        var instintiatedStructure = Instantiate(selectedBuildOption.model, coordinates, Quaternion.identity);
        instintiatedStructure.name = selectedBuildOption.name;
        instintiatedStructure.SetParent(GameObject.Find(selectedBuildOption.parrentGameObject).transform);
    }

    private int RoundByCellsize(float n)
    {
        var intN = Mathf.FloorToInt(n);

        return ((intN / (int)grid.cellSize) * (int)grid.cellSize);
    }

}

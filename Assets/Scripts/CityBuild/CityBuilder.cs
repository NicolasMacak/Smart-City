using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.AI;

using static SimulationObject;

public class CityBuilder : MonoBehaviour
{
    //const string Grid_GROUND_ID = "GridGround";

    /**
     Pridat separe classu v ktorej bude grid pre objekty. Neskor mozno bude treba ine gridy, preto si ponecham abstraktnu deklaraciu
     */

    private BuildOption selectedBuildOption;
    private Orientation selectedOrientation = Orientation.UP;
    private Dictionary<Model, BuildOption> buildOptions = new Dictionary<Model, BuildOption>();

    private GenericGrid<int> grid;

    public enum BuilderAction
    {
        Build,
        Destroy
    }

    void Start()
    {
        grid = new GenericGrid<int>(50-1, 50-1, 5f);
        InitGridGround();
        LoadModels();

        //SimulationSerializer serializer = new SimulationSerializer();
        //serializer.GenerateXML();

        Simulation simulation = ParseSimulationFromXML();

        InsintiateTrafficStructures(simulation.trafficStructures);
        GameObject.Find("NavMeshGenerator").GetComponent<NavMeshSurface>().BuildNavMesh();

        InsintiateConsumerStructures(simulation.consumerStructures);
        

        //GameObject.Find("NavMeshGenerator").GetComponent<NavMeshGenerator>().BakeNavMesh();
        
        TrafficManager.InitializeTraffic();

        //NavMeshGenerator.BakeNavMesh();

        //Debug.Log("Velkost "+simulation.consumerStructures.Count);

        //GameObject housesObj = GameObject.Find("houses");
    }

    private void InsintiateConsumerStructures(List<ConsumerStructure> consumerStructures)
    {
        selectedBuildOption = buildOptions[Model.House];

        foreach (var consumerStructure in consumerStructures)
        {
            //Debug.Log(consumerStructure.ToString());
            selectedOrientation = consumerStructure.orientation;
            PlaceStructureFromXml(consumerStructure);
        }
    }

    private void InsintiateTrafficStructures(List<Road> roads)
    {

        foreach (var road in roads)
        {
            selectedBuildOption = buildOptions[road.model];
            selectedOrientation = road.orientation;

            //Debug.Log(consumerStructure.ToString());

            PlaceStructureFromXml(road);
        }
    }



    private void LoadModels()
    {
        buildOptions.Add(Model.House, new BuildOption(Resources.Load<Transform>("Models/Accomodation/House"), "Houses", CONSTANTS.StructureName.HOUSE, Model.House));
        buildOptions.Add(Model.RoadStraigth, new BuildOption(Resources.Load<Transform>("Models/Traffic/StraightRoad"), "NavMeshGenerator", CONSTANTS.StructureName.ROAD_STRAIGTH, Model.RoadStraigth));
        buildOptions.Add(Model.RoadTurn, new BuildOption(Resources.Load<Transform>("Models/Traffic/RoadTurn"), "NavMeshGenerator", CONSTANTS.StructureName.ROAD_TURN, Model.RoadTurn));

        selectedBuildOption = buildOptions[0];
    }

    private void Update()
    {
        //Debug.Log(RoundByCellsize(Mouse3D.GetMouseWorldPosition().gridPoint.z));
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
            selectedBuildOption = buildOptions[Model.House];
        }
        else if (Input.GetKey("2"))
        {
            selectedBuildOption = buildOptions[Model.RoadStraigth];
        }
        else if (Input.GetKey("3"))
        {
            selectedBuildOption = buildOptions[Model.RoadTurn];
        }
        else if (Input.GetKey("up"))
        {
            selectedOrientation = Orientation.UP;
        }
        else if (Input.GetKey("down"))
        {
            selectedOrientation = Orientation.DOWN;
        }
        else if (Input.GetKey("left"))
        {
            selectedOrientation = Orientation.LEFT;
        }
        else if (Input.GetKey("right"))
        {
            selectedOrientation = Orientation.RIGHT;
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

        clickedPosition.x = BuilderUtilities.RoundByCellsize(clickedPosition.x);
        clickedPosition.z = BuilderUtilities.RoundByCellsize(clickedPosition.z);

        InstintiateStructure(clickedPosition);
    }

    private void PlaceStructureFromXml(SimulationObjectXml simulationObjects)
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

        Destroy(raycastCollision.collidedObject.transform.parent.gameObject);
    }

    private void InitGridGround()
    {
        GameObject gridGround = GameObject.Find(CONSTANTS.SYSTEM_GAME_OBJECT.GRID_GROUND);

        Vector3 rescale = gridGround.transform.localScale;

        float xLenth = grid.widthX /2;
        float zLenth = grid.heightZ /2;

        float xOffset = xLenth * grid.cellSize;
        float zOffset = zLenth * grid.cellSize;

        rescale.z *= zLenth;
        rescale.x *= xLenth;

        gridGround.transform.localScale = rescale;
        //gridGround.transform.localScale.z = 200;
        //gridGround.transform.localScale.x = 100;
        gridGround.transform.position = new Vector3(xOffset, 0, zOffset); //housesObj.transform.position;

        //rescale.z *= 20;
        //rescale.x *= 10;

        //gridGround.transform.localScale = rescale;
        ////gridGround.transform.localScale.z = 200;
        ////gridGround.transform.localScale.x = 100;
        //gridGround.transform.position = new Vector3(50, 0, 100); //housesObj.transform.position;
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
        coordinates = BuilderUtilities.GetRotatedCoordinates(coordinates, selectedOrientation); 

        var instintiatedStructure = Instantiate(
            selectedBuildOption.prefab,
            coordinates,
            BuilderUtilities.GetQuarterionFromOrientation(selectedOrientation)
            );

        AddAttributesToInstintiatedStructure(instintiatedStructure);
        instintiatedStructure.SetParent(GameObject.Find(selectedBuildOption.parrentGameObject).transform);
    }

    private void AddAttributesToInstintiatedStructure(Transform instintiatedStructure)
    {
        instintiatedStructure.name = selectedBuildOption.name;

        SimulationObject simulationObjectScript = instintiatedStructure.gameObject.AddComponent<SimulationObject>();
        simulationObjectScript.orientation = selectedOrientation;
        simulationObjectScript.model = selectedBuildOption.model;
    }
}

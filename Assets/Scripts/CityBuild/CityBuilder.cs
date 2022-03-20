using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;
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

    private void debugTrafficStructures(Simulation simulation)
    {
        foreach(var road in simulation.trafficStructures)
        {
            Debug.Log(road.ToString());
        }
    }

    void Start()
    {
        grid = new GenericGrid<int>(50-1, 50-1, 5f);
        InitGridGround();
        LoadModels();

        Simulation simulation = ParseSimulationFromXML();
        //debugTrafficStructures(simulation);
        InsintiateTrafficStructures(simulation.trafficStructures);
        //GameObject.Find("NavMeshGenerator").GetComponent<NavMeshSurface>().BuildNavMesh();
        
        InsintiateConsumerStructures(simulation.consumerStructures);

        //TrafficManager.InitializeTraffic();
    }

    private void InsintiateConsumerStructures(List<ConsumerStructure> consumerStructures)
    {
        foreach (var consumerStructure in consumerStructures)
        {
            selectedBuildOption = buildOptions[consumerStructure.model];
            //Debug.Log(consumerStructure.ToString());
            selectedOrientation = consumerStructure.orientation;
            PlaceStructureFromXml(consumerStructure); //InstitiateFromXml(consumerStructure); 
        }
    }

    private void InsintiateTrafficStructures(List<Road> roads)
    {

        foreach (var road in roads)
        {
            selectedBuildOption = buildOptions[road.model];
            selectedOrientation = road.orientation;

            //Debug.Log(consumerStructure.ToString());

            PlaceStructureFromXml(road); //InstitiateFromXml(road); 
        }
    }



    private void LoadModels()
    {
        buildOptions.Add(Model.SmallHouse, new BuildOption(Resources.Load<Transform>("Prefabs/Accomodation/SmallHouse"), "Houses", CONSTANTS.StructureName.HOUSE, Model.SmallHouse));
        buildOptions.Add(Model.BigHouse, new BuildOption(Resources.Load<Transform>("Prefabs/Accomodation/BigHouse"), "Houses", CONSTANTS.StructureName.HOUSE, Model.BigHouse));
        buildOptions.Add(Model.RoadStraigth, new BuildOption(Resources.Load<Transform>("Prefabs/Traffic/StraightRoad"), "Roads", CONSTANTS.StructureName.ROAD_STRAIGTH, Model.RoadStraigth));
        buildOptions.Add(Model.RoadTurn, new BuildOption(Resources.Load<Transform>("Prefabs/Traffic/RoadTurn"), "Roads", CONSTANTS.StructureName.ROAD_TURN, Model.RoadTurn));
        buildOptions.Add(Model.Tree, new BuildOption("Green", CONSTANTS.StructureName.TREE, Model.Tree));

        foreach(var buildOption in buildOptions)
        {
            if(buildOption.Value.prefab == null)
            {
                Debug.LogError("PREFAB NOT LOADED FOR " + buildOption.Value.name);
            }
        }

        selectedBuildOption = buildOptions[Model.SmallHouse];
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
            selectedBuildOption = buildOptions[Model.SmallHouse];
        }
        else if (Input.GetKey("2"))
        {
            selectedBuildOption = buildOptions[Model.BigHouse];
        }
        else if (Input.GetKey("3"))
        {
            selectedBuildOption = buildOptions[Model.RoadStraigth];
        }
        else if (Input.GetKey("4"))
        {
            selectedBuildOption = buildOptions[Model.RoadTurn];
        }
        else if (Input.GetKey("5"))
        {
            selectedBuildOption = buildOptions[Model.Tree];
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

        if(selectedBuildOption.model == Model.Tree)
        {
            SetTreeAsBuildingOption();
        }

        InstintiateStructureAndAddAttributes(clickedPosition);
    }

    private void SetTreeAsBuildingOption()
    {
        System.Random r = new System.Random();
        Transform treePrefab = Resources.Load<Transform>("Prefabs/Generic/Trees/Tree_" + r.Next(2, 5));
        
        Vector3 scale = treePrefab.transform.localScale;
        int coeficient = r.Next(-2, 2);

        float sizeCoeficient = ((float)coeficient) / 10;
        scale.x += sizeCoeficient;
        scale.y += sizeCoeficient;
        scale.z += sizeCoeficient;
        treePrefab.transform.localScale = scale;

        Debug.Log(treePrefab.rotation.ToString());
        treePrefab.rotation *= new Quaternion(0,coeficient*90,0,0);
        Debug.Log(treePrefab.rotation.ToString());
        selectedBuildOption.prefab = treePrefab;
    }

    private void PlaceStructureFromXml(SimulationObjectXml simulationObject)
    {
        Vector3 coordinates = new Vector3();
        coordinates.x = simulationObject.position.x * grid.cellSize;
        coordinates.z = simulationObject.position.z * grid.cellSize;

        //RegisterObjectOnGrid(simulationObject);
        InstintiateStructureAndAddAttributes(coordinates);
    }

    //private void RegisterObjectOnGrid(SimulationObjectXml simulationObject)
    //{

    //}

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
        var fileStream = File.Open("Assets/NewHouseSimulation.xml", FileMode.Open); // XML parsing
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

    public void InstitiateFromXml(SimulationObjectXml simulationObject) // testujeme vonku
    {

        var instintiatedStructure = Instantiate(
            selectedBuildOption.prefab,
            simulationObject.GetRealWorldCoordinates(),
            BuilderUtilities.GetQuarterionFromOrientation(selectedOrientation)
        );

        AddGegenricAttributesToInstintiatedStructure(instintiatedStructure);
        AddSpecificAttributesToInstintiatedStrucutre(instintiatedStructure);
        Debug.Log("Prvy");
        instintiatedStructure.SetParent(GameObject.Find(selectedBuildOption.parrentGameObject).transform);
    }
    public void InstintiateStructureAndAddAttributes(Vector3 coordinates)
    {
        
        
            Debug.Log(selectedBuildOption);
        

        coordinates = BuilderUtilities.GetRotatedCoordinates(coordinates, selectedOrientation); 



        var instintiatedStructure = Instantiate(
            selectedBuildOption.prefab,
            coordinates,
            BuilderUtilities.GetQuarterionFromOrientation(selectedOrientation)
            );
        
        AddGegenricAttributesToInstintiatedStructure(instintiatedStructure);
        AddSpecificAttributesToInstintiatedStrucutre(instintiatedStructure);
        //Debug.Log("Prvy");
        instintiatedStructure.SetParent(GameObject.Find(selectedBuildOption.parrentGameObject).transform);
    }

    private void AddSpecificAttributesToInstintiatedStrucutre(Transform instintiatedStructure)
    {
        switch (selectedBuildOption.model)
        {
            //case Model.House: instintiatedStructure.GetComponent<HouseController>().additions =
        }
    }

    private void AddGegenricAttributesToInstintiatedStructure(Transform instintiatedStructure)
    {
        instintiatedStructure.name = selectedBuildOption.name;

        SimulationObject simulationObjectScript = instintiatedStructure.gameObject.AddComponent<SimulationObject>();
        simulationObjectScript.orientation = selectedOrientation;
        simulationObjectScript.model = selectedBuildOption.model;
    }
}

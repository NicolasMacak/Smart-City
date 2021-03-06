using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

public class SimulationXmlGenerator
{
    public static void GenerateXML(Transform GOSimulation)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Simulation));

        //File.Delete("Assets/manualConstructedSimulation.xml");
        TextWriter writer = new StreamWriter("Assets/NewHouseSimulation.xml");

        Simulation simulation = new Simulation();

        simulation.consumerStructures = PrepareConsumerStructures(GameObject.Find("Houses").transform);
        simulation.trafficStructures = PrepareRoadStructures(GameObject.Find("Roads").transform);

        serializer.Serialize(writer, simulation);
        writer.Close();
    }

    private static List<ConsumerStructure> PrepareConsumerStructures(Transform houses)
    {
        List<ConsumerStructure> consumerStructures = new List<ConsumerStructure>();

        int childCount = houses.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            Transform child = houses.GetChild(i);
            consumerStructures.Add(new ConsumerStructure(child));
        }

        return consumerStructures;
    }

    private static List<Road> PrepareRoadStructures(Transform roads)
    {
        List<Road> consumerStructures = new List<Road>();

        int childCount = roads.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = roads.GetChild(i);

            consumerStructures.Add(new Road(Road.DecideCategory(child.name),child));
        }

        return consumerStructures;
    }

    //private static List<T> prepareTStructures<T>(Transform houses)
    //{
    //    List<T> consumerStructures = new List<T>();

    //    int childCount = houses.transform.childCount;

    //    for (int i = 0; i < childCount; i++)
    //    {
    //        Transform child = houses.GetChild(i);
    //        consumerStructures.Add(new T("straigth", child));
    //    }

    //    return consumerStructures;
    //}
}

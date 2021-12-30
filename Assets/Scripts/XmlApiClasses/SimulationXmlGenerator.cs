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
        TextWriter writer = new StreamWriter("manualConstructedSimulation.xml");

        Simulation simulation = new Simulation();

        simulation.consumerStructures = prepareConsumerStructures(GameObject.Find("houses").transform);

        serializer.Serialize(writer, simulation);
        writer.Close();
    }

    private static List<ConsumerStructure> prepareConsumerStructures(Transform houses)
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
}


public class Jozko
{
    public string name { get; set; }
}

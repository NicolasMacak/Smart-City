using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Simulation")]
public class Simulation
{
    [XmlArray("consumerStructures")]
    //[XmlElement("consumerStructures")]
    [XmlArrayItem(ElementName = "consumerStructure")]
    public List<ConsumerStructure> consumerStructures { get; set; }

    [XmlElement("id")]
    public int id { get; set; }
}

public class ConsumerStructures
{
    [XmlArrayItem("consumerStructure")]
    public ConsumerStructure[] consumerStructures { get; set; }
}

public class ConsumerStructure
{
    [XmlElement("id")]
    public int id { get; set; }

    [XmlElement("category")]
    public string category { get; set; }

    [XmlElement("position")]
    public Position position { get; set; }

    override
    public string ToString()
    {
        return "id: " + id+ " category: " + category + " x: " + position.x + " z: " + position.z;
    }
}

public class Position
{
    [XmlElement("x")]
    public int x { get; set; }

    [XmlElement("z")]
    public int z { get; set; }
}


//[XmlRoot("consumerStructures")]
//public class ConsumerStructureso
//{
//    [XmlElement("consumerStructure")]
//    public ConsumerStructure[] items { get; set; }
//}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Simulation")]
public class Simulation
{
    [XmlArray("consumerStructures")]
    [XmlArrayItem(ElementName = "consumerStructure")]
    public List<ConsumerStructure> consumerStructures { get; set; }

    //[XmlArray("roads")]
    //[XmlArrayItem(ElementName = "road")]
    //public List<Road> roads { get; set; }

    

    [XmlElement("id")]
    public int id { get; set; }
}

//public class ConsumerStructures
//{
//    [XmlArrayItem("consumerStructure")]
//    public ConsumerStructure[] consumerStructures { get; set; }
//}

public class ConsumerStructure

{
    public ConsumerStructure() { }

    public ConsumerStructure(Transform consumerStructure)
    {
        this.id = 1;
        this.category = "House";
        this.position = GameObjectUtilities.GetPosition(consumerStructure.transform.position, 10f);
    }

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
    public Position() { }
    public Position(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

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

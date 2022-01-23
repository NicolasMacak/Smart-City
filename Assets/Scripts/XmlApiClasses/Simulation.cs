﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Simulation")]
public class Simulation
{
    [XmlArray("consumerStructures")]
    [XmlArrayItem(ElementName = "consumerStructure")]
    public List<ConsumerStructure> consumerStructures { get; set; }

    [XmlArray("trafficStructures")]
    [XmlArrayItem(ElementName = "trafficStructure")]
    public List<Road> trafficStructures { get; set; }
    //[XmlArray("roads")]
    //[XmlArrayItem(ElementName = "road")]
    //public List<Road> roads { get; set; }
}

//public class ConsumerStructures
//{
//    [XmlArrayItem("consumerStructure")]
//    public ConsumerStructure[] consumerStructures { get; set; }
//}

public class ConsumerStructure : SimulationObject

{
    public ConsumerStructure() { }

    public ConsumerStructure(Transform consumerStructure) : base(consumerStructure) 
    {
        this.category = Category.House;
    } // used when converting simulation to xml

    public enum Category
    {
        House
    }

    Category category;

    override
    public string ToString()
    {
        return "id: " + id+ " category: " + category + " x: " + position.x + " z: " + position.z;
    }
}

public class Road : SimulationObject
{
    public Road() { }

    public enum Category
    {
        Straight,
        Turn
    }

    Category category;

    public Road(Category category, Transform consumerStructure) : base(consumerStructure) 
    {
        this.category = category;
    }

    public static Category DecideCategory(string roadName) 
    {
        if(roadName == CONSTANTS.StructureName.ROAD_STRAIGTH)
        {
            return Category.Straight;
        }
        return Category.Turn;
    }

    override
    public string ToString()
    {
        return "id: " + id + " category: " + category + " x: " + position.x + " z: " + position.z;
    }
}

//public class GenericStructure : SimulationObject
//{
//    public GenericStructure() { }

//    public GenericStructure(Transform consumerStructure) : base(consumerStructure) { }

//    override
//    public string ToString()
//    {
//        return "id: " + id + " category: " + category + " x: " + position.x + " z: " + position.z;
//    }
//}

public class SimulationObject
{
    public enum Orientation
    {
        UP, DOWN, LEFT, RIGHT
    }

    public SimulationObject() { }

    public SimulationObject(Transform simulationObject)
    {
        this.id = 1;
        this.position = SimulationObjectUtilities.GetPosition(simulationObject.transform.position);
        this.orientation = SimulationObjectUtilities.GetOrientationFromAngle(simulationObject.transform.rotation.y);
    }

    [XmlElement("id")]
    public int id { get; set; }

    //[XmlElement("category")]
    //public string category { get; set; }

    [XmlElement("position")]
    public Position position { get; set; }

    [XmlElement("orientation")]
    public Orientation orientation { get; set; }

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

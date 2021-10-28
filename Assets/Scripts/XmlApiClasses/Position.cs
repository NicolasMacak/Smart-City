using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class Positionsss
{
    [XmlElement("x")]
    public int x { get; set; }

    [XmlElement("z")]
    public int z { get; set; }
}

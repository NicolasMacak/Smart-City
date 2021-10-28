using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ConsumerStructuresss
{

    [XmlElement("position")]
    public int position { get; set; }

    [XmlElement("category")]
    public string category { get; set; }
}

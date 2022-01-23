using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlSerializerGO : MonoBehaviour
{
    // Start is called before the first frame update
    public void SerializeSimulation()
    {
        SimulationXmlGenerator.GenerateXML(GameObject.Find("Structures").transform);
        Debug.Log("Simulation serialized");
    }
}

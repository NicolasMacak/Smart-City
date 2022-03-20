using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SimulationObject;
public class BuildOption
{
    public BuildOption(Transform prefab, string parrentGameObject, string name, Model model)
    {
        this.prefab = prefab;
        this.parrentGameObject = parrentGameObject;
        this.name = name;
        this.model = model;
    }

    public BuildOption(string parrentGameObject, string name, Model model)
    {
        this.parrentGameObject = parrentGameObject;
        this.name = name;
        this.model = model;
    }

    public Model model { get; }

    public string parrentGameObject { get; }
    public Transform prefab { get; set; }
    public string name { get; }
}

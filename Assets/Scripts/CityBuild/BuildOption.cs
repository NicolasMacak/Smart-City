using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOption
{
    public BuildOption(Transform model, string parrentGameObject, string name)
    {
        this.model = model;
        this.parrentGameObject = parrentGameObject;
        this.name = name;
    }

    public string parrentGameObject { get; }
    public Transform model { get; }
    public string name { get; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ConsumerStructure;

public class AdditionController : MonoBehaviour
{
    public Addition addition;
    void Start()
    {
        //var parrentAdditions = gameObject.GetComponentInParent<HouseController>().additions;
        //gameObject.SetActive(parrentAdditions.Contains(addition));
        Debug.Log("druhy");
    }

    public Addition GetActiveAddition()
    {
        return gameObject.activeInHierarchy ? addition : Addition.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ConsumerStructure;
public class HouseController : SimulationObject
{

    public List<Addition> additions { get; set; }
    public List<Addition> GetAdditions()
    {
        var additions = new List<Addition>();
        var objectAdditions = gameObject.GetComponentsInChildren<AdditionController>();
        
        foreach(AdditionController additionController in objectAdditions)
        {
            var childAddition = additionController.GetActiveAddition();
            if(childAddition != Addition.None)
            {
                additions.Add(childAddition);
            }
        }

        return additions;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficManager
{
    // Start is called before the first frame update


    public static void InitializeTraffic()
    {
        Transform houses = GameObject.Find("Houses").transform;
        SetWayPointsToCarAgents(houses, GetHouseWaypoints(houses));
    }

    private static void SetWayPointsToCarAgents(Transform houses, List<Vector3> waypoints)
    {
        for(int i=0; i < houses.childCount; i++)
        {
            CarAgent car = houses.GetChild(i).GetComponentInChildren<CarAgent>();// .GetChild(4).GetComponent<CarAgent>();
            car.SetWaypoints(waypoints);
            car.BrmBrm();
        }
    }

    private static List<Vector3> GetHouseWaypoints(Transform houses)
    {
        List<Vector3> waypoinstToReturn = new List<Vector3>();

        for(int i=0; i < houses.childCount; i++)
        {
            //Debug.Log(houses.transform.GetChild(i).GetChild(2).name);
            Vector3 houseWaypoint = houses.GetChild(i).GetChild(2).position;
            waypoinstToReturn.Add(houseWaypoint);
        }

        return waypoinstToReturn;
    }

}

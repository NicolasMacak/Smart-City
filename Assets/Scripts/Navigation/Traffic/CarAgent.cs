using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAgent : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;

    private List<Vector3> waypoints;
    private int waypointsCount;

    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void SetWaypoints(List<Vector3> waypoints)
    {
        
        this.waypoints = waypoints;
        waypointsCount = waypoints.Count;
    }

    public void BrmBrm()
    {
        int random = Random.Range(0, waypointsCount);
        agent.SetDestination(waypoints[random]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

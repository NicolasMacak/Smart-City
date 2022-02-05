using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshGenerator : MonoBehaviour
{
    void Awake()
    {
        NavMeshSurface navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.collectObjects = CollectObjects.Children;
        navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
    }
}

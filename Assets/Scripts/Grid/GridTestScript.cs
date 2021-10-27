using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridTestScript : MonoBehaviour
{

    private GridStructure grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GridStructure(5, 5, 10f);
        grid.SetValue(new Vector3(12f, 0, 5f), 40);
        Debug.Log(grid.GetValue(new Vector3(12f, 0, 5f)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 40);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridStructure
{
    private int widthX;
    private int heightZ;
    private float cellSize;
    private int[,] gridArray;
    // Keby bolo treba, da sa dat aj originPosition Vector3
    // https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722
    // 19:30

    private TextMesh[,] debugTextArray;
    
    public GridStructure(int width, int height, float cellSize)
    {
        this.widthX = width;
        this.heightZ = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < gridArray.GetLength(1); z++)
            {
               debugTextArray[x, z] = UtilsClass.CreateWorldText(
                    gridArray[x, z].ToString(), 
                    null, 
                    GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f,
                    20,
                    Color.white,
                    TextAnchor.MiddleCenter);

                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.red, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);

        SetValue(2, 1, 56);
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    private void GetXZ(Vector3 worldPosition, out int x, out int z) // refactor na normalny datovy typ
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < widthX && z < heightZ)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        Debug.Log(worldPosition);
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public int GetValue(int x, int z)
    {
        if(x >= 0 && z >= 0 && x < widthX && z < heightZ)
        {
            return gridArray[x, z];
        } else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }
}

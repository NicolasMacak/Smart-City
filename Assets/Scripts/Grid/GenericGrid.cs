using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GenericGrid<TGridObject>
{
    public int widthX { get; }
    public int heightZ { get; }
    public float cellSize { get; }
    private TGridObject[,] gridArray;
    // Keby bolo treba, da sa dat aj originPosition Vector3
    // https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722
    // 19:30

    private TextMesh[,] debugTextArray;
    
    public GenericGrid(int width, int height, float cellSize)
    {
        this.widthX = width;
        this.heightZ = height;
        this.cellSize = cellSize;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < gridArray.GetLength(1); z++)
            {
                //debugTextArray[x, z] = UtilsClass.CreateWorldText(
                //     x + "," + z,
                //     null,
                //     GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f,
                //     20,
                //     Color.white,
                //     TextAnchor.MiddleCenter);

                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.red, 100f);
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.red, 100f);
            }
        }

        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 500f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 500f);

        //SetValue(2, 1, 56);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public void SetValue(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < widthX && z < heightZ)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    //public void SetValue(Vector3 worldPosition, TGridObject value)
    //{
    //    Debug.Log(worldPosition);
    //    int x, z;
    //    GetXZ(worldPosition, out x, out z);
    //    SetValue(x, z, value);
    //}

    public TGridObject GetValue(int x, int z)
    {
        if(x >= 0 && z >= 0 && x < widthX && z < heightZ)
        {
            return gridArray[x, z];
        } else
        {
            return default(TGridObject);
        }
    }

    //public TGridObject GetValue(Vector3 worldPosition)
    //{
    //    int x, z;
    //    GetXZ(worldPosition, out x, out z);
    //    return GetValue(x, z);
    //}
}

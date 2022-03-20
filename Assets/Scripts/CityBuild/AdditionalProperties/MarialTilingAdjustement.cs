using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarialTilingAdjustement : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            if(meshRenderer.materials.Length == 0)
            {
                Debug.LogError("Cant acess tiling. No material on structure");
                return;
            }
            meshRenderer.materials[1].mainTextureOffset = new Vector2(Random.Range(0, 0.4f), Random.Range(0, 0.4f)); // Matarial[0] = strecha
        }
    }
}

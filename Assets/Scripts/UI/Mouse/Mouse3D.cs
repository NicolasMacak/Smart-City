using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{   public static Mouse3D Instance { get; private set; }

    //[SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
    //private GameObject gridGround;
    private GameObject objectInRaycast;

    private void Awake()
    {
        Instance = this;
        //gridGround = GameObject.Find("GridGround");
    }

    //private void Update()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
    //    {
    //        //raycastHit.transform.gameObject
    //        //Debug.Log(raycastHit.collider.gameObject);
    //        transform.position = raycastHit.point;
    //    }
    //}

    public static RaycastCollision GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    private RaycastCollision GetMouseWorldPosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(Camera.main.transform.position, ray.GetPoint(999f));
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f)/* && raycastHit.transform.gameObject.name == "GridGround"*/)
        {
            //if()
            //{
            //return raycastHit.point;
            //} 
            
            return new RaycastCollision(raycastHit.transform.gameObject, raycastHit.point) ;

        }
        return new RaycastCollision();
        //return null;
        //else
        //{
        //    return Vector3.up;
        //}
    }
}



//public class Mouse3D : MonoBehaviour
//{
//    public static Mouse3D Instance { get; private set; }

//    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
//    private GameObject gridGround = GameObject.Find("GridGround")

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Update()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
//        {
//            transform.position = raycastHit.point;
//        }
//    }

//    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

//    private Vector3 GetMouseWorldPosition_Instance()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        Debug.DrawLine(Camera.main.transform.position, ray.GetPoint(999f));
//        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
//        {
//            return raycastHit.point;
//        }
//        else
//        {
//            return Vector3.zero;
//        }
//    }
//}

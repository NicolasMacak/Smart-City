using UnityEngine;
using CodeMonkey.Utils;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float penSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit; //todo. grid rozmery

    public float scrollSpeed = 20f;
    public float minY = 20f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += penSpeed * Time.deltaTime;

        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= penSpeed * Time.deltaTime;

        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += penSpeed * Time.deltaTime;

        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= penSpeed * Time.deltaTime;

        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, 100f);
        //pos.y = Mathf.Clamp(pos.y, -panLimit.x, panLimit.y);

        transform.position = pos;

        //Debug.Log(Mouse3D.GetMouseWorldPosition());


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Terrain")))
        //{
        //    Debug.Log(hit.point);
        //}
    }
}

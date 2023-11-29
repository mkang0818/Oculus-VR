using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public GameObject rRevolver;
    public GameObject lRevolver;
    //public GameObject camera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        //        Input.mousePosition.y, -Camera.main.transform.position.z));


        //// 월드 좌표로 변환 (스크린 좌표 -> 월드 좌표)
        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(point);

        ////rRevolver.transform.position = worldMousePosition;
        //rRevolver.transform.position= worldMousePosition - (Vector3.right * 2);
        //lRevolver.transform.position = worldMousePosition - (Vector3.right * -2);
        //Debug.Log("World Mouse Position: " + worldMousePosition);


        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 5 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -5 * Time.deltaTime, 0);
        }
    }
}

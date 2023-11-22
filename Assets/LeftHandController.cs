using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class LeftHandController : MonoBehaviour
{

    GameObject HasBall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Oculus 트리거 버튼 입력 확인
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            // 트리거 버튼이 눌렸을 때 수행할 동작
            Debug.Log("왼손 공 잡기");
            if (HasBall != null) Catchball();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            // 트리거 버튼이 눌렸을 때 수행할 동작
            Debug.Log("왼손 던지기");
            if (HasBall != null) FireProjectile();
        }
    }

    void Catchball()
    {
        HasBall.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void FireProjectile()
    {
        Vector3 playerForward = transform.forward;

        Rigidbody ballRigidbody = HasBall.GetComponent<Rigidbody>();
        ballRigidbody.velocity = playerForward.normalized * 10;

        HasBall = null;
        GameManager.instance.ballCount--;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("Ball"))
        {
            HasBall = col.gameObject;
            print("공 잡을수 있음");
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Ball"))
        {
            HasBall = null;
        }
    }
}

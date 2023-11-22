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
        // Oculus Ʈ���� ��ư �Է� Ȯ��
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            // Ʈ���� ��ư�� ������ �� ������ ����
            Debug.Log("�޼� �� ���");
            if (HasBall != null) Catchball();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            // Ʈ���� ��ư�� ������ �� ������ ����
            Debug.Log("�޼� ������");
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
            print("�� ������ ����");
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

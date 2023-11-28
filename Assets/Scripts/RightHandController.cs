using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class RightHandController : MonoBehaviour
{
    GameObject HasBall;
    public GameObject bulletPrefab;
    public Transform BulletPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Oculus Ʈ���� ��ư �Է� Ȯ��
        /*if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            // Ʈ���� ��ư�� ������ �� ������ ����
            Debug.Log("������ �� ���");
            if (HasBall != null) Catchball();
        }*/
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // Ʈ���� ��ư�� ������ �� ������ ����
            Debug.Log("�Ѿ˹߻�");
            //if (HasBall != null) FireProjectile();
            //FireProjectile();
            Fire();
        }
        //print(GameManager.instance.ballCount);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�Ѿ˹߻�");
            //if (HasBall != null) FireProjectile();
            Fire();
        }
    }
    void Fire()
    {
        // �Ѿ� ������ ����
        GameObject bullet = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);


        Vector3 playerForward = BulletPos.transform.forward;
        // �Ѿ� �߻�
        bullet.GetComponent<Rigidbody>().velocity = playerForward * 100;

        // 2�� �ڿ� �ı�
        Destroy(bullet, 2.0f);
    }

    void Catchball()
    {
        HasBall.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void FireProjectile()
    {
        Instantiate(bulletPrefab,BulletPos.position,Quaternion.identity);

        /*Rigidbody ballRigidbody = bulletPrefab.GetComponent<Rigidbody>();
        Vector3 playerForward = transform.forward;
        ballRigidbody.velocity = playerForward.normalized * 10000;*/
        //HasBall = null;
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

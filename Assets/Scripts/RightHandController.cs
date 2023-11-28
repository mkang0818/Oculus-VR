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
        // Oculus 트리거 버튼 입력 확인
        /*if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            // 트리거 버튼이 눌렸을 때 수행할 동작
            Debug.Log("오른손 공 잡기");
            if (HasBall != null) Catchball();
        }*/
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // 트리거 버튼이 눌렸을 때 수행할 동작
            Debug.Log("총알발사");
            //if (HasBall != null) FireProjectile();
            //FireProjectile();
            Fire();
        }
        //print(GameManager.instance.ballCount);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("총알발사");
            //if (HasBall != null) FireProjectile();
            Fire();
        }
    }
    void Fire()
    {
        // 총알 프리팹 생성
        GameObject bullet = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);


        Vector3 playerForward = BulletPos.transform.forward;
        // 총알 발사
        bullet.GetComponent<Rigidbody>().velocity = playerForward * 100;

        // 2초 뒤에 파괴
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

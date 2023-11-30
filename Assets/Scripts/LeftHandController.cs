using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class LeftHandController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform BulletPos;
    private AudioSource GunSound;

    public HandRayCaster handray;
    float Curcooltime = 0;
    float cooltime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        GunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Curcooltime-= Time.deltaTime;
        if (Curcooltime <= 0)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                Curcooltime = cooltime;
                handray.lineRenderer.enabled = false;
                // 트리거 버튼이 눌렸을 때 수행할 동작
                Debug.Log("총알발사");
                GunSound.Play();
                Fire();
                Invoke("LayOn", 0.5f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Curcooltime = cooltime;
                handray.lineRenderer.enabled = false;
                Debug.Log("총알발사");
                GunSound.Play();
                Fire();
                Invoke("LayOn", 0.5f);
            }
        }
        
    }
    void LayOn()
    {
        handray.lineRenderer.enabled = true;
    }
    void Fire()
    {
        // 총알 프리팹 생성
        GameObject bullet = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);

        // 총알 발사
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;

        // 2초 뒤에 파괴
        Destroy(bullet, 2.0f);
    }
}

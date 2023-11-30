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
                // Ʈ���� ��ư�� ������ �� ������ ����
                Debug.Log("�Ѿ˹߻�");
                GunSound.Play();
                Fire();
                Invoke("LayOn", 0.5f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Curcooltime = cooltime;
                handray.lineRenderer.enabled = false;
                Debug.Log("�Ѿ˹߻�");
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
        // �Ѿ� ������ ����
        GameObject bullet = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);

        // �Ѿ� �߻�
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;

        // 2�� �ڿ� �ı�
        Destroy(bullet, 2.0f);
    }
}

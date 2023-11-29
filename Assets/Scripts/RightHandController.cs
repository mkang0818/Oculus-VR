using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class RightHandController : MonoBehaviour
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
        Curcooltime -= Time.deltaTime;
        if (Curcooltime <= 0)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Curcooltime = cooltime;
                Debug.Log("ÃÑ¾Ë¹ß»ç");
                Fire();
                GunSound.Play();
                handray.lineRenderer.enabled = false;
                Invoke("LayOn", 1);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Curcooltime = cooltime;
                handray.lineRenderer.enabled = false;
                GunSound.Play();
                Debug.Log("ÃÑ¾Ë¹ß»ç");
                Fire();
                Invoke("LayOn", 1);
            }
        }
        
    }
    void LayOn()
    {
        handray.lineRenderer.enabled = true;
    }
    void Fire()
    {
        // ÃÑ¾Ë ÇÁ¸®ÆÕ »ý¼º
        GameObject bullet = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);

        Vector3 playerForward = BulletPos.transform.forward;
        // ÃÑ¾Ë ¹ß»ç
        bullet.GetComponent<Rigidbody>().velocity = playerForward * 100;

        // 2ÃÊ µÚ¿¡ ÆÄ±«
        Destroy(bullet, 2.0f);
    }
}

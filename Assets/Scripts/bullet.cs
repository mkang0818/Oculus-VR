using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerForward = transform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Round"))
        {
            Destroy(gameObject);
        }   
    }
}

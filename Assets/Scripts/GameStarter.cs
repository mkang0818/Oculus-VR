using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameManager_Target gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        gm.isPlay = true;
        //gm.SetAGame();
        print("��ư�浹");
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}

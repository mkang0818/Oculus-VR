using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using VolumetricLines;

public enum ColorType
{
    Blue,
    Green,
    Red,
    White
}
public class Box : MonoBehaviour
{
    [SerializeField] int score;
    //Animator ani;
    [SerializeField] bool isHit = false;
    [SerializeField] List<GameObject> obj = new List<GameObject>();
    List<VolumetricLineBehavior> vl = new List<VolumetricLineBehavior>();

    [SerializeField] ColorType type;
    //[SerializeField] GameObject bullet;
    //[SerializeField] float atkTime;
    //[SerializeField] float atkTimer;
    //bool isAttacked = false;
    bool isScored = false;
    bool activation;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            vl.Add(obj[i].GetComponent<VolumetricLineBehavior>());
        }
        //ani = GetComponent<Animator>();
    }
    private void Update()
    {
        //if (type == Type.Attack)
        //{

        //    Attack();
        //}

    }
    public bool GetIsScored()
    {
        return isScored;
    }
    public void SetIsScored(bool value)
    {
        isScored = value;
    }
    public int GetScore()
    {
        return score;
        //return stat.GetScore();
    }
    public void SetScore(int value)
    {
        score = value;
        //stat.SetScore(value);
    }
    public bool GetActivation()
    {
        return activation;
        //return stat.GetActivation();
    }
    public void SetActivation(bool act)
    {
        activation = act;
        //stat.SetActivation(act);
    }
    public bool GetIsHit()
    {
        return isHit;
    }
    public void SetIsHit(bool value)
    {
        isHit = value;
    }
    public void SetType(ColorType value)
    {
        type = value;
    }
    public void TargetActivation()
    {
        SetActivation(true);
        Debug.Log(GetActivation());
        //SetIsAttacked(false);
        //atkTime = 0;
        //ani.SetBool("activation", true);
        //if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    SetActivation(true);
        //    SetIsAttacked(false);
        //    atkTime = 0;
        //    Debug.Log(stat.GetActivation());
        //}
    }
    public void TargetDeActivation()
    {
        SetActivation(false);
        type = ColorType.White;
        //ani.SetBool("activation", false);
    }
    public void BoxSetting(int value)
    {
        switch (value)
        {
            case 0:
                this.type = ColorType.White;
                for (int i = 0; i < vl.Count; i++)
                {
                    vl[i].LineColor = Color.white;
                }
                break;
            case 1:
                this.type = ColorType.Blue;
                for (int i = 0; i < vl.Count; i++)
                {
                    vl[i].LineColor = Color.blue;
                }
                score = 100;
                break;
            case 2:
                this.type = ColorType.Red;
                for (int i = 0; i < vl.Count; i++)
                {
                    vl[i].LineColor = Color.red;
                }
                score = 500;
                break;
            case 3:
                this.type = ColorType.Green;
                for (int i = 0; i < vl.Count; i++)
                {
                    vl[i].LineColor = Color.green;
                }
                score = 300;
                break;
        }
    }
    //public bool GetIsAttacked()
    //{
    //    return isAttacked;
    //}
    //public void SetIsAttacked(bool value)
    //{
    //    isAttacked = value;
    //}
    //public void Attack()
    //{
    //    atkTime += Time.deltaTime;
    //    if (!isAttacked && atkTime >= atkTimer)
    //    {
    //        if (type == Type.Attack)
    //        {
    //            Instantiate(bullet);
    //            TargetDeActivation();
    //            Debug.Log("공격함");
    //            isAttacked = true;
    //        }
    //    }
    //    if (isAttacked)
    //    {
    //        TargetDeActivation();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (type != ColorType.White)
        {
            if (activation)
            {
                isHit = true;
                TargetDeActivation();
                Debug.Log("TriggerEnter 충돌");
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (type == ColorType.Blue)
    //    {
    //        if (GetActivation())
    //        {
    //            isHit = true;
    //            TargetDeActivation();
    //            Debug.Log("CollisionEnter 충돌");
    //        }
    //    }
    //}
}
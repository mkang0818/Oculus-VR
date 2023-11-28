using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Target,
    Attack,
    Normal
}
public class Target : MonoBehaviour
{
    [SerializeField] int score;
    Animator ani;
    [SerializeField] bool isHit = false;
    
    [SerializeField] Type type;
    [SerializeField] GameObject bullet;
    [SerializeField] float atkTime;
    [SerializeField] float atkTimer;
    bool isAttacked = false;
    bool isScored = false;
    bool activation;
    // Start is called before the first frame update
    void Start()
    {
        ani= GetComponent<Animator>();
    }
    private void Update()
    {
        if(type== Type.Attack)
        {

            Attack();
        }
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
    public void SetType(Type value)
    {
        type = value;
    }
    public void TargetActivation()
    {
        ani.SetBool("activation", true);
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)
        {
            SetActivation(true);
            SetIsAttacked(false);
            SetType(Type.Target);
            atkTime = 0;
        }
    }
    public void TargetDeActivation()
    {
        SetActivation(false);
        type = Type.Normal;
        ani.SetBool("activation", false);
    }
    public bool GetIsAttacked()
    {
        return isAttacked;
    }
    public void SetIsAttacked(bool value)
    {
        isAttacked = value;
    }
    public void Attack()
    {
        atkTime += Time.deltaTime;
        if (!isAttacked&&atkTime >= atkTimer)
        {
            if (type == Type.Attack)
            {
                Instantiate(bullet);
                TargetDeActivation();
                Debug.Log("공격함");
                isAttacked = true;
            }
        }
        if (isAttacked)
        {
            TargetDeActivation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(type==Type.Target)
        {
            if (GetActivation())
            {
                isHit = true;
                TargetDeActivation();
                Debug.Log("충돌");
            }
        }
    }
}
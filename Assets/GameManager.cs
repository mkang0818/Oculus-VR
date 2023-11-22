using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<GameObject> matrix = new List<GameObject>();

    [SerializeField] int score = 0;
    //List<Target> target = new List<Target> ();
    List<Box> target = new List<Box>();
    // 몇 개 뽑을 것인가?
    int drawCount = 0;
    // 뽑은 숫자 저장
    List<int> number = new List<int>();
    public Text scoreText;
    public Button gameStart;
    ///////////////////////
    public GameObject ballPrefab;
    [HideInInspector]
    public int ballCount = 5;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        for (int i = 0; i < matrix.Count; i++)
        {
            //target.Add(matrix[i].GetComponent<Target>());
            target.Add(matrix[i].GetComponent<Box>());
        }

    }
    private void Update()
    {
        if (ballCount <= 4)
        {
            float x = Random.Range(-1.6f, 1.6f);
            float z = Random.Range(-6.9f, -9f);
            Instantiate(ballPrefab, new Vector3(x, -1, z), Quaternion.identity);
            ballCount++;
        }


        //// 한 Round의 시작 및 끝을 정해야 함
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetAGame();
        }

        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(point.ToString());
            //Instantiate(cube,new Vector3(point.x,point.y,point.z-4),Quaternion.identity);
        }

        // Target속성의 Node의 판정을 확인함
        foreach (int i in number)
        {
            if (!target[i].GetIsScored() && target[i].GetIsHit())
            {
                Debug.Log(target[i].GetIsScored());
                target[i].BoxSetting(0);
                score += target[i].GetScore();
                target[i].SetIsScored(true);
            }
        }

        scoreText.text = "Score : " + score;
        //for (int i = 0; i < 4; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        if (i == 0)
        //        {
        //            target[j].BoxSetting(ColorType.Red);
        //        }
        //        if (i == 1)
        //        {
        //            target[j + 4].BoxSetting(ColorType.Blue);
        //        }
        //        if (i == 2)
        //        {
        //            target[j + 8].BoxSetting(ColorType.Green);
        //        }
        //        if (i == 3)
        //        {
        //            target[j + 12].BoxSetting(ColorType.White);
        //        }
        //    }
        //}
    }











    // 무작위 Node를 활성화하는 함수
    void SetAGame()
    {
        SetDrawCount();
        DecideTargetNumber();
        TargetsActivation();

    }
    int SetDrawCount()
    {
        number.Clear();
        drawCount = Random.Range(1, matrix.Count);
        return drawCount;
    }
    void DecideTargetNumber()
    {
        Debug.Log("뽑기 횟수 = " + drawCount);
        for (int i = 0; i < drawCount; i++)
        {
            number.Add(new System.Random().Next(0, target.Count));
            Debug.Log("뽑힌 숫자 = " + number[i]);
        }
    }
    void TargetsActivation()
    {
        for (int i = 0; i < number.Count - 1; i++)
        {
            target[number[i]].SetIsHit(false);
            target[number[i]].BoxSetting(Random.Range(0, 4));
            target[number[i]].SetIsScored(false);
            target[number[i]].TargetActivation();
        }
        //target[number[number.Count-1].SetType(Type.Attack);
        //target[number[number.Count - 1].TargetActivation();
    }

    public void Oncclick()
    {
        SetAGame();
        gameStart.gameObject.SetActive(false);
    }
}
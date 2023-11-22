using Oculus.Interaction.Editor.Generated;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<GameObject> matrix = new List<GameObject>();
    public float gameTimer;
    public float roundTimer;

    bool isGameOver=false;
    float gameTime = 0;
    float roundTime = 0;
    [SerializeField] int score = 0;
    //List<Target> target = new List<Target> ();
    List<Box> target = new List<Box>();
    // 몇 개 뽑을 것인가?
    int drawCount = 0;
    List<int> number= new List<int>();
    // 뽑은 숫자 저장
    List<int> boxNum = new List<int>();
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
        //foreach (int i in boxNum)
        //{
        //    if (!target[i].GetIsScored() && target[i].GetIsHit())
        //    {
        //        Debug.Log(target[i].GetIsScored());
        //        target[i].BoxSetting(0);
        //        score += target[i].GetScore();
        //        target[i].SetIsScored(true);
        //    }
        //}

        RoundSetter();
        GameSetter();

        foreach (int i in boxNum)
        {
            if (target[i].GetIsHit())
            {
                Debug.Log(target[i].GetIsScored());
                score += target[i].GetScore();
                target[i].SetIsHit(false);
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


    void RoundSetter()
    {
        if(!isGameOver)
        {
            roundTime += Time.deltaTime;
        }

        if(roundTime > roundTimer)
        {
            SetAGame();
            roundTime = 0;
            Debug.Log("Round 끝, 게임 세팅됨");
        }
    }
    void GameSetter()
    {
        if (!isGameOver)
        {
            gameTime += Time.deltaTime;
        }

        if(gameTime > gameTimer)
        {
            isGameOver = true;
            Debug.Log("Game 끝.");
        }
    }

    void SetNumber()
    {
        number.Clear();
        for(int i = 0; i < target.Count; i++)
        {
            number.Add(i);
        }
    }

    void TargetInitialization()
    {
        if (boxNum.Count == 0)
        {
            Debug.Log("boxNum.Count is Null");
        }
        else
        {
            foreach (int i in boxNum)
            {
                target[i].TargetDeActivation();
                target[i].SetIsHit(false);
                target[i].BoxSetting(0);
            }
        }
    }





    // 무작위 Node를 활성화하는 함수
    void SetAGame()
    {
        TargetInitialization();
        SetNumber();
        SetDrawCount();
        DecideTargetboxNum();
        TargetsActivation();

    }
    int SetDrawCount()
    {
        boxNum.Clear();
        drawCount = Random.Range(1, matrix.Count);
        return drawCount;
    }
    void DecideTargetboxNum()
    {
        Debug.Log("뽑기 횟수 = " + drawCount);
        
        for (int i = 0; i < drawCount; i++)
        {
            int randomIndex = Random.Range(0, number.Count);
            boxNum.Add(number[randomIndex]);
            number.RemoveAt(randomIndex);
            Debug.Log("뽑힌 숫자 = " + boxNum[i]);
        }
    }
    void TargetsActivation()
    {
        for (int i = 0; i < boxNum.Count; i++)
        {
            target[boxNum[i]].SetIsHit(false);
            target[boxNum[i]].BoxSetting(Random.Range(1, 4));
            target[boxNum[i]].SetIsScored(false);
            target[boxNum[i]].TargetActivation();
        }
        //target[boxNum[boxNum.Count-1].SetType(Type.Attack);
        //target[boxNum[boxNum.Count - 1].TargetActivation();
    }

    public void Oncclick()
    {
        SetAGame();
        gameStart.gameObject.SetActive(false);
    }
}
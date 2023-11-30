using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Target : MonoBehaviour
{
    public static GameManager_Target instance = null;
    public List<GameObject> matrix = new List<GameObject>();
    public float gameTimer;
    public float roundTimer;
    public float startTimer;

    bool isGameStart = false;
    bool isGameOver = false;
    public float StartTime { get { return startTime; } }
    float startTime = 0;
    float gameTime = 0;
    float roundTime = 0;
    [SerializeField] int score = 0;
    List<Target> target = new List<Target> ();
    //List<Box> target = new List<Box>();
    // 몇 개 뽑을 것인가?
    int drawCount = 0;
    List<int> number = new List<int>();
    // 뽑은 숫자 저장
    List<int> boxNum = new List<int>();
    public Text scoreText;
    public GameObject gameFinish;

    Text finishscore;
    ///////////////////////
    //public GameObject ballPrefab;
    [HideInInspector]
    public int ballCount = 5;

    public bool isPlay = false;
    private void Awake()
    {
        isGameStart = true;
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
            target.Add(matrix[i].GetComponent<Target>());
            //target.Add(matrix[i].GetComponent<Box>());
        }
    }
    private void Update()
    {
        scoreText.text = "Score : " + score.ToString();

        finishscore = gameFinish.transform.GetChild(1).GetComponent<Text>();

        finishscore.text = "Score : " + score.ToString();

        if (isGameOver)
        {
            gameFinish.gameObject.SetActive(true);
        }
        //// 한 Round의 시작 및 끝을 정해야 함
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetAGame();
        }

        //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        //        Input.mousePosition.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(point.ToString());
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameReSetter();
        }
        
        if (isPlay)
        {
            GameStarter();
            if (isGameStart)
            {
                RoundSetter();
                GameSetter();
            }
        }

        foreach (int i in boxNum)
        {
            if (target[i].GetIsHit())
            {
                //Debug.Log(target[i].GetIsScored());
                score += target[i].GetScore();
                target[i].SetIsHit(false);
            }
        }

        //scoreText.text = "Score : " + score;
    }

    //void SceneReStarter()
    //{

    //}
    void GameStarter()
    {
        if(isPlay)
        {
            startTime += Time.deltaTime;
            if (startTime >= startTimer)
            {
                isGameStart = true;
            }
        }
        
    }
    void GameReSetter()
    {
        isPlay = false;
        isGameStart = false;
        isGameOver = false;
        startTime = 0;
        roundTime = 0;
        gameTime = 0;

        foreach (int i in boxNum)
        {
            target[i].TargetDeActivation();
        }
    }
    void RoundSetter()
    {
        if (!isGameOver)
        {
            roundTime += Time.deltaTime;
        }

        if (roundTime > roundTimer)
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

        if (gameTime > gameTimer)
        {
            isGameOver = true;
            Debug.Log("Game 끝.");
        }
    }

    void SetNumber()
    {
        number.Clear();
        for (int i = 0; i < target.Count; i++)
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
                //target[i].BoxSetting(0);
            }
        }
    }





    // 무작위 Node를 활성화하는 함수
    public void SetAGame()
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
        //Debug.Log("뽑기 횟수 = " + drawCount);

        for (int i = 0; i < drawCount; i++)
        {
            int randomIndex = Random.Range(0, number.Count);
            boxNum.Add(number[randomIndex]);
            number.RemoveAt(randomIndex);
            //Debug.Log("뽑힌 숫자 = " + boxNum[i]);
        }
    }
    void TargetsActivation()
    {
        for (int i = 0; i < boxNum.Count; i++)
        {
            target[boxNum[i]].SetIsHit(false);
            //target[boxNum[i]].BoxSetting(Random.Range(1, 4));
            target[boxNum[i]].SetIsScored(false);
            target[boxNum[i]].TargetActivation();
        }
        //target[boxNum[boxNum.Count-1].SetType(Type.Attack);
        //target[boxNum[boxNum.Count - 1].TargetActivation();
    }

}

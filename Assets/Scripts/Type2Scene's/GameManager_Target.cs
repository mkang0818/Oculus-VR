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
        GameStarter();
        GameChecker();
        TargetHitChecker();
        GameReSetter();
        //scoreText.text = "Score : " + score;
    }


    // 게임의 라운드와 끝을 확인하는 함수
    void GameChecker()
    {
        if (isGameStart)
        {
            RoundStarter();
            GameFinisher();
        }
    }
    // Target이 맞았는지 확인 후 점수 반환 및, 맞은 횟수 초기화
    void TargetHitChecker()
    {
        foreach (int i in boxNum)
        {
            if (target[i].GetIsHit())
            {
                //Debug.Log(target[i].GetIsScored());
                score += target[i].GetScore();
                target[i].SetIsHit(false);
            }
        }
    }
    // 게임 시작전의 숫자를 세는 함수
    void GameStarter()
    {
        if(!isGameStart)
        {
            startTime += Time.deltaTime;
            if (startTime >= startTimer)
            {
                isGameStart = true;
                SetAGame() ;
            }
        }
        
    }
    // 결과를 초기화하며 재시작함
    void GameReSetter()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isPlay = false;
            isGameStart = false;
            isGameOver = false;
            startTime = 0;
            roundTime = 0;
            gameTime = 0;

            TargetInitialization();
        }
    }
    // 한 라운드를 시작함
    void RoundStarter()
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
    // gameTime이 끝났을 때 게임을 끝냄
    void GameFinisher()
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
    // 뽑힌 Target의 번호를 초기화함
    void SetNumber()
    {
        number.Clear();
        for (int i = 0; i < target.Count; i++)
        {
            number.Add(i);
        }
    }
    // Target을 모두 비활성화 상태로 초기화함
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
    // 한 게임을 시작함
    public void SetAGame()
    {
        TargetInitialization();
        SetNumber();
        SetDrawCount();
        DecideTargetboxNum();
        TargetsActivation();

    }
    // 활성화 할 Target의 갯수를 정함
    int SetDrawCount()
    {
        boxNum.Clear();
        drawCount = Random.Range(1, matrix.Count);
        return drawCount;
    }
    // 활성화 할 Target의 번호를 정함
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
    // 추첨된 Target을 활성화 함
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

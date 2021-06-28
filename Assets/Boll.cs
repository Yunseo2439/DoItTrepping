using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boll : MonoBehaviour
{
    [Header("공 관련")]
    public Text scoretext;
    Rigidbody2D rigid;
    public float minSpeed = 2;
    public float maxSpeed = 4;
    public float minFors = 3;
    public float maxFors = 5;
    public int score;

    [Header("게임오버 관련")]
    public GameObject gameOverObj;
    bool gameOver = false;
    public Text gameOverScore;

    [Header("피버 관련")]
    public GameObject feverCanvas;
    public int feverPoint = 0;
    public Slider feverSlider;
    bool isFever = false;

    [Header("타이틀 관련")]
    public GameObject UiCanvas;
    public GameObject TitleCanvas;


    void Start()
    {
        feverCanvas.SetActive(false);
        UiCanvas.SetActive(false);
        TitleCanvas.SetActive(true);
        gameOverObj.SetActive(false);
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }

    void Update()
    {
 
    }

    void OnMouseDown()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UiCanvas.SetActive(true);
        TitleCanvas.SetActive(false);

        if (rigid.gravityScale == 0)
        {
            rigid.gravityScale = 1;
        }
        rigid.velocity = Vector2.zero;
        if (pos.x > transform.position.x)
        {
            Debug.Log("오른쪽");
            rigid.AddForce(new Vector2(Random.Range(-maxSpeed,-minSpeed),Random.Range(minFors,maxFors)), ForceMode2D.Impulse);
            rigid.AddTorque(Random.Range(0f,60f));
        }
        else
        {
            Debug.Log("왼쪽");
            rigid.AddForce(new Vector2(Random.Range(minSpeed,maxSpeed), Random.Range(minFors, maxFors)), ForceMode2D.Impulse);
            rigid.AddTorque(Random.Range(-60f, 0f));
        }
        score++;
        scoretext.text = $"{score}";
        feverSlider.value = feverPoint;
        if (feverPoint>=30)
        {
            isFever = true;
            if(isFever)
            {
                feverCanvas.SetActive(true);
                StartCoroutine("Fever");
                feverPoint -= feverPoint;
                
            }
        }
        if (!isFever)
        {
            feverCanvas.SetActive(false);
            rigid.gravityScale = 1;
            minSpeed = 2;
            maxSpeed = 4;
            minFors = 3;
            maxFors = 5;
            feverPoint++;
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        gameOver = true;
        gameOverScore.text = $"{score}";
        if (gameOver)
        {
            gameOverObj.SetActive(true);
        }
    }
    IEnumerator Fever()
    {
        rigid.gravityScale = 0.1f;
        minSpeed = 0.1f;
        maxSpeed = 0.1f;
        minFors = 0.1f;
        maxFors = 0.1f;
        yield return new WaitForSeconds(2f);
        isFever = false;
    }
}

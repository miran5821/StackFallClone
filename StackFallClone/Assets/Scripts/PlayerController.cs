using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    bool isCollision;
    [SerializeField] float upForce, downForce;
    [SerializeField] GameObject ballEffect;
    float invincibleTimer;
    bool isInvincible;//yenilmez

    public int currentObstacle;
    public int totalObstacle;
    [SerializeField] Transform obstacleParent;
    [SerializeField] GameObject nextLevelPanel;
    public enum PlayerState
    {
        Prepare,//hazýrlýk
        Playing,//oynayýþ
        Died,//öldü
        Finish//bitiþ
    }
    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacle = 0;
    }
    private void Start()
    {
        StartCoroutine(ObstacleFind());
        
    }
    IEnumerator ObstacleFind() // instantiate edilen objeyi start ta bulamadým
    {
        yield return new WaitForSeconds(0.5f);
        totalObstacle = obstacleParent.childCount;
    }
    bool nextLevel;
    void Update()
    {
        if(playerstate == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
                isCollision = true;
            if (Input.GetMouseButtonUp(0))
                isCollision = false;

            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime * 0.4f;
                if (!ballEffect.activeInHierarchy)
                    ballEffect.SetActive(true);
            }
            else
            {
                if (ballEffect.activeInHierarchy)
                    ballEffect.SetActive(false);

                if (isCollision)
                    invincibleTimer += Time.deltaTime;
                else
                    invincibleTimer -= Time.deltaTime * 0.5f;
            }



            if (invincibleTimer >= 1)//yenilmezlik için gerekli süre
            {
                invincibleTimer = 1;
                isInvincible = true;
            }
            else if (invincibleTimer <= 0)
            {
                invincibleTimer = 0;
                isInvincible = false;
            }
        }

        if(playerstate == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
                playerstate = PlayerState.Playing;
        }
        if (playerstate == PlayerState.Finish)
        {
            nextLevelPanel.SetActive(true);
            rb.isKinematic = true;
        }
    }
    
    private void FixedUpdate()
    {
        if (playerstate == PlayerState.Playing)
        {
            if (isCollision)
            {
                rb.velocity = new Vector3(0, -downForce * Time.fixedDeltaTime);
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollision)
        {
            rb.velocity = new Vector3(0, upForce * Time.deltaTime);
        }
        else
        {
            if (isInvincible)
            {
                if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("plane"))
                {
                    collision.gameObject.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    currentObstacle++;
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("enemy"))
                {
                    collision.gameObject.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    currentObstacle++;
                }
                else if (collision.gameObject.CompareTag("plane"))
                {
                    UIManager.Instance.gameOverPanel.SetActive(true);
                    rb.isKinematic = true;
                }
            }
        }
        UIManager.Instance.UpdateProggresFill(currentObstacle / (float)totalObstacle);

        if (collision.gameObject.CompareTag("Finish")&&playerstate == PlayerState.Playing)
        {
            playerstate = PlayerState.Finish;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!isCollision|| collision.gameObject.CompareTag("Finish"))
        {
            rb.velocity = new Vector3(0, upForce * Time.deltaTime);
        }
    }
}

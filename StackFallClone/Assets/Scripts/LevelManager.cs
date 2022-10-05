using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [HideInInspector]
    [SerializeField] GameObject[] obstaclePrefab = new GameObject[4];
    [SerializeField] GameObject[] obstacleModel;
    [SerializeField] GameObject winPrefab;
    [SerializeField] Transform obstacleParent;
    GameObject temp1Obstacle, temp2Obstacle;
    int level=1;
    float obstacleNumber=0;
    int[] rotateArray = new int[] { 0,0,0,90,180 };

    
    void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        ObstacleGenerator();
        for (obstacleNumber = 0; obstacleNumber > -level-8; obstacleNumber-=0.5f)// for döngüsünün her döngüsü levele göre deðiþir
        {
            //Levellerin zorluðu
            if (level<=20)
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(0, 2)], obstacleParent);
            else if (level>20 && level<50)
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(0, 3)], obstacleParent);
            else
                temp1Obstacle = Instantiate(obstaclePrefab[Random.Range(2, 4)], obstacleParent);

            temp1Obstacle.transform.position = new Vector3(0,obstacleNumber-0.01f,0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 10, 0);
            temp1Obstacle.transform.eulerAngles += Vector3.up * rotateArray[Random.Range(0, 5)];
        }
        temp2Obstacle = Instantiate(winPrefab);
        temp2Obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
        UIManager.Instance.SetLevelText(PlayerPrefs.GetInt("Level"));

    }
    
    public void ObstacleGenerator()
    {
        int index = Random.Range(0, 5);
        switch (index)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i + 16];
                }
                break;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}

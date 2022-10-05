using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //[SerializeField] Obstacle[] obstacles;
    public List<Obstacle> obstacles;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            obstacles.Add(transform.GetChild(i).GetComponent<Obstacle>());
        }
    }
    public void ShatterAllObstacles()
    {
        if (transform.parent != null)
        { 
            transform.parent = null;
        }
        foreach (Obstacle item in obstacles)
        {
            item.Shatter();
        }
        StartCoroutine(RemoveAllShetterPart());
    }

    IEnumerator RemoveAllShetterPart()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

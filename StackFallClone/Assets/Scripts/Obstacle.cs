using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer meshRenderer;
    Collider colider;
    ObstacleController obstacleController;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        colider = GetComponent<Collider>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }
    public void Shatter()
    {
        rb.isKinematic = false;
        colider.enabled = false;
        Vector3 forcePoint = transform.parent.position;//gücün uygulanacaðý yeri merkez alýr
        float parentXpos = transform.parent.position.x;// 0
        float xPos = meshRenderer.bounds.center.x; // daðýlacak nesnenin orta noktasý
        Vector3 subDirection = (parentXpos - xPos < 0 ) ? Vector3.right : Vector3.left; //0- daðýlacak parçanýn x ine göre saða mý sola mý daðýlacak
        Vector3 direction = (Vector3.up * 1.5f + subDirection).normalized;
        float force = Random.Range(20, 40);
        float torque = Random.Range(90, 180);
        rb.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * torque);
        rb.velocity = Vector3.down;
        
    }
   
    
}

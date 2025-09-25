using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2D : MonoBehaviour
{
    [SerializeField] private float catOp;
    [SerializeField] private float catAd;
    [SerializeField] private float angulo;
    [SerializeField] private bool usarAngulo;
    [SerializeField] private bool antihorario = true;

    private Vector3 vector;

    private void Start() {
        float multiplier = antihorario ? 1 : -1;

        if(!usarAngulo){
            catAd = 1 / Mathf.Sqrt(2);
            catOp = 1 / Mathf.Sqrt(2);

            vector = new Vector3(catAd, catOp * multiplier, 0);
        }else{
            vector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0);
        }

        
        vector.Normalize();

        catAd = vector.x;
        catOp = vector.y;

        //X' = X * SIN(angle) - Y * COS(angle)
        //Y' = X * COS(angle) + Y * SIN(angle)
        transform.position = new Vector3(transform.position.x * vector.x - transform.position.y * vector.y, transform.position.x * vector.y + transform.position.y * vector.x,0);
    }

    private void FixedUpdate() {
        if(Input.GetKey(KeyCode.Space)){
            transform.position = new Vector3(transform.position.x * vector.x - transform.position.y * vector.y, transform.position.x * vector.y + transform.position.y * vector.x,0);
        }
    }
}

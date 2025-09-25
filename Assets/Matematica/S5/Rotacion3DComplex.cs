using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion3DComplex : MonoBehaviour
{
    [SerializeField] private Vector3 angulos;
    [SerializeField] private Quaternion qx = Quaternion.identity; //<0,,0,0,1>
    [SerializeField] private Quaternion qy = Quaternion.identity; //<0,,0,0,1>
    [SerializeField] private Quaternion qz = Quaternion.identity; //<0,,0,0,1>

    [SerializeField] private Quaternion r = Quaternion.identity; //<0,,0,0,1>
    private float anguloSen;
    private float anguloCos;

    private void Start() {
        
    }

    private void FixedUpdate() {

        //rotacion z -> x -> y
        anguloSen = Mathf.Sin(Mathf.Deg2Rad * angulos.z * 0.5f);
        anguloCos = Mathf.Cos(Mathf.Deg2Rad * angulos.z * 0.5f);
        qz.Set(0, 0, anguloSen, anguloCos);

        anguloSen = Mathf.Sin(Mathf.Deg2Rad * angulos.x * 0.5f);
        anguloCos = Mathf.Cos(Mathf.Deg2Rad * angulos.x * 0.5f);
        qx.Set(anguloSen, 0, 0, anguloCos);

        anguloSen = Mathf.Sin(Mathf.Deg2Rad * angulos.y * 0.5f);
        anguloCos = Mathf.Cos(Mathf.Deg2Rad * angulos.y * 0.5f);
        qy.Set(0, anguloSen, 0, anguloCos);
        
        r = qy * qx * qz;

        transform.rotation = r;
    }
}

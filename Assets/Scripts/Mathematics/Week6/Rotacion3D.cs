using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mathematics.Week6
{
    public class Rotacion3D : MonoBehaviour
    {
        [SerializeField] protected float angulo;
        [SerializeField] protected Quaternion q = Quaternion.identity; //<0,0,0,1>
        protected float anguloSen;
        protected float anguloCos;

        protected void Start()
        {

        }

        protected virtual void FixedUpdate()
        {
            anguloSen = Mathf.Sin(Mathf.Deg2Rad * angulo * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * angulo * 0.5f);

            q.Set(0, anguloSen, 0, anguloCos);

            transform.rotation = q;
        }
    }
}
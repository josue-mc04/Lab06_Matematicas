using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Mathematics.Week6
{
    public class PlaneController : MonoBehaviour
    {
        [Header("Controls Properties")]
        [SerializeField] private float pitchPlane;
        [SerializeField] private float pitchGain = 1f;
        [SerializeField] private MinMax pitchTreshHold;
        [SerializeField] private float rollPlane;
        [SerializeField] private float rollhGain = 1f;
        [SerializeField] private MinMax rollTreshHold;

        [Header("Rotation Data")]
        [SerializeField] private Quaternion qx = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qy = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qz = Quaternion.identity; //<0,,0,0,1>

        [SerializeField] private Quaternion r = Quaternion.identity; //<0,,0,0,1>
        private float anguloSen;
        private float anguloCos;

        protected float _pitchDirection = 0f;
        protected float _rollDirection = 0f;

        private void FixedUpdate()
        {
            pitchPlane += _pitchDirection * pitchGain;

            pitchPlane = Mathf.Clamp(pitchPlane, pitchTreshHold.MinValue, pitchTreshHold.MaxValue);

            rollPlane += _rollDirection * rollhGain;

            rollPlane = Mathf.Clamp(rollPlane, rollTreshHold.MinValue, rollTreshHold.MaxValue);

            //rotacion z -> x -> y
            anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qz.Set(0, 0, anguloSen, anguloCos);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * pitchPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * pitchPlane * 0.5f);
            qx.Set(anguloSen, 0, 0, anguloCos);

            /*anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qy.Set(0, anguloSen, 0, anguloCos);*/

            //multiplicación y -> x -> z
            r = qy * qx * qz;

            transform.rotation = r;

            UpdatePosition();

            if (transform.position.x > MaxLimitX)
            {
                transform.position = new Vector3(MaxLimitX, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < Min_LimitX)
            {
                transform.position = new Vector3(Min_LimitX, transform.position.y, transform.position.z);
            }

            if (transform.position.y > MaxLimitY)
            {
                transform.position = new Vector3(transform.position.x, MaxLimitY, transform.position.z);
            }
            else if (transform.position.y < Min_LimitY)
            {
                transform.position = new Vector3(transform.position.x, Min_LimitY, transform.position.z);
            }
        }



        //Pitch -> X Axis
        public void RotatePitch(InputAction.CallbackContext context)
        {
            _pitchDirection = context.ReadValue<float>();
        }

        //Roll -> Z Axis
        public void RotateRoll(InputAction.CallbackContext context)
        {
            _rollDirection = context.ReadValue<float>();
        }






        private float _verticalDirection = 0f;
        private float _horizontalDirection = 0f;
        [SerializeField] private float velocitySpeed = 5f;
        [SerializeField] private float returnSpeed = 0.5f; // velocidad del Lerp al volver al centro

        private Rigidbody _myRB;

        private void Start()
        {
            _myRB = GetComponent<Rigidbody>();
            vidas = 3;
        }























        public void TranslateVertical(InputAction.CallbackContext context)
        {
            if (transform.position.y <= MaxLimitY && transform.position.y >= Min_LimitY)
            {
                _verticalDirection = context.ReadValue<float>();
            }
            else
            {
                _verticalDirection = 0f;
            }

        }

        public void TranslateHorizontal(InputAction.CallbackContext context)
        {
            if (transform.position.x <= MaxLimitX && transform.position.x >= Min_LimitX)
            {
                _horizontalDirection = context.ReadValue<float>();
            }
            else
            {
                _horizontalDirection = 0f;
            }

        }

        private void UpdatePosition()
        {
            // si no hay input, regresa al centro (0,0) suavemente
            if (_horizontalDirection == 0f && _verticalDirection == 0f)
            {
                Vector3 target = new Vector3(0f, 0f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, target, Time.fixedDeltaTime * returnSpeed);
                _myRB.linearVelocity = Vector3.zero;
            }
            else
            {
                _myRB.linearVelocity = new Vector3(-_horizontalDirection * velocitySpeed, -_verticalDirection * velocitySpeed, 0f);
            }
        }
        [SerializeField] private float MaxLimitX = 13f;
        [SerializeField] private float MaxLimitY = 6.39f;
        [SerializeField] private float Min_LimitX = -13f;
        [SerializeField] private float Min_LimitY = -0.1f;



        // ------------------- VIDA + GAME OVER -------------------

        [Header("Vida del Avión")]
        [SerializeField] private int vidas = 3;
        [SerializeField] private Vector3 posicionInicial = Vector3.zero;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                vidas--;

                Debug.Log("Vida perdida, vidas restantes: " + vidas);

                if (vidas > 0)
                {
               
                    transform.position = new Vector3(0f, 0f, transform.position.z);
                    _myRB.linearVelocity = Vector3.zero;
                }
                else
                {
               
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    [System.Serializable]
    public struct MinMax
    {
        public float MinValue;
        public float MaxValue;
    }
}

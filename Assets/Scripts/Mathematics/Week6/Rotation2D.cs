using UnityEngine;
using UnityEngine.InputSystem;

namespace Mathematics.Week6
{
    public class Rotation2D : MonoBehaviour
    {
        [SerializeField] private float catOp;
        [SerializeField] private float catAd;
        [SerializeField] private float angulo;
        [SerializeField] private bool usarAngulo;
        [SerializeField] private bool antihorario = true;
        [SerializeField] private Transform centerPivot;

        private Vector3 vector;
        private bool _isPerformed;

        private void Awake()
        {
            //Debug.Log(Mathf.Sin(Mathf.Deg2Rad * 90f));
        }

        private void Start()
        {
            float multiplier = antihorario ? 1 : -1;

            angulo *= multiplier;

            /*if (!usarAngulo)
            {
                catAd = 1 / Mathf.Sqrt(2);
                catOp = 1 / Mathf.Sqrt(2);

                vector = new Vector3(catAd, catOp * multiplier, 0);
            }
            else
            {
                vector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0);
            }*/

            vector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angulo), Mathf.Sin(Mathf.Deg2Rad * angulo), 0);

            //vector.Normalize();

            catAd = vector.x;
            catOp = vector.y;

            //X' = X * COS(angle) - Y * SIN(angle)
            //Y' = X * SIN(angle) + Y * COS(angle)
            //transform.position = new Vector3(transform.position.x * vector.x - transform.position.y * vector.y, transform.position.x * vector.y + transform.position.y * vector.x, 0);

            transform.position = RotateIn2D(transform.position, angulo, centerPivot.position);
        }

        private void FixedUpdate()
        {
            if (_isPerformed)
            {
                //transform.position = new Vector3(transform.position.x * vector.x - transform.position.y * vector.y, transform.position.x * vector.y + transform.position.y * vector.x, 0);

                transform.position = RotateIn2D(transform.position, angulo, centerPivot.position);
            }

            transform.position = RotateIn2D(transform.position, angulo, centerPivot.position);
        }

        public void ApplyTransformation(InputAction.CallbackContext ctx)
        {
            _isPerformed = ctx.performed;
        }

        private Vector3 RotateIn2D(Vector2 position, float angle, Vector2 origin)
        {
            return new Vector3(CalculateX(position, angle, origin), CalculateY(position, angle, origin), 0);
        }

        //X' - a = (X - a) * COS(angle) - (Y - b) * SIN(angle)
        private float CalculateX(Vector2 position, float angle, Vector2 origin)
        {
            float _x = origin.x + (position.x - origin.x) * Mathf.Cos(Mathf.Deg2Rad * angle) - (position.y - origin.y) * Mathf.Sin(Mathf.Deg2Rad * angle);
            return _x;
        }

        //Y' - b = (X - a) * SIN(angle) + (Y - b) * COS(angle)
        private float CalculateY(Vector2 position, float angle, Vector2 origin)
        {
            float _y = origin.y + (position.x - origin.x) * Mathf.Sin(Mathf.Deg2Rad * angle) + (position.y - origin.y) * Mathf.Cos(Mathf.Deg2Rad * angle);
            return _y;
        }
    }
}
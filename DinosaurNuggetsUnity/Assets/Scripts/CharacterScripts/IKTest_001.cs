using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace DinoNuggets.IK
//{
    public class IKTest_001 : MonoBehaviour
    {
        [Header("Joints")]
        [SerializeField] private Transform joint0;
        [SerializeField] private Transform joint1;
        [SerializeField] private Transform hand;

        [Header("Target")]
        [SerializeField] private Transform target;


        private float _length0;
        private float _length1;
    
        // Start is called before the first frame update
        void Start()
        {
            _length0 = Vector2.Distance(joint0.position, joint1.position);
            _length1 = Vector2.Distance(joint1.position, hand.position);
        }

        // Update is called once per frame
        void Update()
        {
            IKCal();
        }

        private void IKCal()
        {
            float jointAngle0;
            float jointAngle1;

            //Distance from Joint0 to target
            float length2 = Vector2.Distance(joint0.position, hand.position);

            //Angle from joint0 and target
            Vector2 diff = target.position - joint0.position;
            float atan = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            if(_length0 + _length1 < length2)
            {
                jointAngle0 = atan;
                jointAngle1 = 0f;
            }
            else
            {
                //Inner angle alpha
                float cosAngle0 = ((length2 * length2) + (_length0 * _length0) - (_length1 * _length1)) / (2 * length2 * _length0);
                float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;

                //Inner angle beta
                float cosAngle1 = ((_length1 * _length1) + (_length0 * _length0) - (length2 * length2)) / (2 * _length1 * _length0);
                float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

                //Working from reference frame
                jointAngle0 = atan - angle0; //Angle A
                jointAngle1 = 180f - angle1; //Angle B
            }

            Vector3 Euler0 = joint0.transform.localRotation.eulerAngles;
            Euler0.z = jointAngle0+0.001f;
            //joint0.transform.localEulerAngles = Euler0;
            joint0.transform.localRotation = Quaternion.Euler(Euler0);

            Vector3 Euler1 = joint1.transform.localRotation.eulerAngles;
            Euler1.z = jointAngle1+0.001f;
            //joint1.transform.localEulerAngles = Euler1;
            joint1.transform.localRotation = Quaternion.Euler(Euler1);
        }



    }
//}
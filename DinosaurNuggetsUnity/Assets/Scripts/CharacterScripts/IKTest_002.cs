﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest_002 : MonoBehaviour
{
    public float angle1;    // Angle of bone 1
    public float angle2;    // Angle of bone 2
    bool solvePosAngle2;    // Solve for positive angle 2 instead of negative angle 2
    float length1;          // Length of bone 1. Assumed to be >= zero
    float length2;          // Length of bone 2. Assumed to be >= zero
    float targetX;          // Target x position for the bones to reach
    float targetY;          // Target y position for the bones to reach
    public GameObject arm_A;
    public GameObject arm_B;
    public GameObject arm_C;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        length1 = Vector3.Distance(arm_A.transform.position, arm_B.transform.position);
        length2 = Vector3.Distance(arm_B.transform.position, arm_C.transform.position);
        targetX = target.transform.position.x;
        targetY = target.transform.position.z;
        IKRigs();
        arm_A.transform.rotation = Quaternion.Euler(new Vector3(0f, angle1 * Mathf.Rad2Deg, 0f));
        arm_B.transform.rotation = Quaternion.Euler(new Vector3(0f, angle2 * Mathf.Rad2Deg, 0f));
    }

    private bool IKRigs()
    {
        const float epsilon = 0.0001f; // used to prevent division by small numbers
    
        bool foundValidSolution = true;
    
        float targetDistSqr = (targetX*targetX + targetY*targetY);
    
        //===
        // Compute a new value for angle2 along with its cosine
        float sinAngle2;
        float cosAngle2;
    
        float cosAngle2_denom = 2*length1*length2;

        if( cosAngle2_denom > epsilon )
        {
            cosAngle2 =   (targetDistSqr - length1*length1 - length2*length2)
                        / (cosAngle2_denom);
    
            // if our result is not in the legal cosine range, we can not find a
            // legal solution for the target
            if( (cosAngle2 < -1.0f) || (cosAngle2 > 1.0f) )
                foundValidSolution = false;
    
            // clamp our value into range so we can calculate the best
            // solution when there are no valid ones
            cosAngle2 = Mathf.Max(-1f, Mathf.Min( 1f, cosAngle2 ) );
    
            // compute a new value for angle2
            angle2 = Mathf.Acos( cosAngle2 );
    
            // adjust for the desired bend direction
            if( !solvePosAngle2 )
                angle2 = -angle2;
    
            // compute the sine of our angle
            sinAngle2 = Mathf.Sin( angle2 );
        }
        else
        {
            // At least one of the bones had a zero length. This means our
            // solvable domain is a circle around the origin with a radius
            // equal to the sum of our bone lengths.
            float totalLenSqr = (length1 + length2) * (length1 + length2);
            if(    targetDistSqr < (totalLenSqr-epsilon)
                || targetDistSqr > (totalLenSqr+epsilon) )
            {
                foundValidSolution = false;
            }
    
            // Only the value of angle1 matters at this point. We can just
            // set angle2 to zero. 
            angle2    = 0.0f;
            cosAngle2 = 1.0f;
            sinAngle2 = 0.0f;
        }
    
        //===
        // Compute the value of angle1 based on the sine and cosine of angle2
        float triAdjacent = length1 + length2*cosAngle2;
        float triOpposite = length2*sinAngle2;
    
        float tanY = targetY*triAdjacent - targetX*triOpposite;
        float tanX = targetX*triAdjacent + targetY*triOpposite;
    
        // Note that it is safe to call Atan2(0,0) which will happen if targetX and
        // targetY are zero
        angle1 = Mathf.Atan2( tanY, tanX );
    
        return foundValidSolution;
    }

}

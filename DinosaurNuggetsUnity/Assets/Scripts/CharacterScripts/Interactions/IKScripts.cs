using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKScripts : MonoBehaviour
{
    private Vector3 rightFootPos, leftFootPos, rightFootIKPos, leftFootIKPos;
    private Quaternion rightFootIKRot, leftFootIKRot;
    private float lastPelvisPosY, lastRightFootPosY, lastLeftFootPosY;
    private Animator anim;

    [Header("Feet Grounder")]
    public bool enableFeetIK = true;
    [Range(0f, 2f)][SerializeField]private float heightFromGroundRayCast = 1.14f;
    [Range(0f, 2f)][SerializeField]private float rayCastDownDist = 1.5f;
    [SerializeField] private LayerMask envLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0f, 1f)][SerializeField]private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0f, 1f)][SerializeField]private float feetToIkPosSpeed = 0.5f;

    public string leftFootAnimVarName = "LeftFootCurve";
    public string rightFootAnimVarName = "RightFootCurve";

    public bool useProIKFeature = false;
    public bool showSolverDebug = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(enableFeetIK == false){return;}
        if(anim == null){return;}

        AdjustFeetTarget(ref rightFootPos, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPos, HumanBodyBones.LeftFoot);

        //find and raycast to the ground to find positions
        FeetPositionSolver(rightFootPos, ref rightFootIKPos, ref rightFootIKRot);
        FeetPositionSolver(leftFootPos, ref leftFootIKPos, ref leftFootIKRot);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if(enableFeetIK == false){return;}
        if(anim == null){return;}

        MovePelvisHeight();

        //ik and pos - utilise pro features?
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        if(useProIKFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVarName));
        }
        MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPos, rightFootIKRot, ref lastRightFootPosY);


        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        if(useProIKFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVarName));
        }
        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPos, leftFootIKRot, ref lastLeftFootPosY);

    }

    private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 posIKHolder, Quaternion rotIKHolder, ref float lastFootPosY)
    {
        Vector3 targetIKPos = anim.GetIKPosition(foot);
        if(posIKHolder != Vector3.zero)
        {
            targetIKPos = transform.InverseTransformPoint(targetIKPos);
            posIKHolder = transform.InverseTransformPoint(posIKHolder);

            float yVar = Mathf.Lerp(lastFootPosY, posIKHolder.y, feetToIkPosSpeed);
            targetIKPos.y += yVar;

            lastFootPosY = yVar;
            
            targetIKPos = transform.TransformPoint(targetIKPos);

            anim.SetIKRotation(foot, rotIKHolder);
        }
        anim.SetIKPosition(foot, targetIKPos);
    }

    private void MovePelvisHeight()
    {
        if(rightFootIKPos == Vector3.zero || leftFootIKPos == Vector3.zero || lastPelvisPosY == 0)
        {
            lastPelvisPosY = anim.bodyPosition.y;
            return;
        }
        float leftOffsetPos = leftFootIKPos.y - transform.position.y;
        float rightOffsetPos = rightFootIKPos.y - transform.position.y;

        float totalOffset = (leftOffsetPos < rightOffsetPos) ? leftOffsetPos : rightOffsetPos;

        Vector3 newPelvisPos = anim.bodyPosition + Vector3.up * totalOffset;

        newPelvisPos.y = Mathf.Lerp(lastPelvisPosY, newPelvisPos.y, pelvisUpAndDownSpeed);

        anim.bodyPosition = newPelvisPos;
        lastPelvisPosY = anim.bodyPosition.y;
    }

    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPos, ref Quaternion feetIKRot)
    {

    }

    private void AdjustFeetTarget(ref Vector3 feetPos, HumanBodyBones foot) 
    {
        
    }



}

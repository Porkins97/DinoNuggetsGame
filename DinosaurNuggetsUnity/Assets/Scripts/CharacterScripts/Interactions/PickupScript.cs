using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DinoInputSystems;

[RequireComponent(typeof(DinoInputSystem))]
public class PickupScript : MonoBehaviour
{

    //Private Variables
    private bool RightHand_Carrying = false;
    private bool LeftHand_Carrying = false;
    private bool RightHand_Hover = false;
    private bool LeftHand_Hover = false;

    private bool RightHand_Object_Locked = false;
    private bool LeftHand_Object_Locked = false;
    private float RightHand_Object_unlockTime = 0f;
    private float LeftHand_Object_unlockTime = 0f;

    private GameObject RightHand_Object;
    private GameObject LeftHand_Object;
    private GameObject RightHand;
    private GameObject LeftHand;

    private DinoInputSystem _DIS;

    //Inherited variables
    private bool dinoRightHand { get { return _DIS.dinoRightHand; } }
    private bool dinoLeftHand { get { return _DIS.dinoLeftHand; } }

    void Start()
    {
        _DIS = GetComponent<DinoInputSystem>();
    }

    public void PickItUp(GameObject hand, GameObject obj, bool locked, float _unlockTime)
    {
        if (hand.name == "RightHand" && RightHand_Carrying == false)
        {
            RightHand_Hover = true;
            RightHand_Object = obj;
            RightHand = hand;
            RightHand_Object_Locked = locked;
            RightHand_Object_unlockTime = _unlockTime;
        }
        if (hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = true;
            LeftHand_Object = obj;
            LeftHand = hand;
            LeftHand_Object_Locked = locked;
            LeftHand_Object_unlockTime = _unlockTime;
        }
    }

    public void PickItUpExit(GameObject hand)
    {
        if (hand.name == "RightHand" && RightHand_Carrying == false)
        {
            RightHand_Hover = false;
            RightHand_Object = null;
            RightHand = null;
        }
        if (hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = false;
            LeftHand_Object = null;
            LeftHand = null;
        }
    }

    private void Update()
    {
        if (dinoRightHand)
        {
            if (RightHand_Object_Locked)
            {
                StartCoroutine(Countdown(RightHand_Object_unlockTime, 0, RightHand_Object));
            }
            else
            {
                PickUpItem(ref RightHand, ref RightHand_Carrying, ref RightHand_Object, RightHand_Hover);
            }
        }
        else
        {
            DropItem(ref RightHand_Carrying, ref RightHand_Object);
        }

        if (dinoLeftHand)
        {
            IEnumerator coRoutine = Countdown(LeftHand_Object_unlockTime, 1, LeftHand_Object);
            if (LeftHand_Object_Locked)
            {
                StartCoroutine(coRoutine);
            }
            else
            {
                StopCoroutine(coRoutine);
                //try { StopCoroutine(coRoutine); } catch (System.Exception) { }
                                
                PickUpItem(ref LeftHand, ref LeftHand_Carrying, ref LeftHand_Object, LeftHand_Hover);
            }
        }
        else
        {
            DropItem(ref LeftHand_Carrying, ref LeftHand_Object);
        }
    }

    private void PickUpItem(ref GameObject hand, ref bool carrying, ref GameObject obj, bool hovering)
    {
        if (hovering && obj != null && !carrying)
        {
            Debug.Log(obj.name);
            //Set Rigidbody off - Turn on Kinematic and set contraints to none.
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            //Set the parent and reset transform and rotation to the parents.
            obj.transform.SetParent(hand.transform);
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localRotation = Quaternion.identity;

            //Set the carrying hand to true, and make sure the items attribute is being used.
            carrying = true;
            obj.GetComponent<ItemAttributes>().beingUsed = true;
        }
    }
    private void DropItem(ref bool carrying, ref GameObject obj)
    {
        if (obj != null && carrying)
        {
            //Set Rigidbody back
            obj.GetComponent<Rigidbody>().isKinematic = false;

            //Remove the parents
            obj.transform.SetParent(obj.GetComponent<ItemAttributes>().initialParent);

            //Make sure all things are now gotten rid of.
            carrying = false;
            obj.GetComponent<ItemAttributes>().beingUsed = false;
        }
    }
    
    IEnumerator Countdown(float seconds, int _input, GameObject obj)
    {

        float Locking = 0f;
        if(_input == 0)
        {
            bool locked = false;
            while(Locking <= seconds)
            {
                
                Locking += Time.deltaTime;
                if (obj != null)
                {
                    obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(true);
                    obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = Locking / seconds;
                }

                yield return null;
                
                if (!dinoRightHand || !RightHand_Hover || obj == null)
                {
                    locked = true;
                    if (obj != null)
                    {
                        obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(false);
                        obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = 0;
                    }
                    break;
                }
            }
            if(locked == false)
            {
                RightHand_Object_Locked = false;
                obj.GetComponent<ItemAttributes>().Locked = false;
                if (obj != null)
                {
                    obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(false);
                    obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = 0;
                }
            }
            
        }

        else if (_input == 1)
        {
            bool locked = false;
            while (Locking <= seconds)
            {
                Locking += Time.deltaTime;
                if (obj != null)
                {
                    obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(true);
                    obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = Locking / seconds;
                }
                yield return null;

                if (!dinoLeftHand || !LeftHand_Hover || obj == null)
                {
                    locked = true;
                    if (obj != null)
                    {
                        obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(false);
                        obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = 0;
                    }
                    break;
                }                
            }
            if (locked == false)
            {
                LeftHand_Object_Locked = false;
                obj.GetComponent<ItemAttributes>().Locked = false;
                if (obj != null)
                {
                    obj.GetComponent<ItemAttributes>().LoadingUI.SetActive(false);
                    obj.GetComponent<ItemAttributes>().unlockImage.fillAmount = 0;
                }
            }
        }
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DinoInputSystems;

[RequireComponent(typeof(DinoInputSystem))]
public class PickupScript : MonoBehaviour
{

    //Private Variables
    [SerializeField] private bool RightHand_Carrying = false;
    [SerializeField] private bool LeftHand_Carrying = false;
    [SerializeField] private bool RightHand_Hover = false;
    [SerializeField] private bool LeftHand_Hover = false;

    [SerializeField] private bool RightHand_Object_Locked = false;
    [SerializeField] private bool LeftHand_Object_Locked = false;
    [SerializeField] private float RightHand_Object_unlockTime = 0f;
    [SerializeField] private float LeftHand_Object_unlockTime = 0f;

    [SerializeField] private GameObject RightHand_Object;
    [SerializeField] private GameObject LeftHand_Object;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject LeftHand;

    public List<GameObject> AllItems = new List<GameObject>();

    private DinoInputSystem _DIS;
    private DinoSceneManager _DSM;

    //Inherited variables
    private bool dinoRightHand { get { return _DIS.dinoRightHand; } }
    private bool dinoLeftHand { get { return _DIS.dinoLeftHand; } }
    private bool dinoAction { get { return _DIS.dinoAction; } }

    private enum Hand { right, left }








    void Start()
    {
        _DIS = GetComponent<DinoInputSystem>();
        if (_DSM == null)
        {
            _DSM = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
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
            IEnumerator coRoutine = Countdown(RightHand_Object_unlockTime, 0, RightHand_Object);
            if (RightHand_Object_Locked )
            {
                StartCoroutine(coRoutine);
            }
            else
            {
                StopCoroutine(coRoutine);
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
                PickUpItem(ref LeftHand, ref LeftHand_Carrying, ref LeftHand_Object, LeftHand_Hover);
            }
        }
        else
        {
            DropItem(ref LeftHand_Carrying, ref LeftHand_Object);
        }

        if (dinoAction)
        {
            if(RightHand_Object != null)
            {
                CutObjects(RightHand_Object, Hand.right);
            }
            else if(LeftHand_Object != null)
            {
                CutObjects(LeftHand_Object, Hand.left);
            }
        }
    }

    private void CutObjects(GameObject obj, Hand hand)
    {
        ItemAttributes item = obj.GetComponent<ItemAttributes>();
        if(item == null || (int)item.GameType < 50 || (int)item.GameType > 99 || item.onCuttingBoard == false)
        {
            return;
        }
        SO_Ingredients foundIngredient = _DSM.ingredientList.Find(x => x.type == obj.GetComponent<ItemAttributes>().GameType);
        if(foundIngredient.slicedVersion == null)
        {
            return;
        }

        StartCoroutine(CuttingRoutine(1.0f, hand, obj));

    }

    IEnumerator CuttingRoutine(float timeTaken, Hand hand, GameObject obj)
    {
        float currentTime = 0f;

        if(hand == Hand.right) //If RightHand
        {
            bool cut = true;
            while (currentTime < timeTaken)
            {
                if (!dinoAction || !RightHand_Hover || obj == null)
                {
                    cut = false;
                    Debug.Log("Lifted Up Button!!");
                    break;
                }
                currentTime += Time.deltaTime;
                yield return null;
            }
            if (cut && obj != null)
            {
                CutAndDestroy(obj);
                Debug.Log("Right");

                
            }
        }
        else //If LeftHand
        {
            bool cut = true;
            while (currentTime < timeTaken)
            {
                if (!dinoAction || !LeftHand_Hover || obj == null)
                {
                    cut = false;
                    Debug.Log("Lifted Up Button!!");
                    break;
                }
                currentTime += Time.deltaTime;
                yield return null;
            }
            if (cut && obj != null)
            {
                CutAndDestroy(obj);
                Debug.Log("Left");
               
            }
        }
    }
    private void CutAndDestroy(GameObject obj)
    {
        IngredientType type = obj.GetComponent<ItemAttributes>().GameType;
        SO_Ingredients foundIngredient = _DSM.ingredientList.Find(x => x.type == type);
        GameObject Instance = Instantiate(foundIngredient.slicedVersion.ingredientPrefab, obj.transform.position, obj.transform.rotation, _DSM.SpawnTransform);
        Debug.Log(Instance.name);
        obj.GetComponent<ItemAttributes>().currentCuttingBoard.GetComponent<CuttingBoard>().Removed(obj);
        AllItems.Remove(obj);
        Destroy(obj);
        Debug.Log("Cut!!");
    }



    private void PickUpItem(ref GameObject hand, ref bool carrying, ref GameObject obj, bool hovering)
    {
        if (hovering && obj != null && !carrying)
        {
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
                yield return null;
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
                yield return null;
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

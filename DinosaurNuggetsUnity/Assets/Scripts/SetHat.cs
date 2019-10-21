using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHat : MonoBehaviour
{
    public Button[] buttons;
    int enumButtonVal;
    ApplyHat applyhat;
    public GameObject dino;

    public void GetButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = new int();
            index = i;
            buttons[i].onClick.AddListener(delegate { OnSelect(index); });
        }
    }
    private void OnSelect(int index = 0)
    {
        Debug.Log(index);
        dino.GetComponent<ApplyHat>().PassHatnum(index);
    }
}

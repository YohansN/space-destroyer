using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShieldController : MonoBehaviour
{
    public int shieldsLeft; //Quantidade atual de escudos sobrando
    public int numOfShieldsVisible; //Quantidade total de escudos visiveis na tela.
    public Image[] shields;
    public Sprite fullShield;
    public Sprite emptyShield;
    public Player playerInfo;

    // Update is called once per frame
    void Update()
    {
        if(shieldsLeft > numOfShieldsVisible) 
        { 
            shieldsLeft = numOfShieldsVisible;
        }

        for (int i = 0; i < shields.Length; i++)
        {
            if(i < shieldsLeft)
                shields[i].sprite = fullShield;
            else
                shields[i].sprite = emptyShield;

            if(i < numOfShieldsVisible)
                shields[i].enabled = true;
            else
                shields[i].enabled = false;
        }
    }
}

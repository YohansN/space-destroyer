using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private bool isShieldActive = true;
    [SerializeField] public float shieldActiveTime = 3f;
    [SerializeField] public float shieldRechargeTime = 10f;
    [SerializeField] private Collider2D shieldCollider;
    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private int shieldsLeft;
    public UIShieldController uiShield;
    [SerializeField] private bool shieldIsOn = false;
    [SerializeField] private bool isRecharging = false;
    [SerializeField] private AudioSource activateSF;

  
    // Start is called before the first frame update
    void Start()
    {
        shieldCollider = GetComponent<Collider2D>();
        shieldSprite = GetComponent<SpriteRenderer>();    

        shieldCollider.enabled = false;
        shieldSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        uiShield.shieldsLeft = shieldsLeft; //Atualiza a UI
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (shieldsLeft != 0) && !shieldIsOn)
        {
            StartCoroutine(ShieldOn());
        }

        if (shieldsLeft < uiShield.numOfShieldsVisible && !isRecharging)
        {
            StartCoroutine(ShieldRecharge());
        }
    }

    public IEnumerator ShieldOn()
    {
        activateSF.Play();
        shieldIsOn = true;
        shieldsLeft--;
        //Debug.Log("Escuto ativado");
        shieldCollider.enabled = true;
        shieldSprite.enabled = true;

        yield return new  WaitForSeconds(shieldActiveTime);
        shieldCollider.enabled = false;
        shieldSprite.enabled = false;
        //Debug.Log("Escuto desativado");
        shieldIsOn = false;
    }

    public IEnumerator ShieldRecharge()
    {
        isRecharging = true;
        yield return new WaitForSeconds(shieldRechargeTime);
        shieldsLeft++;
        isRecharging = false;
    }
}

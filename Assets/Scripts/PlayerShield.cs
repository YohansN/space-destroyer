using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private bool isShieldActive = true;
    [SerializeField] private float shieldTime = 3f;
    [SerializeField] private Collider2D shieldCollider;
    [SerializeField] private SpriteRenderer shieldSprite;


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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(ShieldOn());
        }
        
    }

    public IEnumerator ShieldOn()
    {
        Debug.Log("Escuto ativado");
        shieldCollider.enabled = true;
        shieldSprite.enabled = true;

        yield return new  WaitForSeconds(shieldTime);
        shieldCollider.enabled = false;
        shieldSprite.enabled = false;
        Debug.Log("Escuto desativado");
    }
}

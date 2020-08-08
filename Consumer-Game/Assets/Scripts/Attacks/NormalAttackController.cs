using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackController : MonoBehaviour, IPooledObject
{


    protected bool isPlayerAttack;
    protected float timeToLive;
    

    [SerializeField] protected int damage = 1;
    [SerializeField] protected float attackDuration = 1f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0f){
            gameObject.SetActive(false);
        }
    }

    public virtual void OnObjectSpawn(){
        timeToLive = attackDuration;
    }


    public virtual void SetPlayerEnemyAttack(bool isPlayer){
        isPlayerAttack = isPlayer;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) //check for collisions, aka dmg
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == "Enemy" && isPlayerAttack)
        {
            hitTarget.GetComponent<HealthInterface>().ApplyDamage(damage, Elements.Element.Neutral);
            Debug.Log("hit Enemy");
        }
        else if(hitTarget.tag ==  "Player" && !isPlayerAttack){
            hitTarget.GetComponent<HealthInterface>().ApplyDamage(damage, Elements.Element.Neutral);
            Debug.Log("hit Player");
        }

    }
}

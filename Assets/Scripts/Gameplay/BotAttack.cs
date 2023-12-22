using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BotAttack : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform movement;


    public float attackDelayTime = 2f;
    public float detectionRange = 10f; // khoang cach nhan dien nhan vat
    public float attackRange = 2f; // Khoang cach tan cong

    public BotMovement botMovement;
    public AnimatedSpriteRenderer spriteRendererAtkUp;
    public AnimatedSpriteRenderer spriteRendererAtkDown;
    public AnimatedSpriteRenderer spriteRendererAtkLeft;
    public AnimatedSpriteRenderer spriteRendererAtkRight;
    public AnimatedSpriteRenderer activeSpriteRenderer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        movement = GameObject.FindGameObjectWithTag("Player").transform; // dat tag cho player
        TryGetComponent(out botMovement);
    }
    private void Update()
    {
        //UpdateBot();
    }

    public void DoAttack(MovementController target) // tan cong player
    {
        StartCoroutine(DoAttackCo(target));
    }

    private IEnumerator DoAttackCo(MovementController target) // hien thi spiterender tan cong theo huong cua player
    {   
        botMovement.canMove = false;

        yield return new WaitForSeconds(attackDelayTime);

        botMovement.canMove = true;

        switch (target.direction)
        {
            case var up when target.direction.y > 0:
                spriteRendererAtkUp.enabled = true;
                spriteRendererAtkDown.enabled = false;
                spriteRendererAtkLeft.enabled = false;
                spriteRendererAtkRight.enabled = false;
                activeSpriteRenderer = spriteRendererAtkUp;
                break;
            case var down when target.direction.y < 0:
                spriteRendererAtkUp.enabled = false;
                spriteRendererAtkDown.enabled = true;
                spriteRendererAtkLeft.enabled = false;
                spriteRendererAtkRight.enabled = false;
                activeSpriteRenderer = spriteRendererAtkDown;
                break;
            case var left when target.direction.x < 0:
                spriteRendererAtkUp.enabled = false;
                spriteRendererAtkDown.enabled = false;
                spriteRendererAtkLeft.enabled = true;
                spriteRendererAtkRight.enabled = false;
                activeSpriteRenderer = spriteRendererAtkDown;
                break;
            case var right when target.direction.x > 0:
                spriteRendererAtkUp.enabled = false;
                spriteRendererAtkDown.enabled = false;
                spriteRendererAtkLeft.enabled = false;
                spriteRendererAtkRight.enabled = true;
                activeSpriteRenderer = spriteRendererAtkDown;
                break;
            default:
                break;
        }

        Debug.Log("ATTACK PLAYER");
        
        target.health -= 1;
        if (target.health <= 0)
            target.DeathSequence();
    }

    
}

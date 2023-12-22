using UnityEngine;

public class BotMovement : MovementController
{
    public float checkDistance = 1f;
    MovementController player;
    BotAttack botAttack;
    public float timer = 2f;
    private float _cacheTimer;

    private void Start()   // khoi tao bien va lay cac thanh phan can thiet tu movementcontroller
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        TryGetComponent(out botAttack);
        _cacheTimer = timer;
    }

    public bool canMove = true;
    protected override void Update()
    {
        direction = CheckDirection();  // xac dinh huong di chuyen
        timer -= Time.deltaTime;

        if (!canMove)
        {
            direction = Vector2.zero;
        }

        if (direction != Vector2.zero && timer <= 0 && Vector2.Distance((Vector2)this.transform.position, player.transform.position) <= botAttack.attackRange) // neu player trong khoang cach nhat dinh se tan cong
        {
            botAttack.DoAttack(player);
            timer = _cacheTimer;
        }
    }

    protected override void FixedUpdate()
    {
        Vector2 position = rigidbody.position; // xac dinh vi tri cua player
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation); // di chuyen toi vi tri cua player
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            if (shield > 0)
            {
                shield -= 1;
                return;
            }

            if (shield <= 0)
            {
                health -= 1;
                if (health <= 0)
                {
                    DeathSequence();
                }
            }

        }

    }

    public override void DeathSequence()
    {
        GameObject.Destroy(this.gameObject);

    }

    private Vector2 CheckDirection()  // xac dinh huong can di chuyen bang cach lay vector ngược tu bot toi player
    {
        Vector2 currentDirection = -(this.transform.position - player.transform.position);
        var dir = new Vector2();
        dir.x += 1;
        if (Vector2.Distance((Vector2)this.transform.position + dir, player.transform.position) <= checkDistance)
        {
            SetDirection(currentDirection, spriteRendererRight);
            return currentDirection;
        }
        dir = Vector2.zero;


        dir.x += -1;
            if (Vector2.Distance((Vector2)this.transform.position + dir, player.transform.position) <= checkDistance)
        {
            SetDirection(currentDirection, spriteRendererLeft);
            return currentDirection;
        }
        dir = Vector2.zero;

        dir.y += 1;
        if (Vector2.Distance((Vector2)this.transform.position + dir, player.transform.position) <= checkDistance)
        {
            SetDirection(currentDirection, spriteRendererUp);
            return currentDirection;
        }
        dir = Vector2.zero;

        dir.y += -1;
        if (Vector2.Distance((Vector2)this.transform.position + dir, player.transform.position) <= checkDistance)
        {
            SetDirection(currentDirection, spriteRendererDown);
            return currentDirection;
        }
        dir = Vector2.zero;
        return dir;
    }

    protected override void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        if (!canMove)
            return;

        base.SetDirection(newDirection, spriteRenderer);
    }

    #region OLD
    //public float moveSpeed = 2.0f; // toc do di chuyen
    //public float changeDirectionInterval = 2.0f; // thoi gian thay doi huong di
    //private float timeSinceLastDirectionChange;
    //private Vector3 currentDirection;

    //private void Start()
    //{
    //    GetRandomDirection(); // khoi tao huong di ban dau
    //}

    //private void Update()
    //{
    //    transform.Translate(currentDirection * moveSpeed * Time.deltaTime);   // Di chuyen theo huong hien tai

    //    timeSinceLastDirectionChange += Time.deltaTime;  // dem thoi gian va thay doi huong di sau khi ket thuc
    //    if (timeSinceLastDirectionChange >= changeDirectionInterval)
    //    {
    //        GetRandomDirection();
    //        timeSinceLastDirectionChange = 0;
    //    }
    //}

    //private void GetRandomDirection()
    //{
    //    float randomX = Random.Range(-1f, 1f);
    //    float randomZ = Random.Range(-1f, 1f);
    //    currentDirection = new Vector3(randomX, 0, randomZ).normalized; // chon huong di ngau nhien 
    //}
    #endregion
}

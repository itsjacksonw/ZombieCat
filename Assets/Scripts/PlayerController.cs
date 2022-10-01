using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D box;
    public Animator animator;
    public GameObject GreenBullet;
    public GameObject Barrel;

    private Vector2 movement;
    Vector3 mouseDirection;
    float mouseAngle;

    public float speed;
    public float dashSpeed;
    public float dashLength;
    private float nextDashTime;
    public float dashCooldown;
    private float dashTime;

    public float maxHealth;
    private float health;

    public float shootSpeed;
    public float shootCooldown;
    private float nextShotTime;

    public Slider healthBar;
    public GameObject GameManager;

    public Camera mainCam;
    public Camera firstCam;
    public Camera secondCam;
    public Camera thirdCam;
    public Camera fourthCam;
    public Camera fifthCam;
    public Camera sixthCam;
    public Camera finalCam;

    private float deathTime = 10000000f;
    private bool isDead = false;

    private float friendTime = 1000000000f;
    private bool hasTalked = false;
    private bool isTalking = false;
    private int chat = 0;
    private int subChat = 0;


    public GameObject one1;
    public GameObject one2;
    public GameObject one3;
    public GameObject one4;
    public GameObject one5;
    public GameObject two1;
    public GameObject two2;
    public GameObject two3;
    public GameObject two4;
    public GameObject three1;
    public GameObject three2;
    public GameObject three3;
    public GameObject four1;
    public GameObject four2;
    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;
    public GameObject f5;
    public GameObject f21;
    public GameObject f22;
    public GameObject f23;
    public GameObject f24;

    private GameObject lastText;

    public bool ending = false;

    public int deaths = 0;
    public int stage = 0;
    public int friend = 0;

    public GameObject annoying;
    public GameObject annoying2;

    public Camera endcam1;
    public Camera endcam2;


    public AudioSource deathSound;
    public AudioSource dashSound;
    public AudioSource clickSound;
    public AudioSource shootSound;
    public AudioSource playerHurtSound;

    private GameObject resume;
    private GameObject exit;
    public GameObject resumeMain;
    public GameObject exitMain;
    private int destroyed = 1;

    void OnEnable()
    {
        deaths = PlayerPrefs.GetInt("deaths");
        stage = PlayerPrefs.GetInt("stage");
        friend = PlayerPrefs.GetInt("friend");
    }


    void OnDisable()
    {
        PlayerPrefs.SetInt("deaths", deaths);
        PlayerPrefs.SetInt("stage", stage);
        PlayerPrefs.SetInt("friend", friend);
        
    }

    void Start()
    {
        health = maxHealth;
        body = transform.GetComponent<Rigidbody2D>();
        box = transform.GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();

        if (health <= 0 && !isDead)
        {
            Death();
        }

        healthBar.value = health / maxHealth;

        

        if (Time.time > deathTime)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isTalking)
        {
            body.velocity = new Vector2(0, 0);
        }

        
    }


    void ProcessInputs()
    {
        
        if (!isTalking)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            movement = new Vector2(moveX, moveY).normalized;

            mouseDirection = Input.mousePosition;
            mouseDirection = mainCam.ScreenToWorldPoint(mouseDirection);
            mouseDirection.z = 0.0f;
            mouseDirection = mouseDirection - transform.position;
            mouseDirection = mouseDirection.normalized;
            mouseAngle = (Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg) - 0f;
            Direction();


            if (Input.GetButton("Fire1") && Time.time > nextShotTime && Time.time > dashTime)
            {
                Shoot();
            }

            if (Input.GetButton("Dash") && Time.time > nextDashTime)
            {
                Dash();
            }
 
        }

        if (Input.GetButtonDown("Fire1") && friend == 0 && isTalking)
        {
            FirstChat();
            clickSound.Play();
        }

        if (Input.GetButtonDown("Fire1") && friend == 1 && isTalking)
        {
            SecondChat();
            clickSound.Play();
        }

        if (Input.GetButtonDown("Fire1") && friend == 2 && isTalking)
        {
            ThirdChat();
            clickSound.Play();
        }

        if (Input.GetButtonDown("Fire1") && friend >= 3 && isTalking)
        {
            FourthChat();
            clickSound.Play();
        }

        if (Input.GetButtonDown("Fire1") && friend == 4 && isTalking && stage == 1)
        {
            Final1Chat();
            clickSound.Play();
        }

        if (Input.GetButtonDown("Fire1") && friend == 5 && isTalking)
        {
            Final2Chat();
            clickSound.Play();
        }


        if (Input.GetButtonDown("Fire1") && ending)
        {
            clickSound.Play();
            endcam1.enabled = false;
            endcam2.enabled = true;
            Destroy(this);
        }

        if(Input.GetButton("Pause") && Time.timeScale == 1f)
        {
            destroyed = 0;
            Time.timeScale = 0f;
            resume = Instantiate(resumeMain, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            exit = Instantiate(exitMain, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        }

        if(Time.timeScale == 1f && destroyed == 0)
        {
            Destroy(resume);
            Destroy(exit);
            destroyed = 1;
        }
        

    }

    void Move()
    {
        if (Time.time > dashTime)
        {
            body.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }
    }

    void Direction()
    {
        if (mouseAngle < 90 && mouseAngle > -90 && movement.x == 0 && movement.y == 0)
        {
            animator.Play("Right");
        }
        if (mouseAngle > 90 || mouseAngle < -90 && movement.x == 0 && movement.y == 0)
        {
            animator.Play("Left");
        }

        if (mouseAngle <= 90 && mouseAngle >= -90)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.Play("WalkRight");
            }
            
        }

        if (mouseAngle > 90 || mouseAngle < -90)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.Play("WalkLeft");
            }

        }
    }

    void Shoot()
    {
        shootSound.Play();
        GameObject shot;
        nextShotTime = Time.time + shootCooldown;

        shot = Instantiate(GreenBullet, new Vector2(Barrel.transform.position.x, Barrel.transform.position.y), transform.rotation);
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(mouseDirection * shootSpeed, ForceMode2D.Impulse);
    }

    void Dash()
    {
        dashSound.Play();
        nextDashTime = Time.time + dashCooldown;
        dashTime = Time.time + dashLength;

        body.velocity = mouseDirection * dashSpeed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Damage(5);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Damage(1);
        }

        if(collision.gameObject.tag == "FriendTrigger" && !hasTalked)
        {
            Debug.Log("Friend Trigger");
            Friend();
        }

        if (collision.gameObject.tag == "FinalBoss")
        {
            Debug.Log("Boss Trigger");
            stage = 1;
        }

        if (collision.gameObject.tag == "Exit")
        {
            Debug.Log("Exit");
            
            if(stage == 1 || deaths > 10)
            {
                EndGame();
            }
        }
    }

    void Damage(int damage)
    {
        playerHurtSound.Play();
        health = health - damage;
    }

    void Death()
    {
        deathSound.Play();

        healthBar.gameObject.SetActive(false);
        Destroy(annoying);
        Destroy(annoying2);

        mainCam.enabled = false;
        firstCam.enabled = false;
        secondCam.enabled = false;
        thirdCam.enabled = false;
        fourthCam.enabled = false;
        fifthCam.enabled = false;
        sixthCam.enabled = false;
        finalCam.enabled = false;
        endcam1.enabled = false;
        endcam2.enabled = false;

        if (deaths == 0)
        {
            firstCam.enabled = true;
        }

        else if (friend == 5)
        {
            finalCam.enabled = true;
        }

        else if (deaths == 1)
        {
            secondCam.enabled = true;
        }

        else if (deaths == 2)
        {
            thirdCam.enabled = true;
        }

        else if (deaths == 3)
        {
            fourthCam.enabled = true;
        }

        else if (deaths == 4)
        {
            fifthCam.enabled = true;
        }

        else if (deaths == 5)
        {
            sixthCam.enabled = true;
        }

        else if (deaths > 5)
        {
            int num = Random.Range(1, 7);
            switch(num)
            {
                case 1:
                    firstCam.enabled = true;
                    break;
                case 2:
                    secondCam.enabled = true;
                    break;
                case 3:
                    thirdCam.enabled = true;
                    break;
                case 4:
                    fourthCam.enabled = true;
                    break;
                case 5:
                    fifthCam.enabled = true;
                    break;
                case 6:
                    sixthCam.enabled = true;
                    break;
            }
            
        }




        deathTime = Time.time + 3.0f;

        isDead = true;


        deaths = deaths + 1;
    }

    void Friend()
    {
        clickSound.Play();
        hasTalked = true;
        isTalking = true;
        friendTime = Time.time;

        if (friend == 0)
        {
            FirstChat();
        }

        if (friend == 1)
        {
            SecondChat();
        }

        if (friend == 2)
        {
            ThirdChat();
        }

        if (friend >= 3 && stage == 0)
        {
            FourthChat();
        }

        if (friend == 4 && stage == 1)
        {
            Final1Chat();
        }

        if (friend == 5 && stage == 1)
        {
            Final2Chat();
        }
    }


    void FirstChat()
    {
        Debug.Log("First Chat");
        chat = 1;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(one1, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(one2, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {
            Destroy(lastText);
            lastText = Instantiate(one3, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 3;
        }

        else if (subChat == 3)
        {
            Destroy(lastText);
            lastText = Instantiate(one4, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 4;
        }

        else if (subChat == 4)
        {
            Destroy(lastText);
            lastText = Instantiate(one5, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 5;
        }

        else if (subChat == 5)
        {
            
            Debug.Log("5");
            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 1;
            health = maxHealth;
        }

    }


    void SecondChat()
    {
        Debug.Log("Second Chat");
        chat = 2;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(two1, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(two2, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {
            Destroy(lastText);
            lastText = Instantiate(two3, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 3;
        }

        else if (subChat == 3)
        {
            Destroy(lastText);
            lastText = Instantiate(two4, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 4;
        }


        else if (subChat == 4)
        {
           
            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 2;
            health = maxHealth;
        }

    }

    void ThirdChat()
    {
        Debug.Log("Third Chat");
        chat = 3;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(three1, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(three2, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {
            Destroy(lastText);
            lastText = Instantiate(three3, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 3;
        }

        else if (subChat == 3)
        {

            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 3;
            health = maxHealth;
        }

    }

    void FourthChat()
    {
        Debug.Log("Fourth Chat");
        chat = 4;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(four1, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(four2, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {

            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 4;
            health = maxHealth;
        }

    }

    void Final1Chat()
    {
        Debug.Log("Final Chat");
        chat = 5;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(f1, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(f2, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {
            Destroy(lastText);
            lastText = Instantiate(f3, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 3;
        }

        else if (subChat == 3)
        {
            Destroy(lastText);
            lastText = Instantiate(f4, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 4;
        }

        else if (subChat == 4)
        {
            Destroy(lastText);
            lastText = Instantiate(f5, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 5;
        }

        else if (subChat == 5)
        {

            Debug.Log("5");
            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 5;
            health = maxHealth;
        }

    }


    void Final2Chat()
    {
        Debug.Log("Final 2 Chat");
        chat = 6;

        if (subChat == 0)
        {
            Debug.Log("0");
            lastText = Instantiate(f21, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 1;
        }

        else if (subChat == 1)
        {
            Debug.Log("1");
            Destroy(lastText);
            lastText = Instantiate(f22, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 2;
        }

        else if (subChat == 2)
        {
            Destroy(lastText);
            lastText = Instantiate(f23, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 3;
        }

        else if (subChat == 3)
        {
            Destroy(lastText);
            lastText = Instantiate(f24, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            subChat = 4;
        }

        else if (subChat == 4)
        {

            Debug.Log("5");
            Destroy(lastText);
            subChat = 0;
            isTalking = false;
            friend = 6;
            health = maxHealth;
        }

    }

    void EndGame()
    {
        healthBar.gameObject.SetActive(false);
        Destroy(annoying);
        Destroy(annoying2);

        Debug.Log("end");
        mainCam.enabled = false;
        firstCam.enabled = false;
        secondCam.enabled = false;
        thirdCam.enabled = false;
        fourthCam.enabled = false;
        fifthCam.enabled = false;
        sixthCam.enabled = false;
        finalCam.enabled = false;
        endcam1.enabled = true;
        endcam2.enabled = false;

        ending = true;
    }
}

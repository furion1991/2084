using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    idle,
    patroling,
    attacking
}

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject target;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                Die();
            }
        }
    }
    
    [SerializeField]
    private float _health = 10;
    [SerializeField]
    private EPowerUpType[] possibleDrop;
    private EEnemyState _state = EEnemyState.idle;
    private GameObject fireArm;
    private GameObject rightArm;
    private Weapon _weapon;
    private Vector3 patrolPoint;
    private Vector3 pos;
    private bool movingFinished;
    

    private WeaponType _enemyWeaponType
    {
        get { return _enemyWeaponType; }
        set { _weapon.SetType(value); }
    }

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
        _weapon.weaponOwner = transform.tag;
        fireArm = GameObject.Find("Fire Arm Enemy");
        rightArm = GameObject.Find("Right Arm en");
        rightArm.SetActive(true);
        fireArm.SetActive(false);
        pos = transform.position;
        patrolPoint = transform.position + Random.insideUnitSphere * 3;
        patrolPoint.y = 0;

    }
    private void LateUpdate()
    {

        Patrol();

    }
    private void Patrol()
    {
        GameObject player = GameObject.Find("Player");
        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            target = player;
            MoveTowardsTarget();
            Attack();
            pos = transform.position;
            return;
        }
        else
        {
            fireArm.SetActive(false);
            rightArm.SetActive(true);
            pos = transform.position;
            target = null;
        }
        if (Vector3.Distance(pos,patrolPoint) > 0.5f)
        {
            patrolPoint.y = 0;
            Vector3 destToRot = patrolPoint - pos;
            Quaternion rotation = Quaternion.LookRotation(destToRot);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 720 * Time.deltaTime);
            float u = 0.2f * speed * Time.deltaTime;
            transform.position = (1 - u) * pos + u * patrolPoint;
            pos = transform.position;
        }
        else if (Vector3.Distance(pos,patrolPoint) < 0.5f)
        {
            patrolPoint = transform.position + Random.insideUnitSphere * 3;
            patrolPoint.y = 0;
        }
        
    }
    private void MoveTowardsTarget()
    {
        if (target.tag == "Player")
        {
            _state = EEnemyState.attacking;
        }
        Vector3 pos, toPos;
        float u = 0.2f * speed * Time.deltaTime;
        pos = transform.position;
        toPos = target.transform.position;
        Vector3 distToRot = target.transform.position - pos;
        Quaternion rot = Quaternion.LookRotation(distToRot);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 720 * Time.deltaTime);
        transform.position = (1 - u) * pos + u * toPos;
        
    }
    private void Die()
    {
        target = null;
        Vector3 kickBack = -(transform.position + new Vector3(0, 1, 15));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(kickBack);
        Invoke("DestroyBody", 2);
    }
    private void Attack()
    {
        if (target != null)
        {
            fireArm.SetActive(true);
            rightArm.SetActive(false);
            _weapon.Fire();
            if (_weapon.ammo <= 0)
            {
                _weapon.ammo = 100;
            }
        }
    }
    private void DestroyBody()
    {
        Destroy(gameObject);
        DropPowerUp();
    }
    private void DropPowerUp()
    {
        int randNdx = Random.Range(0, possibleDrop.Length);
        GameObject go = Instantiate(PowerUpList.POWERUPDEFINITIONS[possibleDrop[randNdx]].appearance);
        go.transform.position = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        Ability ab = go.GetComponent<Ability>();
        if (go.tag == "Projectile")
        {
            health--;
            Destroy(other.gameObject);
            print("Projectile Entered");
            return;
        }
        if (ab != null)
        {
            if (ab.type == EAbilityType.telekenesis)
            {
                Telekenesis tk = go.GetComponent<Telekenesis>();
                Enemy enemy = GetComponent<Enemy>();
                
                print("onTriggerEnter is called");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //print("OnTriggerStayIsCalled");
    }
}

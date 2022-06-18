using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EPlayerState
{
    idle,
    moving,
    attacking
}
public class Player : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10f;
    public float rotationSpeed = 3;
    public Text healthRepresentation;
    public Text ammoRepresentation;
    
    public float heatlh
    {
        get { return _heatlh; }
        set
        {
            _heatlh = value;
            
            if (_heatlh <= 0)
            {
                print("You ded!");
            }
            if (_heatlh > 10)
            {
                _heatlh = 10;
            }
            healthRepresentation.text = $"Health:\n{_heatlh}";
        }
    }
    public int playerAmmunition
    {
        get { return weapon.ammo; }
        set 
        {
            ammoRepresentation.text = $"Ammo:\n{weapon.ammo}";
            weapon.ammo = value; 
        }
    }


    private float _heatlh = 10f;
    private EPlayerState playerState = EPlayerState.idle;
    private Weapon weapon;
    private GameObject fireArm;
    private GameObject rightArm;
    private float lastMoveMade;
    private Implant implant;
    private ILongRangeWeapon longRangeWeapon;
    private ICloseRangeWeapon closeRangeWeapon;
    

    private void Awake()
    {
        InitialisePlayer();
    }
    private void Update()
    {
        RotateToMouse();
        
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            MoveOnButtonPressing();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FireCurrentWeapon();
        }
        if (Input.GetMouseButton(1))
        {
            implant.UseAbility();
        }
        if (weapon != null && Time.time - weapon.lastShotMade > 2)
        {
            HideWeapon();
        }
        if (Time.time - lastMoveMade > 2)
        {
            playerState = EPlayerState.idle;
        }
    }
    private void FireCurrentWeaponSolidTest()
    {
        longRangeWeapon.Fire();
    }
    private void InitialisePlayer()
    {
        implant = GameObject.Find("Implant").GetComponent<Implant>();
        implant.activeImplantAbility = EAbilityType.telekenesis;
        fireArm = GameObject.Find("Fire Arm");
        rightArm = GameObject.Find("Right Arm");
        weapon = GetComponentInChildren<Weapon>();
        playerAmmunition = 100;
        ammoRepresentation.text = $"Ammo:\n{weapon.ammo}";
        rightArm.SetActive(true);
        fireArm.SetActive(false);
        healthRepresentation.text = $"Health:\n{_heatlh}";
    }
    private void RotateToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePosition - playerPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
    private void FireCurrentWeapon()
    {
        rightArm.SetActive(false);
        fireArm.SetActive(true);
        weapon = GetComponentInChildren<Weapon>();
        weapon.Fire();
        playerAmmunition = weapon.ammo;
        playerState = EPlayerState.attacking;
    }
    private void HideWeapon()
    {
        rightArm.SetActive(true);
        fireArm.SetActive(false);
        playerState = EPlayerState.idle;
    }
    private void MoveOnButtonPressing()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        playerState = EPlayerState.moving;
        lastMoveMade = Time.time;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Wall")
        {
            print("You hit a wall collider");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        PowerUp pu = go.GetComponent<PowerUp>();
        if (go != null)
        {
            if (pu != null)
            {
                if (pu.type == EPowerUpType.health)
                {
                    heatlh++;
                    print($"Your health is now {heatlh}");
                    Destroy(other.gameObject);
                }
                if (pu.type == EPowerUpType.ammo)
                {
                    print("You picked up ammo");
                    weapon.ammo++;
                    playerAmmunition = weapon.ammo;
                    Destroy(other.gameObject);
                }
               
            }
        }
        if (go.tag == "Wall")
        {
            print("You hit a wall trigger");
        }
        if (go.tag == "EnemyProjectile")
        {
            heatlh--;
            Destroy(other.gameObject);
        }
    }

}

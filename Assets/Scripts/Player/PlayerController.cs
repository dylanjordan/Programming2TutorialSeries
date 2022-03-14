using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public List<GameObject> _weaponPrefabs;

    private Interactable _currentInteractable;

    public GameObject _player;
    public GameObject _deadBody;
    public GameObject _weaponArms;
    public GameObject _noWeaponArms;
    public GameObject _weaponParent;
    public GameObject _weaponObjInHand = null;

    private DoomControls _dc;

    private PlayerInput _playerInput;

    private string _currentControlScheme;

    public string _mouseInputName = "MouseKey";
    public string _controllerInputName = "Controller";

    public Weapon _weaponInHand = null;

    public Rigidbody2D _rb;

    public Text _playerHealthDisp;
    public Text _deathMessage;

    public float _maxHealth = 100.0f;
    public float _currentHealth = 100.0f;
    public float _coolDown = 3;

    [Range(1.0f, 10.0f)]
    public float _playerDefaultSpeed = 10.0f;
    public float _currentSpeed = 10.0f;

    private float _nextBoost = 0;

    public bool _isDead = false;
    public bool _isHoldingWeapon = false;
    public bool _SpeedBoost = false;

    private bool _usingMouseInput = true;
    private bool _LastHoldVal = false;
    private bool _firing = false;
    private bool _boost;

    private int _currentWeapon = -1;
    // Start is called before the first frame update
    private void Awake()
    {
        _dc = new DoomControls();

        _playerInput = GetComponent<PlayerInput>();

        _currentControlScheme = _playerInput.currentControlScheme;

        InitInputActions();

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        ResetHealth();
    }

    private void OnEnable()
    {
        _dc.Enable();
    }

    private void OnDisable()
    {
        _dc.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeaponDisplay();

        UpdateHealthDisplay();

        CheckIfDead();

        if (_firing)
        {
            FireWeapon();
            Debug.Log("Weapon Fired");
        }
    }

    private void InitInputActions()
    {
        _dc.Doom_Default.Movement.performed += ctx => MovePlayer(ctx.ReadValue<Vector2>());

        _dc.Doom_Default.Movement.canceled += ctx => MovePlayer(new Vector2());

        _dc.Doom_Default.Aiming.performed += ctx => AimPlayer(ctx.ReadValue<Vector2>());

        _dc.Doom_Default.AimingController.performed += ctx => AimPlayer(ctx.ReadValue<Vector2>());

        _dc.Doom_Default.Attack.started += ctx => _firing = true;

        _dc.Doom_Default.Attack.canceled += ctx => _firing = false;

        _dc.Doom_Default.Interact.performed += ctx => InteractWithCurrentObject();

        _dc.Doom_Default.CycleWeaponDown.started += ctx => CycleWeaponDown();

        _dc.Doom_Default.CycleWeaponUp.started += ctx => CycleWeaponUp();

        _dc.Doom_Default.MeleeEquip.started += ctx => EquipMelee();

        _dc.Doom_Default.SpeedBoost.started += ctx => _boost = true;
    }

    public void UpdateCurrentControlScheme()
    {
        if (_playerInput != null)
        {
            _currentControlScheme = _playerInput.currentControlScheme;

            if (_mouseInputName == _currentControlScheme)
            {
                _usingMouseInput = true;
            }
            else
            {
                _usingMouseInput = false;
            }
        }

        if (_playerInput == null)
        {
            Debug.LogError("there is no player input!");
        }
    }

    private void MovePlayer(Vector2 movementDir)
    {
        _rb.velocity = movementDir * (_playerDefaultSpeed);
    }

    private void AimPlayer(Vector2 aimPos)
    {
        Vector2 lookAtDir;
        if (_usingMouseInput)
        {
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_player.transform.position);
            lookAtDir = aimPos - new Vector2(playerScreenPoint.x, playerScreenPoint.y);
            lookAtDir.Normalize();
        }
        else
        {
            lookAtDir = aimPos;
        }

        if (lookAtDir.normalized != lookAtDir)
        {
            Debug.LogWarning("LookAtDir Not Normalized");
        }

        float angle = Mathf.Atan2(-lookAtDir.x, lookAtDir.y) * Mathf.Rad2Deg;

        _player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void InteractWithCurrentObject()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }
    private void CycleWeaponUp()
    {
        if (_currentWeapon == _weaponPrefabs.Count - 1)
        {
            UnequipWeapon();
        }
        else
        {
            EquipWeapon(_currentWeapon + 1);
        }
    }

    private void CycleWeaponDown()
    {
        if (_currentWeapon == -1)
        {
            EquipWeapon(_weaponPrefabs.Count - 1);
        }
        else if (_currentWeapon == 0)
        {
            UnequipWeapon();
        }
        else
        {
            EquipWeapon(_currentWeapon - 1);
        }

    }

    private void EquipMelee()
    {
        EquipWeapon(0);
    }
    private void UnequipWeapon()
    {
        if (_weaponParent.transform.childCount > 0)
        {
            Destroy(_weaponParent.transform.GetChild(0).gameObject);

            _weaponObjInHand = null;
            _weaponInHand = null;
        }

        _isHoldingWeapon = false;

        _currentWeapon = -1;
    }

    private void EquipWeapon(int num)
    {
        if (_currentWeapon != num)
        {
            UnequipWeapon();

            _isHoldingWeapon = true;

            if (num < _weaponPrefabs.Count)
            {
                _weaponObjInHand = Instantiate(_weaponPrefabs[num], _weaponParent.transform);
                _weaponInHand = _weaponObjInHand.GetComponent<Weapon>();
            }

            _currentWeapon = num;
        }
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log(damage);
    }

    public void HealDamage (float heal)
    {
        _currentHealth += heal;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void SpeedBoost(float speed)
    {
        _playerDefaultSpeed += speed;
    }

    public void ResetSpeed()
    {
        _currentSpeed = _playerDefaultSpeed;
    }
    private void UpdateWeaponDisplay()
    {
        if (_LastHoldVal != _isHoldingWeapon)
        {
            _weaponArms.SetActive(_isHoldingWeapon);

            _noWeaponArms.SetActive(!_isHoldingWeapon);
        }

        _LastHoldVal = _isHoldingWeapon;
    }

    private void UpdateHealthDisplay()
    {
        if (_playerHealthDisp != null)
        {
            _playerHealthDisp.text = _currentHealth.ToString();
        }
    }

    private void CheckIfDead()
    {
        if (_currentHealth <= 0.0f && !_isDead)
        {
            _currentHealth = 0.0f;

            Die();
        }
    }

    private void Die()
    {
        _isDead = true;

        _player.SetActive(false);

        _deadBody.SetActive(true);

        _deathMessage.gameObject.SetActive(true);

        Time.timeScale = 0.0f;
    }

    private void FireWeapon()
    {
        if (_weaponInHand != null)
        {
            _weaponInHand.Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable objectToInteract;

        if (collision.TryGetComponent<Interactable>(out objectToInteract))
        {
            _currentInteractable = objectToInteract;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable objectLeavingInteract;

        if (collision.TryGetComponent<Interactable>(out objectLeavingInteract))
        {
            if (objectLeavingInteract == _currentInteractable)
            {
                _currentInteractable = null;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public List<GameObject> _weaponPrefabs;

    public GameObject _player;
    public GameObject _deadBody;
    public GameObject _weaponArms;
    public GameObject _noWeaponArms;
    public GameObject _weaponParent;
    public GameObject _weaponObjInHand = null;

    public Weapon _weaponInHand = null;

    public Rigidbody2D _rb;

    public Text _playerHealthDisp;
    public Text _deathMessage;

    public float _maxHealth = 100.0f;
    public float _currentHealth = 100.0f;

    [Range(1.0f, 10.0f)]
    public float _playerSpeed = 10.0f;

    public bool _isDead = false;
    public bool _isHoldingWeapon = false;
    
    private bool _LastHoldVal = false;
    private bool _firing = false;

    private int _currentWeapon = -1;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        ResetHealth();
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
        }

        MovePlayer();

        Vector2 aimPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        AimPlayer(aimPos);

        WeaponInput();
    }

    private void MovePlayer()
    {
        Vector2 movementDir = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            movementDir += new Vector2(0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementDir += new Vector2(0.0f, -1.0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDir += new Vector2(-1.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementDir += new Vector2(1.0f, 0.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _firing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _firing = false;
        }

        _rb.velocity = movementDir * (_playerSpeed);
    }

    private void AimPlayer(Vector2 aimPos)
    {
        Vector2 lookAtDir;

        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_player.transform.position);
        lookAtDir = aimPos - new Vector2(playerScreenPoint.x, playerScreenPoint.y);

        float angle = Mathf.Atan2(-lookAtDir.x, lookAtDir.y) * Mathf.Rad2Deg;

        _player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void WeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CycleWeaponDown();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CycleWeaponUp();
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
    }

    public void HealDamage (float heal)
    {
        _currentHealth += heal;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
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
}
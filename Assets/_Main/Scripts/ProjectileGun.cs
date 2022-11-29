using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Events
using UnityEngine.UI;

public class ProjectileGun : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to assign the bullet
    [SerializeField] private GameObject _bullet;
    // Reference to assign the camera
    [SerializeField] private Camera _fpsCamera;
    // Reference to assign the Attack Point
    [SerializeField] private Transform _attackPoint;

    [SerializeField] private Button fireBtn;
    // Bullet Forces involved
    [SerializeField] private float _shootForce, _upwardForce;
    // Gun Stats
    [SerializeField] private float _timeBtwnShooting, _spread, _reloadTime, _timeBtwnShots;
    // Gun's Damage
    [SerializeField] private float _damage;
    // Magazine size and Bullets per tap
    [SerializeField] private int _magazineSize, _bulletsTap;
    // Bool to check if hold down the shoot button is allowed
    private bool _allowShootButton;
    // Variables for the bullets left and bullets shot
    private int _bulletsLeft, _bulletsShot;
    // Bools to check the Shooting Status 
    private bool _shooting, _readyToShoot, _reloading;
    // Bool that checks if the ResetShot Method is ready allowed to Invoke
    private bool _allowInvoke = true;

    // UI variables
    // Variable to assign the muzzle flash
    [SerializeField] private GameObject _muzzleFlash;
    // Variable to assign the ammo display
    [SerializeField] private TextMeshProUGUI _ammoDisplay;
    // Variable to control when button is pressed
    private bool _fireButtonPressed;

    private void Awake()
    {
        // At the begining the bullets left is equal to the magazine size, meaning the magazine is full
        _bulletsLeft = _magazineSize;
        // And the Player is ready to shoot
        _readyToShoot = true;
        _fireButtonPressed = false;
    }

    private void Start()
    {
        fireBtn.onClick.AddListener(() => { OnFireButtonPressed(); });
    }


    // Update is called once per frame
    void Update()
    {
        // Invokes the Player Input Method
        PlayerInput();

        // Set ammo Display if it exists
        if (_ammoDisplay != null)
        {
            // ammo display show the magazineSize is divided by bulletsTap to show actually how many clicks the Player has
            _ammoDisplay.SetText(_bulletsLeft / _bulletsTap + " / " + _magazineSize / _bulletsTap);
        }
    }

    public void PlayerInput()
    {
        // Check if the Player is allowed to hold the fire button        
        if (_allowShootButton && _fireButtonPressed)
        {
            // If holding the mouse left button shooting would be true
            //_shooting = Input.GetKey(KeyCode.S);
            _shooting = _fireButtonPressed;
        }
        // If the Player is NOT allowed to hold the fire button
        else
        {
            // The Player needs to tap the button every time to shoot
            //_shooting = Input.GetKeyDown(KeyCode.S);
            _shooting = _fireButtonPressed;
        }

        // ***** RELOADING *****
        // The conditions for reloading are whenever the R key is pressed
        // There are no bullets left in the magazine
        // The player has not yet reloaded
        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < _magazineSize && !_reloading)
        {
            Reload();
        }

        // Auto Reloading
        // Whenever the Player wants to shoot and is not reloading and there are no bullets left in the magazine (0 bullets)
        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft <= 0)
        {
            Reload();
        }

        // Checks if the Player is Ready to Shoot and Shooting (Meaning that the Input has given)
        // Also the condition has to check if the Player is not reloading and has bullets in the magazine
        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft > 0)
        {
            // If so the bullets shot is set to 0
            _bulletsShot = 0;
            _fireButtonPressed = false;
            // Invokes the Shoot Method
            Shoot();
        }
    }

    private void OnFireButtonPressed()
    {
        _fireButtonPressed = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _fireButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _fireButtonPressed = false;
    }

    private void Shoot()
    {
        // Sets the bool to _readyToShoot false because the Player is now Shooting
        _readyToShoot = false;

        // ****** RAYCAST SYSTEM ******
        // Finds the exact hit position of the Attack Point with Raycast
        // Access to the camera and ViewportPointToRay 0.5f, 0.5f, 0 = middle of the screen
        Ray ray = _fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // hit variable
        RaycastHit hit;
        // Target Point Vector to check if the Raycast hits something
        Vector3 targetPoint;

        // Check if the Raycast has hit something
        if (Physics.Raycast(ray, out hit))
        {
            // If hits something the hit point is in fact the Target Point
            targetPoint = hit.point;
            // Message to know what we shoot
            //Debug.Log(hit.transform.name);

            TargetLife target = hit.transform.GetComponent<TargetLife>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
        }
        // If the Ray does not hit anything then it shot to the air
        else
        {
            // A point far away from the player is the Target Point
            targetPoint = ray.GetPoint(77);
        }

        // Calculate the Direction from Attack Point to Target Point
        Vector3 directionWithoutSpread = targetPoint - _attackPoint.position;

        // Calculate the spread
        float xSpread = Random.Range(-_spread, _spread);
        float ySpread = Random.Range(-_spread, _spread);

        // Recalculate the Direction
        // Adds Spread to the last direction
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(xSpread, ySpread, 0);

        // Instantiate the bullet at the Attack Point position with no rotation
        // The instantiate bullets are stored in the currentBullet variable
        GameObject currentBullet = Instantiate(_bullet, _attackPoint.position, Quaternion.identity);

        // Instantiate the Audio Manager for the Shot Sound
        AudioManager.Instance.ShotPlayPlayer();

        // Rotate the bullet at the direction the Player shoots
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add Forces to the Bullet 
        // Access to the Rigidbody of the instantiate bullet and add force in the direction with spread times the shootForce with impulse
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * _shootForce, ForceMode.Impulse);
        // For granades we add an Upward Force
        currentBullet.GetComponent<Rigidbody>().AddForce(_fpsCamera.transform.up * _upwardForce, ForceMode.Impulse);

        // Instantiate Muzzle Flash
        // If theres is a muzzle flash
        if (_muzzleFlash != null)
        {
            // Instantiate at the Attack Point
            Instantiate(_muzzleFlash, _attackPoint.position, Quaternion.identity);
        }

        GameObject.Destroy(currentBullet, 3f);

        // Countdown how many bullets are left
        _bulletsLeft--;
        // Countup how many bullets are shot
        _bulletsShot++;

        // Invokes ResetShot 
        if (_allowInvoke)
        {   // _timeBtwnShooting as delay
            Invoke(nameof(ResetShot), _timeBtwnShooting);
            // We only need to Invoke once
            _allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        // The bools are true
        _readyToShoot = true;
        _allowInvoke = true;
    }

    private void Reload()
    {
        // Instantiate the Audio Manager for the Reloading Sound
        AudioManager.Instance.PlayReloading();
        // The Player is reloading
        _reloading = true;
        // Invokes the ReloadFinished with _reloadTime as delay
        Invoke(nameof(ReloadFinished), _reloadTime);
    }

    private void ReloadFinished()
    {
        // Fill the magazine
        _bulletsLeft = _magazineSize;
        // And the Player is not reloading
        _reloading = false;
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // To Instantiate the AudioManager in other scripts
    public static AudioManager Instance { get; private set; }

    // References to the Shot
    [SerializeField] private AudioClip _playerShot;
    [SerializeField] private GameObject _gun;
    // References when Reloading
    [SerializeField] private AudioClip _reloading;
    [SerializeField] private GameObject _magazine;
    // References to the Steps
    [SerializeField] private AudioClip _stepsAC;
    [SerializeField] private GameObject _steps;
    // References for Graunt
    // The AudioClip as an Array to randomize the graunt sounds
    [SerializeField] private AudioClip[] _damage;
    [SerializeField] private GameObject _impactZone;
    //References when Healing
    [SerializeField] private AudioClip _healing;
    [SerializeField] private GameObject _medKit;

    // AudioSources
    private AudioSource _audioSourceGun;
    private AudioSource _audioSourceStepsSound;
    private AudioSource _audioSourceMagazine;
    private AudioSource _audioSourceDamage;
    private AudioSource _audioSourceHealing;

    private void Awake()
    {
        // Checks if that the Instance is null and there is no duplicates
        // It SHOULD NOT execute this block but there will be executed if there is somo error        
        if (Instance != null)
        {
            // Message when there is an error 
            Debug.LogError("There's more than one AudioManager " + transform + " - " + Instance);
            // Prevents the existence of the duplicate AudioManager by destroying it
            Destroy(gameObject);
            // The execution of the method is done
            return;
        }
        // Instanciate this class with the prestablished properties       
        Instance = this;
    }

    private void Start()
    {
        _audioSourceGun = _gun.GetComponent<AudioSource>();
        _audioSourceStepsSound = _steps.GetComponent<AudioSource>();
        _audioSourceMagazine = _magazine.GetComponent<AudioSource>();
        _audioSourceDamage = _impactZone.GetComponent<AudioSource>();
        _audioSourceHealing = _medKit.GetComponent<AudioSource>();
    }

    public void ShotPlayPlayer()
    {
        Debug.Log("Piuh Piuh");
        PlaySound(_playerShot);
    }

    public void PlaySteps()
    {
        Debug.Log("Step Step");
        PlaySound(_stepsAC);
    }

    public void PlayReloading()
    {
        Debug.Log("Reloading");
        PlaySound(_reloading);
    }

    public void PlayDamageTaken()
    {
        Debug.Log("Auch");
        _audioSourceDamage.clip = _damage[Random.Range(0, _damage.Length)];
        _audioSourceDamage.Play();
    }

    public void PlayHealing()
    {
        Debug.Log("Im OK");
        PlaySound(_healing);
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSourceGun.PlayOneShot(audioClip);
    }
}

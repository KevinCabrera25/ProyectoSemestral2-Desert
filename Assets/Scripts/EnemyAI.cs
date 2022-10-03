using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Variable to assign the reference of the Agent
    [SerializeField] private NavMeshAgent _agent;
    // Variable to assign the reference of the Player
    [SerializeField] private Transform _player;
    // Variables to assing the Layers
    public LayerMask Ground, Player;

    // Enemy is patroling
    [SerializeField] private Vector3 _walkPoint;
    // Variable to check if the Walk Point is set
    bool walkPointSet;
    // Controls the Walk Range
    [SerializeField] private float _walkPointRange;

    // Enemy is Attacking
    [SerializeField] private float _attacksCD; // Time between attacks
    // To check if the Enemy has already attacked
    bool alreadyAttacked;
    // Reference to assign the laser Enemy
    [SerializeField] private GameObject _laserEnemy;
    // Reference to assign the Attack Point Enemy
    [SerializeField] private Transform _attackPointEnemy;
    // Variable to assign the shooting
    [SerializeField] private float _shootingSpeed;
    // Enemy's Damage
    [SerializeField] private float _enemysDamage;

    // States
    // Defines the Enemy's Range
    // Sight Range must be greater than Attack Range in order for the Enemy to Chase the Player 
    [SerializeField] private float _sightRange, _attackRange;
    // Checks if the Player is in the Range
    private bool _playerInSightRange, _playerInAttackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        // Finds for the Player
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("I've found the Player");
        // Assign the NavMeshAgent
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Checks for the Sight Range                       Position  Sight Range  Layer Player
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, Player);
        // Checks for the Attack Range                      Position  Attack Range  Layer Player
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, Player);

        // If the Player is not in Sight Range nor in Attack Range
        if(!_playerInSightRange && !_playerInAttackRange)
        {
            // The Enemy is Patrolling
            Patrolling();
        }

        // If the Player is in Sight Range but not in Attack Range
        if(_playerInSightRange && !_playerInAttackRange)
        {
            // The Enemy is Chasing
            ChasePlayer();
        }

        // If the Player is Sight Range and in Attack Range
        if (_playerInAttackRange && _playerInSightRange)
        {
            // The Enemy is Attacking the Player
            AttackPlayer();
        }
    }

    // ****** PATROLLING ******
    private void Patrolling()
    {
        // If there is no walkPointSet then invokes the SearchWalkPoint Method
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        // If there is a walkPointSet
        if(walkPointSet)
        {
            // The Enemy walks to the walkPoint
            _agent.SetDestination(_walkPoint);
        }

        // Calculates the distance to that walkPoint
        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        // If the distance is less than 1 the walkPoint is reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            // Resets the walkPointSet to search for a new WalkPoint
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculates a Random point in the _walkPointRange
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        // Transform to the Random Point
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // To verify if this point is within the map we check if is on Ground with Raycast
        if(Physics.Raycast(_walkPoint, -transform.up, 2f, Ground)) // If is on Ground
        {
            // The bool is true, we have a walkPointSet
            walkPointSet = true;
        }

    }

    // ****** CHASING ******
    private void ChasePlayer()
    {
        // The Enemy moves towards the Player's position
        _agent.SetDestination(_player.position);
    }

    // ****** ATTACKING ******
    private void AttackPlayer()
    {
        // Ensures the Enemy is not in motion
        _agent.SetDestination(transform.position);
        // When the Enemy is attacking it looks at the Player
        transform.LookAt(_player);

        // If the Enemy has not yet attacked
        if(!alreadyAttacked)
        {
            // Invokes Shooting Method
            Shooting();
            // Sets the bool to true
            alreadyAttacked = true;
            // Invokes the ResetAttack function, and the _attacksCD as a delay
            Invoke(nameof(ResetAttack), _attacksCD);


        }
       
    }

    private void Shooting()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Vector3 targetPlayerPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPlayerPoint = hit.point;
            Debug.Log(hit.transform.name);

            // Reference to the Player's PH to take Damage    
            PlayerHP playerHP = hit.transform.GetComponent<PlayerHP>();
            if(playerHP != null)
            {
                playerHP.PlayerDamageTaken(_enemysDamage);
            }           
        }
        else 
        {
            targetPlayerPoint = ray.GetPoint(77f);
        }

        Vector3 direction = targetPlayerPoint - _attackPointEnemy.position;

        GameObject currentLaserEnemy = Instantiate(_laserEnemy, _attackPointEnemy.position, Quaternion.identity);

        currentLaserEnemy.transform.forward = direction.normalized;

        currentLaserEnemy.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootingSpeed, ForceMode.Impulse);

        // Destroys the Laser Enemy after 3 secs
        GameObject.Destroy(currentLaserEnemy, 3f);

        /*
        // SHOOTING ATTACK CODE
        Rigidbody rb = Instantiate(_laserEnemy, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        */
    }

    private void ResetAttack()
    {
        // Sets the bool to false
        alreadyAttacked = false;
    }
}

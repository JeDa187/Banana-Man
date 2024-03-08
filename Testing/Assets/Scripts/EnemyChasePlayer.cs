using UnityEngine;
using UnityEngine.AI;

// Controls an enemy AI that chases the player when within a certain distance
public class EnemyChasePlayer : MonoBehaviour
{
    public Transform targetTransform; // The target's transform that the enemy will chase
    public float reevaluatePathTime = 0.3f; // Interval at which the enemy reevaluates the path to the target
    public float chaseDistanceThreshold = 3.0f; // The threshold distance to start chasing the target

    private NavMeshAgent navigationAgent; // The navigation agent used for pathfinding
    private Animator movementAnimator; // Animator for controlling the movement animations
    private float pathUpdateTimer = 0.0f; // Timer for tracking when to reevaluate the path

    // Initialize the agent and animator components
    void Start()
    {
        navigationAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        movementAnimator = GetComponent<Animator>(); // Get the Animator component
    }

   
    void Update()
    {
        // Countdown the timer
        pathUpdateTimer -= Time.deltaTime;

        // If the timer runs out, recalculate the path to the target
        if (pathUpdateTimer < 0.0f)
        {
            // Calculate the squared distance to the target for efficiency
            float squaredDistanceToTarget = (targetTransform.position - navigationAgent.destination).sqrMagnitude;

            // Check if the target is within the chase threshold
            if (squaredDistanceToTarget > chaseDistanceThreshold * chaseDistanceThreshold)
            {
                // Update the destination of the NavMeshAgent to the target's position
                navigationAgent.destination = targetTransform.position;

                // Reset the path update timer
                pathUpdateTimer = reevaluatePathTime;
            }
        }

        // Update the speed parameter of the animator with the magnitude of the agent's velocity
        movementAnimator.SetFloat("speed", navigationAgent.velocity.magnitude);
    }
}

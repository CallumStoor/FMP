using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Camera playerCamera;

    [Header("Distance")]
    [SerializeField] private float avoidDistance = 10f;
    [SerializeField] private float stopRange = 3f;

    [Header("Look Detection")]
    [SerializeField] private float lookThreshold = 0.8f;
    [SerializeField] private float lookTimeRequired = 2f;

    [Header("Flee")]
    [SerializeField] private float fleeDistance = 15f;
    [SerializeField] private float fleeDuration = 5f;

    [Header("Reposition")]
    [SerializeField] private float repositionDistance = 12f;

    private float distance;
    private float currentLookTime;
    private float fleeTimer;

    private bool isFleeing;
    private bool isRepositioning;

    private Vector3 repositionTarget;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // We rotate manually
        agent.updateRotation = false;
    }

    private void Update()
    {
        // Update distance first
        distance = Vector3.Distance(transform.position, target.position);

        // Check if player is looking at monster
        CheckIfLookedAt();

        // =========================
        // FLEEING
        // =========================

        if (isFleeing)
        {
            fleeTimer -= Time.deltaTime;

            FleeFromPlayer();

            // Stop fleeing after timer ends
            if (fleeTimer <= 0f)
            {
                isFleeing = false;

                // Move to another side of player
                StartReposition();

                currentLookTime = 0f;
            }

            return;
        }

        // =========================
        // REPOSITIONING
        // =========================

        if (isRepositioning)
        {
            agent.isStopped = false;
            agent.SetDestination(repositionTarget);

            FaceMovementDirection();

            float repositionDistanceToTarget =
                Vector3.Distance(transform.position, repositionTarget);

            // Once reached new position
            if (repositionDistanceToTarget < 1.5f)
            {
                isRepositioning = false;
            }

            return;
        }

        // =========================
        // NORMAL AI BEHAVIOUR
        // =========================

        // Too far away -> move closer
        if (distance > avoidDistance + stopRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);

            FaceMovementDirection();
        }

        // Within ideal range -> stop and look at player
        else if (distance >= avoidDistance &&
                 distance <= avoidDistance + stopRange)
        {
            agent.isStopped = true;

            FaceTarget();
        }

        // Too close -> move away
        else
        {
            agent.isStopped = false;

            Vector3 direction =
                (transform.position - target.position).normalized;

            Vector3 newPosition =
                transform.position + direction * 5f;

            agent.SetDestination(newPosition);

            FaceMovementDirection();
        }
    }

    private void CheckIfLookedAt()
    {
        // Don't check while fleeing
        if (isFleeing)
            return;

        // Only detect looking while stopped
        bool inStoppingRange =
            distance >= avoidDistance &&
            distance <= avoidDistance + stopRange;

        if (!inStoppingRange)
        {
            currentLookTime = 0f;
            return;
        }

        Vector3 directionToMonster =
            (transform.position - playerCamera.transform.position).normalized;

        float dot = Vector3.Dot(
            playerCamera.transform.forward,
            directionToMonster
        );

        // Player is looking at monster
        if (dot > lookThreshold)
        {
            currentLookTime += Time.deltaTime;

            if (currentLookTime >= lookTimeRequired)
            {
                isFleeing = true;
                fleeTimer = fleeDuration;
            }
        }
        else
        {
            currentLookTime = 0f;
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 directionAway =
            (transform.position - target.position).normalized;

        Vector3 fleePosition =
            transform.position + directionAway * fleeDistance;

        agent.isStopped = false;
        agent.SetDestination(fleePosition);

        FaceMovementDirection();
    }

    private void StartReposition()
    {
        isRepositioning = true;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            // Random angle around player
            float randomAngle = Random.Range(0f, 360f);

            Vector3 direction =
                Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;

            Vector3 potentialPosition =
                target.position + direction * repositionDistance;

            // Keep on NavMesh
            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                potentialPosition,
                out hit,
                5f,
                NavMesh.AllAreas))
            {
                // Reject positions inside detection range
                float distanceToPlayer =
                    Vector3.Distance(hit.position, target.position);

                if (distanceToPlayer > avoidDistance + stopRange + 2f)
                {
                    repositionTarget = hit.position;
                    return;
                }
            }
        }

        // Fallback position if no valid point found
        repositionTarget =
            transform.position + transform.right * repositionDistance;
    }

    private void FaceTarget()
    {
        Vector3 lookDirection =
            target.position - transform.position;

        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation =
                Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                Time.deltaTime * 5f
            );
        }
    }

    private void FaceMovementDirection()
    {
        Vector3 velocity = agent.velocity;

        velocity.y = 0;

        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion rotation =
                Quaternion.LookRotation(velocity.normalized);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                Time.deltaTime * 5f
            );
        }
    }
}
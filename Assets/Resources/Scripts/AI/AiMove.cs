using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]

public class AiMove : MonoBehaviour
{
    [SerializeField] private float maxTimeWaiting;

    private NavMeshAgent agent;
    private NavMeshData groundForMove;
    private Vector3 groundCenter;
    private Vector3 groundSize;

    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        groundForMove = GetComponentInParent<NavMeshSurface>().navMeshData;
        animator = GetComponent<Animator>();
        groundCenter = groundForMove.sourceBounds.center;
        groundSize = groundForMove.sourceBounds.size;
        Move();
    }

    private void Move()
    {
        agent.SetDestination(GeneratePointToMove());
        animator.SetBool("move", true);
        StartCoroutine(CheckFinish());
    }

    private void StoppingMove()
    {
        animator.SetBool("move", false);
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(Random.Range(2, maxTimeWaiting));
        Move();
    }

    private IEnumerator CheckFinish()
    {
        yield return new WaitForFixedUpdate();
        while (agent.remainingDistance > agent.stoppingDistance*1.5)
        {
            yield return new WaitForFixedUpdate();
        }

        StoppingMove();
    }

    private Vector3 GeneratePointToMove()
    {
        return transform.TransformPoint(groundForMove.sourceBounds.ClosestPoint(groundCenter + RandomPoint(groundSize)));
    }

    private Vector3 RandomPoint(Vector3 vector)
    {
        vector /= 2;
        return new Vector3
        (
            Random.Range(-vector.x, vector.x),
            Random.Range(-vector.y, vector.y),
            Random.Range(-vector.z, vector.z)
        );
    }
}

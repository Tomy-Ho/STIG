using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] List<Transform> targetPoints = new();
    [SerializeField] bool enableMovement = false;

    Animator anim;

    NavMeshAgent agent;

    int currentTargetIdx = 0;

    bool handled = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (enableMovement)
        {

            agent.SetDestination(targetPoints[currentTargetIdx].position);
        }


        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*         var dist = Vector3.Distance(transform.position, agent.destination);
                if(dist < 0.5f){
                    // TODO: Maybe idle here and then go to next point?


                } */
        anim.SetFloat("Horizontal", agent.velocity.x);
        anim.SetFloat("Vertical", agent.velocity.y);

        var sideways = Mathf.Abs(agent.velocity.x) > Mathf.Abs(agent.velocity.y);
        anim.SetBool("Sideways", sideways);

        var moving = (Mathf.Abs(agent.velocity.x) > 0.0f || Mathf.Abs(agent.velocity.y) > 0.0f) && (agent.remainingDistance >= agent.stoppingDistance);
        anim.SetBool("Moving", moving);

        if (!agent.pathPending && enableMovement)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 0.1f)
            {
                //if (!agent.hasPath)
                //{
                if (!handled)
                {
                    StartCoroutine(idle());
                }
                //}
            }
        }

    }

    IEnumerator idle()
    {
        handled = true;

        var randWaitTime = UnityEngine.Random.Range(2.0f, 10.0f);

        Debug.Log("waiting: " + randWaitTime + "s.");

        yield return new WaitForSeconds(randWaitTime);

        /*         if (currentTargetIdx >= (targetPoints.Count - 1))
                {
                    currentTargetIdx = 0;
                }
                else
                {
                    currentTargetIdx++;
                } */

        currentTargetIdx = UnityEngine.Random.Range(0, targetPoints.Count);


        agent.SetDestination(targetPoints[currentTargetIdx].position);

        Debug.Log("new dest idx: " + currentTargetIdx);

        handled = false;
    }
}

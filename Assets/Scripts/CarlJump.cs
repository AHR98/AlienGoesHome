using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class CarlJump : MonoBehaviour
{
    private Animator animator;

    public AnimationCurve m_Curve = new AnimationCurve();

    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                animator.SetBool("Jump", true);
                yield return StartCoroutine(Parabola(agent, 1.5f, 1.5f));
                agent.CompleteOffMeshLink();
                //animator.SetBool("Jump", false);
            }

            yield return null;
            animator.SetBool("Jump", false);
        }

    }

   
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }

   

}

using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IState<Enemy>
{
    private float timer;
    private Vector3 newPos;

    public void OnEnter(Enemy t)
    {
        timer = t.wanderTimer;
        t.agent.speed = 5;
        t.agent.isStopped = false;
    }

    public void OnExcute(Enemy t)
    {
        float checkEnemyIdle = Vector3.Distance(t.agent.transform.position, newPos);
        if (checkEnemyIdle < 0.1f)
        {
            t.ChangeAnim(CacheString.ANIM_IDLE);
        }
        else
        {
            t.ChangeAnim(CacheString.ANIM_RUN);
        }

        if (t.target != null)
        {
            t.agent.isStopped = true;
            t.agent.speed = 0f;
            t.ChangeAnim(CacheString.ANIM_IDLE);
            t.ChangeState(new StateAttack());
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= t.wanderTimer)
            {
                newPos = RandomNavSphere(t.transform.position, t.wanderRadius, -1);
                t.agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public void OnExit(Enemy t)
    {

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

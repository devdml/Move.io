using Lean.Pool;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTimer;
    private IState<Enemy> currentState;

    protected override void Start()
    {
        base.Start();
        ChangeState(new StatePatrol());
    }

    protected override void Update()
    {
        if (isDead)
        { 
            return;
        }

        base.Update();

        if (isHit == true)
        {
            LevelManager.Instance.enemies.Remove(this);
            ChangeAnim(CacheString.ANIM_DIE);
            LeanPool.Despawn(this, 2f);
            agent.isStopped = true;
            isDead = true;
        }
        else
        {
            if (currentState != null)
            {
                currentState.OnExcute(this);
            }
        }
    }

    private void OnDisable()
    {
        isHit = false;
        listTarget.Clear();
        target = null;
        isDead = false;
    }

    public void ChangeState(IState<Enemy> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}

using System.Diagnostics;

public class StateAttack : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {

    }

    public void OnExcute(Enemy t)
    {
        if (t.target != null)
        {
            t.RotateToTarget();
            if (t.agent.speed == 0)
            {
                t.Attack();
            }
        }
        else
        {
            t.ChangeState(new StatePatrol());
        }
    }

    public void OnExit(Enemy t)
    {

    }
}

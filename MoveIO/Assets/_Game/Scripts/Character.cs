using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected Transform skin;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float timeDelay;
    [SerializeField] private Animator anim;

    private Vector3 dir;

    protected string currentAnimName = CacheString.ANIM_IDLE;

    public List<GameObject> listTarget = new List<GameObject>();
    public GameObject target;

    [Header("Bool")]
    public bool isAttack = false;
    public bool isDead = false;
    public bool isHit = false;

    protected virtual void Start()
    {
        init();
    }

    protected virtual void Update()
    {
        if (this != null)
        {
            FindClosestTarget();
        }

        if (listTarget.Count == 0)
        {
            target = null;
        }
    }

    //Private

    private void init()
    {
        isAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(CacheString.LAYER_CHARACTER))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (character.isDead == false)
            {
                listTarget.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(CacheString.LAYER_CHARACTER))
        {
            listTarget.Remove(other.gameObject);
        }
    }

    public void FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        if (listTarget.Count > 0)
        {
            for (int i = 0; i < listTarget.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, listTarget[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = listTarget[i];
                }
            }
        }
        else
        {
            target = null;
            StopCoroutine(nameof(DelayShoot));
        }
    }

    private void Shooting()
    {
        if (isDead == true) return;
        if (target != null)
        {
            dir = target.transform.position - transform.position;
        }

        Bullet bulletSpwan = LeanPool.Spawn(bullet, shootPoint.position, shootPoint.rotation);

        bulletSpwan.SeekDir(dir);

        bulletSpwan.SeekAttacker(this);

        bulletSpwan.OnDespawn(1f);
    }

    //protected
    protected IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(0.5f);
        Shooting();
    }

    //Public

    public void Attack()
    {
        if (isAttack == false) return;
        isAttack = false;
        StartCoroutine(nameof(DelayShoot));
        ChangeAnim(CacheString.ANIM_ATTACK);
        Invoke(nameof(ResetAttack), timeDelay);
    }

    public void ResetAttack()
    {
        currentAnimName = CacheString.ANIM_IDLE;
        isAttack = true;
    }


    public void RotateToTarget()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            if (dir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(skin.rotation, lookRotation, 20f * Time.deltaTime).eulerAngles;
                skin.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}

using Lean.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Character character;

    private Vector3 dir;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        rb.velocity = dir.normalized * speed;
    }

    public void OnDespawn(float timeDespawn)
    {
        LeanPool.Despawn(gameObject, timeDespawn);
    }

    public void SeekDir(Vector3 dir)
    {
        this.dir = dir;
    }

    public void SeekAttacker(Character character)
    {
        this.character = character;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer(CacheString.LAYER_CHARACTER))
        {
            Character ca = other.GetComponent<Character>();
            if (ca != null)
            {
                ca.isHit = true;
                character.listTarget.Remove(character.target);
                character.target = null;
                LeanPool.Despawn(this);
            }  
        }
    }
}

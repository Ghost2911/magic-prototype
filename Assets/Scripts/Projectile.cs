using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float range;
    public GameObject particles;
    public float speed = 10f;
    
    public Transform owner;
    public float lifetime = 3f;

    private float a;
    private float b = 5f;

    private Vector3 startPos;
    private Vector3 endPos;

    private float prevCalcPoint;
    private float prevZ;
    private float endZ;
    private Vector2 castDirection;

    public void InitialSetup(Vector3 target, Transform owner, Vector3 offset, Vector2 swipeDir)
    {
        lifetime = 3f;
        prevCalcPoint = 0f;
        transform.position += offset;
        startPos = transform.position;
        castDirection = swipeDir;
        endPos = target;
        this.owner = owner;
        
        a = Vector3.Distance(transform.position, target)/2;
        prevZ = -a;
        endZ = a;

        transform.LookAt(target);
        StartCoroutine(MoveAlongEllipse());
    }

    IEnumerator MoveAlongEllipse()
    {

        float difference = 0f;
        while (true)
        {
            if (castDirection.y == 0)
            {
                float x = castDirection.x * Mathf.Sqrt(Mathf.Pow(b, 2) - (Mathf.Pow(b, 2) * Mathf.Pow(prevZ, 2)) / Mathf.Pow(a, 2));
                difference = x - prevCalcPoint;
                prevCalcPoint = x;
                transform.position += transform.forward * Time.deltaTime * speed + transform.right * difference;
            }
            else
            {
                float y = castDirection.y * Mathf.Sqrt(Mathf.Pow(b, 2) - (Mathf.Pow(b, 2) * Mathf.Pow(prevZ, 2)) / Mathf.Pow(a, 2));
                difference = y - prevCalcPoint;
                prevCalcPoint = y;
                transform.position += transform.forward * Time.deltaTime * speed + transform.up * difference;
            }
            prevZ += Time.deltaTime * speed;
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
                Destroy(this.gameObject);
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != owner)
        {
            Debug.Log($"Object hit - {collision.transform.name}");
            if (collision.transform.CompareTag("Shield"))
            {
                StopAllCoroutines();
                InitialSetup(startPos, collision.transform, Vector3.zero, castDirection * new Vector2(-1, 1));
                return;
            }
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>()?.Damage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject explossion = Instantiate(particles,transform.position,Quaternion.identity);
        Destroy(explossion, 0.4f);
    }
}

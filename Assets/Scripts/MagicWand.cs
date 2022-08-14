using UnityEngine;

public class MagicWand : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform shield;
    public Vector3 GetRaycastPosition()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit))
        {
            Debug.Log(rayHit.point);

            if (rayHit.transform.CompareTag("Shield"))
                return rayHit.transform.parent.position;
            if (rayHit.transform.CompareTag("Player"))
                return rayHit.transform.position;
            return rayHit.point;
        }
        return Vector3.zero;
    }

    public void RightCast()
    {
        GameObject go = Instantiate(prefabs[0], transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().InitialSetup(GetRaycastPosition(), shield, transform.parent.forward, Vector2.right);
    }

    public void LeftCast()
    {
        GameObject go = Instantiate(prefabs[1], transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().InitialSetup(GetRaycastPosition(), shield, transform.parent.forward, Vector2.left);
    }

    public void UpCast()
    {
        GameObject go = Instantiate(prefabs[2], transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().InitialSetup(GetRaycastPosition(), shield, transform.parent.forward, Vector2.up);
    }

    public void DownCast()
    {
        GameObject go = Instantiate(prefabs[2], transform.position, Quaternion.identity);
        go.GetComponent<Projectile>().InitialSetup(GetRaycastPosition(), shield, transform.parent.forward, Vector2.down);
    }
}

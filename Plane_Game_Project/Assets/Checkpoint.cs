using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        Destroy(GetComponent<MeshRenderer>());
        

        Rigidbody[] rbs = Instantiate(DestroyedPrefab, transform.position, transform.rotation * Quaternion.Euler(-90, 0,0))
        .GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(500, transform.position, 10);
            Vector3 randomTorque = Random.onUnitSphere * Random.Range(2000f, 5000f);
            rb.AddTorque(randomTorque);

        }

        Destroy(gameObject);
    }

    public GameObject DestroyedPrefab;
}

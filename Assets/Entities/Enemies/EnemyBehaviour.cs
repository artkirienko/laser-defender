using UnityEngine;

// Enemy shooting pattern etc.
public class EnemyBehaviour : MonoBehaviour
{
    public GameObject Beam;
    public float Health = 150f;
    public float BeamSpeed = 5f;
    public float ShotsPerSecond = 0.5f;

    private float _beamOffset = 0.7f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var beam = collider.gameObject.GetComponent<Projectile>();
        if (beam)
        {
            Health -= beam.GetDamage();
            beam.Hit();
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        var shotProbability = Time.deltaTime * ShotsPerSecond;
        if (Random.value < shotProbability)
        {
            Fire();
        }
    }   

    private void Fire()
    {
        var startPosition = transform.position + new Vector3(0, -_beamOffset, 0);
        var beam = Instantiate(Beam, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -BeamSpeed, 0);
    }
}
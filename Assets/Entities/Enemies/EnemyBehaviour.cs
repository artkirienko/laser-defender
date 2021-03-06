using UnityEngine;

/// <summary>
/// Enemy behaviour patterns.
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    public GameObject Beam;
    public float Health = 150f;
    public float BeamSpeed = 5f;
    public float ShotsPerSecond = 0.5f;
    public int EnemyValue = 150;
    public AudioClip DeathSound;
    public AudioClip FireSound;

    private float _beamOffset = 0.7f;
    private ScoreTracker _scoreTracker;


    void Start()
    {
        _scoreTracker = GameObject.Find("Score").GetComponent<ScoreTracker>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var beam = collider.gameObject.GetComponent<Projectile>();
        if (beam)
        {
            Health -= beam.GetDamage();
            beam.Hit();
            if (Health <= 0)
            {
                DestroyEnemy();
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

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        _scoreTracker.Score(EnemyValue);
    }

    private void Fire()
    {
        var startPosition = transform.position + new Vector3(0, -_beamOffset, 0);
        var beam = Instantiate(Beam, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -BeamSpeed, 0);
        AudioSource.PlayClipAtPoint(FireSound, startPosition);
    }
}

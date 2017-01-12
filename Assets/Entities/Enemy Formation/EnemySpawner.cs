using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

	void Start ()
	{
        // ����� ������ �� ������� ������������ ��������.
        foreach (Transform child in transform)
	    {
            var enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
	}
	
	void Update ()
    {
	}
}

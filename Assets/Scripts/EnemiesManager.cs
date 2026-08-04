using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private UnityEvent<Transform>onEnemyDestroy;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private  UnityEvent onAllEnemiesDestroyed;
    private int enemiesDestroyed = 0;
    private LevelData currentLevelData;
    public void SetLevel()
    {
        enemiesDestroyed = 0;
        currentLevelData = levelManager.GetCurrentLevelData();
        foreach (EnemiesData enemyData in currentLevelData.enemiesData)
        {
            StartCoroutine(spawnEnemy(enemyData));
        }    
        
    }
    private IEnumerator spawnEnemy(EnemiesData enemyData)
    {
    yield return new WaitForSeconds(enemyData.spawnTime);
    Enemy enemy = PoolManager.Instance.GetObject(enemyData.enemyPrefab.gameObject,Vector3.zero, true).GetComponent<Enemy>();
    enemy.Target = target;
    enemy.PositionEnemy();
    }
    private void HandleEnemyDeath(Transform enemyTransfrom)
    {
        onEnemyDestroy?.Invoke(enemyTransfrom);
        enemiesDestroyed++;
        if (enemiesDestroyed >= currentLevelData.enemiesData.Length)
        {
            onAllEnemiesDestroyed?.Invoke();
        }
    }
}

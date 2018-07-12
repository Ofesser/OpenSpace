using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class MonsterRespawnController : MonoBehaviour {

    private enum AvailableMonsters
    {
        Skeleton,
        Werewolf,
        Ogre
    }

    public static MonsterRespawnController Instance;

    public GameObject[] monsterList;
    public GameObject summonEffect;

    [SerializeField]
    private List<GameObject> summonedMonsterList;
    [HideInInspector]
    public List <GameObject> respawnPoints;

    [HideInInspector]
    public float respawnTime = 1f;

    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
	
	private void Start () {
        summonedMonsterList = new List<GameObject>();
        respawnPoints = new List<GameObject>();
        GameoverScreenController.Instance.playAgainButton.GetComponent<GameButton>().buttonOnPress += RemoveAllMonsters;
        for (int i = 0; i < transform.childCount; i++)
        {
            respawnPoints.Add(transform.GetChild(i).gameObject);
        }
        StartCoroutine(RepeatSummoning());
	}
	
    private IEnumerator RepeatSummoning ()
    {
        while (GlobalValues.gameIsActive)
        {
            yield return new WaitForSeconds(respawnTime);

            var weights = new Dictionary<AvailableMonsters, int>();
            weights.Add(AvailableMonsters.Skeleton, 80);
            weights.Add(AvailableMonsters.Werewolf, 15);
            weights.Add(AvailableMonsters.Ogre, 5);

            AvailableMonsters selectedMonster = WeightedRandomizer.From(weights).TakeOne(); d
            StartCoroutine(SummonMonster(monsterList[(int)selectedMonster], respawnPoints[Random.Range(0,respawnPoints.Count)]));
        }
    }

    private IEnumerator SummonMonster (GameObject monsterPrefab, GameObject summonPoint)
    {
        Instantiate(summonEffect, summonPoint.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        summonedMonsterList.Add(Instantiate(monsterPrefab, summonPoint.transform.position, Quaternion.identity));
    }

    public void RemoveAllMonsters ()
    {
        foreach (GameObject monster in summonedMonsterList)
        {
            Destroy(monster);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPooler : MonoBehaviour {
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static AttackPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion Singleton

    


	// Use this for initialization
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag,  objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, GameObject character, bool isPlayerAttack)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " does not exist");
            return null;
        }
       
        GameObject newObj =   poolDictionary[tag].Dequeue();
        newObj.SetActive(true);

        NormalAttackController normalAttack = newObj.GetComponent<NormalAttackController>();

        if(normalAttack != null){
            normalAttack.SetPlayerEnemyAttack(isPlayerAttack);
        }

        newObj.transform.SetParent(character.transform);
        newObj.transform.localPosition = position;
        newObj.transform.localRotation = rotation;



        

        poolDictionary[tag].Enqueue(newObj);

        return newObj;
    }


}

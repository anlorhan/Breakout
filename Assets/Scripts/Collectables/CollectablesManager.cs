using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    #region Singleton
    private static CollectablesManager _Instance;

    public static CollectablesManager Instance => _Instance;
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            _Instance = this;

        }
    }
    #endregion

    public List<Collectable> Buffs;
    public List<Collectable> Debuffs;

    [Range(0,100)]
    public float BuffChance;
    [Range(0, 100)]
    public float DebuffChance;
}

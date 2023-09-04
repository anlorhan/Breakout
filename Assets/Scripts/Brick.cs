using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    public static event Action<Brick> OnBrickDestruction;
    public ParticleSystem DestroyEffect;
    private SpriteRenderer sr;
    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        applycollisionlogic(ball);
    }

    private void applycollisionlogic(Ball ball)
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            BrickManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestruction?.Invoke(this);
            OnBrickDestroy();
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            sr.sprite = BrickManager.Instance.Sprites[hitPoints - 1];
        }
    }

    private void OnBrickDestroy()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float debuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;

        if (buffSpawnChance <= CollectablesManager.Instance.BuffChance)
        {
            alreadySpawned = true;
            Collectable newBuff = SpawnCollectable(true);
        }
        if (debuffSpawnChance <= CollectablesManager.Instance.DebuffChance && !alreadySpawned)
        {
            Collectable newDebuff = SpawnCollectable(false);
        }
        
    }

    private Collectable SpawnCollectable(bool isBuff)
    {
        List<Collectable> temp;
        if (isBuff)
        {
            temp = CollectablesManager.Instance.Buffs;
        }
        else
        {
            temp = CollectablesManager.Instance.Debuffs;
        }

        int bufIndex = UnityEngine.Random.Range(0, temp.Count);
        Collectable prefab = temp[bufIndex];
        Collectable newCollectable = Instantiate(prefab,transform.position, Quaternion.identity) as Collectable;
        GameManager.Instance.CollectableCollection.Add(newCollectable);

        return newCollectable;
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(brickPos.x, brickPos.y, brickPos.z - .2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPos, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }

    public void Init(Transform containerTransform, Sprite sprite, Color color, int hitpoints)
    {
        transform.SetParent(containerTransform);
        sr.sprite = sprite;
        sr.color = color;
        hitPoints = hitpoints;
    }
}

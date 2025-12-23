using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Object Pool", menuName = "GameData/Pool", order = 1)]
public class ObjectPool : ScriptableObject
{
    // public List<Animal> animal;

    // public AnimalIndexItem indexItem;
    // public AnimalItem animalItem;

    // public Animal GetAnimal(AnimalType type, Vector3 position)
    // {
    //     return FastPoolManager.GetPool(animal[(int)type].gameObject)
    //         .FastInstantiate<Animal>(position, Quaternion.identity);
    // }

    // public void DestroyAnimal(AnimalType type, GameObject go)
    // {
    //     FastPoolManager.GetPool(animal[(int)type]).FastDestroy(go);
    // }


    // public Bullet GetAnimalIndexItem(Transform parent)
    // {
    //     return FastPoolManager
    //         .GetPool(indexItem.gameObject)
    //         .FastInstantiate<AnimalIndexItem>(parent);
    // }

    // public void DestroyAnimalIndexItem(AnimalIndexItem item)
    // {
    //     FastPoolManager.GetPool(item.gameObject).FastDestroy(item.gameObject);
    // }

    // public AnimalItem GetAnimalItem(Transform parent)
    // {
    //     return FastPoolManager
    //         .GetPool(animalItem.gameObject)
    //         .FastInstantiate<AnimalItem>(parent);
    // }

    // public void DestroyAnimalItem(AnimalItem item)
    // {
    //     FastPoolManager.GetPool(item.gameObject).FastDestroy(item.gameObject);
    // }
    public Bullet bullet;
    public Bullet GetBullet(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(bullet.gameObject)
            .FastInstantiate<Bullet>(position, rotation, parent);
    }

    public void DestroyBullet(GameObject go)
    {
        FastPoolManager.GetPool(bullet).FastDestroy(go);
    }

    public CoinMove Coin;
    public CoinMove GetCoinMove(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(Coin.gameObject)
            .FastInstantiate<CoinMove>(position, rotation, parent);
    }
    public void DestroyCoin(GameObject coin)
    {
        FastPoolManager.GetPool(Coin).FastDestroy(coin);
    }

    public TrapMove trap;
    public TrapMove GetTrap(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(trap.gameObject)
            .FastInstantiate<TrapMove>(position, rotation, parent);
    }
    public void DestroyTrap(GameObject trap)
    {
        FastPoolManager.GetPool(this.trap).FastDestroy(trap);
    }

    public MiniBoss boss;
    public MiniBoss GetBoss(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(boss.gameObject)
            .FastInstantiate<MiniBoss>(position, rotation, parent);
    }
    public void DestroyBoss(GameObject boss)
    {
        FastPoolManager.GetPool(this.boss).FastDestroy(boss);
    }

    public BossSkillDamage bossSkill;
    public BossSkillDamage GetBossSkill(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(bossSkill.gameObject)
            .FastInstantiate<BossSkillDamage>(position, rotation, parent);
    }
    public void DestroyBossSkill(GameObject bossSkill)
    {
        FastPoolManager.GetPool(this.bossSkill).FastDestroy(bossSkill);
    }

    public BossSkillWarning bossSkillWarning;
    public BossSkillWarning GetBossSkillWarning(Vector3 position, Quaternion rotation, Transform parent)
    {
        return FastPoolManager
            .GetPool(bossSkillWarning.gameObject)
            .FastInstantiate<BossSkillWarning>(position, rotation, parent);
    }
    public void DestroyBossSkillWarning(GameObject bossSkillWarning)
    {
        FastPoolManager.GetPool(this.bossSkillWarning).FastDestroy(bossSkillWarning);
    }
}
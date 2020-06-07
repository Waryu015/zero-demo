﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    /**属性*/
    // 生命点
    public float healthyPoint = 3;
    // 攻击伤害值
    public int damage = 1;
    // 伤害闪烁时间
    public float damageFlashTime = 0.1f;
    // 精灵原始颜色（用于闪烁还原）
    protected Color originColor;

    /**组件*/
    protected SpriteRenderer spriteRenderer;
    public GameObject bloodEffect;
    private EnemyDropItemConfig enemyDropItemConfig;

    /**掉落物*/
    public Item[] dropItems;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        enemyDropItemConfig = GetComponent<EnemyDropItemConfig>();
    }

    // 受伤方法
    public void GetDamage(float damage)
    {
        
        healthyPoint -= damage;
        FlashColor();
        // 增加流血效果（试验，并不好看）
        //bloodEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -5);
        //Instantiate(bloodEffect);

        // 攻击时摄影机抖动效果（试验，效果并不好）
        //CameraShakeController.cameraShake.shake();

        if (healthyPoint <= 0)
        {
            if(enemyDropItemConfig != null)
            {
                Item item = DropItemUtil.RandomItem(enemyDropItemConfig.items);
                if (item != null)
                {
                    item.transform.position = new Vector3(transform.position.x, transform.position.y + 1, item.transform.position.z);
                    Instantiate(item);
                }
            }
            Destroy(gameObject);
        }
    }

    // 受伤闪烁
    protected void FlashColor()
    {
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", damageFlashTime);
    }

    // 重置颜色
    protected void ResetColor()
    {
        spriteRenderer.color = originColor;
    }

    protected void DropItem()
    {

    }

}

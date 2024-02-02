﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigScene : BYSingletonMono<ConfigScene> 
{
    [SerializeField]
    private List<Transform> enemy_spawns;

    //public Transform GetEnemySpawnPoint()
    //{
    //    int index =UnityEngine.Random.Range(0,enemy_spawns.Count);
    //    return enemy_spawns[index];

    //    //return enemy_spawns.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    //}

    private List<Transform> usedSpawnPoints = new List<Transform>();

    public Transform GetEnemySpawnPoint()
    {
        if (enemy_spawns.Count == 0)
        {
            Debug.LogError("No enemy spawn points available!");
            return null;
        }

        // Lọc những điểm spawn chưa được sử dụng
        List<Transform> availableSpawnPoints = enemy_spawns.Except(usedSpawnPoints).ToList();

        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("All enemy spawn points have been used. Reusing points...");
            usedSpawnPoints.Clear();
            availableSpawnPoints = new List<Transform>(enemy_spawns);
        }

        // Chọn một điểm spawn ngẫu nhiên từ những điểm còn lại
        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        Transform selectedSpawnPoint = availableSpawnPoints[index];

        // Đánh dấu điểm spawn đã được sử dụng
        usedSpawnPoints.Add(selectedSpawnPoint);

        return selectedSpawnPoint;
    }
}
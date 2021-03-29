﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        
        [SerializeField] private Levels storedLevels;
        private int actualLevel;
        private int nodesAvailableOnLevel;

        public Action OnSandwichDone;
        

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            LoadLevels();
            LoadActualLevelValue();
        }

        private void LoadLevels()
        {
            if (storedLevels == null || storedLevels.levels.Count == 0)
                storedLevels = Resources.Load<Levels>("levels");
        }

        private void LoadActualLevelValue()
        {
            actualLevel = (PlayerPrefs.HasKey("actualLevel")) ? PlayerPrefs.GetInt("actualLevel") : 2;
        }

        // Get data from created Levels, if it has predefined information, or if should generate a new level
        public Level GetCurrentLevelData()
        {
            var currentLevel = storedLevels.levels[actualLevel - 1];

            if (currentLevel.autoGenerated)
                nodesAvailableOnLevel = currentLevel.piecesAmount;
            else
                nodesAvailableOnLevel = currentLevel.nodes.Where(n => n.content != NodeContent.Empty).ToList().Count;

            return currentLevel;
        }

        public void DecreaseNodesAvailable()
        {
            nodesAvailableOnLevel--;
            if (nodesAvailableOnLevel == 1)
            {
                OnSandwichDone?.Invoke();
            }
        }
    }
}
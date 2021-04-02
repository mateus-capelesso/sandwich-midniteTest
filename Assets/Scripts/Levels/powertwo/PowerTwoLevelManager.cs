using System;
using UnityEditor;
using UnityEngine;

namespace Levels.powertwo
{
    public class PowerTwoLevelManager : LevelManager
    {
        public static PowerTwoLevelManager Instance;
        public PowerTwoLevelGenerator generator;
        [SerializeField] private PowerTwoLevelsHolder storedPowerTwoLevels;
        
        public Action OnSandwichDone;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            LoadLevels();
            LoadActualLevelValue();
        }
        
        private void LoadLevels()
        {
            if (Instance == null)
                Instance = this;
            
            if (storedPowerTwoLevels == null || storedPowerTwoLevels.levels.Count == 0)
                storedPowerTwoLevels = Resources.Load<PowerTwoLevelsHolder>("PowerTwo/PowerTwoLevelsHolder");
        }
        
        protected override void LoadActualLevelValue()
        {
            PlayerPrefs.DeleteAll();
            ActualLevel = PlayerPrefs.HasKey("actualPowerUpLevels") ? PlayerPrefs.GetInt("actualPowerUpLevels") : 1;
        }
        
        protected override void AddLevel()
        {
            ActualLevel++;
            PlayerPrefs.SetInt("actualPowerUpLevel", ActualLevel);
            PlayerPrefs.Save();
        }
        
        // Get data from created Levels, if it has predefined information, or if should generate a new level
        public PowerTwoLevel GetCurrentLevelData()
        {
            PowerTwoLevel currentPowerTwoLevel;
            if (ActualLevel - 1 >= storedPowerTwoLevels.levels.Count)
            {
                currentPowerTwoLevel = generator.CreateNewLevel(ActualLevel - 1);
                storedPowerTwoLevels.levels.Add(currentPowerTwoLevel);
                
#if UNITY_EDITOR
                SaveLevel(currentPowerTwoLevel, ActualLevel);
#endif
            }
            else
            {
                currentPowerTwoLevel = storedPowerTwoLevels.levels[ActualLevel - 1];
            }
            
            NodesAvailableOnLevel = currentPowerTwoLevel.autoGenerated ? currentPowerTwoLevel.piecesAmount : currentPowerTwoLevel.nodes.Count;

            return currentPowerTwoLevel;
        }
        
        public void DecreaseNodesAvailable()
        {
            NodesAvailableOnLevel--;
            if (NodesAvailableOnLevel == 1)
            {
                OnSandwichDone?.Invoke();
            }
        }
        
        private void SaveLevel(PowerTwoLevel powerTwoLevel, int actualLevel)
        {
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(powerTwoLevel, $"Assets/Resources/PowerTwo/Levels/{actualLevel}.asset");
            AssetDatabase.SaveAssets();
#endif
        }
        
    }
}
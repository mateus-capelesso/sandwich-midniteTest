using System;
using UnityEngine;
using UnityEditor;

namespace Levels.sandwich
{
    public class SandwichLevelManager : LevelManager
    {
        public static SandwichLevelManager Instance;
        public SandwichLevelGenerator generator;
        [SerializeField] private SandwichLevelsHolder storedSandwichLevelsHolder;
        
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
            
            if (storedSandwichLevelsHolder == null || storedSandwichLevelsHolder.levels.Count == 0)
                storedSandwichLevelsHolder = Resources.Load<SandwichLevelsHolder>("Sandwich/SandwichLevelsHolder");
        }
        
        protected override void LoadActualLevelValue()
        {
            // PlayerPrefs.DeleteAll();
            ActualLevel = PlayerPrefs.HasKey("actualSandwichLevels") ? PlayerPrefs.GetInt("actualSandwichLevels") : 1;
        }
        
        protected override void AddLevel()
        {
            ActualLevel++;
            PlayerPrefs.SetInt("actualSandwichLevels", ActualLevel);
            PlayerPrefs.Save();
        }
        
        // Get data from created Levels, if it has predefined information, or if should generate a new level
        public SandwichLevel GetCurrentLevelData()
        {
            SandwichLevel currentSandwichLevel;
            if (ActualLevel - 1 >= storedSandwichLevelsHolder.levels.Count)
            {
                currentSandwichLevel = generator.CreateNewLevel(ActualLevel - 1);
                storedSandwichLevelsHolder.levels.Add(currentSandwichLevel);
                
#if UNITY_EDITOR
                SaveLevel(currentSandwichLevel, ActualLevel);
#endif
            }
            else
            {
                currentSandwichLevel = storedSandwichLevelsHolder.levels[ActualLevel - 1];
            }
            
            NodesAvailableOnLevel = currentSandwichLevel.autoGenerated ? currentSandwichLevel.piecesAmount : currentSandwichLevel.nodes.Count;

            return currentSandwichLevel;
        }
        
        public void DecreaseNodesAvailable()
        {
            NodesAvailableOnLevel--;
            if (NodesAvailableOnLevel == 1)
            {
                OnSandwichDone?.Invoke();
            }
        }
        
        private void SaveLevel(SandwichLevel sandwichLevel, int actualLevel)
        {
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(sandwichLevel, $"Assets/Resources/Sandwich/Levels/{actualLevel}.asset");
            AssetDatabase.SaveAssets();
#endif
        }
        
    }
}
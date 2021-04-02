using System;
using UnityEngine;

namespace Levels
{
    public abstract class LevelManager : MonoBehaviour
    {
        
        protected int ActualLevel;
        protected int NodesAvailableOnLevel;

        
        public static Action<int> OnNewLevelStart;
        
        public static Action OnWin;
        public static Action OnLose;
        
        private void Start()
        {
            OnWin += AddLevel;
        }

        protected abstract void LoadActualLevelValue();

        protected abstract void AddLevel();

        public void CallNewLevel()
        {
            OnNewLevelStart?.Invoke(ActualLevel);
        }

        public void LevelEnded(bool win = false)
        {
            if(win)
                OnWin?.Invoke();
            else
                OnLose?.Invoke();
        }
        
        
    }
}
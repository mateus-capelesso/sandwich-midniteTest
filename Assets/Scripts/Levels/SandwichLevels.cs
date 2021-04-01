using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [Serializable]
    [CreateAssetMenu(fileName = "SandwichLevelsHolder", menuName = "Sandwich/Create Holder", order = 1)]
    public class SandwichLevels : ScriptableObject
    {
        public List<SandwichLevel> levels; 
    }
}
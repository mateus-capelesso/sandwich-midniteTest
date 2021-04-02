using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels.sandwich
{
    [Serializable]
    [CreateAssetMenu(fileName = "SandwichLevelsHolder", menuName = "Sandwich/Create Holder", order = 1)]
    public class SandwichLevelsHolder : ScriptableObject
    {
        public List<SandwichLevel> levels; 
    }
}
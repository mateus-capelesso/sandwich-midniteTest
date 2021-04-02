using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "PowerTwoLevelsHolder", menuName = "PowerTwo/Create Holder", order = 1)]
    public class PowerTwoLevelsHolder : ScriptableObject
    {
        public List<PowerTwoLevel> levels; 
    }
}
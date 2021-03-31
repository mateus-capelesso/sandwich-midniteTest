using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Levels Holder", menuName = "Create Levels Holder", order = 0)]
    public class Levels : ScriptableObject
    {
        public List<Level> levels; 
    }
}
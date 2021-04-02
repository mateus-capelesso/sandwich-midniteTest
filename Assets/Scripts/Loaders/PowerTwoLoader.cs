using Ingredients;
using PowerTwo;
using UnityEngine;

namespace Loaders
{
    public class PowerTwoLoader : ObjectLoader
    {
        private GameObject _empty;
        private GameObject _power2;
        private GameObject _power4;
        private GameObject _power8;
        private GameObject _power16;
        private GameObject _power32;
        private GameObject _power64;
        private GameObject _power128;
        private GameObject _power256;
        private GameObject _power512;
        private GameObject _power1024;
        private GameObject _power2048;

        private void Start()
        {
            LoadObjects();
        }

        private void LoadObjects()
        {
            _empty = Resources.Load<GameObject>("PowerTwo/Prefabs/Empty");
            _power2 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power2");
            _power4 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power4");
            _power8 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power8");
            _power16 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power16");
            _power32 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power32");
            _power64 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power64");
            _power128 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power128");
            _power256 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power256");
            _power512 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power512");
            _power1024 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power1024");
            _power2048 = Resources.Load<GameObject>("PowerTwo/Prefabs/Power2048");
        }

        public override GameObject GetObjectFromContent(int contentIndexType)
        {
            var content = (PowerTwoNodeType) contentIndexType;
            switch (content)
            {
                case PowerTwoNodeType.Empty:
                    return _empty;
                case PowerTwoNodeType.Power2:
                    return _power2;
                case PowerTwoNodeType.Power4:
                    return _power4;
                case PowerTwoNodeType.Power8:
                    return _power8;
                case PowerTwoNodeType.Power16:
                    return _power16;
                case PowerTwoNodeType.Power32:
                    return _power32;
                case PowerTwoNodeType.Power64:
                    return _power64;
                case PowerTwoNodeType.Power128:
                    return _power128;
                case PowerTwoNodeType.Power256:
                    return _power256;
                case PowerTwoNodeType.Power512:
                    return _power512;
                case PowerTwoNodeType.Power1024:
                    return _power1024;
                case PowerTwoNodeType.Power2048:
                    return _power2048;
                default:
                    Debug.LogError($"Couldn't find ingredient {content}");
                    return null;
            }
        }
    }
}
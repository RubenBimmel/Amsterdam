using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "SpringConfig", menuName = "Config/Spring", order = 0)]
    public class SpringConfig : ScriptableObject
    {
        public float stiffness;
        public float damping;
        public float bumpStop;
        public float restPosition;
        public float minLength;
        public float maxLength;
    }
}
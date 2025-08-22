using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static T GetComponentUpInHierarchy<T>(this Component component, bool includeInactive = false) where T : Component
        {
            while (true)
            {
                var parent = component.transform.parent;
                if (parent == null)
                {
                    return null;
                }

                var componentUpInHierarchy = parent.GetComponent<T>();
                if (componentUpInHierarchy != null) return componentUpInHierarchy;

                component = parent;
            }
        }
    }
}
using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class CameraExtension
    {
        public static RaycastHit? RaycastMouseToLayerMask(this Camera camera, LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                return hit;
            }
            else
            {
                return null;
            }
        }

        public static RaycastHit? RaycastMouseToCollider(this Camera camera, Collider collider)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (collider.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit;
            }
            else
            {
                return null;
            }
        }
    }
}

﻿using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class CameraExtension
    {
        public static RaycastHit? RaycastMouseToLayerMask(this Camera camera, LayerMask layerMask)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                return hit;
            }
            return null;
        }

        public static RaycastHit? RaycastMouseToCollider(this Camera camera, Collider collider)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                return hit;
            }
            return null;
        }


        /// <summary>
        /// Same as Camera.WorldToScreenPoint, except if the point is
        /// behind the camera then reflect it in the camera's forward
        /// plane first (with an epsilon to guarantee that the point
        /// is in front).
        /// </summary>
        public static Vector2 WorldToScreenPointReflected(this Camera camera, Vector3 pos)
        {
            var camTrans = camera.transform;
            var camPos = camTrans.position;
            var camForward = camTrans.forward;
            var posFromCam = pos - camPos;
            var camDot = Vector3.Dot(camForward, posFromCam);
            if (camDot <= 0)
            {
                var proj = camForward * camDot * 1.01f;
                pos = camPos + (posFromCam - proj);
            }

            return RectTransformUtility.WorldToScreenPoint(camera, pos);
        }
    }
}

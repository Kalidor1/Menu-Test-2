using UnityEngine;

namespace Extensions
{
    public static class CameraExtensions
    {
        public static Bounds OrthographicBounds(this Camera camera)
        {
            var height = camera.orthographicSize * 2;
            var width = height * camera.aspect;

            Bounds bounds = new Bounds(
                camera.transform.position,
                new Vector3(width, height, 0));

            return bounds;
        }
    }
}
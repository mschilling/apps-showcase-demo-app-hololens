using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

namespace HoloLens
{

    public class CursorManager
    {
        [Tooltip("Drag the Cursor object to show when it hits a hologram.")]
        public GameObject CursorOnHolograms;

        [Tooltip("Drag the Cursor object to show when it does not hit a hologram.")]
        public GameObject CursorOffHolograms;

        [Tooltip("Distance, in meters, to offset the cursor from the collision point.")]
        public float DistanceFromCollision = 0.01f;


        private void Awake()
        {
            // Hide the Cursors to begin with.
            if (CursorOnHolograms != null)
            {
                CursorOnHolograms.SetActive(false);
            }
            if (CursorOffHolograms != null)
            {
                CursorOffHolograms.SetActive(false);
            }

            

            if (GazeManager.Instance == null)
            {
                Debug.LogWarning("cursormanager requires a gazemanager in your scene.");
            }
        }

        private void LateUpdate()
        {
            if (CursorOnHolograms != null)
            {
                CursorOnHolograms.SetActive(GazeManager.Instance.HitObject);
            }
            if (CursorOffHolograms != null)
            {
                CursorOffHolograms.SetActive(!GazeManager.Instance.HitObject);
            }

            // Place the cursor at the calculated position.
            GazeManager.Instance.gameObject.transform.position = GazeManager.Instance.HitPosition + GazeManager.Instance.GazeNormal * DistanceFromCollision;

            // Orient the cursor to match the surface being gazed at.
            GazeManager.Instance.gameObject.transform.up = GazeManager.Instance.GazeNormal;
        }
    }
}


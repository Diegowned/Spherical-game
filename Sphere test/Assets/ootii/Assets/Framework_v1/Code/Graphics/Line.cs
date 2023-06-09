﻿using UnityEngine;
using com.ootii.Collections;

namespace com.ootii.Graphics
{
    /// <summary>
    /// Provides a way to store lines for our graphics rendering
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Defines where and when the line will display.
        /// 0 = scene view from editor
        /// 1 = scene view from game
        /// 2 = game view from editor
        /// 3 = game view from game
        /// </summary>
        public int Scope = 0;

        /// <summary>
        /// Transform that provides the reference for the line positions
        /// </summary>
        public Transform Transform = null;

        /// <summary>
        /// Starting point of the line
        /// </summary>
        public Vector3 Start = Vector3.zero;

        /// <summary>
        /// Ending point of the line
        /// </summary>
        public Vector3 End = Vector3.zero;

        /// <summary>
        /// Color of the line
        /// </summary>
        public Color Color = Color.white;

        /// <summary>
        /// GameTime that the line will expire.
        /// </summary>
        public float ExpirationTime = 0f;

        // ******************************** OBJECT POOL ********************************

        /// <summary>
        /// Allows us to reuse objects without having to reallocate them over and over
        /// </summary>
        private static ObjectPool<Line> sPool = new ObjectPool<Line>(20, 5);

        /// <summary>
        /// Returns the number of items allocated
        /// </summary>
        /// <value>The allocated.</value>
        public static int Length
        {
            get { return sPool.Length; }
        }

        /// <summary>
        /// Pulls an object from the pool.
        /// </summary>
        /// <returns></returns>
        public static Line Allocate()
        {
            // Grab the next available object
            Line lInstance = sPool.Allocate();

            // Return the allocated instance (should be cleaned before
            // being put back into the pool)
            return lInstance;
        }

        /// <summary>
        /// Pulls an object from the pool.
        /// </summary>
        /// <returns></returns>
        public static Line Allocate(Line rSource)
        {
            // Grab the next available object
            Line lInstance = sPool.Allocate();
            lInstance.Scope = rSource.Scope;
            lInstance.Color = rSource.Color;
            lInstance.Start = rSource.Start;
            lInstance.End = rSource.End;
            lInstance.ExpirationTime = rSource.ExpirationTime;
            lInstance.Transform = rSource.Transform;

            // Return the allocated instance (should be cleaned before
            // being put back into the pool)
            return lInstance;
        }

        /// <summary>
        /// Returns an element back to the pool.
        /// </summary>
        /// <param name="rEdge"></param>
        public static void Release(Line rInstance)
        {
            if (object.ReferenceEquals(rInstance, null)) { return; }

            // Set values
            rInstance.Scope = 0;
            rInstance.Transform = null;
            rInstance.Start = Vector3.zero;
            rInstance.End = Vector3.zero;
            rInstance.Color = Color.white;
            rInstance.ExpirationTime = 0f;

            // Make it available to others.
            sPool.Release(rInstance);
        }
    }
}

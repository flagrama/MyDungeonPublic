namespace MyDungeon.Utilities
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Extensions to the MonoBehaviour class
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Invoke using a delegate
        /// </summary>
        /// <param name="me"></param>
        /// <param name="delegateName">Name of a function to invoke</param>
        /// <param name="time">Time to wait before invoking the delegate</param>
        public static void Invoke(this MonoBehaviour me, Action delegateName, float time)
        {
            me.StartCoroutine(ExecuteAfterTime(delegateName, time));
        }

        private static IEnumerator ExecuteAfterTime(Action theDelegate, float delay)
        {
            yield return new WaitForSeconds(delay);
            theDelegate();
        }
    }
}

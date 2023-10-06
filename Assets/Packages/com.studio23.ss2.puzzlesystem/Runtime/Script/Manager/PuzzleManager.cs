using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Studio23.SS2.PuzzleSystem
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }else
            {
                Destroy(gameObject);
            }
        }
    }
}

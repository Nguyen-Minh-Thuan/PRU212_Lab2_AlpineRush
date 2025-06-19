using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class GameConfiguration : MonoBehaviour
    {

        public float _maxStageSpeed = 100f;
        public float _stageMutiplier = 2f;

        public float _stageTime = 60f; // Time in seconds for each stage
        public float _stageTimeMultiplier = 1.5f; // Multiplier for stage time increase

        public float _stageSpeedIncrease = 0.5f; // Speed increase per stage
        public float _stageSpeedMultiplier = 1.1f; // Multiplier for speed increase per stage

        public float _totalMupliers = 1f; // Total multipliers applied to the game

        private IEnumerator StageUpdate()
        {
            while (true) // Repeat the process indefinitely
            {
                float countdown = _stageTime;
                while (countdown > 0f)
                {
                    //Debug.Log($"Next stage in: {Mathf.CeilToInt(countdown)} seconds");
                    yield return new WaitForSeconds(1f);
                    countdown -= 1f;
                }

                // Stage update
                _totalMupliers *= _stageSpeedMultiplier; // Apply stage multiplier
                _maxStageSpeed *= _stageSpeedMultiplier;
                //Debug.Log($"Stage updated! New stage time: {_stageTime}, MaxStageSpeed: {_maxStageSpeed}");
            }
        }

        void Start()
        {

            StartCoroutine(StageUpdate()); // Start the countdown coroutine once
        }



    }
}

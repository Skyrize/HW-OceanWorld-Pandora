using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnSide : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float attackRange = 5;
    [Header("References")]
    [SerializeField] private AIVision vision;
    [SerializeField] private AIController controller;
    [SerializeField] private BoatController boatController;
    [SerializeField] private AIFireControl fireControl;

    public readonly float updateRate = 0.5f;
    private float updateTimeout = 0f;

    void updateStartegy()
    {
        if (!vision.lastKnownPlayerPos.HasValue) return;
        float distance = Vector3.Distance(vision.lastKnownPlayerPos.Value, transform.position);
        Vector3 estimatedPlayerPos = vision.estimatedPlayerPosIn(distance / boatController.maxSpeed / 2).Value;
        Vector3 relativePlayersPos = transform.InverseTransformPoint(estimatedPlayerPos);

        FireSide selectedSide = fireControl.availableFireSide[FireSide.RIGHT] ? FireSide.RIGHT : FireSide.LEFT;
        if (relativePlayersPos.x > 0 && fireControl.availableFireSide[FireSide.RIGHT])
        {
            selectedSide = FireSide.RIGHT;
        } else if (relativePlayersPos.x < 0 && fireControl.availableFireSide[FireSide.LEFT])
        {
            selectedSide = FireSide.LEFT;
        }
        

        Vector3 offset = Quaternion.AngleAxis(-90, Vector3.up) * vision.lastKnownPlayerForward.Value * 5;
        // if (selectedSide == FireSide.LEFT) offset *= -1;
        // Vector3 targetPos = transform.TransformPoint(relativePlayersPos) + offset;
        Vector3 targetPos = vision.lastKnownPlayerPos.Value;
        Vector3 targetRight = vision.lastKnownPlayerRight.Value;
        Vector3 targetForward = vision.lastKnownPlayerForward.Value;
        Vector3 rightPoint = Vector3.zero;
        Vector3 leftPoint = Vector3.zero;        
        
        float dot = Vector3.Dot(targetForward, transform.forward);
        if (dot >= 0.5f || dot <= -0.5f) { // Coming from front or back
            rightPoint = targetPos + targetRight * attackRange;
            leftPoint = targetPos - targetRight * attackRange;
        } else { // coming from left or right
            rightPoint = targetPos + targetForward * attackRange;
            leftPoint = targetPos - targetForward * attackRange;
        }


        Vector3 leftDir = leftPoint - transform.position;
        Vector3 rightDir = rightPoint - transform.position;

        //Debug
        // Vector3 pos = transform.position + Vector3.up * 5;
        // Debug.DrawLine(pos, pos + leftDir, Color.blue, updateRate);
        // Debug.DrawLine(pos, pos + rightDir, Color.red, updateRate);
        // Debug.DrawLine(pos, pos + transform.forward * 5, Color.yellow, updateRate);

        //TODO : si on arrive sur le côté -> viser avant ou arrière
        // Si on arrive sur l'avant ou l'arrière, viser droite ou gauche

        float dotLeft = Vector3.Dot(leftDir.normalized, transform.forward);
        float dotRight = Vector3.Dot(rightDir.normalized, transform.forward);

        if (dotLeft > dotRight) {
            Debug.DrawLine(rightPoint, rightPoint + Vector3.up * 100f, Color.magenta, updateRate);
            controller.setTarget(leftPoint);
        } else {
            Debug.DrawLine(leftPoint, leftPoint + Vector3.up * 100f, Color.magenta, updateRate);
            controller.setTarget(rightPoint);
        }
        // if (transform.InverseTransformPoint(targetPos).x > 0) {
        //     Debug.DrawLine(rightPoint, rightPoint + Vector3.up * 100f, Color.magenta, updateRate);
        //     Debug.DrawLine(leftPoint, leftPoint + Vector3.up * 100f, Color.cyan, updateRate);
        //     controller.setTarget(leftPoint);
        // } else {
        //     Debug.DrawLine(rightPoint, rightPoint + Vector3.up * 100f, Color.cyan, updateRate);
        //     Debug.DrawLine(leftPoint, leftPoint + Vector3.up * 100f, Color.magenta, updateRate);
        //     controller.setTarget(rightPoint);
        // }

    }

    void Update()
    {
        updateTimeout -= Time.deltaTime;
        if (updateTimeout < 0f)
        {
            updateStartegy();
            updateTimeout = updateRate;
        }
    }
}

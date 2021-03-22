using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FireSide
{
    LEFT,
    RIGHT,
    FRONT,
}

[System.Serializable]
public enum AttackType
{
    ORBIT_ATTACK,
    FRONTAL_ATTACK,
    SIDE_ATTACK
}

public class AttackOnSide : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected bool debug = true;
    [SerializeField] protected AttackType attackType = AttackType.SIDE_ATTACK;
    [SerializeField] protected float attackRange = 5;
    public bool FIRE_RIGHT = true;
    public bool FIRE_LEFT = true;

    [Header("References")]
    [SerializeField] private AIVision vision;
    [SerializeField] private AIController controller;
    [SerializeField] private BoatController boatController;
    [SerializeField] private AIFireControl fireControl;

    public readonly float updateRate = 0.5f;
    private float updateTimeout = 0f;

    void SetFrontAttack()
    {
        Vector3 targetPos = vision.lastKnownPlayerPos.Value;
        Vector3 aimDirection = (targetPos - transform.position).normalized;
        float dot = Vector3.Dot(-aimDirection, transform.forward);
        Vector3 aimPos;

        if (dot < 0.8f) {
            aimPos = targetPos + aimDirection * attackRange * 3;

        } else {
            aimPos = targetPos + aimDirection * attackRange;
        }
        if (debug) Debug.DrawLine(aimPos, aimPos + Vector3.up * 100f, Color.magenta, updateRate);
        controller.setTarget(aimPos);
    }

    void SetOrbitAttack()
    {
        Vector3 targetPos = vision.lastKnownPlayerPos.Value;
        Vector3 aimDirection = (transform.position - targetPos).normalized;
        Vector3 aimPos = targetPos + aimDirection * attackRange;

        if (debug) Debug.DrawLine(aimPos, aimPos + Vector3.up * 100f, Color.magenta, updateRate);
        controller.setTarget(aimPos);
    }

    void SetSideAttack()
    {
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

        float dotLeft = Vector3.Dot(leftDir.normalized, transform.forward);
        float dotRight = Vector3.Dot(rightDir.normalized, transform.forward);

        if (dotLeft > dotRight) {
            if (debug) Debug.DrawLine(rightPoint, rightPoint + Vector3.up * 100f, Color.magenta, updateRate);
            controller.setTarget(leftPoint);
        } else {
            if (debug) Debug.DrawLine(leftPoint, leftPoint + Vector3.up * 100f, Color.magenta, updateRate);
            controller.setTarget(rightPoint);
        }

    }

    void updateStartegy()
    {
        if (!vision.lastKnownPlayerPos.HasValue) return;
        float distance = Vector3.Distance(vision.lastKnownPlayerPos.Value, transform.position);
        Vector3 estimatedPlayerPos = vision.estimatedPlayerPosIn(distance / boatController.maxSpeed / 2).Value;
        Vector3 relativePlayersPos = transform.InverseTransformPoint(estimatedPlayerPos);

        FireSide selectedSide = FIRE_RIGHT ? FireSide.RIGHT : FireSide.LEFT;
        if (relativePlayersPos.x > 0 && FIRE_RIGHT)
        {
            selectedSide = FireSide.RIGHT;
        } else if (relativePlayersPos.x < 0 && FIRE_LEFT)
        {
            selectedSide = FireSide.LEFT;
        }
        
        switch (attackType)
        {
            case AttackType.FRONTAL_ATTACK:
            SetFrontAttack();
            break;
            case AttackType.ORBIT_ATTACK:
            SetOrbitAttack();
            break;
            case AttackType.SIDE_ATTACK:
            SetSideAttack();
            break;
        }
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

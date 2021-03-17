using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FireSide
{
    LEFT,
    RIGHT,
    FRONT,
}

public class AttackOnSide : MonoBehaviour
{
    [Header("Settings")]
    public bool FIRE_RIGHT = true;
    public bool FIRE_LEFT = true;

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

        FireSide selectedSide = FIRE_RIGHT ? FireSide.RIGHT : FireSide.LEFT;
        if (relativePlayersPos.x > 0 && FIRE_RIGHT)
        {
            selectedSide = FireSide.RIGHT;
        } else if (relativePlayersPos.x < 0 && FIRE_LEFT)
        {
            selectedSide = FireSide.LEFT;
        }

        Vector3 offset = Quaternion.AngleAxis(-90, Vector3.up) * vision.lastKnownPlayerForward.Value * 5;
        // if (selectedSide == FireSide.LEFT) offset *= -1;
        Vector3 targetPos = transform.TransformPoint(relativePlayersPos) + offset;

        controller.setTarget(targetPos);
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

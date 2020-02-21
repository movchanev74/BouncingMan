using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum BouncingManState { Run, Jumping, ReadyToJump }
public class BouncingMan : MonoBehaviour
{
    const string JUMP_PLACE_TAG = "JumpPlace";
    const string JUMP_TRIGGER = "IsFlip";

    public SettingsController SettingsController;
    public Transform JumpPlace;
    public ScoreContoller ScoreContoller;

    public float BouncingManRunSpeed = 1.0f;
    public float BouncingManJumpSpeed = 1.0f;

    private Animator animator;
    private BouncingManState currentState = BouncingManState.Run;
    private Vector3 startPosition;
    private Vector3 jumpPosition;

    public void JumpToPosition(Vector3 clickPosition)
    {
        jumpPosition = new Vector3(clickPosition.x, transform.position.y, transform.position.z);
        if (currentState == BouncingManState.ReadyToJump)
        {
            ScoreContoller.IncreaseScore();
            SettingsController.SetNextJumpSetting();
            currentState = BouncingManState.Jumping;
            float jumpDuration = Vector3.Distance(transform.position, jumpPosition) / BouncingManJumpSpeed;
            animator.SetTrigger(JUMP_TRIGGER);
            transform.DOJump(jumpPosition, SettingsController.CurrentJumpSetting.JumpHeight, 1, jumpDuration).AppendCallback(delegate
            {
                OnJumpEnded();
            });
        }
    }

    public void LoadSaveData(BouncingManSaveData savedata)
    {
        currentState = savedata.State;
        transform.position = savedata.Position;
    }

    public BouncingManSaveData GetSaveData()
    {
        var save = new BouncingManSaveData()
        {
            State = currentState
        };

        switch(currentState)
        {
            case BouncingManState.Jumping:
                save.Position = jumpPosition;
                save.State = BouncingManState.Run;
                break;
            case BouncingManState.ReadyToJump:
            case BouncingManState.Run:
                save.Position = transform.position;
                break;
        }
        return save;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == JUMP_PLACE_TAG)
        {
            currentState = BouncingManState.ReadyToJump;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == JUMP_PLACE_TAG && currentState != BouncingManState.Jumping)
        {
            Restart();
        }
    }

    private void Restart()
    {
        currentState = BouncingManState.Run;
        transform.position = startPosition;
    }

    private void OnJumpEnded()
    {
        currentState = BouncingManState.Run;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }


    private void Update()
    {
        if (currentState.Equals(BouncingManState.Run) || currentState.Equals(BouncingManState.ReadyToJump))
        {
            transform.position += Vector3.left * Time.deltaTime * BouncingManRunSpeed;
        }
    }
}
[Serializable]
public class BouncingManSaveData
{
    [SerializeField]
    public BouncingManState State;
    [SerializeField]
    public SerializableVector3 Position;
}

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}

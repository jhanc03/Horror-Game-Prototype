using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    struct RoomPositions
    {
        public int numPos;
        public List<Vector3> roomPositions;
        public List<float> roomRotations;
        public List<string> roomPoses;

        public RoomPositions(List<Vector3> roomPositions, List<float> roomRotations, List<string> roomPoses)
        {
            this.roomPositions = roomPositions;
            this.roomRotations = roomRotations;
            this.roomPoses = roomPoses;
            numPos = roomPositions.Count;
        }
    }
    RoomPositions lDoor, rDoor, cam1, cam2, cam3, cam4, cam5, cam6;

    const double MOThreshold = 4.07;
    double MOTimer;

    Transform monsterPos;
    Animator monsterPose;

    void Start()
    {
        MOTimer = 0.0f;

        //Room Positions

        lDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.644f, 0, -3.745f) }, new List<float> { 31.91f }, new List<string> { "ReachOut1" });
        rDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.816f, 0, 3.364f) }, new List<float> { 141.678f }, new List<string> { "ReachOut1" });
        cam1 = new RoomPositions(new List<Vector3> { new Vector3(-11.776f, 0, -3.019f), new Vector3(-4.54f, 0, -7.909f) }, new List<float> { 82.92f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam2 = new RoomPositions(new List<Vector3> { new Vector3(-11.54f, 0, 6.51f), new Vector3(-4.685f, 0, 1.684f) }, new List<float> { 99.039f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam3 = new RoomPositions(new List<Vector3> { new Vector3(-15.042f, 0, -8.036f), new Vector3(-15.103f, 0, 7.878f) }, new List<float> { 9.954f, 154.538f }, new List<string> { "Crouch", "Crouch" });
        cam4 = new RoomPositions(new List<Vector3> { new Vector3(-29.701f, 0, 16.708f), new Vector3(-20.894f, 0, 17.264f) }, new List<float> { 128.179f, -146.098f }, new List<string> { "Idle2", "Idle2" });
        cam5 = new RoomPositions(new List<Vector3> { new Vector3(-26.231f, 0, 10.193f), new Vector3(-23.314f, 0, -7.945f) }, new List<float> { 168.267f, 219.431f }, new List<string> { "Idle1", "ReachOut2" });
        cam6 = new RoomPositions(new List<Vector3> { new Vector3(-28.147f, 0, 18.667f), new Vector3(-30.024f, 0, 20.738f) }, new List<float> { 301.821f, 244.604f }, new List<string> { "LieDown", "LieDown" });

        GameObject monster = GameObject.FindGameObjectWithTag("Monster");
        monsterPos = monster.GetComponent<Transform>();
        monsterPose = monster.GetComponentInChildren<Animator>();

        RoomPositions idk = cam6;
        int idk2 = 1;

        monsterPos.position = idk.roomPositions[idk2];
        monsterPos.localEulerAngles = new Vector3(0, idk.roomRotations[idk2], 0);
        monsterPose.SetTrigger(idk.roomPoses[idk2]);

    }

    void Update()
    {
        MOTimer += Time.deltaTime;
        if (MOTimer > MOThreshold)
        {
            //Move monster

            MOTimer = 0.0f;
        }
    }
}
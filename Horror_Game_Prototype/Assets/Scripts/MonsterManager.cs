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
        //Positions:
        //Cam 6 = Vector3(-28.653,0,18.11), Vector3(-30.531,0,21.747)
        //Cam 5 = Vector3(-23.52,0,9.87), Vector3(-26.37,0,4.96), Vector3(-23.59,0,-6.74)
        //Cam 4 = Vector3(-30.15,0,16.2), Vector3(-23.552,0,12.069), Vector3(-20.453,0,17.226)
        //Cam 3 = Vector3(-13.77,0,-8.46), Vector3(-16.044,0,8.186)
        //Cam 2 = Vector3(-11.54,0,6.51), Vector3(-4.413,0,1.126)
        //Cam 1 = Vector3(-11.78,0,-3.18), Vector3(-5.505,0,-8.497)
        //LDoor = Vector3(0.156,0,-2.914)
        //RDoor = Vector3(0.156,0,2.635)

        lDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.644f, 0, -3.745f) }, new List<float> { 31.91f }, new List<string> { "ReachOut1" });
        rDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.816f, 0, 3.364f) }, new List<float> { 141.678f }, new List<string> { "ReachOut1" });
        cam1 = new RoomPositions(new List<Vector3> { new Vector3(-11.776f, 0, -3.019f), new Vector3(-4.54f, 0, -7.909f) }, new List<float> { 82.92f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam2 = new RoomPositions(new List<Vector3> { new Vector3(-11.54f, 0, 6.51f), new Vector3(-4.685f, 0, 1.684f) }, new List<float> { 99.039f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam3 = new RoomPositions(new List<Vector3> { new Vector3(-15.042f, 0, -8.036f), new Vector3(-16.044f, 0, 8.186f) }, new List<float> { 9.954f, 36.695f }, new List<string> { "Crouch", "Crouch" });
        /*cam4 = new RoomPositions(new List<Vector3> { new Vector3(-30.15f, 0, 16.2f), new Vector3(-23.552f, 0, 12.069f), new Vector3(-20.453f, 0, 17.226f) }, new List<string> { "asddw", "sdadw", "asdawdaw" });
        cam5 = new RoomPositions(new List<Vector3> { new Vector3(-23.52f, 0, 9.87f), new Vector3(-26.37f, 0, 4.96f), new Vector3(-23.59f, 0, -6.74f) }, new List<string> { "wedwad", "saduawdu", "sdhsefhgse" });
        cam6 = new RoomPositions(new List<Vector3> { new Vector3(-28.653f, 0, 18.11f), new Vector3(-30.531f, 0, 21.747f) }, new List<string> { "wefdesw", "sdjfe" });*/

        GameObject monster = GameObject.FindGameObjectWithTag("Monster");
        monsterPos = monster.GetComponent<Transform>();
        monsterPose = monster.GetComponentInChildren<Animator>();

        RoomPositions idk = cam3;
        int idk2 = 0;

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
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
    RoomPositions lDoor, rDoor, cam1, cam2, cam3, cam4, cam5, cam6, office;
    List<RoomPositions> roomPositions;
    int currentRoom, currentRoomPosition;
    bool choosePos;
    bool jumpscareReady;
    float breathTimer = 0.0f;

    const double MOThreshold = 8.04;//4.07;
    double MOTimer;
    int DC;

    GameController gameController;
    Transform monsterPos;
    Animator monsterPose;

    AudioSource audioSource, audioSource2;
    public AudioClip respawn, breathing1, breathing2, breathing3, hit1, hit2, jumpscare, snarl1, snarl2, snarl3, scream1, scream2, scream3;
    AudioClip clipToPlay = null;
    float moveSfx;
    int sfxChance = 10;
    float sfxTimer = 0.0f;

    void Start()
    {
        MOTimer = 0.0f;
        DC = 15;

        //Room Positions

        lDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.644f, 0, -3.745f) }, new List<float> { 31.91f }, new List<string> { "ReachOut1" });
        rDoor = new RoomPositions(new List<Vector3> { new Vector3(-0.816f, 0, 3.364f) }, new List<float> { 141.678f }, new List<string> { "ReachOut1" });
        cam1 = new RoomPositions(new List<Vector3> { new Vector3(-11.776f, 0, -3.019f), new Vector3(-4.54f, 0, -7.909f) }, new List<float> { 82.92f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam2 = new RoomPositions(new List<Vector3> { new Vector3(-11.54f, 0, 6.51f), new Vector3(-4.685f, 0, 1.684f) }, new List<float> { 99.039f, 36.695f }, new List<string> { "Idle2", "Idle2" });
        cam3 = new RoomPositions(new List<Vector3> { new Vector3(-15.042f, 0, -8.036f), new Vector3(-15.103f, 0, 7.878f) }, new List<float> { 9.954f, 154.538f }, new List<string> { "Crouch", "Crouch" });
        cam4 = new RoomPositions(new List<Vector3> { new Vector3(-29.701f, 0, 16.708f), new Vector3(-20.894f, 0, 17.264f) }, new List<float> { 128.179f, -146.098f }, new List<string> { "Idle2", "Idle2" });
        cam5 = new RoomPositions(new List<Vector3> { new Vector3(-26.231f, 0, 10.193f), new Vector3(-23.314f, 0, -7.945f) }, new List<float> { 168.267f, 219.431f }, new List<string> { "Idle1", "ReachOut2" });
        cam6 = new RoomPositions(new List<Vector3> { new Vector3(-28.147f, 0, 18.667f), new Vector3(-30.024f, 0, 20.738f) }, new List<float> { 301.821f, 244.604f }, new List<string> { "LieDown", "LieDown" });
        office = new RoomPositions(new List<Vector3> { new Vector3(0.0f, 0, 0.0f) }, new List<float> { 90.0f }, new List<string> { "Jumpscare" });

        roomPositions = new List<RoomPositions>();
        roomPositions.Add(lDoor);
        roomPositions.Add(rDoor);
        roomPositions.Add(cam1);
        roomPositions.Add(cam2);
        roomPositions.Add(cam3);
        roomPositions.Add(cam4);
        roomPositions.Add(cam5);
        roomPositions.Add(cam6);
        roomPositions.Add(office);

        gameController = GetComponent<GameController>();
        GameObject monster = GameObject.FindGameObjectWithTag("Monster");
        monsterPos = monster.GetComponent<Transform>();
        monsterPose = monster.GetComponentInChildren<Animator>();
        audioSource = monster.GetComponent<AudioSource>();
        audioSource2 = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        currentRoom = 7;
        currentRoomPosition = 1;
        UpdateMonsterPosition(currentRoom, currentRoomPosition);

        audioSource.volume = MainMenuScript.volumeLevel;
        audioSource2.volume = MainMenuScript.volumeLevel;
    }

    void Update()
    {
        MOTimer += Time.deltaTime;
        if (MOTimer > MOThreshold && !jumpscareReady && !gameController.won)
        {
            int randomNumber = Random.Range(0, 20);
            if (randomNumber < DC)
            {
                //Move monster
                switch (currentRoom)
                {
                    //LDoor
                    case 0:
                        //Attack
                        if (gameController.GetLDoorClosed())
                        {
                            //Door is closed, go back to start
                            currentRoom = 7;
                            moveSfx = 0.64f;
                            clipToPlay = respawn;
                        }
                        else
                        {
                            //Death >:)
                            jumpscareReady = true;
                            gameController.JumpscareReady();
                        }    
                        break;

                    //RDoor
                    case 1:
                        //Attack
                        if (gameController.GetRDoorClosed())
                        {
                            //Door is closed, go back to start
                            currentRoom = 7;
                            moveSfx = 0.64f;
                            clipToPlay = respawn;
                        }
                        else
                        {
                            //Death >:)
                            jumpscareReady = true;
                            gameController.JumpscareReady();
                        }
                        break;

                    //Cam1
                    case 2:
                        if (currentRoomPosition == 0)
                        {
                            currentRoomPosition = 1;
                        }
                        else
                        {
                            //Move to LDoor
                            currentRoom = 0;
                            moveSfx = 0.64f;
                        }
                        break;

                    //Cam2
                    case 3:
                        if (currentRoomPosition == 0)
                        {
                            currentRoomPosition = 1;
                        }
                        else
                        {
                            //Move to RDoor
                            currentRoom = 1;
                            moveSfx = 0.64f;
                        }
                        break;

                    //Cam3
                    case 4:
                        if (currentRoomPosition == 0)
                        {
                            //Left side - go to cam 1
                            currentRoom = 2;
                            currentRoomPosition = 1;
                            choosePos = false;
                            moveSfx = 0.6f;
                        }
                        else
                        {
                            //Right side - go to cam 2
                            currentRoom = 3;
                            currentRoomPosition = 1;
                            choosePos = false;
                            moveSfx = 0.6f;
                        }
                        break;

                    //Cam4
                    case 5:
                        if (Random.Range(0.0f, 2.0f) <= 1.0f)
                        {
                            //Go to cam 3
                            currentRoom = 4;
                            currentRoomPosition = 1;
                            choosePos = false;
                            moveSfx = 0.4f;
                        }
                        else
                        {
                            //Go to cam 5
                            currentRoom = 6;
                            moveSfx = 0.2f;
                        }
                        break;

                    //Cam5
                    case 6:
                        if (Random.Range(0.0f, 2.0f) <= 1.0f)
                        {
                            //Go to cam 3
                            currentRoom = 4;
                            currentRoomPosition = 0;
                            choosePos = false;
                            moveSfx = 0.4f;
                        }
                        else
                        {
                            //Go back to cam 4
                            currentRoom = 5;
                            moveSfx = 0.2f;
                        }
                        break;

                    //Cam6
                    case 7:
                        currentRoom = 5;
                        moveSfx = 0.2f;
                        break;
                }
                if (choosePos) currentRoomPosition = Random.Range(0, roomPositions[currentRoom].roomPositions.Count);
                UpdateMonsterPosition(currentRoom, currentRoomPosition);
                if (Random.Range(0.0f, 2.0f) <= 1.0f /*&& currentRoom != 0 && currentRoom != 1*/)
                {
                    //Play random move sfx
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            clipToPlay = snarl1;
                            break;

                        case 1:
                            clipToPlay = snarl2;
                            break;

                        case 2:
                            clipToPlay = snarl3;
                            break;
                    }
                    if (currentRoom == 7)
                    {
                        audioSource.PlayOneShot(respawn, moveSfx);
                    }
                    else
                    {
                        audioSource.PlayOneShot(clipToPlay, moveSfx);
                    }
                    clipToPlay = null;
                }
            }

            MOTimer = 0.0f;
            choosePos = true;
        }

        //Play random sfx
        sfxTimer += Time.deltaTime;
        if (sfxTimer >= 8.4f)
        {
            if (Random.Range(0.0f, 100.0f) <= sfxChance)
            {
                switch (Random.Range(0, 5))
                {
                    case 0:
                        clipToPlay = hit1;
                        break;

                    case 1:
                        clipToPlay = hit2;
                        break;

                    case 2:
                        clipToPlay = scream1;
                        break;

                    case 3:
                        clipToPlay = scream2;
                        break;

                    case 4:
                        clipToPlay = scream3;
                        break;
                }
                audioSource2.PlayOneShot(clipToPlay, 0.14f);
                clipToPlay = null;
            }
            sfxTimer = 0.0f;
        }
    }

    private void UpdateMonsterPosition(int room, int roomPos)
    {
        monsterPos.position = roomPositions[room].roomPositions[roomPos];
        monsterPos.localEulerAngles = new Vector3(0, roomPositions[room].roomRotations[roomPos], 0);
        monsterPose.SetTrigger(roomPositions[room].roomPoses[roomPos]);
    }

    public void Breathing()
    {
        //Play monster breathing
        breathTimer += Time.deltaTime;
        if (!audioSource.isPlaying && breathTimer >= 6.4f)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    audioSource.PlayOneShot(breathing1, 0.4f);
                    break;

                case 1:
                    audioSource.PlayOneShot(breathing2, 0.4f);
                    break;

                case 2:
                    audioSource.PlayOneShot(breathing3, 0.4f);
                    break;
            }
            breathTimer = 0.0f;
        }
    }

    public void MonsterJumpscare()
    {
        audioSource.Stop();
        audioSource.loop = false;
        if (!MainMenuScript.jumpscare)
        {
            UpdateMonsterPosition(8, 0);
            audioSource.PlayOneShot(jumpscare, 0.4f);
        }
        else
        {
            monsterPos.transform.position = new Vector3(0.0f, -5.0f, 0.0f);
            gameController.winLossText.text = "Boo!";
        }
    }

    public void MuteSources()
    {
        audioSource.volume = 0;
        audioSource2.volume = 0;
    }
}
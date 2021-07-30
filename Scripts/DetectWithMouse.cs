using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.SceneManagement;


public class DetectWithMouse : MonoBehaviour
{

    public Camera fpsCam;
    public float range;
    public Button backButton;
    public Button PuzzleBackButton;
    public Image aim;
    public LayerMask layerMask;
    [Header("Backpack Slots")]
    public GameObject backpackButton;
        public GameObject backpackContent;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public UITriggerEvent UITE_placeHolder;
    public bool puzzleSeal = false;
    public bool PuzzleOnenThree = false;
    public GameObject monster;
    public GameObject spot;
    public GameObject objectGate;
    public LoadPlayer SLsystem;
    public Vector3 playerPosition;
    public bool loadFile = false;
    public StartMenuControl loadControl;
    public GameObject youWin;
    public KillPlayer loadLevelAfterEverything;
    GameStateController gameController;


    bool ZSeal = false;
    private InteractiveObject interactive;
    private void Start()
    {
        loadLevelAfterEverything = GetComponent<KillPlayer>();
        backButton.onClick.AddListener( UITE_placeHolder.TurnOffPanel);
        gameController = this.gameObject.GetComponent<GameStateController>();
        loadControl.LoadData();
        string path = Application.dataPath + "/StreamingAssets/save/player.cxy";
        if (File.Exists(path)) 
        {

            if (loadControl.loadInt == 1)
            {
                SLsystem.Load();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Detect();
        }

        // interactives without puzzle
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward.normalized, out hit, range);
            
            if (hit.transform)
            {
                interactive = hit.transform.GetComponent<InteractiveObject>();

                if (interactive)
                {
                    if (hit.transform.gameObject.tag == "posInteractive")
                    {
                        interactive.PositionAction();
                        Debug.Log("OK pos");
                    }
                    else if (hit.transform.gameObject.tag == "rotInteractive")
                    {
                        interactive.RotationAction();
                        Debug.Log("OK rot");
                    }
                    else if (hit.transform.gameObject.tag == "collectInteractive")
                    {
                        interactive.Collect();
                    }


                }



            }
        }

       /* if (Input.GetKeyDown(KeyCode.B))
        {
            BackPackSwitch();
        }*/

       if (ZSeal)
        {

            if (Input.GetKey(KeyCode.Z))
            {
                gameController.MoveToNextEvent();
                FinalPuzzleSolve();
                Debug.Log("FinalPuzzle");

            }
        }



       

    }

    

    void Detect()
    {


        RaycastHit hit;



        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward.normalized, out hit, range, layerMask))////////////////////////////////////////////////////////
        {

            if (hit.transform.gameObject.tag == "blockPuzzle")
            {
                PuzzleCamSwitch BlockPuzzleSwitch = hit.transform.gameObject.GetComponent<PuzzleCamSwitch>();
                BlockPuzzleSwitch.GoToPuzzle();
                aim.gameObject.SetActive(false);
                PuzzleBackButton.gameObject.SetActive(true);

                if (BlockPuzzleSwitch.gameObject.GetComponent<Puzzle>().state == Puzzle.PuzzleState.Solved && object3.activeSelf)
                {
                    gameController.StartEvent(40);// the mark on the two maps? They are the same?
                    PuzzleOnenThree = true;
                    TriggerEvent triggerPanel = hit.collider.gameObject.GetComponentInChildren<TriggerEvent>();
                    UITriggerEvent UIPanel = hit.collider.gameObject.GetComponentInChildren<UITriggerEvent>();
                    if (triggerPanel != null)
                    {
                        triggerPanel.CallStory();
                    }
                    if (UIPanel != null)
                    {
                        UIPanel.TurnOnPanel();
                        backButton.gameObject.SetActive(true);
                        aim.gameObject.SetActive(false);
                    }
                }
            }


            if (hit.transform.gameObject.tag == "altar")
            {
                monster.SetActive(false);
                TriggerEvent triggerPanel = hit.collider.gameObject.GetComponent<TriggerEvent>();
                triggerPanel.initialEventID = 46;

                triggerPanel.CallStory();
                loadLevelAfterEverything.EndScene();
                youWin.SetActive(true);
            }




            if (hit.transform.gameObject.tag == "finalPuzzle")
            {
                TriggerEvent triggerPanel = hit.collider.gameObject.GetComponent<TriggerEvent>();

                if (PuzzleOnenThree)
                {


                    if (triggerPanel != null)
                    {
                        if (puzzleSeal)// ready to step onto the spot in living room
                        {
                            triggerPanel.initialEventID = 47; // "The painting on the wall and the star chart! The seal must have something to do with this."
                            triggerPanel.CallStory();
                            ZSeal = true;

                        }
                        else// not all puzzle solved
                        {
                            triggerPanel.initialEventID = 45; // "it seem i have to solve the puzzles in the room before any of these things become clear."
                            triggerPanel.CallStory();
                        }

                    }
                }
                else// not all puzzle solved
                {
                    triggerPanel.initialEventID = 45; // "it seem i have to solve the puzzles in the room before any of these things become clear."
                    triggerPanel.CallStory();
                }
            }


            TriggerEvent triggerReceiver = hit.collider.gameObject.GetComponent<TriggerEvent>();
            UITriggerEvent UIReceiver = hit.collider.gameObject.GetComponent<UITriggerEvent>();

            if (triggerReceiver || UIReceiver != null)
            {
                gameController.Pause();


                // half screen dialogue on
                if (triggerReceiver != null)
                {
                    triggerReceiver.CallStory();

                }


                //full screen UI panel on
                if (UIReceiver != null)
                {

                    UIReceiver.TurnOnPanel();
                    backButton.gameObject.SetActive(true);
                    aim.gameObject.SetActive(false);

                }

                



            }
        }
            
    }




 /*   public void BackPackSwitch()
    {
        backPackOpen = !backPackOpen;
        if (backPackOpen)
        {
            backpackButton.SetActive(true);
            backpackContent.SetActive(true);
            Debug.Log("open");
        }
        else
        {
            backpackButton.SetActive(false);
            backpackContent.SetActive(false);
            Debug.Log("close");

        }
    }*/

 



    public void FinalPuzzleSolve()
    {
        playerPosition = fpsCam.GetComponentInParent<Transform>().position;
        object2.SetActive(false);
        ZSeal = false;
        //move the player to where the seal is at
        /* Vector3 direction = spot.transform.position - fpsCam.gameObject.transform.position;
         direction = direction.normalized;
         fpsCam.transform.Translate(direction * Time.deltaTime);*/
        TriggerEvent ZEvent = GetComponent<TriggerEvent>();

        ZEvent.initialEventID = 41; //"If all my guesses are right - if I was the one who had to do this thing"
        ZEvent.CallStory();
       /* time += Time.deltaTime;
        if (time >= readingTime)
        {
            ZEvent.initialEventID = 13; //"I could die."
            ZEvent.CallStory();
        }*/
        objectGate.SetActive(false);
        monster.SetActive(true);
        //open the gate

        SLsystem.SavePlayer();

    }





}

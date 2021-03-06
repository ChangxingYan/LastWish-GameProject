using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

    public Texture2D image;
    public int blocksPerLine = 4;
    public int shuffleLength = 20;
    public float defaultMoveDuration = .2f;
    public float shuffleMoveDuration = .1f;
    public Camera cam;
    public AudioSource audioControl;
    public DetectWithMouse backPackSwitch;
    public enum PuzzleState { Solved, Shuffling, InPlay };
    public PuzzleState state;

    Block emptyBlock;
    Block[,] blocks;
    Queue<Block> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int prevShuffleOffset;
    UITriggerEvent UITriggerEvent;

    void OnLevelWasLoaded()
    {
        if(this.GetComponentsInChildren<Transform>().Length <=1)
        {
            CreatePuzzle();
            StartShuffle();
        }

    }
    void Start()
    {
        if (this.GetComponentsInChildren<Transform>().Length <= 2)
        {
            CreatePuzzle();
            StartShuffle();
        }

    }
    void Update()
    {

    }

    void CreatePuzzle()
    {

        blocks = new Block[blocksPerLine, blocksPerLine];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;

               block.transform.position += new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }


   cam.orthographicSize = blocksPerLine * .55f;

        inputs = new Queue<Block>();
    }

    void PlayerMoveBlockInput(Block blockToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MakeNextPlayerMove()
    {
		while (inputs.Count > 0 && !blockIsMoving)
		{
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
		}
    }

    void MoveBlock(Block blockToMove, float duration)
    {
		if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
		{
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

			Vector2Int targetCoord = emptyBlock.coord;
			emptyBlock.coord = blockToMove.coord;
			blockToMove.coord = targetCoord;

			Vector2 targetPosition = emptyBlock.transform.position;
			emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
		}
    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                MakeNextShuffleMove();
            }
            else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
            if (offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining--;
                    prevShuffleOffset = offset;
                    break;
                }
            }
        }
      
    }

    void CheckIfSolved()
    {
        foreach (Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }

        state = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
        audioControl.Play();        // bzackPackSwitch.BackPackSwitch();
        backPackSwitch.object1.SetActive(true);

        if (backPackSwitch.object3.activeSelf)
        {
            backPackSwitch.gameObject.GetComponent<GameStateController>().StartEvent(40);

        backPackSwitch.PuzzleOnenThree = true;
        backPackSwitch.backButton.gameObject.SetActive(true);
        UITriggerEvent = this.GetComponentInChildren<UITriggerEvent>();
        UITriggerEvent.TurnOnPanel();
            audioControl.Play();

        }
    }
}

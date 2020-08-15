using UnityEngine;
using DG.Tweening;

public class ParkourMechanics : MonoBehaviour
{
    [Header("Wall_Platform")]
    [SerializeField] private GameObject wallPlatform;
    [SerializeField] private float wallPlatform_EndPosition = 0f;
    [SerializeField] private float wallPlatform_Duration = 0f;


    [Header("Block_Platform")]
    [SerializeField] private GameObject blockPlatform;
    [SerializeField] private float blockPlatform_EndPosition = 0f;
    [SerializeField] private float blockPlatform_Duration = 0f;

    [Header("Tower_Movement")]
    [SerializeField] private GameObject towerLeft;
    [SerializeField] private GameObject towerRight;
    [SerializeField] private float tower_StartPosition = 0f;
    [SerializeField] private float tower_EndPosition = 0f;
    [SerializeField] private float tower_Duration = 0f;

    [Header("Piston_Movement")]
    [SerializeField] private GameObject topPiston;
    [SerializeField] private GameObject midPiston;
    [SerializeField] private GameObject endPiston;
    [SerializeField] private float piston_EndPosition = 0f;
    [SerializeField] private float piston_Duration = 0f;

    [Header("Sky_Blocks")]
    [SerializeField] private GameObject leftBlock;
    [SerializeField] private GameObject rightBlock;
    [SerializeField] private float leftBlock_EndPosition = 0f;
    [SerializeField] private float rightBlock_EndPosition = 0f;
    [SerializeField] private float block_Duration = 0f;

    private Sequence wallPlatform_Sequence;

    private Sequence blockPlatform_Sequence;

    private Sequence leftTower_Sequence;
    private Sequence rightTower_Sequence;

    private Sequence topPiston_Sequence;
    private Sequence midPiston_Sequence;
    private Sequence endPiston_Sequence;

    private Sequence leftBlock_Sequence;
    private Sequence rightBlock_Sequence;

    private void Start()
    {
        DOTween.Init();

        PlatformInit();
        SlideInit();
        PistonInit();
        SkyBlocksInit();
    }
    private void PlatformInit()
    {
        wallPlatform_Sequence = DOTween.Sequence();
        blockPlatform_Sequence = DOTween.Sequence();

        PlatformRiseAndFall(wallPlatform_Sequence,
                            wallPlatform.transform,
                            wallPlatform.transform.localPosition.y,
                            wallPlatform_EndPosition,
                            wallPlatform_Duration);

        PlatformRiseAndFall(blockPlatform_Sequence,
                            blockPlatform.transform,
                            blockPlatform.transform.localPosition.y,
                            blockPlatform_EndPosition,
                            blockPlatform_Duration);
    }
    private void SlideInit()
    {
        leftTower_Sequence = DOTween.Sequence();
        rightTower_Sequence = DOTween.Sequence();



        LeftRightMove(leftTower_Sequence,
                            towerLeft.transform,
                            tower_StartPosition,
                            tower_EndPosition,
                            tower_Duration);

        LeftRightMove(rightTower_Sequence,
                            towerRight.transform,
                            tower_EndPosition,
                            tower_StartPosition,
                            tower_Duration);
    }
    private void PistonInit()
    {
        topPiston_Sequence = DOTween.Sequence(); ;
        midPiston_Sequence = DOTween.Sequence(); ;
        endPiston_Sequence = DOTween.Sequence();

        PlatformRiseAndFall(topPiston_Sequence,
                       topPiston.transform,
                       topPiston.transform.localPosition.y,
                       endPiston.transform.localPosition.y,
                       piston_Duration);

        PlatformRiseAndFall(midPiston_Sequence,
                       midPiston.transform,
                       midPiston.transform.localPosition.y,
                       piston_EndPosition,
                       .5f);

        PlatformRiseAndFall(endPiston_Sequence,
                       endPiston.transform,
                       endPiston.transform.localPosition.y,
                       piston_EndPosition,
                       piston_Duration);
    }
    private void SkyBlocksInit()
    {
        leftBlock_Sequence = DOTween.Sequence();
        rightBlock_Sequence = DOTween.Sequence();

        SkyBlockMovement(leftBlock_Sequence,
                       leftBlock.transform,
                       leftBlock.transform.localPosition.x,
                       leftBlock_EndPosition,
                       block_Duration);

        SkyBlockMovement(rightBlock_Sequence,
                       rightBlock.transform,
                       rightBlock.transform.localPosition.x,
                       rightBlock_EndPosition,
                       block_Duration);
    }

    private void PlatformRiseAndFall(Sequence sequence, Transform platform, float startPos, float endPos, float time)
    {
        sequence.Append(platform.DOLocalMoveY(endPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).// can turn into variable
                 Append(platform.DOLocalMoveY(startPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).
                 SetLoops(-1);
    }

    private void LeftRightMove(Sequence sequence, Transform platform, float startPos, float endPos, float time)
    {
        sequence.Append(platform.DOLocalMoveZ(endPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).// can turn into variable
                 Append(platform.DOLocalMoveZ(startPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).
                 SetLoops(-1);
    }

    private void SkyBlockMovement(Sequence sequence, Transform platform, float startPos, float endPos, float time)
    {
        sequence.Append(platform.DOLocalMoveX(endPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).// can turn into variable
                 Append(platform.DOLocalMoveX(startPos, time).
                 SetEase(Ease.InOutSine).
                 SetDelay(.5f)).
                 SetLoops(-1);
    }
}

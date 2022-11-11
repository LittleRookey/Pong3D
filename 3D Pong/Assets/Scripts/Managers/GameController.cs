using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } 

    [SerializeField] private float matChangingTime = 0.2f; // time for material change 
    [SerializeField] private float incrementBallSpeed = 1f; // amount of speed to increment on score
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform ballStartPos;
    [SerializeField] private Transform GameExitCanvas;

    // References
    PongBallMovement ballMovement; 
    Rigidbody ballRB;
    WaitForSeconds waitSeconds;

    // values to be used
    private string smoothnessInput = "_Glossiness";
    float originVal = 0.5f;
    float finalVal = 1f;

    
    [field:SerializeField ] public int Score { get; private set; } // score of the game
    private string scoreTemplate;

    private GameObject player;

    public UnityAction OnGameRestart;
    public UnityAction OnGameEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        ballMovement = GameObject.FindGameObjectWithTag("Ball").GetComponent<PongBallMovement>();
        ballRB = ballMovement.GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");

        waitSeconds = new WaitForSeconds(matChangingTime / 2);

        Score = 0;
        scoreTemplate = "Score: ";
        scoreText.SetText(scoreTemplate + Score.ToString());
    }

    private void OnEnable()
    {
        ballMovement.OnBallCollisionEnter += ChangeMaterialPropOnCollision;
        ballMovement.OnBallCollisionEnter += OnBallScoreCollision;
        ballMovement.OnBallCollisionEnter += OnBallGameEndCollision;
        ballMovement.OnBallCollisionExit += SetMaterialOnCollisionExit;
    }

    private void OnDisable()
    {
        ballMovement.OnBallCollisionEnter -= ChangeMaterialPropOnCollision;
        ballMovement.OnBallCollisionEnter -= OnBallScoreCollision;
        ballMovement.OnBallCollisionEnter -= OnBallGameEndCollision;
        ballMovement.OnBallCollisionExit -= SetMaterialOnCollisionExit;
    }

    private void Start()
    {
        AbilityHolder.GetAbilityHolderOfType(player, eAbilityType.Restart).isLocked = true;
        ballRB.isKinematic = true;
        GameExitCanvas.gameObject.SetActive(false);
    }

    // Opens the Exit Game Window
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameExitCanvas.gameObject.activeInHierarchy)
            {
                GameExitCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            } else
            {
                GameExitCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    /// <summary>
    /// Handles Ball Score Event
    /// </summary>
    /// <param name="collision">Object ball collides with</param>
    private void OnBallScoreCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            Score += 1;
            scoreText.SetText(scoreTemplate + Score.ToString());
            ballMovement.IncrementSpeed(incrementBallSpeed);
        }
    }

    /// <summary>
    /// Handles GameEnd Event 
    /// </summary>
    /// <param name="collision">Object ball collides with</param>
    private void OnBallGameEndCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Front"))
        {
            ballRB.isKinematic = true;
            // show red eye
            // show Restart Text
            OnGameEnd?.Invoke();
        }
    }

    /// <summary>
    /// Restarts the Game by reset of scores and ball position
    /// </summary>
    public void Restart()
    {
        OnGameRestart?.Invoke();
        // reset score
        Score = 0;
        scoreText.SetText(scoreTemplate + Score.ToString());
        // reset ball to static 
        ballMovement.transform.position = ballStartPos.position;
    }

    /// <summary>
    /// Handles Ball Collision Enter Events On Wall, changing the material value
    /// </summary>
    /// <param name="collision">Object ball collides with</param>
    private void ChangeMaterialPropOnCollision(Collision collision)
    {
        MeshRenderer rend;
        if (collision.gameObject.TryGetComponent<MeshRenderer>(out rend)) {
            rend.material.SetFloat(smoothnessInput, finalVal);
        }

    }

    /// <summary>
    /// Handles Ball Collision Exit Events On Wall, changing the material value
    /// </summary>
    /// <param name="collision">Object ball collides with</param>
    private void SetMaterialOnCollisionExit(Collision collision)
    {
        MeshRenderer rend;
        if (collision.gameObject.TryGetComponent<MeshRenderer>(out rend))
        {
            StartCoroutine(StartChangeSmoothness(rend));
        }
    }

    /// <summary>
    /// Coroutine to run smoothness back to normal
    /// </summary>
    private IEnumerator StartChangeSmoothness(Renderer rend)
    {
        yield return waitSeconds;
        rend.material.SetFloat(smoothnessInput, originVal);
    }

    // Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}

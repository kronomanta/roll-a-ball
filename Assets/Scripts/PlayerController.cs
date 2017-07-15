using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float SpeedAccelerometer = 10;

    public int ScoreCountToWin;
    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Text WinText;

    private Rigidbody _rigid;
    private int score = 0;


    #region acceletormeter

    //accelerometer
    private Vector3 zeroAccelerationOffset;

    #endregion

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        UpdateScoreText();
        WinText.enabled = false;

        // Preventing mobile devices going in to sleep mode 
        //(actual problem if only accelerometer input is used)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ResetAxes();
    }

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Exit condition for Desktop devices
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        else
        {
            // Exit condition for mobile devices
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }


    private void FixedUpdate()
    {
        Vector3 movement;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            float moveHorizintal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(moveHorizintal, 0, moveVertical) * Speed;
        }
        else
        {
            // Player movement in mobile devices

            //get input by accelerometer
            Vector3 currentAcceleration = Input.acceleration - zeroAccelerationOffset;
            movement = new Vector3(currentAcceleration.x, 0.0f, currentAcceleration.y) * SpeedAccelerometer;

            //movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y) * SpeedAccelerometer;
        }

        //no need to multiple by Time.deltaTime
        _rigid.AddForce(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score;
        if (score >= ScoreCountToWin)
        {
            WinText.enabled = true;
        }
    }

    /// <summary>
    /// Reset accelerometer
    /// </summary>
    private void ResetAxes()
    {
        zeroAccelerationOffset = Input.acceleration;
    }

}

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject branchPrefab;
    public GameObject raindropPrefab;
    public AudioSource rainAudioSource;
    public GameObject player;
    private Camera cam;

    float cameraWidth;
    float cameraHeight;

    public float spawnYOffset = 2.0f; 

    // ========== BRANCH SETTINGS ==========
    public float minBranchTime = 3f;
    public float maxBranchTime = 5f;
    private float branchTimer;

    // ========== RAIN SETTINGS ==========
    public float rainStartDepth = 0.0f;
    
    public float minRainDuration = 5.0f;
    public float maxRainDuration = 12.0f;
    
    public float minDryDuration = 15.0f;
    public float maxDryDuration = 30.0f;

    public float minRainInterval = 2f;
    public float maxRainInterval = 5f;

    private float currentPhaseDuration;
    private float rainCycleTimer;
    private float rainDropTimer;
    private bool isRaining = false;

    // ========== DIFFICULTY SETTINGS ==========
    private float startY;
    private float currentDepth;

    void Start()
    {
        branchTimer = Random.Range(minBranchTime, maxBranchTime);
        
        currentPhaseDuration = Random.Range(minDryDuration, maxDryDuration);

        if(player != null) startY = player.transform.position.y;
        
        cam = GetComponentInParent<Camera>();
        if(cam != null)
        {
            cameraHeight = cam.orthographicSize;
            cameraWidth = cameraHeight * cam.aspect;
        } else
        {
            Debug.LogWarning("Camera not found in parent hierarchy.");
        }
    }

    void Update()
    {
        if(player == null)
        {
            // Stop rain sound if player is missing
            if(rainAudioSource != null && rainAudioSource.isPlaying)
            {
                rainAudioSource.Stop();
            }
            return;
        }

        currentDepth = startY - player.transform.position.y;

        HandleBranches();
        HandleRain();
    }

    void HandleBranches()
    {
        branchTimer -= Time.deltaTime;

        if(branchTimer <= 0)
        {
            SpawnBranch();

            float spacingBuffer = currentDepth * 0.0005f;
            
            float currentMin = minBranchTime + spacingBuffer;
            float currentMax = maxBranchTime + spacingBuffer;

            branchTimer = Random.Range(currentMin, currentMax);
        }
    }

    void HandleRain()
    {
        if (currentDepth < rainStartDepth) return;

        rainCycleTimer += Time.deltaTime;

        if (rainCycleTimer > currentPhaseDuration)
        {
            ToggleRainState();
        }

        if(isRaining)
        {
            rainDropTimer -= Time.deltaTime;
            if(rainDropTimer <= 0)
            {
                SpawnRaindrop();
                rainDropTimer = Random.Range(minRainInterval, maxRainInterval);
            }
        }
    }

    void ToggleRainState()
    {
        isRaining = !isRaining;
        rainCycleTimer = 0;

        if (isRaining)
        {
            // Start Raining
            currentPhaseDuration = Random.Range(minRainDuration, maxRainDuration);
            if(rainAudioSource != null) rainAudioSource.Play();
        }
        else
        {
            // Stop Raining
            currentPhaseDuration = Random.Range(minDryDuration, maxDryDuration);
            if(rainAudioSource != null) rainAudioSource.Stop();
        }
    }

    void SpawnBranch()
    {
        bool spawnLeft = Random.value > 0.5f;

        float spawnX;
        float spawnY = player.transform.position.y - cameraHeight - spawnYOffset;
        Quaternion rotation;

        if(spawnLeft)
        {
            spawnX = -cameraWidth;
            rotation = Quaternion.identity;
        } else {
            spawnX = cameraWidth;
            rotation = Quaternion.Euler(0, 180, 0);
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        Instantiate(branchPrefab, spawnPosition, rotation);
    }

    void SpawnRaindrop()
    {
        float spawnX = Random.Range(-cameraWidth, cameraWidth);
        float spawnY = player.transform.position.y + cameraHeight + spawnYOffset;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject rainDrop = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity);

        float randomScale = Random.Range(0.5f, 1.25f);
        rainDrop.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        rainDrop.transform.SetParent(cam.transform, true);
    }
}
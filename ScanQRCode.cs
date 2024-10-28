using UnityEngine;
using UnityEngine.UI;
using ZXing;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class QRCodeScanner : MonoBehaviour
{
    public RawImage cameraDisplay;
    public Button okButton;
    private WebCamTexture webCamTexture;
    private IBarcodeReader barcodeReader;
    private Animator okButtonAnimator;
    public Animator qrDetectedAnimator;

    private Dictionary<string, string> qrCodeToSceneMap = new Dictionary<string, string>
    {
        { "RedPanda", "05_GetSprite" },
        { "PyroMonkey", "05_GetSprite" },
        { "Taiyaki", "05_GetSprite" },
        { "UFOCat", "05_GetSprite" },
        { "HydroPhantasm", "05_GetSprite" },
        { "PurpleFighter", "05_GetSprite" },
        { "PlusPlayer", "05_GetSprite" },
        { "Red", "09_ScanRed" },
        { "Green", "10_ScanGreen" },
        { "Blue", "11_ScanBlue" },
        { "NightmareBeGone", "13_NightmareBeGone" },
        { "Squargles2018", "14_Squargles2018" },
        { "SevenSeeds", "16_SevenSeeds" },
        { "Axil", "17_Axil" },
        { "MarketLane", "18_MarketLane" },
        { "SplitGirl", "21A_FightSplitGirl" },
        { "SwapKitty", "21B_FightSwapKitty" },
        { "SmokeFace", "21C_FightSmokeFace" },
        { "GraffitiGorilla", "21D_FightGraffitiGorilla" },
        { "RedAttack", "23_SpriteAttack" },
        { "GreenAttack", "23_SpriteAttack" },
        { "BlueAttack", "23_SpriteAttack" },
    };

    private string targetScene;
    private bool qrDetected = false;
    private bool scanRedPandaMode = false;
    private bool scanPyroMonkeyMode = false;
    private bool scanTaiyakiMode = false;
    private bool scanUFOCatMode = false;
    private bool scanHydroPhantasmMode = false;
    private bool scanPurpleFighterMode = false;
    private bool scanPlusPlayerMode = false;
    private bool scanPrimaryColorsMode = false;
    private bool scanAttackColorsMode = false;
    private bool scanActivePlay01Mode = false;
    private bool scanActivePlay02Mode = false;
    private bool scanCoffeeMode = false;
    private bool scanBossMode = false;
    private bool scanSmokeFaceMode = false;
    private bool scanGraffitiGorillaMode = false;

    void Start()
    {
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        #endif

        StartCamera();
        barcodeReader = new BarcodeReader();
        
        okButtonAnimator = okButton.GetComponent<Animator>();
        okButton.onClick.AddListener(OnOkButtonPressed);
        okButton.interactable = false;

        SetButtonColor(false);

        ConfigureScannerMode();
    }

    void Update()
    {
        qrDetected = false;

        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            try
            {
                var result = barcodeReader.Decode(webCamTexture.GetPixels32(), webCamTexture.width, webCamTexture.height);
                if (result != null)
                {
                    Debug.Log($"QR Code Detected: {result.Text}");
                    string qrText = result.Text;

                    if (scanRedPandaMode) ScanRedPanda(qrText);
                    if (scanPyroMonkeyMode) ScanPyroMonkey(qrText);
                    if (scanTaiyakiMode) ScanTaiyaki(qrText);
                    if (scanUFOCatMode) ScanUFOCat(qrText);
                    if (scanHydroPhantasmMode) ScanHydroPhantasm(qrText);
                    if (scanPurpleFighterMode) ScanPurpleFighter(qrText);
                    if (scanPlusPlayerMode) ScanPlusPlayer(qrText);
                    if (scanPrimaryColorsMode) ScanRGB(qrText);
                    if (scanAttackColorsMode) ScanRGBAttack(qrText);
                    if (scanActivePlay01Mode) ScanActive01Mode(qrText);
                    if (scanActivePlay02Mode) ScanActive02Mode(qrText);
                    if (scanCoffeeMode) ScanBaristaMode(qrText);
                    if (scanBossMode) ScanGraffitiBossMode(qrText);
                    if (scanSmokeFaceMode) ScanSmokeFaceMode(qrText);
                    if (scanGraffitiGorillaMode) ScanGraffitiGorillaMode(qrText);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error decoding QR code: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Camera is not playing.");
        }

        if (qrDetected)
        {
            TriggerOkButtonAnimation(true);
            TriggerQRDetectedAnimation(true);
            okButton.interactable = true;
            SetButtonColor(true);
        }
        else
        {
            TriggerOkButtonAnimation(false);
            TriggerQRDetectedAnimation(false);
            okButton.interactable = false;
            SetButtonColor(false);
        }
    }

    private void ConfigureScannerMode()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        scanRedPandaMode = false;
        scanPyroMonkeyMode = false;
        scanTaiyakiMode = false;
        scanUFOCatMode = false;
        scanHydroPhantasmMode = false;
        scanPurpleFighterMode = false;
        scanPlusPlayerMode = false;
        scanPrimaryColorsMode = false;
        scanAttackColorsMode = false;
        scanActivePlay01Mode = false;
        scanActivePlay02Mode = false;
        scanCoffeeMode = false;
        scanBossMode = false;
        scanSmokeFaceMode = false;
        scanGraffitiGorillaMode = false;

        switch (sceneName)
        {
            case "04A_ScanRedPanda":
                scanRedPandaMode = true;
                break;
            case "04B_ScanPyroMonkey":
                scanPyroMonkeyMode = true;
                break;
            case "04C_ScanTaiyaki":
                scanTaiyakiMode = true;
                break;
            case "04D_ScanUFOCat":
                scanUFOCatMode = true;
                break;
            case "04E_ScanHydroPhantasm":
                scanHydroPhantasmMode = true;
                break;
            case "04F_ScanPurpleFighter":
                scanPurpleFighterMode = true;
                break;
            case "04G_ScanPlusPlayer":
                scanPlusPlayerMode = true;
                break;
            case "04H_ScanRGB":
                scanPrimaryColorsMode = true;
                break;
            case "04I_ScanRGBAttack":
                scanAttackColorsMode = true;
                break;
            case "04J_ScanActive01Mode":
                scanActivePlay01Mode = true;
                break;
            case "04K_ScanActive02Mode":
                scanActivePlay02Mode = true;
                break;
            case "04L_ScanBaristaMode":
                scanCoffeeMode = true;
                break;
            case "04M_ScanGraffitiBossMode":
                scanBossMode = true;
                break;
            case "04N_ScanSmokeFace":
                scanSmokeFaceMode = true;
                break;
            case "04O_ScanGraffitiGorilla":
                scanGraffitiGorillaMode = true;
                break;
            default:
                Debug.LogWarning("No scanner mode configured for this scene.");
                break;
        }
    }

    private void ScanRedPanda(string qrText)
    {
        if (qrText == "RedPanda")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("RedPanda QR code detected.");
        }
    }

    private void ScanPyroMonkey(string qrText)
    {
        if (qrText == "PyroMonkey")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("PyroMonkey QR code detected.");
        }
    }

    private void ScanTaiyaki(string qrText)
    {
        if (qrText == "Taiyaki")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("Taiyaki QR code detected.");
        }
    }

    private void ScanUFOCat(string qrText)
    {
        if (qrText == "UFOCat")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("UFOCat QR code detected.");
        }
    }

    private void ScanHydroPhantasm(string qrText)
    {
        if (qrText == "HydroPhantasm")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("HydroPhantasm QR code detected.");
        }
    }

    
    private void ScanPurpleFighter(string qrText)
    {
        if (qrText == "PurpleFighter")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("PurpleFighter QR code detected.");
        }
    }

    private void ScanPlusPlayer(string qrText)
    {
        if (qrText == "PlusPlayer")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("PlusPlayer QR code detected.");
        }
    }

    private void ScanRGB(string qrText)
    {
        if (qrText == "Red" || qrText == "Green" || qrText == "Blue")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;

            switch (qrText)
            {
                case "Red":
                    qrDetectedAnimator.SetTrigger("TriggerRed");
                    break;
                case "Green":
                    qrDetectedAnimator.SetTrigger("TriggerGreen");
                    break;
                case "Blue":
                    qrDetectedAnimator.SetTrigger("TriggerBlue");
                    break;
            }

            Debug.Log("RGB QR code detected.");
        }
    }

    private void ScanRGBAttack(string qrText)
    {
        if (qrText == "RedAttack" || qrText == "GreenAttack" || qrText == "BlueAttack")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;

            PlayerPrefs.SetString("SpriteSelectedAttack", qrText);
            PlayerPrefs.Save();

            switch (qrText)
            {
                case "RedAttack":
                    qrDetectedAnimator.SetTrigger("TriggerRedAttack");
                    break;
                case "GreenAttack":
                    qrDetectedAnimator.SetTrigger("TriggerGreenAttack");
                    break;
                case "BlueAttack":
                    qrDetectedAnimator.SetTrigger("TriggerBlueAttack");
                    break;
            }

            Debug.Log($"{qrText} QR code detected. Attack type saved.");
        }
    }

    private void ScanActive01Mode(string qrText)
    {
        if (qrText == "NightmareBeGone")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("ActivePlay01 QR code detected.");
        }
    }

    private void ScanActive02Mode(string qrText)
    {
        if (qrText == "Squargles2018")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("ActivePlay02 QR code detected.");
        }
    }

    private void ScanBaristaMode(string qrText)
    {
        if (qrText == "MarketLane" || qrText == "SevenSeeds" || qrText == "Axil")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("Barista code detected.");
        }
    }

    private void ScanGraffitiBossMode(string qrText)
    {
        if (qrText == "SplitGirl" || qrText == "SwapKitty")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("Boss code detected.");
        }
    }

    private void ScanSmokeFaceMode(string qrText)
    {
        if (qrText == "SmokeFace")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("Smoke Face detected.");
        }
    }

    private void ScanGraffitiGorillaMode(string qrText)
    {
        if (qrText == "GraffitiGorilla")
        {
            targetScene = qrCodeToSceneMap[qrText];
            qrDetected = true;
            Debug.Log("Graffiti Gorilla detected.");
        }
    }

    void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            webCamTexture = new WebCamTexture(devices[0].name);
            webCamTexture.Play();

            SetAspectRatio(1f);

            if (cameraDisplay != null)
            {
                cameraDisplay.texture = webCamTexture;
                AdjustRawImageAspectRatio();
            }
            Debug.Log("Camera started: " + devices[0].name);
        }
        else
        {
            Debug.LogError("No camera found!");
        }
    }

    void SetAspectRatio(float targetAspectRatio)
    {
        float currentAspect = (float)webCamTexture.width / webCamTexture.height;
        if (currentAspect != targetAspectRatio)
        {
            Debug.Log("Adjusting camera feed to 1:1 aspect ratio");
            if (currentAspect > targetAspectRatio)
            {
                float scaleFactor = currentAspect / targetAspectRatio;
                cameraDisplay.transform.localScale = new Vector3(scaleFactor, 1f, 1f);
            }
            else
            {
                float scaleFactor = targetAspectRatio / currentAspect;
                cameraDisplay.transform.localScale = new Vector3(1f, scaleFactor, 1f);
            }
        }
    }

    void AdjustRawImageAspectRatio()
    {
        RectTransform rt = cameraDisplay.GetComponent<RectTransform>();
        float size = Mathf.Min(rt.rect.width, rt.rect.height);
        rt.sizeDelta = new Vector2(size, size);
    }

    void TriggerOkButtonAnimation(bool detected)
    {
        if (okButtonAnimator != null)
        {
            if (detected)
            {
                okButtonAnimator.SetTrigger("DetectQR");
            }
            else
            {
                okButtonAnimator.SetTrigger("MissingQR");
            }
        }
        else
        {
            Debug.LogWarning("Animator component not found on the OK button.");
        }
    }

    void TriggerQRDetectedAnimation(bool detected)
    {
        if (qrDetectedAnimator != null)
        {
            if (detected)
            {
                qrDetectedAnimator.SetTrigger("QRCodeDetected");
            }
            else
            {
                qrDetectedAnimator.SetTrigger("QRCodeNotDetected");
            }
        }
    }

    void SetButtonColor(bool isDetected)
    {
        ColorBlock colorBlock = okButton.colors;
        okButton.colors = colorBlock;
    }

    void OnOkButtonPressed()
    {
        if (qrDetected)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            string[] transitionScenes = {
                "04A_ScanRedPanda", 
                "04B_ScanPyroMonkey", 
                "04C_ScanTaiyaki", 
                "04D_ScanUFOCat", 
                "04E_ScanHydroPhantasm", 
                "04F_ScanPurpleFighter", 
                "04G_ScanPlusPlayer"
            };

            bool needsSceneTransition = System.Array.Exists(transitionScenes, scene => scene == currentScene);

            if (needsSceneTransition)
            {
                Debug.Log("OK Button Pressed. Loading scene: " + targetScene);
                FindObjectOfType<SceneTransitionManager>().LoadNextScene(targetScene);
            }
            else
            {
                Debug.Log("Directly loading scene: " + targetScene);
                SceneManager.LoadScene(targetScene);
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
            #endif
        }
    }
}

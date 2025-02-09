using UnityEngine;
using UnityEngine.UI;

public class AndroidGraphicsApiSelection : MonoBehaviour
{
    [SerializeField] private Text _currentGraphicsApi;

    [SerializeField] private Button _vulkanButton;
    [SerializeField] private Button _openGlesButton;
    [SerializeField] private Button _autoButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _forceCrashButton;

    void Start()
    {
        if (_currentGraphicsApi != null)
            _currentGraphicsApi.text = $"SystemInfo.graphicsDeviceVersion is {SystemInfo.graphicsDeviceVersion}";

        _vulkanButton.onClick.RemoveAllListeners();
        _vulkanButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("AndroidGraphicsApi", "vulkan");
        });

        _openGlesButton.onClick.RemoveAllListeners();
        _openGlesButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("AndroidGraphicsApi", "gles");
        });

        _autoButton.onClick.RemoveAllListeners();
        _autoButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteKey("AndroidGraphicsApi");
        });

        _saveButton.onClick.RemoveAllListeners();
        _saveButton.onClick.AddListener(() =>
        {
            PlayerPrefs.Save();
        });

        _quitButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _forceCrashButton.onClick.RemoveAllListeners();
        _forceCrashButton.onClick.AddListener(() =>
        {
            UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
        });
    }
}

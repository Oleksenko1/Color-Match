using System.Collections;
using UnityEngine;
using Zenject;
using System.IO;

public class UIShareButton : MonoBehaviour
{
    [Inject] private UIGameoverPanel gameoverPanel;
    [Inject] private UIScore scoreScript;

    private string shareMessage;
    public void ClickShareButton()
    {
        int score = scoreScript.GetScore();

        string regularScore = $"I just scored {score} points in Color Match! Can you beat my score? \uD83C\uDF08\u2728 #ColorMatch" + 
                                "\nDownload: https://terrintin.itch.io/color-match";
        string newRecord = $"New high score in Color Match: {score} points! I'm the ultimate color master! \uD83C\uDF08\uD83C\uDFC6 #ColorMatch" +
                                "\nDownload: https://terrintin.itch.io/color-match";

        shareMessage = gameoverPanel.IsNewRecord() ? newRecord : regularScore;

        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        string filePath = Path.Combine(Application.temporaryCachePath, "shared image.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Color match").SetText(shareMessage).Share();
    }
}

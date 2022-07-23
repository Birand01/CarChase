using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CanvasType
{
    MainMenuScreen,
    GameScreen,
    EndScreen
}
public class CanvasManager : Singleton<CanvasManager>
{
    List<CanvasController> canvasControllerList;
    CanvasController lastActiveCanvas;

    protected override void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();

        // Deactivate other Menus and Activate only MainMenu
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        //First activate the canvasType 
        SwitchCanvas(CanvasType.MainMenuScreen);
    }
    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { Debug.LogWarning("Desired Canvas was not Found"); }
    }
}

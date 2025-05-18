using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
public class TutorialManager : MonoBehaviour
{
    public GameObject mainCam;
    public Inventory inventory;

    public GameObject Cinematica;
    public Text loretxt;               
    [TextArea] public string fullText;

    public GameObject PuertaClick;
    public Text Clickdoortxt;
    [TextArea] public string Clickdoor;

    public GameObject InDoor;
    public GameObject InDoorback;
    public Text DoorExplan;
    [TextArea] public string OneExplanTxt;
    [TextArea] public string TwoExplanTxt;

    public GameObject PrevUp;
    public Text PrevUptxt;
    [TextArea] public string PrevUpwrite;

    public GameObject TutUpDoor;
    public Text TutUpDoortxt;
    [TextArea] public string TutUpDoorWrite;
    [TextArea] public string TutUpDoorWritetwo;

    public GameObject PressAuto;

    public GameObject BuyAuto;
    public Text BuyAutotxt;
    [TextArea] public string BuyAutoWrite;

    public float typingSpeed = 0.03f;
    public GameObject UITutorial;
    private void OnEnable()
    {        
        StartCoroutine(TutoFirst());
    }
    private IEnumerator TutoFirst()
    {
        loretxt.text = "...";
        yield return new WaitForSeconds(2);
        loretxt.text = "";

        foreach (char c in fullText)
        {
            loretxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began));

        Cinematica.SetActive(false);
        PuertaClick.SetActive(true);

        Clickdoortxt.text = "";

        yield return new WaitForSeconds(0.5f);

        foreach (char c in Clickdoor)
        {
            Clickdoortxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitUntil(() => mainCam != null && !mainCam.activeInHierarchy);

        //dentro de la puerta
        PuertaClick.SetActive(false);
        InDoor.SetActive(true);
        DoorExplan.text = "";
        foreach (char c in OneExplanTxt)
        {
            DoorExplan.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began));
        DoorExplan.text = "";
        foreach (char c in TwoExplanTxt)
        {
            DoorExplan.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(6f);
        yield return new WaitUntil(() => inventory.Mat1 > 4.1f);
        InDoorback.SetActive(false);
        //fuera de la puerta, sin el menu de mejora abierto
        yield return new WaitUntil(() =>    inventory.Mat1 > 4.1f && mainCam.activeInHierarchy);
        InDoor.SetActive(false);
        PrevUp.SetActive(true);
        PrevUptxt.text = "";

        foreach (char c in PrevUpwrite)
        {
            PrevUptxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitUntil(() => !PrevUp.activeInHierarchy);

        //con el menu de mejora abierto
        TutUpDoor.SetActive(true);
        TutUpDoortxt.text = "";

        foreach (char c in TutUpDoorWrite)
        {
            TutUpDoortxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitUntil(() =>    Input.GetMouseButtonDown(0) ||    (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began));

        TutUpDoortxt.text = "";
        foreach (char c in TutUpDoorWritetwo)
        {
            TutUpDoortxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began));
        TutUpDoor.SetActive(false);
        yield return new WaitUntil(() => inventory.Mat1 > 1000f && mainCam.activeInHierarchy);
        PressAuto.SetActive(true);
        yield return new WaitUntil(() => !PressAuto.activeInHierarchy);
        BuyAuto.SetActive(true);
        BuyAutotxt.text = "";
        foreach (char c in BuyAutoWrite)
        {
            BuyAutotxt.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.GetTouch(0).position;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
        
    }
    

}

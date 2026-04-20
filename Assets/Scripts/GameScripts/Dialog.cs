using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Dialog : MonoBehaviour
{
    
    public GameObject NextButton;

    public DescriptionCreator DescriptionCreator;
    public CountdownTimer CountdownTimer;
    private bool IsdialogActive = true;
    public bool IsQuestComplete;// = PlayerPrefs.GetBool("IsQuestComplete", false);
    public int Talkstage;
    public bool PlayerHasPropellerHat = false;
    
    
    private bool IsPressspaceTextVisible = false;
    public TMP_Text PressspaceText;
    public TMP_Text Stage0Text;
    public TMP_Text Stage1Text;
    public TMP_Text Stage2QuestdoneText;
    public TMP_Text Stage2QuestnotdoneText;
    public TMP_Text Stage3Text;
    public TMP_Text StageDisapointedText;

    public GameObject Questpanel;

    public static object Instance { get; internal set; }

    void Update()
    {
        Talkstage= PlayerPrefs.GetInt("Talkstage", 0);
        if (Keyboard.current.eKey.wasPressedThisFrame&& PressspaceText )
    {
            NextButton.SetActive(true);
            if(CountdownTimer.timeRemaining < 200&& Talkstage==0){ 

                Talkstage = -1; PlayerPrefs.SetInt("Talkstage", Talkstage); 
                PlayerPrefs.Save(); DescriptionCreator.isShimiAngry = true;
            }
        
            OnTalkies();
        }
    }
     void OnTriggerEnter(Collider other)
     {
        
        if (other.CompareTag("ShimiArea") && IsdialogActive== true )
        {
            Debug.Log("shimi area entered");
            Debug.Log("Bito has entered the active dialog zone.");
            PressspaceText.enabled = true;
            IsPressspaceTextVisible = true;
        }
        if (other.CompareTag("Propeller Hat"))
        {
            PlayerHasPropellerHat= true;
            PlayerPrefs.SetInt("PlayerHasPHat", PlayerHasPropellerHat ? 1 : 0);
            PlayerPrefs.Save();
            Destroy(other.gameObject);
        }
     }

     void OnTriggerExit(Collider other)
     {
        if (other.CompareTag("ShimiArea") && IsPressspaceTextVisible== true)
        {
            Debug.Log("Bito has exited the active dialog zone.");
            PressspaceText.enabled = false;
            IsPressspaceTextVisible = false;
        }
     }
 
    void OnTalkies()
    {
        PressspaceText.enabled = false;
        IsPressspaceTextVisible = false;
          if (IsdialogActive== true&& Talkstage == -1)
        {
            //shimiAnimator.SetTrigger("Angry")= true;
            StageDisapointedText.enabled = true;
        DescriptionCreator.isShimiAngry = true;
            IsdialogActive= false;
          
        }
         else if (IsdialogActive== true&& Talkstage == 0)
        {
              StageDisapointedText.enabled = false;
            Stage0Text.enabled = true;
            Talkstage++;
            PlayerPrefs.SetInt("Talkstage", Talkstage);
            PlayerPrefs.Save(); 
  
        }
          else if (IsdialogActive== true&& Talkstage == 1)
        {
              Stage0Text.enabled = false;
            Stage1Text.enabled = true;
            Talkstage++;
            Questpanel.SetActive(true);
            PlayerPrefs.SetInt("Talkstage", Talkstage);
            PlayerPrefs.Save();
            
        }
          else if (IsdialogActive== true&& Talkstage == 2&& IsQuestComplete== true)
        {
                Stage1Text.enabled = false;
            Stage2QuestdoneText.enabled = true;// your so hardworking 
            Questpanel.SetActive(false); 
            DescriptionCreator.completedAllQuests = true;
            DescriptionCreator.isShimiHappy = true;
            Talkstage++;
            PlayerPrefs.SetInt("Talkstage", Talkstage);
            PlayerPrefs.Save();
          
        }
             else if (IsdialogActive== true&& Talkstage == 2&& IsQuestComplete== false)
        {
                Stage2QuestdoneText.enabled = false;
                Stage1Text.enabled = false;
            Stage2QuestnotdoneText.enabled = true;
         
        }
          else if (IsdialogActive== true&& Talkstage == 3 && IsQuestComplete== true)
        {
                Stage2QuestdoneText.enabled = false;
                Stage2QuestnotdoneText.enabled = false;
            Stage3Text.enabled = true;
            IsdialogActive= false; 
            Talkstage++;
            PlayerPrefs.SetInt("Talkstage", Talkstage);// will save to 4  but should never replay as interact is off
            PlayerPrefs.Save();
          
        }
    }
   public void ClearPrefs() { PlayerPrefs.DeleteAll(); }
public void CloseDialogueUI()
{
    // Turn off all dialogue text
    Stage0Text.enabled = false;
    Stage1Text.enabled = false;
    Stage2QuestdoneText.enabled = false;
    Stage2QuestnotdoneText.enabled = false;
    Stage3Text.enabled = false;
    StageDisapointedText.enabled = false;

    // Disable the whole button object
   NextButton.SetActive(false); 

}


}

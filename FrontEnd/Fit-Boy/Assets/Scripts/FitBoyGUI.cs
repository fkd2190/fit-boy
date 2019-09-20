using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class FitBoyGUI : MonoBehaviour
{
    public Controller controller;
    public GameObject LoadingPanel;
    public GameObject questButtonPrefab;

    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        LoadingPanel.SetActive(true);
        yield return null;
        InputField loginUsername = GameObject.Find("LoginUsername").GetComponent<InputField>();
        InputField loginPassword = GameObject.Find("LoginPassword").GetComponent<InputField>();
        Text ErrorText = GameObject.Find("LoginErrorText").GetComponent<Text>();
        Color ErrorTextColor = ErrorText.color;

        User user = controller.GetWebServerCommunicator().AuthenticateUser(loginUsername.text, loginPassword.text);
        controller.SetUser(user);

        if (user != null)
        {
            UpdateProfileGUI(user);
            FillQuestGUI();
            GameObject.Find("LoginPanel").SetActive(false);
            ErrorText.color = ErrorTextColor;
        }
        else
        {
            ErrorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage().Replace("\\n", "\n");
            ErrorText.color = Color.red;
        }
        LoadingPanel.SetActive(false);
    }

    public void RegisterButton()
    {
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        LoadingPanel.SetActive(true);
        yield return null;
        InputField registerUsername = GameObject.Find("RegisterUsername").GetComponent<InputField>();
        InputField registerEmail = GameObject.Find("RegisterEmail").GetComponent<InputField>();
        InputField registerPassword = GameObject.Find("RegisterPassword").GetComponent<InputField>();
        InputField registerConfirmPassword = GameObject.Find("RegisterConfirmPassword").GetComponent<InputField>();
        Text ErrorText = GameObject.Find("RegisterErrorText").GetComponent<Text>();
        Color ErrorTextColor = ErrorText.color;

        if (!registerPassword.text.Equals(registerConfirmPassword.text))
        {
            ErrorText.text = "Passwords do not match";
            ErrorText.color = Color.red;
        }
        else
        {
            if (controller.GetWebServerCommunicator().RegisterUser(registerUsername.text, registerEmail.text, registerPassword.text))
            {
                GameObject.Find("RegisterPanel").SetActive(false);
                ErrorText.color = ErrorTextColor;
            }
            else
            {
                ErrorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage();
                ErrorText.color = Color.red;
            }
        }
        LoadingPanel.SetActive(false);
    }

    public void UpdateProfileGUI(User user)
    {
        GameObject.Find("ProfileUsername").GetComponent<Text>().text = user.GetUsername();
        GameObject.Find("XPSlider").GetComponent<Slider>().value = user.GetXp();
        GameObject.Find("ProfileXp").GetComponent<Text>().text = user.GetXp() + "/100";
        GameObject.Find("ProfileLevelValue").GetComponent<Text>().text = "" + user.GetLevel();

        LinkedList<Quest> userQuests = controller.GetUser().GetQuests();
        foreach(Quest quest in userQuests)
        {
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.info.Title;
            newQuest.transform.Find("Description").GetComponent<Text>().text = quest.info.Desc;
            //Add details
            Transform detailPanel = newQuest.transform.Find("DetailPanel");
            detailPanel.transform.Find("StartCoordinate").GetComponent<Text>().text = quest.Start_co.Lat + ", " + quest.Start_co.Lon;
            detailPanel.transform.Find("EndCoordinate").GetComponent<Text>().text = quest.Stop_co.Lat + ", " + quest.Stop_co.Lon;
            detailPanel.transform.Find("XP").GetComponent<Text>().text = "" + quest.Xp_reward;
            detailPanel.transform.Find("Level").GetComponent<Text>().text = "" + quest.Level;

            newQuest.transform.SetParent(GameObject.Find("Content").transform, false);
        }
    }

    public void FillQuestGUI()
    {
        for(int i = 0; i<3; i++)
        {
            Make_quests mq = new Make_quests();
            Quest quest = mq.Gen_Quest();
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.info.Title;
            newQuest.transform.Find("Description").GetComponent<Text>().text = quest.info.Desc;
            newQuest.transform.Find("QuestDetails").GetComponent<Text>().text = "Xp: " + quest.Xp_reward + "Level: " + quest.Level;
            newQuest.GetComponent<QuestObject>().lat = 60.192438;
            newQuest.GetComponent<QuestObject>().lon = 24.965575;

            newQuest.transform.SetParent(GameObject.Find("NewQuests").transform, false);
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        RectTransform menuRect = menu.GetComponent<RectTransform>();
        float xPos = menuRect.localPosition.x;
        //Close all menus
        foreach (Transform child in GameObject.Find("MenuPanels").transform)
        {
            child.GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        }

        //Toggle menu
        if(xPos == 0)
        {
            menuRect.localPosition = new Vector3(-800, 100, 0);
        }
        else
        {
            menuRect.localPosition = new Vector3(0, 100, 0);
        }
    }

    public void LogOut(GameObject LoginPanel)
    {
        GameObject.Find("SettingsPanel").GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        LoginPanel.SetActive(true);
        controller.GetWebServerCommunicator().UpdateUser(controller.GetUser());
        controller.SetUser(null);
    }
}

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
        userQuests.AddLast(new Quest("Title", 1, 2.3f, "2019-09-09", "2019-9-09", new GPSCoordinate(12.345678, 12.345678), new GPSCoordinate(12.345678, 12.345678)));
        foreach(Quest quest in userQuests)
        {
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.title;
            newQuest.transform.Find("Distance").GetComponent<Text>().text = quest.distance + " km";
            //Add details
            Transform detailPanel = newQuest.transform.Find("DetailPanel");
            detailPanel.transform.Find("StartTime").GetComponent<Text>().text = quest.startTime.ToString("yyyy-MM-dd HH:mm:ss");
            detailPanel.transform.Find("EndTime").GetComponent<Text>().text = quest.endTime.ToString("yyyy-MM-dd HH:mm:ss");
            detailPanel.transform.Find("StartCoordinate").GetComponent<Text>().text = quest.startCoordinate.Latitude + ", " + quest.startCoordinate.Longitude;
            detailPanel.transform.Find("EndCoordinate").GetComponent<Text>().text = quest.endCoordinate.Latitude + ", " + quest.endCoordinate.Longitude;
            detailPanel.transform.Find("XP").GetComponent<Text>().text = "" + quest.xpLevels;

            newQuest.transform.SetParent(GameObject.Find("Content").transform, false);
        }
    }

    public void UpdateQuestGUI()
    {

    }

    public void DrawQuestOnMap()
    {

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

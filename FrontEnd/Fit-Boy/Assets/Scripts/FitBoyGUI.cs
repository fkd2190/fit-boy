using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;

public class FitBoyGUI : MonoBehaviour
{
    public Controller controller;
    public GameObject LoadingPanel;
    public GameObject questButtonPrefab;

    public void ResetPasswordButton()
    {
        StartCoroutine(ResetPassword());
    }

    public IEnumerator ResetPassword()
    {
        LoadingPanel.SetActive(true);
        yield return null;

        //do login stuff

    }

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
        user.SetFriends(controller.GetWebServerCommunicator().GetFriends(user.GetUserID()));
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
        foreach (Transform child in GameObject.Find("Content").transform)
        {
            Destroy(child.gameObject);
        }
        GameObject.Find("ProfileUsername").GetComponent<Text>().text = user.GetUsername();
        GameObject.Find("XPSlider").GetComponent<Slider>().value = user.GetXp();
        GameObject.Find("ProfileXp").GetComponent<Text>().text = user.GetXp() + "/100";
        GameObject.Find("ProfileLevelValue").GetComponent<Text>().text = "" + user.GetLevel();

        LinkedList<Quest> userQuests = controller.GetUser().GetQuests();
        foreach(Quest quest in userQuests)
        {
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.GetComponent<Button>().interactable = false;
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.info.Title;
            newQuest.transform.Find("Description").GetComponent<Text>().text = quest.info.Desc;
            newQuest.transform.Find("Description").GetComponent<Text>().text = "Xp: " + quest.Xp_reward + " Level: " + quest.Level;
            //Add details
            

            newQuest.transform.SetParent(GameObject.Find("Content").transform, false);
        }
    }

    public void FillQuestGUI()
    {
        foreach (Transform child in GameObject.Find("NewQuests").transform)
        {
            Destroy(child.gameObject);
        }
        Make_quests mq = new Make_quests();
        for (int i = 0; i<3; i++)
        {
            Quest quest = mq.Gen_Quest();
            //quest.Stop_co = new GPSCoordinate(60.190986, 24.966124, ""); //USe these values when testing in editor.
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.GetComponent<QuestObject>().quest = quest;
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.info.Title;
            newQuest.transform.Find("Description").GetComponent<Text>().text = quest.info.Desc;
            newQuest.transform.Find("QuestDetails").GetComponent<Text>().text = "Xp: " + quest.Xp_reward + " Level: " + quest.Level;
            newQuest.transform.Find("QuestLocation").GetComponent<Text>().text = quest.Stop_co.Name;

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

    public void StartQuest(Quest quest)
    {
        GameObject.Find("Directions").GetComponent<DirectionsFactory>().endPos = new Vector2d(quest.Stop_co.Lat, quest.Stop_co.Lon);
        GameObject.Find("QuestPanel").GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        controller.SetActiveQuest(quest);
        Debug.Log(quest.Stop_co.Lat);
        Debug.Log("Quest Started");
    }

    public void LogOut(GameObject LoginPanel)
    {
        GameObject.Find("SettingsPanel").GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        LoginPanel.SetActive(true);
        controller.GetWebServerCommunicator().UpdateUser(controller.GetUser());
        controller.SetUser(null);
        controller.SetActiveQuest(null);
    }
}

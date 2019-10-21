using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.IO;
using System; 

public class FitBoyGUI : MonoBehaviour
{
    public Controller controller;
    public GameObject LoadingPanel;
    public GameObject questButtonPrefab;
    public GameObject cube;
    public GameObject friendButtonPrefab;
    public GameObject successDialog;

    public void Start()
    {
        string filePath = Application.persistentDataPath + "/settings.dat";
        if (File.Exists(filePath))
        {
            string[] fileContents = File.ReadAllLines(filePath);
            Debug.Log(fileContents[0]);
            User user = controller.GetWebServerCommunicator().AuthenticateUser(fileContents[0], "emptyPassword", true);

            controller.SetUser(user);

            if (user != null)
            {
                user.SetFriends(controller.GetWebServerCommunicator().GetFriends(user.GetUserID()));
                UpdateProfileGUI(user);
                FillQuestGUI();
                UpdateFriendGUI();
                GameObject.Find("LoginPanel").SetActive(false);
            }
        }
    }

    public void ChangePasswordButton()
    {
        StartCoroutine(ChangePassword());
    }

    public IEnumerator ChangePassword()
    {
        LoadingPanel.SetActive(true);
        yield return null;

        InputField oldPassword = GameObject.Find("OldPassword").GetComponent<InputField>();
        InputField newPassword = GameObject.Find("ChangePassword").GetComponent<InputField>();
        InputField confirmPassword = GameObject.Find("ConfirmChangePassword").GetComponent<InputField>();
        Text errorText = GameObject.Find("SettingsErrorText").GetComponent<Text>();

        if (newPassword.text.Equals(confirmPassword.text))
        {
            if(controller.GetWebServerCommunicator().UpdateUser(controller.GetUser(), "", oldPassword.text, newPassword.text))
            {
                successDialog.SetActive(true);
                errorText.text = "";
            }
            else
            {
                errorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage();
            }
        }
        else
        {
            errorText.text = "New password is not the same as the confirm password.";
        }
        oldPassword.text = "";
        newPassword.text = "";
        confirmPassword.text = "";
        LoadingPanel.SetActive(false);
    }

    public void ChangeEmailButton()
    {
        StartCoroutine(ChangeEmail());
    }

    public IEnumerator ChangeEmail()
    {
        LoadingPanel.SetActive(true);
        yield return null;

        InputField email = GameObject.Find("ChangeEmail").GetComponent<InputField>();
        Text errorText = GameObject.Find("SettingsErrorText").GetComponent<Text>();

        if (controller.GetWebServerCommunicator().UpdateUser(controller.GetUser(), email.text, "",""))
        {
            errorText.text = "";
            successDialog.SetActive(true);
        }
        else
        {
            errorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage();
        }
        email.text = "";
        LoadingPanel.SetActive(false);
    }

    public void AddFriendButton()
    {
        StartCoroutine(AddFriend());
    }

    public IEnumerator AddFriend()
    {
        LoadingPanel.SetActive(true);
        yield return null;

        InputField friendUsername = GameObject.Find("AddFriendUsername").GetComponent<InputField>();
        Text errorText = GameObject.Find("AddFriendErrorText").GetComponent<Text>();

        if (controller.GetWebServerCommunicator().AddFriend(controller.GetUser().GetUsername(), friendUsername.text))
        {
            friendUsername.text = "";
            errorText.text = "";
            User u = controller.GetUser();
            u.SetFriends(controller.GetWebServerCommunicator().GetFriends(u.GetUserID()));
            UpdateFriendGUI();
        }
        else
        {
            errorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage();
        }
        LoadingPanel.SetActive(false);
    }

    public void UpdateFriendGUI()
    {
        foreach (Transform child in GameObject.Find("FriendsList").transform)
        {
            Destroy(child.gameObject);
        }

        foreach (User friend in controller.GetUser().GetFriends())
        {
            GameObject newFriend = Instantiate(friendButtonPrefab);
            newFriend.GetComponent<FriendButtonPrefabScript>().friend = friend;
            newFriend.transform.Find("FriendUsername").GetComponent<Text>().text = friend.GetUsername();
            newFriend.transform.Find("FriendXpValue").GetComponent<Text>().text = "" + friend.GetXp();
            newFriend.transform.Find("FriendLevelValue").GetComponent<Text>().text = "" + friend.GetLevel();
            //Add details


            newFriend.transform.SetParent(GameObject.Find("FriendsList").transform, false);
        }
    }

    public void ResetPasswordButton()
    {
        StartCoroutine(ResetPassword());
    }

    public IEnumerator ResetPassword()
    {
        LoadingPanel.SetActive(true);
        yield return null;

        InputField emailField = GameObject.Find("ForgotPasswordEmail").GetComponent<InputField>();
        Text errorText = GameObject.Find("ForgotPasswordErrorText").GetComponent<Text>();

        if (controller.GetWebServerCommunicator().ResetPassword(emailField.text))
        {
            LoadingPanel.SetActive(false);
            emailField.text = "";
            errorText.text = "";
        }
        else
        {
            LoadingPanel.SetActive(false);
            errorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage();
        }

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
        Toggle rememberToggle = GameObject.Find("RememberMeToggle").GetComponent<Toggle>();
        Text ErrorText = GameObject.Find("LoginErrorText").GetComponent<Text>();

        User user = controller.GetWebServerCommunicator().AuthenticateUser(loginUsername.text, loginPassword.text);
        controller.SetUser(user);

        if (user != null)
        {
            user.SetFriends(controller.GetWebServerCommunicator().GetFriends(user.GetUserID()));
            UpdateProfileGUI(user);
            FillQuestGUI();
            UpdateFriendGUI();
            if (rememberToggle.isOn)
            {
                string filePath = Application.persistentDataPath + "/settings.dat";
                string[] fileContents = new string[1];
                if (File.Exists(filePath))
                {
                    fileContents = File.ReadAllLines(filePath);
                }
                fileContents[0] = loginUsername.text;
                File.WriteAllLines(filePath, fileContents);
            }

            loginUsername.text = "";
            loginPassword.text = "";
            ErrorText.text = "";

            GameObject.Find("LoginPanel").SetActive(false);
        }
        else
        {
            ErrorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage().Replace("\\n", "\n");
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

        if (!registerPassword.text.Equals(registerConfirmPassword.text))
        {
            ErrorText.text = "Passwords do not match";
        }
        else
        {
            if (controller.GetWebServerCommunicator().RegisterUser(registerUsername.text, registerEmail.text, registerPassword.text))
            {
                registerUsername.text = "";
                registerEmail.text = "";
                registerPassword.text = "";
                registerConfirmPassword.text = "";
                GameObject.Find("RegisterPanel").SetActive(false);
                ErrorText.text = "";
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
            newQuest.transform.Find("QuestLocation").GetComponent<Text>().text = quest.Stop_co.Name;
            newQuest.transform.Find("QuestDetails").GetComponent<Text>().text = "Xp: " + quest.Xp_reward + " Level: " + quest.Level;
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
        DrawRadiationZones();
        controller.SetActiveQuest(quest);
        DirectionsFactory df = GameObject.Find("Directions").GetComponent<DirectionsFactory>();
        df.endPos = new Vector2d(quest.Stop_co.Lat, quest.Stop_co.Lon);
        GameObject.Find("QuestPanel").GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        df.Query();
    }

    public void LogOut(GameObject LoginPanel)
    {
        GameObject.Find("SettingsPanel").GetComponent<RectTransform>().localPosition = new Vector3(-800, 100, 0);
        LoginPanel.SetActive(true);
        controller.GetWebServerCommunicator().UpdateUser(controller.GetUser());
        controller.SetUser(null);
        controller.SetActiveQuest(null);
        controller.RadiationZones = null;
        GameObject.Find("Directions").GetComponent<DirectionsFactory>().Query();
        foreach (Transform child in GameObject.Find("RadiationZones").transform)
        {
            Destroy(child.gameObject);
        }
        string filePath = Application.persistentDataPath + "/settings.dat";
        if (File.Exists(filePath))
        {
            string[] fileContents = File.ReadAllLines(filePath);
            fileContents[0] = "";
            File.WriteAllLines(filePath, fileContents);
        }
    }

    private void DrawRadiationZones()
    {
        foreach (Transform child in GameObject.Find("RadiationZones").transform)
        {
            Destroy(child.gameObject);
        }

        AbstractMap map = GameObject.Find("Map").GetComponent<AbstractMap>();
        Vector2d location = GameObject.Find("PlayerTarget").transform.GetGeoPosition(map.CenterMercator, map.WorldRelativeScale);
        controller.RadiationZones = Gen_Rad_Zone.In_rad_zone(new GPSCoordinate(location.x, location.y, "")); //Generate radiation zones around player location
        //controller.RadiationZones = new ArrayList();
        //controller.RadiationZones.Add(new Radiation_Zone(new GPSCoordinate(60.19235, 24.96611, ""), 70));
        foreach (Radiation_Zone zone in controller.RadiationZones)
        {
            GameObject radZone = Instantiate(cube);
            radZone.transform.MoveToGeocoordinate(zone.coordinate.Lat, zone.coordinate.Lon, map.CenterMercator, map.WorldRelativeScale);
            GameObject g = new GameObject();
            double newLat = zone.coordinate.Lat + ((zone.radius / 1000) / 6371) * (180 / Math.PI);
            Debug.Log(newLat);
            g.transform.MoveToGeocoordinate(newLat, zone.coordinate.Lon, map.CenterMercator, map.WorldRelativeScale);
            double scale = (g.GetComponent<Transform>().localPosition.z - radZone.GetComponent<Transform>().localPosition.z) / zone.radius;
            Debug.Log("Scale: " + scale);
            radZone.transform.localScale = new Vector3((float)(scale * zone.radius * 2), 0.1f, (float)(scale * zone.radius * 2));
            Destroy(g);
            radZone.transform.SetParent(GameObject.Find("RadiationZones").transform, false);
        }
    }

    public void CloseFriendProfilePanel()
    {
        Transform t = GameObject.Find("FriendProfilePanel").GetComponent<Transform>();
        t.localPosition = new Vector3(-800, 100, 0);
    }
}

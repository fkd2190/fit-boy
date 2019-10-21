using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendButtonPrefabScript : MonoBehaviour
{
    public User friend;
    public GameObject questButtonPrefab;

    public void ViewFriend()
    {
        StartCoroutine(ViewFriendAction());
    }

    public IEnumerator ViewFriendAction()
    {
        GameObject loadingPanel = GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().LoadingPanel;
        loadingPanel.SetActive(true);
        yield return null;
        GameObject friendPanel = GameObject.Find("FriendProfilePanel");
        Debug.Log(friend.GetUsername());
        friend = GameObject.Find("InitClasses").GetComponent<Controller>().GetWebServerCommunicator().AuthenticateUser(friend.GetUsername(), "", true);

        foreach (Transform child in GameObject.Find("FriendQuests").transform)
        {
            Destroy(child.gameObject);
        }
        GameObject.Find("FriendProfileUsername").GetComponent<Text>().text = friend.GetUsername();
        GameObject.Find("FriendXPSlider").GetComponent<Slider>().value = friend.GetXp();
        GameObject.Find("FriendProfileXp").GetComponent<Text>().text = friend.GetXp() + "/100";
        GameObject.Find("FriendProfileLevelValue").GetComponent<Text>().text = "" + friend.GetLevel();

        LinkedList<Quest> userQuests = friend.GetQuests();
        foreach (Quest quest in userQuests)
        {
            GameObject newQuest = Instantiate(questButtonPrefab);
            newQuest.GetComponent<Button>().interactable = false;
            newQuest.transform.Find("QuestTitle").GetComponent<Text>().text = quest.info.Title;
            newQuest.transform.Find("Description").GetComponent<Text>().text = quest.info.Desc;
            newQuest.transform.Find("QuestLocation").GetComponent<Text>().text = quest.Stop_co.Name;
            newQuest.transform.Find("QuestDetails").GetComponent<Text>().text = "Xp: " + quest.Xp_reward + " Level: " + quest.Level;
            //Add details


            newQuest.transform.SetParent(GameObject.Find("FriendQuests").transform, false);
        }
        loadingPanel.SetActive(false);
        friendPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
    }

    public void UnFriend()
    {
        StartCoroutine(UnFriendAction());
    }

    public IEnumerator UnFriendAction()
    {
        GameObject loadingPanel = GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().LoadingPanel;
        loadingPanel.SetActive(true);
        yield return null;
        Controller c = GameObject.Find("InitClasses").GetComponent<Controller>();
        c.GetWebServerCommunicator().DeleteFriend(c.GetUser().GetUsername(), friend.GetUsername());
        c.GetUser().SetFriends(c.GetWebServerCommunicator().GetFriends(c.GetUser().GetUserID()));
        GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().UpdateFriendGUI();
        loadingPanel.SetActive(false);
    }
}

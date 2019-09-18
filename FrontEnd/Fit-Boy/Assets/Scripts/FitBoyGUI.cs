using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class FitBoyGUI : MonoBehaviour
{
    public Controller controller;

    public void LoginButton()
    {
        InputField loginUsername = GameObject.Find("LoginUsername").GetComponent<InputField>();
        InputField loginPassword = GameObject.Find("LoginPassword").GetComponent<InputField>();
        Text ErrorText = GameObject.Find("LoginErrorText").GetComponent<Text>();
        Color ErrorTextColor = ErrorText.color;

        controller.SetUser(controller.GetWebServerCommunicator().AuthenticateUser(loginUsername.text, loginPassword.text));

        if (controller.GetUser() != null)
        {
            GameObject.Find("LoginCanvas").SetActive(false);
            ErrorText.color = ErrorTextColor;
        }
        else
        {
            ErrorText.text = controller.GetWebServerCommunicator().GetLastErrorMessage().Replace("\\n", "\n");
            ErrorText.color = Color.red;
        }
    }

    public void RegisterButton()
    {
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
    }
}

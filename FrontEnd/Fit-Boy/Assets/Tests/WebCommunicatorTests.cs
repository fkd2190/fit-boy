using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Drawing;
using System.Windows;

namespace Tests
{
    public class WebCommunicatorTests
    {
        private WebServerCommunicator communicator;
        private string username;
        private string email;
        private string password;

        //
        //Setup and teardown
        //
        [SetUp]
        public void SetUp()
        {
            communicator = new WebServerCommunicator();
            username = "3135035_fitboy";
            email = "3135035_fitboy@fitboy.tk";
            password = "password";
        }

        [TearDown]
        public void TearDown()
        {
            communicator.DeleteUser(username);
        }

        //
        //Register User tests
        //
        [Test]
        public void RegisterValidUser()
        {
            bool result = communicator.RegisterUser(username, email, password);
            Assert.IsTrue(result);
        }

        [Test]
        public void RegisterInvalidEmailAddress()
        {
            email = "someone@example"; //change email address to one that doesnt conform
            bool result = communicator.RegisterUser(username, email, password);
            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterExistingEmail()
        {
            //Add a dummy user to the database
            communicator.RegisterUser("dummyUser", email, password);
            bool result = communicator.RegisterUser(username, email, password);
            communicator.DeleteUser("dummyUser");
            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterExistingUsername()
        {
            communicator.RegisterUser(username, "3135035_fitboy@example.com", password);
            bool result = communicator.RegisterUser(username, email, password);
            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterEmptyPassword()
        {
            bool result = communicator.RegisterUser(username, email, "");
            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterEmptyUsername()
        {
            bool result = communicator.RegisterUser("", email, password);
            Assert.IsFalse(result);
        }

        //
        //Authenticate user tests
        //
        [Test]
        public void AuthenticateValidUser()
        {
            communicator.RegisterUser(username, email, password);
            User user = communicator.AuthenticateUser(username, password);
            Assert.NotNull(user);
        }

        [Test]
        public void FailedAuthenticationUsername()
        {
            communicator.RegisterUser(username, email, password);
            User user = communicator.AuthenticateUser("otherUsername", password);
            Assert.IsNull(user);
        }

        [Test]
        public void FailedAuthenticationPassword()
        {
            communicator.RegisterUser(username, email, password);
            User user = communicator.AuthenticateUser(username, "otherPassword");
            Assert.IsNull(user);
        }

        //
        //Upload user tests
        //
        //[Test]
        //public void UpdateValidUser()
        //{
        //    communicator.RegisterUser(username, email, password);
        //    User user = communicator.AuthenticateUser(username, password);
        //    user.SetXp(20);
        //    bool result = communicator.UpdateUser(user);
        //    Assert.IsTrue(result);

        //    //Check if value updated in database by pulling user from database again
        //    user = communicator.AuthenticateUser(username, password);
        //    Assert.AreEqual(user.GetXp(), 20);
        //}

        //
        //Upload Quest tests
        //

        //[Test]
        //public void UploadValidQuest()
        //{
        //    Quest quest = new Quest();
        //    quest.title = "example";
        //    quest.distance = 10;
        //    quest.startTime = DateTime.Now;
        //    quest.endTime = DateTime.Now;
        //    quest.startCoordinate = new GPSCoordinate(12.345678, 12.345678);
        //    quest.endCoordinate = new GPSCoordinate(12.345678, 12.345678);
        //    quest.xpLevels = 10;

        //    communicator.RegisterUser(username, email, password);
        //    User user = communicator.AuthenticateUser(username, password);

        //    bool result = communicator.UploadQuest(quest, user);

        //    Assert.IsTrue(result);
        //}

    }
}

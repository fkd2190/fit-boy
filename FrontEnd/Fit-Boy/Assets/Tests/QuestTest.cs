using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class QuestTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void QuestTestSimplePasses()
        {
            Make_quests mq = new Make_quests();
            Quest quest = mq.Gen_Quest();
            Assert.IsNotNull(quest);
        }
    }
}

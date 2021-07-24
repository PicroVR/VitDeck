using NUnit.Framework;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.TestTools;
#if VRC_SDK_VRCSDK3
using VRC.SDKBase;
#elif VRC_SDK_VRCSDK2
using VRCSDK2;
#endif

namespace VitDeck.Validator.Test
{
    public class TestVRCTriggerConfigRule
    {
        [Test]
#if !VRC_SDK_VRCSDK2 
        [Ignore("VRCSDK2 に依存したテストなので無視する")]
#endif
        public void TestValidateError()
        {
#if VRC_SDK_VRCSDK2 || VRC_SDK_VRCSDK3 
            var rule = new VRCTriggerConfigRule(
                            "VRC_Triggerコンポーネントの設定を検証するルール",
                            new VRC_EventHandler.VrcBroadcastType []{
                                VRC_EventHandler.VrcBroadcastType.Local },
                            new VRC_Trigger.TriggerType[] {
                                VRC_Trigger.TriggerType.Custom,
                                VRC_Trigger.TriggerType.OnInteract,
                                VRC_Trigger.TriggerType.OnEnterTrigger,
                                VRC_Trigger.TriggerType.OnExitTrigger,
                                VRC_Trigger.TriggerType.OnPickup,
                                VRC_Trigger.TriggerType.OnDrop,
                                VRC_Trigger.TriggerType.OnPickupUseDown,
                                VRC_Trigger.TriggerType.OnPickupUseUp   },
                            new VRC_EventHandler.VrcEventType[] {
                                VRC_EventHandler.VrcEventType.ActivateCustomTrigger,
                                VRC_EventHandler.VrcEventType.AudioTrigger,
                                VRC_EventHandler.VrcEventType.PlayAnimation,
                                VRC_EventHandler.VrcEventType.SetComponentActive,
                                VRC_EventHandler.VrcEventType.SetGameObjectActive,
                                VRC_EventHandler.VrcEventType.AnimationBool,
                                VRC_EventHandler.VrcEventType.AnimationFloat,
                                VRC_EventHandler.VrcEventType.AnimationInt,
                                VRC_EventHandler.VrcEventType.AnimationIntAdd,
                                VRC_EventHandler.VrcEventType.AnimationIntDivide,
                                VRC_EventHandler.VrcEventType.AnimationIntMultiply,
                                VRC_EventHandler.VrcEventType.AnimationIntSubtract,
                                VRC_EventHandler.VrcEventType.AnimationTrigger},
                            new string[] {
                                "0f0f1424d69ba1f4bb125329dde4d15f",
                                "6584b33cb63ec934495c6115c2686f2a" });

            var finder = new ValidationTargetFinder();
            var target = finder.Find("Assets/VitDeck/Validator/Tests/Data/VRCTriggerConfigRule", true);
            var result = rule.Validate(target);
            Assert.That(result.RuleName,Is.EqualTo("VRC_Triggerコンポーネントの設定を検証するルール"));
            Assert.That(result.Issues.Count, Is.EqualTo(8));

            Assert.That(result.Issues[0].target.name, Is.EqualTo("Cube (13)"));
            Assert.That(result.Issues[0].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[0].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のTriggerは使用できません:{0}", "OnEnable")));
            //Assert.That(result.Issues[0].solution, Is.EqualTo("使用可能なTriggerに変更するか、使用申請をして下さい。"));

            Assert.That(result.Issues[1].target.name, Is.EqualTo("Cube (14)"));
            Assert.That(result.Issues[1].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[1].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のActionは使用できません:{0}", "SendRPC")));
            //Assert.That(result.Issues[1].solution, Is.EqualTo("使用可能なActionに変更するか、使用申請をして下さい。"));

            Assert.That(result.Issues[2].target.name, Is.EqualTo("Cube (15)"));
            Assert.That(result.Issues[2].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[2].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のBroadcastTypeは使用できません:{0}", "AlwaysUnbuffered")));
            //Assert.That(result.Issues[2].solution, Is.EqualTo("使用可能なBroadcastTypeに変更するか、使用申請をして下さい。"));

            Assert.That(result.Issues[3].target.name, Is.EqualTo("Cube (16)"));
            Assert.That(result.Issues[3].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[3].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のBroadcastTypeは使用できません:{0}", "Always")));
            //Assert.That(result.Issues[3].solution, Is.EqualTo("使用可能なBroadcastTypeに変更するか、使用申請をして下さい。"));
            Assert.That(result.Issues[4].target.name, Is.EqualTo("Cube (16)"));
            Assert.That(result.Issues[4].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[4].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のTriggerは使用できません:{0}", "OnTimer")));
            //Assert.That(result.Issues[4].solution, Is.EqualTo("使用可能なTriggerに変更するか、使用申請をして下さい。"));
            Assert.That(result.Issues[5].target.name, Is.EqualTo("Cube (16)"));
            Assert.That(result.Issues[5].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[5].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のActionは使用できません:{0}", "SpawnObject")));
            //Assert.That(result.Issues[5].solution, Is.EqualTo("使用可能なActionに変更するか、使用申請をして下さい。"));

            Assert.That(result.Issues[6].target.name, Is.EqualTo("Cube (17)"));
            Assert.That(result.Issues[6].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[6].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のActionは使用できません:{0}", "TeleportPlayer")));
            //Assert.That(result.Issues[6].solution, Is.EqualTo("使用可能なActionに変更するか、使用申請をして下さい。"));

            Assert.That(result.Issues[7].target.name, Is.EqualTo("Cube (20)"));
            Assert.That(result.Issues[7].level, Is.EqualTo(IssueLevel.Error));
            //Assert.That(result.Issues[7].message, Is.EqualTo(string.Format("VRC_Triggerコンポーネントで次のActionは使用できません:{0}", "SetVelocity")));
            //Assert.That(result.Issues[7].solution, Is.EqualTo("使用可能なActionに変更するか、使用申請をして下さい。"));
#endif
        }
    }
}

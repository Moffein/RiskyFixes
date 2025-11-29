using RoR2;
using UnityEngine.SceneManagement;
using UnityEngine;
using RoR2.EntityLogic;
using System;

namespace RiskyFixes.Fixes.Stages.VoidRaid
{
    public class VoidRaidCutsceneProtection : FixBase<VoidRaidCutsceneProtection>
    {
        public override string ConfigCategoryString => "Stages - Planeterium";

        public override string ConfigOptionName => "(Server-Side) Cutscene Protection";

        public override string ConfigDescriptionString => "Players are immuneduring the initial Voidling spawn cutscesne.";

        protected override void ApplyChanges()
        {
            RoR2.Stage.onServerStageBegin += Stage_onServerStageBegin;
        }

        private void Stage_onServerStageBegin(Stage stage)
        {
            var scene = SceneManager.GetActiveScene();
            if (scene != null && scene.name == "voidraid")
            {
                SetHookEnabled(true);
            }
            else
            {
                SetHookEnabled(false);
            }
        }

        private static bool hookAdded = false;
        private static void SetHookEnabled(bool enabled)
        {
            if (enabled == hookAdded)
            {
                return;
            }
            else if (enabled)
            {
                hookAdded = true;
                On.RoR2.EntityLogic.DelayedEvent.CallDelayed += DelayedEvent_CallDelayed;
            }
            else
            {
                hookAdded = false;
                On.RoR2.EntityLogic.DelayedEvent.CallDelayed -= DelayedEvent_CallDelayed;
            }
        }

        private static void DelayedEvent_CallDelayed(On.RoR2.EntityLogic.DelayedEvent.orig_CallDelayed orig, DelayedEvent self, float timer)
        {
            orig(self, timer);
            if (self.gameObject.name == "BossStartTrigger")
            {
                TimerQueue timerQueue = null;
                switch (self.timeStepType)
                {
                    case DelayedEvent.TimeStepType.Time:
                        timerQueue = RoR2Application.timeTimers;
                        break;
                    case DelayedEvent.TimeStepType.UnscaledTime:
                        timerQueue = RoR2Application.unscaledTimeTimers;
                        break;
                    case DelayedEvent.TimeStepType.FixedTime:
                        timerQueue = RoR2Application.fixedTimeTimers;
                        break;
                }
                if (timerQueue != null)
                {
                    timerQueue.CreateTimer(timer, new Action(() => {
                        Util.SetPlayersInvulnerableServer(true, 7.6f);
                    }));
                    SetHookEnabled(false);
                }
            }
        }
    }
}

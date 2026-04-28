using TaleWorlds.MountAndBlade;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EfeTournamentMod
{
    public class Main : MBSubModuleBase
    {
        // 1. Aşama: Mod yüklenirken (arkaplanda) Harmony'i başlatıyoruz. Burası en güvenli yer.
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            Harmony harmony = new Harmony("com.efeyanik.tournamentmod");
            harmony.PatchAll();
        }

        // 2. Aşama: Ana menü ekranı yüklenip karşımıza gelmeden hemen önceki an.
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            // UI artık hazır olduğu için mesajımızı buraya koyuyoruz.
            InformationManager.DisplayMessage(new InformationMessage("Efe'nin Turnuva Modu basariyla yuklendi!", Colors.Green));
        }
    }
}
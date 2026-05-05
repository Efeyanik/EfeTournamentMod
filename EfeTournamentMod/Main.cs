using System;
using System.IO;
using TaleWorlds.MountAndBlade;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EfeTournamentMod
{
    public class Main : MBSubModuleBase
    {
        // Modun arka planda yüklenmeye başladığı ilk an
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                // Proje için benzersiz bir Harmony ID'si oluşturuyoruz
                var harmony = new Harmony("com.efeyanik.tournamentmod.patch");

                // TournamentRewardPatch dahil tüm patch'leri otomatik olarak uygular
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                // Runtime'da bir çökme olursa log dosyasını masaüstüne yazdır
             
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string logFile = Path.Combine(desktopPath, "Mod_Hata_Log.txt");
                File.WriteAllText(logFile, "Harmony Yukleme Hatasi:\n\n" + ex.ToString());
            }
        }

        
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            
        }
    }
}
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;

namespace EfeTournamentMod
{
   
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetParticipantArmor")]
    public class TournamentArmorPatch
    {
        public static void Postfix(CharacterObject participant, ref Equipment __result)
        {
            if (participant != null && participant.IsPlayerCharacter && ModSettings.KendiEsyamlaGir)
            {
                // Oyunun bu maç için tasarladığı orijinal zırh paketinde at var mı?
                bool atOlsunMu = __result[EquipmentIndex.Horse].Item != null;

                // 'new Equipment()' ile KLON yaratıyoruz
                Equipment gercekEkipman = new Equipment(participant.HeroObject.BattleEquipment);

                // Eğer oyun bu maçta at istemiyorsa (yaya dövüşüyse), klondan atı sil
                if (!atOlsunMu)
                {
                    gercekEkipman[EquipmentIndex.Horse] = EquipmentElement.Invalid;
                    gercekEkipman[EquipmentIndex.HorseHarness] = EquipmentElement.Invalid;
                }

                __result = gercekEkipman;
            }
        }
    }

    [HarmonyPatch(typeof(TournamentParticipant), "set_MatchEquipment")]
    public class TournamentWeaponPatch
    {
        public static void Prefix(TournamentParticipant __instance, ref Equipment value)
        {
            if (__instance.Character != null && __instance.Character.IsPlayerCharacter && ModSettings.KendiEsyamlaGir)
            {
                // Oyunun bu el için vereceği silahlarda at var mı?
                bool atOlsunMu = value[EquipmentIndex.Horse].Item != null;

                // Gerçek savaş eşyalarımızın klonunu alıyoruz
                Equipment gercekEkipman = new Equipment(__instance.Character.HeroObject.BattleEquipment);

                // Eğer maç yaya maçıysa atı ve eyerini (HorseHarness) yolluyoruz
                if (!atOlsunMu)
                {
                    gercekEkipman[EquipmentIndex.Horse] = EquipmentElement.Invalid;
                    gercekEkipman[EquipmentIndex.HorseHarness] = EquipmentElement.Invalid;
                }

                value = gercekEkipman;
            }
        }
    }
}
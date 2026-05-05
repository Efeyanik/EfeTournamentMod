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
            if (participant != null && participant.IsPlayerCharacter)
            {
                // Turnuva zırhı yerine kend karakterimizin ekipmanını veriyoruz.
                __result = participant.HeroObject.BattleEquipment;
            }
        }
    }

    
    [HarmonyPatch(typeof(TournamentParticipant), "set_MatchEquipment")]
    public class TournamentWeaponPatch
    {
        
        public static void Prefix(TournamentParticipant __instance, ref Equipment value)
        {
            if (__instance.Character != null && __instance.Character.IsPlayerCharacter)
            {
                //kendi gerçek savaş ekipmanımızı veriyoruz.
                value = __instance.Character.HeroObject.BattleEquipment;
            }
        }
    }
}
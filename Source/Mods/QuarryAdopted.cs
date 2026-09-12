using HarmonyLib;
using Multiplayer.API;
using Multiplayer.Compat;
using Verse;

namespace MultiplayerQuarryAdoptedPatch.Source.Mods;

/// <summary>
///     Multiplayer Patch for Quarry [Adopted] by Ogliss, Last Update: 1 Sep, 2025 @ 6:00am
///     https://steamcommunity.com/sharedfiles/filedetails/?id=2007576583
/// </summary>
[MpCompatFor("Ogliss.TheWhiteCrayon.Quarry")]
public class QuarryAdoptedPatch
{
    public QuarryAdoptedPatch(ModContentPack _content)
    {
        LongEventHandler.ExecuteWhenFinished(LatePatch);
    }

    private static void LatePatch()
    {
        Log.Message("[Multiplayer Quarry Adopted Patch] Initializing...");

        var _buildingQuarryType = GetBuilding_Quarry();
        if (_buildingQuarryType == null)
            return;

        RegisterSyncMethod(_buildingQuarryType, "MineModeResources");
        RegisterSyncMethod(_buildingQuarryType, "MineModeBlocks");
        RegisterSyncMethod(_buildingQuarryType, "MineModeChunks");
        RegisterSyncMethod(_buildingQuarryType, "ToggleAutoHaul");

        Log.Message("[Multiplayer Quarry Adopted Patch] Initialized.");
    }

    #region Registers

    private static void RegisterSyncMethod(Type _type, string _methodName)
    {
        var _method = AccessTools.Method(_type, _methodName);
        if (_method == null)
        {
            Log.Error($"[Multiplayer Quarry Adopted Patch] Could not find " +
                      $"{_type.FullName}.{_methodName}().");

            return;
        }

        MP.RegisterSyncMethod(_method);
    }

    #endregion

    #region Getters

    private static Type GetBuilding_Quarry()
    {
        var _buildingQuarryType = AccessTools.TypeByName("Quarry.Building_Quarry");

        if (_buildingQuarryType != null) return _buildingQuarryType;

        Log.Error("[Multiplayer Quarry Adopted Patch] Could not find " +
                  "Quarry.Building_Quarry.");

        return null;
    }

    #endregion
}
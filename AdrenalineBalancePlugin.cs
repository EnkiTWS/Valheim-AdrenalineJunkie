
using System;
using System.Runtime.CompilerServices;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AdrenalineJunkie
{
    [BepInPlugin(
        "com.enki.adrenalineJunkie",
        "AdrenalineJunkie",
        "1.0.2")]
    public class AdrenalineJunkiePlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        internal static ConfigEntry<float> DecayMultiplier;
        internal static ConfigEntry<float> DelayMultiplier;
        internal static ConfigEntry<bool> EnableDelayMultiplier;
        internal static ConfigEntry<float> GainMultiplier;

        private Harmony _harmony;

        // Store each player's original curves without keeping
        // destroyed Player objects alive.
        private static readonly ConditionalWeakTable<Player, OriginalCurves>
            Originals = new ConditionalWeakTable<Player, OriginalCurves>();

        private sealed class OriginalCurves
        {
            internal readonly AnimationCurve Decay;
            internal readonly AnimationCurve Delay;
            internal readonly AnimationCurve Gain;

            internal OriginalCurves(Player player)
            {
                Decay = CloneCurve(player.m_adrenalineDegen);
                Delay = CloneCurve(player.m_adrenalineDegenDelay);
                Gain = CloneCurve(player.m_adrenalineGainMultiplier);
            }
        }

        private void Awake()
        {
            Log = Logger;

            DecayMultiplier = Config.Bind(
                "Adrenaline",
                "DecayMultiplier",
                0.50f,
                new ConfigDescription(
                    "Multiplier for Adrenaline decay. " +
                    "0 disables natural decay; 1 is vanilla.",
                    new AcceptableValueRange<float>(0f, 3f)));

            DelayMultiplier = Config.Bind(
                "Adrenaline",
                "DelayMultiplier",
                1.50f,
                new ConfigDescription(
                    "Multiplier for the delay before decay begins. " +
                    "1 is vanilla.",
                    new AcceptableValueRange<float>(0.1f, 5f)));

            EnableDelayMultiplier = Config.Bind(
                "Adrenaline",
                "EnableDelayMultiplier",
                false,
                "Enable the custom Adrenaline decay delay. " +
                "When disabled, the vanilla delay is used.");
                
            GainMultiplier = Config.Bind(
                "Adrenaline",
                "GainMultiplier",
                1.25f,
                new ConfigDescription(
                    "Multiplier for positive Adrenaline gains. " +
                    "1 is vanilla.",
                    new AcceptableValueRange<float>(0.1f, 5f)));

            DecayMultiplier.SettingChanged += OnSettingChanged;
            DelayMultiplier.SettingChanged += OnSettingChanged;
            EnableDelayMultiplier.SettingChanged += OnSettingChanged;
            GainMultiplier.SettingChanged += OnSettingChanged;

            _harmony = new Harmony("com.enki.adrenalinejunkie");
            _harmony.PatchAll();

            Logger.LogInfo("AdrenalineJunkie 1.0.2 loaded.");
        }

        private void OnSettingChanged(object sender, EventArgs args)
        {
            ApplyToLocalPlayer();
        }

        private static AnimationCurve CloneCurve(AnimationCurve source)
        {
            if (source == null)
                return null;

            var clone = new AnimationCurve(source.keys);
            clone.preWrapMode = source.preWrapMode;
            clone.postWrapMode = source.postWrapMode;

            return clone;
        }

        private static AnimationCurve ScaleCurve(
            AnimationCurve original,
            float multiplier)
        {
            if (original == null)
                return null;

            AnimationCurve result = CloneCurve(original);
            Keyframe[] keys = result.keys;

            for (int i = 0; i < keys.Length; i++)
            {
                Keyframe key = keys[i];

                key.value *= multiplier;

                // Tangents represent slopes, so scale them too
                // to preserve the original curve's shape.
                if (!float.IsInfinity(key.inTangent))
                    key.inTangent *= multiplier;

                if (!float.IsInfinity(key.outTangent))
                    key.outTangent *= multiplier;

                keys[i] = key;
            }

            result.keys = keys;
            return result;
        }

        internal static void ApplyToLocalPlayer()
        {
            Player player = Player.m_localPlayer;

            if (player == null)
                return;

            try
            {
                OriginalCurves original = Originals.GetValue(
                    player,
                    p => new OriginalCurves(p));

                // Use the stored originals every time.
                // This prevents multiplier stacking.
                player.m_adrenalineDegen = ScaleCurve(
                    original.Decay,
                    DecayMultiplier.Value);

                player.m_adrenalineDegenDelay = ScaleCurve(
                    original.Delay,
                    EnableDelayMultiplier.Value
                        ? DelayMultiplier.Value
                        : 1f);

                player.m_adrenalineGainMultiplier = ScaleCurve(
                    original.Gain,
                    GainMultiplier.Value);

                LogCurve("Decay", player.m_adrenalineDegen);
                LogCurve("Delay", player.m_adrenalineDegenDelay);
                LogCurve("Gain", player.m_adrenalineGainMultiplier);

                Log.LogInfo(
                    "AdrenalineJunkie applied: " +
                    "Decay=" + DecayMultiplier.Value +
                    "x, Delay=" +
                    (EnableDelayMultiplier.Value
                        ? DelayMultiplier.Value + "x"
                        : "Vanilla (disabled)") +
                    ", Gain=" + GainMultiplier.Value + "x");
            }
            catch (Exception ex)
            {
                Log.LogError(
                    "Failed to apply AdrenalineJunkie: " + ex);
            }
        }

        private static void LogCurve(string name, AnimationCurve curve)
        {
            if (curve == null || curve.length == 0)
            {
                Log.LogWarning(name + " curve is null or empty.");
                return;
            }

            Log.LogInfo(
                name + " curve: " + curve.length +
                " keys, at 0%=" + curve.Evaluate(0f) +
                ", at 50%=" + curve.Evaluate(0.5f) +
                ", at 100%=" + curve.Evaluate(1f));
        }

        private void OnDestroy()
        {
            if (DecayMultiplier != null)
                DecayMultiplier.SettingChanged -= OnSettingChanged;

            if (DelayMultiplier != null)
                DelayMultiplier.SettingChanged -= OnSettingChanged;

            if (GainMultiplier != null)
                GainMultiplier.SettingChanged -= OnSettingChanged;

            if (EnableDelayMultiplier != null)
                EnableDelayMultiplier.SettingChanged -= OnSettingChanged;

            if (_harmony != null)
                _harmony.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.SetLocalPlayer))]
    internal static class LocalPlayerPatch
    {
        private static void Postfix()
        {
            AdrenalineJunkiePlugin.ApplyToLocalPlayer();
        }
    }
}

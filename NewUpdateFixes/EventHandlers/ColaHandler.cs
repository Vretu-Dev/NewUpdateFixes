using LabApi.Events.Arguments.PlayerEvents;
using CustomPlayerEffects;
using PlayerStatsSystem;
using PlayerRoles.FirstPersonControl;

namespace NewUpdateFixes.EventHandlers
{
    public static class ColaHandler
    {
        public static void RegisterEvents()
        {
            LabApi.Events.Handlers.PlayerEvents.UsingItem += OnUsingItem;
            LabApi.Events.Handlers.PlayerEvents.Hurting += OnHurting;
        }
        public static void UnregisterEvents()
        {
            LabApi.Events.Handlers.PlayerEvents.UsingItem -= OnUsingItem;
            LabApi.Events.Handlers.PlayerEvents.Hurting -= OnHurting;
        }
        private static void OnHurting(PlayerHurtingEventArgs ev)
        {
            if (NewUpdateFixes.Instance.Config.OldColaHealthDrain)
            {
                if (ev.DamageHandler is UniversalDamageHandler universalDamage)
                {
                    if (universalDamage.TranslationId == DeathTranslations.Scp207.Id)
                    {
                        byte intensity = ev.Player.GetEffect<Scp207>().Intensity;

                        if (ev.Player.RoleBase is IFpcRole fpc)
                        {
                            PlayerMovementState state = fpc.FpcModule.CurrentMovementState;
                            float newDamage = 0f;

                            switch (intensity)
                            {
                                case 1:
                                    newDamage = GetDamageBasedOnMovementState(state, 0.1f, 0.15f, 0.4f, 1f);
                                    break;
                                case 2:
                                    newDamage = GetDamageBasedOnMovementState(state, 0.15f, 0.23f, 0.6f, 1.5f);
                                    break;
                                case 3:
                                    newDamage = GetDamageBasedOnMovementState(state, 0.25f, 0.38f, 1f, 2.5f);
                                    break;
                            }
                            //universalDamage.Damage = newDamage;
                            // Now reflection to set the inaccessible property
                            var damageProperty = typeof(UniversalDamageHandler).GetProperty("Damage", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                            if (damageProperty != null && damageProperty.CanWrite)
                            {
                                damageProperty.SetValue(universalDamage, newDamage);
                            }
                        }
                    }
                }
            }
        }
        private static float GetDamageBasedOnMovementState(PlayerMovementState state, float crouchingDamage, float sneakingDamage, float walkingDamage, float sprintingDamage)
        {
            switch (state)
            {
                case PlayerMovementState.Crouching:
                    return crouchingDamage;
                case PlayerMovementState.Sneaking:
                    return sneakingDamage;
                case PlayerMovementState.Walking:
                    return walkingDamage;
                case PlayerMovementState.Sprinting:
                    return sprintingDamage;
                default:
                    return 0f;
            }
        }
        private static void OnUsingItem(PlayerUsingItemEventArgs ev)
        {
            if (NewUpdateFixes.Instance.Config.OldColaSpeed)
            {
                byte intensity = ev.Player.GetEffect<Scp207>().Intensity;

                if (ev.UsableItem.Type == ItemType.SCP207)
                {
                    if (intensity == 0)
                    {
                        ev.Player.EnableEffect<MovementBoost>(4);
                    }
                    else if (intensity == 1)
                    {
                        ev.Player.DisableEffect<MovementBoost>();
                        ev.Player.EnableEffect<MovementBoost>(6);
                    }
                    else if (intensity == 2)
                    {
                        ev.Player.DisableEffect<MovementBoost>();
                        ev.Player.EnableEffect<MovementBoost>(3);
                    }
                }
            }
            if (ev.UsableItem.Type == ItemType.SCP500)
            {
                if (NewUpdateFixes.Instance.Config.Scp500CuresTrauma)
                {
                    ev.Player.DisableEffect<Traumatized>();
                }
                if (NewUpdateFixes.Instance.Config.OldColaSpeed)
                {
                    ev.Player.DisableEffect<MovementBoost>();
                }
            }
        }
    }
}
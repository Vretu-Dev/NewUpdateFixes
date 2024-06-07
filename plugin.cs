using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp914;
using InventorySystem;
using System;

namespace NewUpdateFixes
{
    public class ColaFix : Plugin<Config>
    {
        /// <inheritdoc>
        internal GenHandler GenHandler { get; private set; }

        /// <inheritdoc>
        public override string Name => "NewUpdateFixes";

        /// <inheritdoc>
        public override string Prefix => "NUF";

        /// <inheritdoc>
        public override string Author => "Half";

        /// <inheritdoc>
        public override Version Version => new Version(1, 0, 0);

        /// <inheritdoc>
        public override Version RequiredExiledVersion => new Version(8, 9, 2);

        /// <inheritdoc>
        public override void OnEnabled()
        {

            base.OnEnabled();

            SubscribeEvents();
        }

        /// <inheritdoc>
        public override void OnDisabled()
        {

            UnsubscribeEvents();

            base.OnDisabled();
        }

        /// <inheritdoc>
        protected void SubscribeEvents()
        {

            GenHandler = new(Config);

            Exiled.Events.Handlers.Player.UsingItem += GenHandler.OnUsingItem;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += GenHandler.OnUpgradingInventoryItem;
            Exiled.Events.Handlers.Scp914.UpgradingPickup += GenHandler.OnUpgradingPickup;



        }

        /// <inheritdoc>
        protected void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= GenHandler.OnUsingItem;
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem -= GenHandler.OnUpgradingInventoryItem;
            Exiled.Events.Handlers.Scp914.UpgradingPickup -= GenHandler.OnUpgradingPickup;

            GenHandler = null;
        }
    }


    internal class GenHandler
    {
        private Config config;
        internal GenHandler(Config instance)
        {
            config = instance;
        }

        //internal void UpgradeItemInventory(ItemType input, ItemType result, Scp914.Scp914KnobSetting knobSetting, int chance, UpgradingInventoryItemEventArgs ev )
        //{
        //    if (ev.KnobSetting == knobSetting && ev.Item.Type == input)
        //    {
        //        ev.Player.RemoveItem(ev.Item);
        //        var random = UnityEngine.Random.Range(1, chance);
        //        if (random == 1)
        //        {
        //            ev.Player.Inventory.ServerAddItem(result);
        //        }
        //    }

        //}

        internal void UpgradeItemFloor(ItemType input, ItemType result, Scp914.Scp914KnobSetting knobSetting, int chance, UpgradingPickupEventArgs ev)
        {
            if (ev.KnobSetting == knobSetting && ev.Pickup.Type == input)
            {
                var random = UnityEngine.Random.Range(1, chance);
                if (random == 1)
                {
                    Exiled.API.Features.Pickups.Pickup.CreateAndSpawn(result, ev.OutputPosition, ev.Pickup.Rotation);
                }

                ev.Pickup.Destroy();
            }

        }

        internal void UpgradeItemHand(ItemType input, ItemType result, Scp914.Scp914KnobSetting knobSetting, int chance, UpgradingInventoryItemEventArgs ev)
        {
            if (ev.KnobSetting == knobSetting && ev.Item.Type == input)
            {
                ev.Player.CurrentItem.Destroy();
                var random = UnityEngine.Random.Range(1, chance);
                if (random == 1)
                {
                    ev.Player.Inventory.ServerAddItem(result);
                }
            }

        }


        internal void OnUpgradingPickup(UpgradingPickupEventArgs ev)
        {
            if (config.OldColaRecipes914Dropped)
            {

                UpgradeItemFloor(ItemType.GrenadeFlash, ItemType.SCP207, Scp914.Scp914KnobSetting.VeryFine, 4, ev);

                UpgradeItemFloor(ItemType.Adrenaline, ItemType.SCP1853, Scp914.Scp914KnobSetting.VeryFine, 3, ev);

                UpgradeItemFloor(ItemType.SCP1853, ItemType.SCP207, Scp914.Scp914KnobSetting.Fine, 1, ev);

            }

        }

        internal void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            //if (config.OldColaRecipes914Inv)
            //{
            //    UpgradeItemInventory(ItemType.Coin, ItemType.SCP207, Scp914.Scp914KnobSetting.Fine, 1, ev);

            //    //UpgradeItemInventory(ItemType.GrenadeFlash, ItemType.SCP207, Scp914.Scp914KnobSetting.VeryFine, 4, ev);

            //    //UpgradeItemInventory(ItemType.Adrenaline, ItemType.SCP1853, Scp914.Scp914KnobSetting.VeryFine, 3, ev);

            //    //UpgradeItemInventory(ItemType.SCP1853, ItemType.SCP207, Scp914.Scp914KnobSetting.Fine, 1, ev);

            //}

            if (config.OldColaRecipes914Hand)
            {
                UpgradeItemHand(ItemType.GrenadeFlash, ItemType.SCP207, Scp914.Scp914KnobSetting.VeryFine, 4, ev);

                UpgradeItemHand(ItemType.Adrenaline, ItemType.SCP1853, Scp914.Scp914KnobSetting.VeryFine, 3, ev);

                UpgradeItemHand(ItemType.SCP1853, ItemType.SCP207, Scp914.Scp914KnobSetting.Fine, 1, ev);


            }
            

        }

        internal void OnUsingItem(UsingItemEventArgs ev)
        {
            
            byte intensity = ev.Player.GetEffect(EffectType.Scp207).Intensity;

            if (config.OldColaSpeed)
            {
                if (ev.Item.Type == ItemType.SCP207)
                {

                    
                    if (intensity == 0)
                    {

                        ev.Player.EnableEffect(EffectType.MovementBoost, 9);
                    }
                    else if (intensity == 1)
                    {
                        ev.Player.DisableEffect(EffectType.MovementBoost);
                        ev.Player.EnableEffect(EffectType.MovementBoost, 15);
                    }
                    else if (intensity == 2)
                    {
                        ev.Player.DisableEffect(EffectType.MovementBoost);
                        ev.Player.EnableEffect(EffectType.MovementBoost, 7);
                    }

                }
            }


            if (ev.Item.Type == ItemType.SCP500)
            {

                if (config.Scp500CuresTrauma)
                {
                    ev.Player.DisableEffect(EffectType.Traumatized);
                }

                if (config.OldColaSpeed) 
                {
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                }
            }
        }
    }
}

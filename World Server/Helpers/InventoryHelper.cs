using Framework.Database.Tables;
using Platform;

namespace World_Server.Helpers
{
    enum WoWEquipSlot : byte
    {
        None,
        Head,
        Neck,
        Shoulders,
        Shirt,
        Vest,
        Waist,
        Legs,
        Feet,
        Wrist,
        Hands,
        Ring,
        Trinket,
        Onehand,
        Shield,
        Bow,
        Back,
        Twohand,
        Bag,
        Tabbard,
        Robe,
        Mainhand,
        Offhand,
        Held,
        Ammo,
        Thrown,
        Ranged,
        Ranged2,
        Relic
    }

    public class InventoryHelper
    {
        public static WorldItems[] GenerateInventoryByIDs(string ids)
        {
            if (ids == null) return null;

            string[] itemList = ids.Split(",");

            WorldItems[] inventory = new WorldItems[19];
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i].Length > 0)
                {
                    var itemEntry = itemList[i];
                    WorldItems item = Program.Database.GetItem(int.Parse(itemEntry));
                    if (item != null)
                    {
                        switch ((WoWEquipSlot)item.InventoryType)
                        {
                            case WoWEquipSlot.Head:
                                inventory[0] = item;
                                break;
                            case WoWEquipSlot.Shirt:
                                inventory[3] = item;
                                break;
                            case WoWEquipSlot.Vest:
                            case WoWEquipSlot.Robe:
                                inventory[4] = item;
                                break;
                            case WoWEquipSlot.Waist:
                                inventory[5] = item;
                                break;
                            case WoWEquipSlot.Legs:
                                inventory[6] = item;
                                break;
                            case WoWEquipSlot.Feet:
                                inventory[7] = item;
                                break;
                            case WoWEquipSlot.Wrist:
                                inventory[8] = item;
                                break;
                            case WoWEquipSlot.Hands:
                                inventory[9] = item;
                                break;
                            case WoWEquipSlot.Ring:
                                inventory[10] = item;
                                break;
                            case WoWEquipSlot.Trinket:
                                inventory[12] = item;
                                break;
                            case WoWEquipSlot.Mainhand:
                            case WoWEquipSlot.Onehand:
                            case WoWEquipSlot.Twohand:
                                inventory[15] = item;
                                break;
                            case WoWEquipSlot.Offhand:
                            case WoWEquipSlot.Shield:
                            case WoWEquipSlot.Bow:
                                inventory[16] = item;
                                break;
                        }
                    }
                }
            }
            return inventory;
        }

    }
}

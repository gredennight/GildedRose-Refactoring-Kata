using System.Collections.Generic;
using System;
namespace csharp
{
    public class GildedRose
    {
        const int MinQuality = 0;
        const int MaxQuality = 50;
        enum ItemSpecialCases
        {
            None,
            AgedBrie,
            Sulfuras, 
            BackstagePasses, 
            Conjured
        }
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }
        ItemSpecialCases GetItemName(string name)
        {
            switch (name)
            {
                case "Aged Brie":
                    return ItemSpecialCases.AgedBrie;
                    break;
                case "Sulfuras, Hand of Ragnaros":
                    return ItemSpecialCases.Sulfuras;
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    return ItemSpecialCases.BackstagePasses;
                    break;
                case "Conjured Mana Cake":
                    return ItemSpecialCases.Conjured;
                    break;
                default:
                    return ItemSpecialCases.None;
                    break;
            }

        }
        void UpdateItem(ref Item item, int sellInValueFactor, int qualityValueFactor)
        {
            item.SellIn += sellInValueFactor;
            item.Quality = Math.Max(MinQuality, Math.Min(MaxQuality, item.Quality + qualityValueFactor));

        }
        bool IsExpired(Item item)
        {
            bool output = true;
            if (item.SellIn > 0)
                output = false;
            return output;
        }
        Item GetNewAgedBrie(Item item)
        {
            if (IsExpired(item))
                UpdateItem(ref item, -1, 2);
            else
                UpdateItem(ref item, -1, 1);
            return item;
        }
        Item GetNewBackstagesPasses(Item item)
        {
            if (IsExpired(item))
            {
                UpdateItem(ref item, -1, 0);
                item.Quality = 0;
            }
            else
            {
                if (item.SellIn <= 10 && item.SellIn > 5)
                {
                    UpdateItem(ref item, -1, 2);
                }
                else if (item.SellIn <= 5)
                {
                    UpdateItem(ref item, -1, 3);
                }
                else
                {
                    UpdateItem(ref item, -1, 1);
                }

            }
            return item;
        }
        Item GetNewConjured(Item item)
        {
            if (IsExpired(item))
            {
                UpdateItem(ref item, -1, -4);
            }
            else
            {
                UpdateItem(ref item, -1, -2);
            }
            return item;
        }
        Item GetNewDefault(Item item)
        {
            if (IsExpired(item))
            {
                UpdateItem(ref item, -1, -2);
            }
            else
            {
                UpdateItem(ref item, -1, -1);
            }
            return item;
        }
        public void UpdateQuality()
        {
            for (int itemIndex = 0; itemIndex < Items.Count; itemIndex++)
            {
                switch (GetItemName(Items[itemIndex].Name))
                {
                    case ItemSpecialCases.AgedBrie:
                        Items[itemIndex] = GetNewAgedBrie(Items[itemIndex]);
                        break;
                    case ItemSpecialCases.Sulfuras:
                        break;
                    case ItemSpecialCases.BackstagePasses:
                        Items[itemIndex] = GetNewBackstagesPasses(Items[itemIndex]);
                        break;
                    case ItemSpecialCases.Conjured:
                        Items[itemIndex] = GetNewConjured(Items[itemIndex]);
                        break;
                    case ItemSpecialCases.None:
                        Items[itemIndex] = GetNewDefault(Items[itemIndex]);
                        break;

                }
            }
        }
    }
}

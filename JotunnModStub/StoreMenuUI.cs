using System;
using UnityEngine;
using Jotunn.Managers;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace JotunnModStub
{
    class StoreMenuUI
    {
        public static GameObject activePanel;

        public static GameObject Render(List<Container> inventories, Func<ItemDrop.ItemData, bool> onPurchaseItem)
        {
            if (isActive())
            {
                Unmount();
            }
            activePanel = CreatePanel();
            RenderItems(activePanel, inventories, onPurchaseItem);
            activePanel.SetActive(true);
            return activePanel;
        }

        public static bool isActive()
        {
            return activePanel != null;
        }

        public static void Unmount ()
        {
            if (activePanel != null)
            {
                activePanel.SetActive(false);
                activePanel = null;
            }
        }

        private static void RenderItems(GameObject panel, List<Container> inventories, Func<ItemDrop.ItemData, bool> onPurchaseItem)
        {
            var offset = 0f;
            inventories.ForEach(inventory =>
            {
                inventory.GetInventory().GetAllItems().ForEach(item =>
                {
                    RenderItem(panel, item, offset, () => onPurchaseItem(item));
                    offset += 30f;
                });
            });
        }

        private static GameObject RenderItem(GameObject parent, ItemDrop.ItemData item, float offset, UnityAction onClick)
        {
            var price = "$10";
            var name = item.m_dropPrefab;
            var count = item.m_stack;
            var title = price + " - (" + count + "x) " + name;
            // Button as a component, on click listener
            var button = GUIManager.Instance.CreateButton(
                title,
                parent.transform,
                new Vector2(0.2f, 0f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0f, offset),
                300,
                25
            );
            button.GetComponent<Button>().onClick.AddListener(onClick);
            button.SetActive(true);
            return button;
        }

        private static GameObject CreatePanel()
        {
            return GUIManager.Instance.CreateWoodpanel(
                GUIManager.PixelFix.transform,
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0f, 0f),
                500f, 300f
            );
        }
    }
}

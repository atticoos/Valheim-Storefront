using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jotunn.Entities;
using UnityEngine;

namespace JotunnModStub
{
    class StoreManager
    {

        private static StoreManager instance;
        
        public static StoreManager GetInstance ()
        {
            if (instance == null)
            {
                instance = new StoreManager();
            }
            return instance;
        }

        public void TogglePointOfSale(Container container)
        {
            if (isOpen()) {
                ClosePointOfSale();
            } else {
                OpenPointOfSale(container);
            }
        }

        public void OpenPointOfSale(Container container)
        {
            StoreMenuUI.Render(InventoryManager.GetInstance().GetInventories(), PurchaseItem);
        }

        public void ClosePointOfSale()
        {
            StoreMenuUI.Unmount();
        }

        public bool isOpen()
        {
            return StoreMenuUI.isActive();
        }

        public bool PurchaseItem(ItemDrop.ItemData item)
        {
            if (CashRegisterManager.GetInstance().TransferCoinsToCashRegister())
            {
                InventoryManager.GetInstance().TransferItemToPlayer(item);
                StoreMenuUI.Render(InventoryManager.GetInstance().GetInventories(), PurchaseItem);
                return true;
            }
            return false;
        }

    }
}

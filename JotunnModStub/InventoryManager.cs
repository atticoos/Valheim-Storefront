using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jotunn.Entities;
using UnityEngine;

namespace JotunnModStub
{
    class InventoryManager
    {
        private static InventoryManager instance;

        public static InventoryManager GetInstance()
        {
            if (instance == null)
            {
                instance = new InventoryManager();
            }
            return instance;
        }

        public bool TransferItemToPlayer(ItemDrop.ItemData item)
        {
            Player player = Player.m_localPlayer;
            List<Container> containers = GetInventories();
            foreach (Container container in containers)
            {
                foreach(ItemDrop.ItemData inventoryItem in container.GetInventory().GetAllItems())
                {
                    if (inventoryItem == item)
                    {
                        player.GetInventory().MoveItemToThis(container.GetInventory(), item);
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Container> GetInventories()
        {
            Vector3 position = Player.m_localPlayer.transform.position;
            List<Piece> nearbyPieces = new List<Piece>();
            Piece.GetAllPiecesInRadius(position, 10f, nearbyPieces);

            List<Container> nearbyContainers = new List<Container>();
            foreach (Piece piece in nearbyPieces)
            {
                if (piece.TryGetComponent<Container>(out Container container))
                {
                    if (container.m_piece.m_name == "Storefront - Inventory")
                    {
                        nearbyContainers.Add(container);
                    }
                }
            }
            return nearbyContainers;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JotunnModStub
{
    class CashRegisterManager
    {
        private static CashRegisterManager instance;

        public static CashRegisterManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CashRegisterManager();
            }
            return instance;
        }

        public Boolean TransferCoinsToCashRegister()
        {
            Player currentPlayer = Player.m_localPlayer;
            List<Container> containers = GetCashRegisters();

            List<ItemDrop.ItemData> coinStacks = currentPlayer.GetInventory().GetAllItems()
                .FindAll(item => item.m_dropPrefab.name == "Coins");

            if (coinStacks.Count <= 0) {
                return false;
            }

            foreach (Container container in containers) {
                var transferred = container.GetInventory().MoveItemToThis(
                    currentPlayer.GetInventory(),
                    coinStacks[0],
                    10, // TODO: Transfer actual coin price
                    1,
                    1
                );

                if (transferred) {
                    return true;
                }
            }

            return false;
        }

        public List<Container> GetCashRegisters()
        {
            Vector3 position = Player.m_localPlayer.transform.position;
            List<Piece> nearbyPieces = new List<Piece>();
            Piece.GetAllPiecesInRadius(position, 30f, nearbyPieces);

            List<Container> nearbyContainers = new List<Container>();

            nearbyPieces.ForEach(piece => {
                if (piece.TryGetComponent<Container>(out Container container)) {
                    if (StorefrontPieces.IsCashRegisterPiece(container.m_piece)) {
                        nearbyContainers.Add(container);
                    }
                }
            });
            return nearbyContainers;
        }
    }
}

using System;
using Jotunn.Entities;
using Jotunn.Managers;

namespace JotunnModStub
{
    class StorefrontPieces
    {
        public static void InitializeAllPieces()
        {
            InitializePointOfSalePiece();
            InitializeCashRegisterPiece();
            InitializeInventoryPiece();
        }
        public static void InitializePointOfSalePiece()
        {
            InitializePiece(
                "StorefrontChest", 
                "Storefront - Point of Sale", 
                "A chest for you to conduct business."
            );
        }

        public static void InitializeInventoryPiece()
        {
            InitializePiece(
                "StorefrontInventory",
                "Storefront - Inventory",
                "A chest that holds your sellable inventory."
            );
        }

        public static void InitializeCashRegisterPiece()
        {
            InitializePiece(
                "StorefrontCashRegister",
                "Storefront - Cash Register",
                "A chest that holds your coins from sales."
            );
        }

        public static bool IsPointOfSalePiece(Piece piece)
        {
            return piece.m_name == "Storefront - Point of Sale";
        }

        public static bool IsInventoryPiece(Piece piece)
        {
            return piece.m_name == "Storefront - Inventory";
        }

        public static bool IsCashRegisterPiece(Piece piece)
        {
            return piece.m_name == "Storefront - Cash Register";
        }

        private static void InitializePiece(String key, String name, String desc)
        {
            CustomPiece CP = new CustomPiece(key, "piece_chest_wood", "Hammer");
            CP.Piece.m_name = name;
            CP.Piece.m_description = desc;
            CP.Piece.m_resources = new Piece.Requirement[]
            {
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("Wood"),
                    m_amount = 1
                }
            };
            PieceManager.Instance.AddPiece(CP);
        }
    }
}

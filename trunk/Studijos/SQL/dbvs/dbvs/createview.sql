CREATE VIEW kauo4157.InventoryPositions AS
    SELECT Inventory.ID, MAX(Item.Card) AS Card, Count(*) AS Quantity, 
           Item.Position, SUM(Card.Use_Times - Item.Usage) AS Times_Left
    FROM kauo4157.Inventory, kauo4157.Item, kauo4157.Card
    WHERE Item.Inventory = Inventory.ID AND Card.Nr = Item.Card
    GROUP BY Inventory.ID, Item.Position;

CREATE VIEW kauo4157.UserItems AS
    SELECT UserVsInventory.User, Item.Nr, Item.Inventory, Item.Position
    FROM kauo4157.Item, kauo4157.UserVsInventory
    WHERE Item.Inventory = UserVsInventory.Inventory;

CREATE VIEW kauo4157.InventoryTypes AS
SELECT ID, (

CASE WHEN TYPE =1
THEN 'Bag'
WHEN TYPE =2
THEN 'Bank'
WHEN TYPE =3
THEN 'Box'
ELSE 'Inventory'
END 
) AS 
Type , Size
FROM kauo4157.Inventory;
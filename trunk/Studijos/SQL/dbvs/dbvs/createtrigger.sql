CREATE TRIGGER kauo4157.ChangeItemPosition
    NO CASCADE BEFORE UPDATE ON kauo4157.Item
    REFERENCING NEW AS NewTbl
    FOR EACH ROW MODE DB2SQL
    WHEN (NewTbl.Inventory NOT NULL AND 
          NewTbl.Position > (
            SELECT Size From kauo4157.Inventory WHERE Inventory.ID = NewTbl.Inventory
          )
         )
    SIGNAL SQLSTATE '88888'('Inventory doesn't have such position!')
    WHEN (
          NewTbl.Card <> COALESCE((
            SELECT Card FROM InventoryPositions 
            WHERE InventoryPositions.Nr = NewTbl.Inventory 
              AND InventoryPositions.Position = NewTbl.Position
          ), NewTbl.Card))
    SIGNAL SQLSTATE '88889'('There are different card in this position!!');

CREATE TRIGGER kauo4157.InsertItem
    NO CASCADE BEFORE INSERT ON kauo4157.Item
    REFERENCING NEW AS NewTbl
    FOR EACH ROW MODE DB2SQL
    WHEN (NewTbl.Inventory NOT NULL AND 
          NewTbl.Position > (
            SELECT Size From kauo4157.Inventory WHERE Inventory.ID = NewTbl.Inventory
          )
         )
    SIGNAL SQLSTATE '88888'('Inventory doesn't have such position!')
    WHEN (
          NewTbl.Card <> COALESCE((
            SELECT Card FROM InventoryPositions 
            WHERE InventoryPositions.Nr = NewTbl.Inventory 
              AND InventoryPositions.Position = NewTbl.Position
          ), NewTbl.Card))
    SIGNAL SQLSTATE '88889'('There are different card in this position!!');
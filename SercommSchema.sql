CREATE DATABASE IF NOT EXISTS sercomm;
USE sercomm;

DROP TABLE IF EXISTS SercommInventoryCategory;

CREATE TABLE SercommInventoryCategory (
  Id int NOT NULL AUTO_INCREMENT,
  Name text NOT NULL,
  CreationDate datetime not null default current_timestamp,
  PRIMARY KEY (Id)
);

DROP TABLE IF EXISTS SercommInventoryItem;

CREATE TABLE SercommInventoryItem (
  Id int NOT NULL AUTO_INCREMENT,
  Name text NOT NULL,
  Make varchar(50),
  CreationDate datetime not null default current_timestamp,
  SercommInventoryCategoryId int,
  PRIMARY KEY (Id),
  FOREIGN KEY (SercommInventoryCategoryId) REFERENCES SercommInventoryCategory(Id)
);

DROP TABLE IF EXISTS SercommInventoryItemEntry;

CREATE TABLE SercommInventoryItemEntry (
  SercommInventoryEntryId int NOT NULL,
  SercommInventoryItemId int NOT NULL,
  Quantity int NOT NULL,
  CreationDate datetime not null default current_timestamp,
  FOREIGN KEY (SercommInventoryItemId) REFERENCES SercommInventoryItem(Id),
  FOREIGN KEY (SercommInventoryEntryId) REFERENCES SercommInventoryEntry(Id),
  PRIMARY KEY (SercommInventoryEntryId, SercommInventoryItemId)
);

DROP TABLE IF EXISTS SercommInventoryEntry;

CREATE TABLE SercommInventoryEntry (
  Id int NOT NULL AUTO_INCREMENT,
  ReservationNumber int DEFAULT NULL,
  EmployeeId int DEFAULT NULL,
  ReservationPdfFilePath text,
  CreationDate datetime not null default current_timestamp,
  PRIMARY KEY (Id)
);



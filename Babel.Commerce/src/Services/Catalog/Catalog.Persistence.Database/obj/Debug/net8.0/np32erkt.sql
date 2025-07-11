IF OBJECT_ID(N'[Catalog].[__EFMigrationsHistory]') IS NULL
BEGIN
    IF SCHEMA_ID(N'Catalog') IS NULL EXEC(N'CREATE SCHEMA [Catalog];');
    CREATE TABLE [Catalog].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Catalog') IS NULL EXEC(N'CREATE SCHEMA [Catalog];');
GO

CREATE TABLE [Catalog].[Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Price] decimal(18,4) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);
GO

CREATE TABLE [Catalog].[Stocks] (
    [ProductInStockId] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Stock] int NOT NULL,
    CONSTRAINT [PK_Stocks] PRIMARY KEY ([ProductInStockId]),
    CONSTRAINT [FK_Stocks_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Catalog].[Products] ([ProductId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Description', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Catalog].[Products]'))
    SET IDENTITY_INSERT [Catalog].[Products] ON;
INSERT INTO [Catalog].[Products] ([ProductId], [Description], [Name], [Price])
VALUES (1, N' Description for product 1', N'Product1', 559.0),
(2, N' Description for product 2', N'Product2', 934.0),
(3, N' Description for product 3', N'Product3', 846.0),
(4, N' Description for product 4', N'Product4', 587.0),
(5, N' Description for product 5', N'Product5', 718.0),
(6, N' Description for product 6', N'Product6', 985.0),
(7, N' Description for product 7', N'Product7', 785.0),
(8, N' Description for product 8', N'Product8', 242.0),
(9, N' Description for product 9', N'Product9', 650.0),
(10, N' Description for product 10', N'Product10', 979.0),
(11, N' Description for product 11', N'Product11', 191.0),
(12, N' Description for product 12', N'Product12', 973.0),
(13, N' Description for product 13', N'Product13', 241.0),
(14, N' Description for product 14', N'Product14', 956.0),
(15, N' Description for product 15', N'Product15', 594.0),
(16, N' Description for product 16', N'Product16', 494.0),
(17, N' Description for product 17', N'Product17', 354.0),
(18, N' Description for product 18', N'Product18', 508.0),
(19, N' Description for product 19', N'Product19', 190.0),
(20, N' Description for product 20', N'Product20', 176.0),
(21, N' Description for product 21', N'Product21', 239.0),
(22, N' Description for product 22', N'Product22', 901.0),
(23, N' Description for product 23', N'Product23', 721.0),
(24, N' Description for product 24', N'Product24', 731.0),
(25, N' Description for product 25', N'Product25', 351.0),
(26, N' Description for product 26', N'Product26', 233.0),
(27, N' Description for product 27', N'Product27', 545.0),
(28, N' Description for product 28', N'Product28', 60.0),
(29, N' Description for product 29', N'Product29', 268.0),
(30, N' Description for product 30', N'Product30', 541.0),
(31, N' Description for product 31', N'Product31', 731.0),
(32, N' Description for product 32', N'Product32', 121.0),
(33, N' Description for product 33', N'Product33', 239.0),
(34, N' Description for product 34', N'Product34', 324.0),
(35, N' Description for product 35', N'Product35', 583.0),
(36, N' Description for product 36', N'Product36', 520.0),
(37, N' Description for product 37', N'Product37', 816.0),
(38, N' Description for product 38', N'Product38', 418.0),
(39, N' Description for product 39', N'Product39', 550.0),
(40, N' Description for product 40', N'Product40', 777.0),
(41, N' Description for product 41', N'Product41', 514.0),
(42, N' Description for product 42', N'Product42', 478.0);
INSERT INTO [Catalog].[Products] ([ProductId], [Description], [Name], [Price])
VALUES (43, N' Description for product 43', N'Product43', 107.0),
(44, N' Description for product 44', N'Product44', 605.0),
(45, N' Description for product 45', N'Product45', 354.0),
(46, N' Description for product 46', N'Product46', 703.0),
(47, N' Description for product 47', N'Product47', 166.0),
(48, N' Description for product 48', N'Product48', 840.0),
(49, N' Description for product 49', N'Product49', 372.0),
(50, N' Description for product 50', N'Product50', 517.0),
(51, N' Description for product 51', N'Product51', 339.0),
(52, N' Description for product 52', N'Product52', 782.0),
(53, N' Description for product 53', N'Product53', 110.0),
(54, N' Description for product 54', N'Product54', 928.0),
(55, N' Description for product 55', N'Product55', 610.0),
(56, N' Description for product 56', N'Product56', 667.0),
(57, N' Description for product 57', N'Product57', 483.0),
(58, N' Description for product 58', N'Product58', 644.0),
(59, N' Description for product 59', N'Product59', 65.0),
(60, N' Description for product 60', N'Product60', 614.0),
(61, N' Description for product 61', N'Product61', 580.0),
(62, N' Description for product 62', N'Product62', 91.0),
(63, N' Description for product 63', N'Product63', 187.0),
(64, N' Description for product 64', N'Product64', 350.0),
(65, N' Description for product 65', N'Product65', 177.0),
(66, N' Description for product 66', N'Product66', 526.0),
(67, N' Description for product 67', N'Product67', 677.0),
(68, N' Description for product 68', N'Product68', 972.0),
(69, N' Description for product 69', N'Product69', 107.0),
(70, N' Description for product 70', N'Product70', 789.0),
(71, N' Description for product 71', N'Product71', 271.0),
(72, N' Description for product 72', N'Product72', 636.0),
(73, N' Description for product 73', N'Product73', 140.0),
(74, N' Description for product 74', N'Product74', 397.0),
(75, N' Description for product 75', N'Product75', 233.0),
(76, N' Description for product 76', N'Product76', 837.0),
(77, N' Description for product 77', N'Product77', 680.0),
(78, N' Description for product 78', N'Product78', 568.0),
(79, N' Description for product 79', N'Product79', 657.0),
(80, N' Description for product 80', N'Product80', 246.0),
(81, N' Description for product 81', N'Product81', 966.0),
(82, N' Description for product 82', N'Product82', 581.0),
(83, N' Description for product 83', N'Product83', 171.0),
(84, N' Description for product 84', N'Product84', 321.0);
INSERT INTO [Catalog].[Products] ([ProductId], [Description], [Name], [Price])
VALUES (85, N' Description for product 85', N'Product85', 718.0),
(86, N' Description for product 86', N'Product86', 472.0),
(87, N' Description for product 87', N'Product87', 394.0),
(88, N' Description for product 88', N'Product88', 390.0),
(89, N' Description for product 89', N'Product89', 800.0),
(90, N' Description for product 90', N'Product90', 495.0),
(91, N' Description for product 91', N'Product91', 194.0),
(92, N' Description for product 92', N'Product92', 117.0),
(93, N' Description for product 93', N'Product93', 490.0),
(94, N' Description for product 94', N'Product94', 498.0),
(95, N' Description for product 95', N'Product95', 744.0),
(96, N' Description for product 96', N'Product96', 163.0),
(97, N' Description for product 97', N'Product97', 530.0),
(98, N' Description for product 98', N'Product98', 602.0),
(99, N' Description for product 99', N'Product99', 993.0),
(100, N' Description for product 100', N'Product100', 191.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Description', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Catalog].[Products]'))
    SET IDENTITY_INSERT [Catalog].[Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductInStockId', N'ProductId', N'Stock') AND [object_id] = OBJECT_ID(N'[Catalog].[Stocks]'))
    SET IDENTITY_INSERT [Catalog].[Stocks] ON;
INSERT INTO [Catalog].[Stocks] ([ProductInStockId], [ProductId], [Stock])
VALUES (1, 1, 40),
(2, 2, 13),
(3, 3, 21),
(4, 4, 15),
(5, 5, 27),
(6, 6, 46),
(7, 7, 12),
(8, 8, 29),
(9, 9, 46),
(10, 10, 7),
(11, 11, 32),
(12, 12, 2),
(13, 13, 9),
(14, 14, 49),
(15, 15, 7),
(16, 16, 9),
(17, 17, 17),
(18, 18, 44),
(19, 19, 16),
(20, 20, 35),
(21, 21, 48),
(22, 22, 21),
(23, 23, 45),
(24, 24, 14),
(25, 25, 26),
(26, 26, 43),
(27, 27, 19),
(28, 28, 24),
(29, 29, 19),
(30, 30, 39),
(31, 31, 45),
(32, 32, 0),
(33, 33, 44),
(34, 34, 7),
(35, 35, 37),
(36, 36, 17),
(37, 37, 48),
(38, 38, 23),
(39, 39, 38),
(40, 40, 12),
(41, 41, 45),
(42, 42, 10);
INSERT INTO [Catalog].[Stocks] ([ProductInStockId], [ProductId], [Stock])
VALUES (43, 43, 38),
(44, 44, 10),
(45, 45, 10),
(46, 46, 24),
(47, 47, 3),
(48, 48, 9),
(49, 49, 29),
(50, 50, 0),
(51, 51, 45),
(52, 52, 35),
(53, 53, 24),
(54, 54, 46),
(55, 55, 27),
(56, 56, 45),
(57, 57, 2),
(58, 58, 39),
(59, 59, 24),
(60, 60, 14),
(61, 61, 8),
(62, 62, 19),
(63, 63, 10),
(64, 64, 40),
(65, 65, 7),
(66, 66, 42),
(67, 67, 44),
(68, 68, 1),
(69, 69, 25),
(70, 70, 10),
(71, 71, 21),
(72, 72, 44),
(73, 73, 31),
(74, 74, 2),
(75, 75, 12),
(76, 76, 17),
(77, 77, 38),
(78, 78, 31),
(79, 79, 17),
(80, 80, 45),
(81, 81, 32),
(82, 82, 14),
(83, 83, 22),
(84, 84, 46);
INSERT INTO [Catalog].[Stocks] ([ProductInStockId], [ProductId], [Stock])
VALUES (85, 85, 25),
(86, 86, 32),
(87, 87, 17),
(88, 88, 38),
(89, 89, 30),
(90, 90, 33),
(91, 91, 29),
(92, 92, 11),
(93, 93, 28),
(94, 94, 18),
(95, 95, 29),
(96, 96, 9),
(97, 97, 33),
(98, 98, 22),
(99, 99, 14),
(100, 100, 41);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductInStockId', N'ProductId', N'Stock') AND [object_id] = OBJECT_ID(N'[Catalog].[Stocks]'))
    SET IDENTITY_INSERT [Catalog].[Stocks] OFF;
GO

CREATE INDEX [IX_Products_ProductId] ON [Catalog].[Products] ([ProductId]);
GO

CREATE UNIQUE INDEX [IX_Stocks_ProductId] ON [Catalog].[Stocks] ([ProductId]);
GO

INSERT INTO [Catalog].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250425182329_InicialCarritoProductosDB', N'8.0.15');
GO

COMMIT;
GO


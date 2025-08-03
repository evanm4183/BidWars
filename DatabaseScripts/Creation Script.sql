CREATE TABLE [Product] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ProductCategoryId] int NOT NULL,
  [ProductName] varchar(100) NOT NULL,
  [Description] varchar(200) NOT NULL
)
GO

CREATE TABLE [ProductCategory] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [CategoryName] varchar(20) NOT NULL
)
GO

CREATE TABLE [Auction] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ProductId] int NOT NULL,
  [LocationId] int NOT NULL,
  [StartingAmount] money NOT NULL,
  [StartTime] datetime2 NOT NULL,
  [EndTime] datetime2 NOT NULL
)
GO

CREATE TABLE [Bid] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [AuctionId] int NOT NULL,
  [BidderId] nvarchar(450) NOT NULL,
  [PlacedAt] datetime2 NOT NULL
)
GO

CREATE TABLE [Location] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [StreetAddress] varchar(50) NOT NULL,
  [City] varchar(50) NOT NULL,
  [State] char(2) NOT NULL,
  [Zip] char(5) NOT NULL
)
GO

ALTER TABLE [Auction] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id])
GO

ALTER TABLE [Bid] ADD FOREIGN KEY ([BidderId]) REFERENCES [AspNetUsers] ([Id])
GO

ALTER TABLE [Bid] ADD FOREIGN KEY ([AuctionId]) REFERENCES [Auction] ([Id])
GO

ALTER TABLE [Auction] ADD FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id])
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([ProductCategoryId]) REFERENCES [ProductCategory] ([Id])
GO

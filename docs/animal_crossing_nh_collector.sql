USE master;
GO

-- Crear una nueva base de datos
CREATE DATABASE animal_crossing_nh_collector_db;
GO

USE animal_crossing_nh_collector_db;
GO

-- Crear tablas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetUsers' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetUsers] (
        [Id] NVARCHAR(256) NOT NULL PRIMARY KEY,
        [UserName] NVARCHAR(256) NULL,
        [NormalizedUserName] NVARCHAR(256) NULL,
        [Email] NVARCHAR(256) NULL,
        [NormalizedEmail] NVARCHAR(256) NULL,
        [EmailConfirmed] BIT NOT NULL DEFAULT 0,
        [PasswordHash] NVARCHAR(256) NULL,
        [SecurityStamp] NVARCHAR(256) NULL,
        [ConcurrencyStamp] NVARCHAR(256) NULL,
        [PhoneNumber] NVARCHAR(256) NULL,
        [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0,
        [TwoFactorEnabled] BIT NOT NULL DEFAULT 0,
        [LockoutEnd] DATETIMEOFFSET NULL,
        [LockoutEnabled] BIT NOT NULL DEFAULT 0,
        [AccessFailedCount] INT NOT NULL DEFAULT 0,
        CONSTRAINT UQ_AspNetUsers_Email UNIQUE (Email)
    );
    CREATE INDEX IX_AspNetUsers_UserName ON [dbo].[AspNetUsers](UserName);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetRoles' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id] NVARCHAR(256) NOT NULL PRIMARY KEY,
        [Name] NVARCHAR(256) NULL,
        [NormalizedName] NVARCHAR(256) NULL,
        [ConcurrencyStamp] NVARCHAR(256) NULL,
        CONSTRAINT UQ_AspNetRoles_Name UNIQUE (Name)
    );
    CREATE INDEX IX_AspNetRoles_NormalizedName ON [dbo].[AspNetRoles](NormalizedName);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetUserRoles' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] NVARCHAR(256) NOT NULL,
        [RoleId] NVARCHAR(256) NOT NULL,
        CONSTRAINT PK_AspNetUserRoles PRIMARY KEY NONCLUSTERED ([UserId], [RoleId]),
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE,
        FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles](Id) ON DELETE CASCADE
    );
    CREATE NONCLUSTERED INDEX IX_AspNetUserRoles_UserId ON [dbo].[AspNetUserRoles](UserId);
    CREATE NONCLUSTERED INDEX IX_AspNetUserRoles_RoleId ON [dbo].[AspNetUserRoles](RoleId);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetUserClaims' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [UserId] NVARCHAR(256) NOT NULL,
        [ClaimType] NVARCHAR(256) NOT NULL,
        [ClaimValue] NVARCHAR(256) NOT NULL,
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_AspNetUserClaims_UserId ON [dbo].[AspNetUserClaims](UserId);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetUserLogins' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] NVARCHAR(128) NOT NULL,
        [ProviderKey] NVARCHAR(128) NOT NULL,
        [ProviderDisplayName] NVARCHAR(256) NULL,
        [UserId] NVARCHAR(256) NOT NULL,
        CONSTRAINT PK_AspNetUserLogins PRIMARY KEY NONCLUSTERED ([LoginProvider], [ProviderKey]),
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_AspNetUserLogins_UserId ON [dbo].[AspNetUserLogins](UserId);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AspNetUserTokens' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[AspNetUserTokens] (
        [UserId] NVARCHAR(256) NOT NULL,
        [LoginProvider] NVARCHAR(128) NOT NULL,
        [Name] NVARCHAR(128) NOT NULL,
        [Value] NVARCHAR(MAX) NULL,
        CONSTRAINT PK_AspNetUserTokens PRIMARY KEY NONCLUSTERED ([UserId], [LoginProvider], [Name]),
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_AspNetUserTokens_UserId ON [dbo].[AspNetUserTokens](UserId);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Item' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Item] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(255) NOT NULL,
        [Type] NVARCHAR(50) NOT NULL
    );
    CREATE INDEX IX_Item_Name ON [dbo].[Item](Name);
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserItem' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[UserItem] (
        [UserId] NVARCHAR(256) NOT NULL,
        [ItemId] INT NOT NULL,
        CONSTRAINT PK_UserItem PRIMARY KEY NONCLUSTERED ([UserId], [ItemId]),
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE,
        FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Item](Id) ON DELETE CASCADE
    );
    CREATE NONCLUSTERED INDEX IX_UserItem_UserId_ItemId ON [dbo].[UserItem] (UserId, ItemId);
END
GO

CREATE TABLE [dbo].[AdminTbl] (
    [AdminId]      INT          IDENTITY (1, 1) NOT NULL,
    [AdminAdSoyad] VARCHAR (50) NOT NULL,
    [AdminSifre]   VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AdminId] ASC)
);

CREATE TABLE [dbo].[EnstrumanTbl] (
    [EnsId]       INT          IDENTITY (1, 1) NOT NULL,
    [EnsAd]       VARCHAR (50) NOT NULL,
    [EnsMarka]    VARCHAR (50) NOT NULL,
    [EnsMiktar]   INT          NOT NULL,
    [EnsUcret]    INT          NOT NULL,
    [EnsKategori] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([EnsId] ASC)
);

CREATE TABLE [dbo].[FaturaTbl] (
    [FaturaId]    INT          IDENTITY (1, 1) NOT NULL,
    [FaturaTutar] INT          NOT NULL,
    [FaturaTarih] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([FaturaId] ASC)
);


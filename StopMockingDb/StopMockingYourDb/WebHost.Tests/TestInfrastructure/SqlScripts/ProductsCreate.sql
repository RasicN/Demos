IF NOT EXISTS (SELECT * from sysobjects WHERE name='Products' and xtype='U')
    CREATE TABLE [dbo].[Products](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Name] [nvarchar](500) NOT NULL,        
        [Price] [decimal],
        [Vendor] [nvarchar](150) NOT NULL
        CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED
            ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
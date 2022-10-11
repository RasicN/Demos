IF NOT EXISTS (SELECT * from sysobjects WHERE name='ProductReviews' and xtype='U')
    CREATE TABLE [dbo].[ProductReviews](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [ProductId] [int] NOT NULL,
        [Rating] [decimal],
        [Comments] [nvarchar](max) NULL
        CONSTRAINT [PK_dbo.ProductReviews] PRIMARY KEY CLUSTERED
            ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
IF db_id('Test') IS NULL 
    CREATE DATABASE WebAPI

GO

CREATE TABLE [dbo].[Tax_Information](
	[TaxId] [bigint] IDENTITY(1,1) NOT NULL,
	[Account] [text] NULL,
	[Description] [text] NULL,
	[Currency] [nchar](10) NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Tax_Information] PRIMARY KEY CLUSTERED 
(
	[TaxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



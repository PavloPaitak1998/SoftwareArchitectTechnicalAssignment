IF OBJECT_ID(N'[dbo].[TransactionStatus]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[TransactionStatus](
		[TransactionStatusId] INT NOT NULL,
		[Status]			  NVARCHAR(8) NOT NULL,
		[UnifiedFormat]		  NCHAR(1) NOT NULL,

		CONSTRAINT PK_Transaction_TransactionStatusId PRIMARY KEY ([TransactionStatusId])
  );
END
GO

IF  OBJECT_ID(N'[dbo].[TransactionStatus]', N'U') IS NOT NULL
BEGIN
	DECLARE @TransactionStatus_TEMP TABLE(
		[TransactionStatusId] INT NOT NULL,
		[Status]			  NVARCHAR(8) NOT NULL,
		[UnifiedFormat]		  NCHAR(1) NOT NULL
	);

	INSERT INTO @TransactionStatus_TEMP VALUES
	(1, 'Approved' , 'A'),
	(2, 'Failed'   , 'R'),
	(3, 'Finished' , 'D'),
	(4, 'Rejected' , 'R'),
	(5, 'Done'     , 'D');

	INSERT INTO [dbo].[TransactionStatus] ([TransactionStatusId], [Status] ,[UnifiedFormat]) 
	SELECT t.[TransactionStatusId], t.[Status], t.[UnifiedFormat] 
	FROM @TransactionStatus_TEMP AS t 
	WHERE NOT EXISTS (SELECT * FROM [dbo].[TransactionStatus] AS a WHERE  t.[Status] = a.[Status]);
END
GO

IF OBJECT_ID(N'[dbo].[CurrencyCode]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[CurrencyCode](
		[CurrencyCodeId] INT NOT NULL,
		[CurrencyCode]   NCHAR(3) NOT NULL,

		CONSTRAINT PK_CurrencyCode_CurrencyCodeId PRIMARY KEY ([CurrencyCodeId])
  );
END
GO

IF  OBJECT_ID(N'[dbo].[CurrencyCode]', N'U') IS NOT NULL
BEGIN
	DECLARE @CurrencyCode_TEMP TABLE(
		[CurrencyCodeId] INT NOT NULL,
		[CurrencyCode]   NCHAR(3) NOT NULL
	);

	INSERT INTO @CurrencyCode_TEMP VALUES
	(1, 'USD'),
	(2, 'EUR'),
	(3, 'UAH');

	INSERT INTO [dbo].[CurrencyCode] ([CurrencyCodeId], [CurrencyCode]) 
	SELECT t.[CurrencyCodeId], t.[CurrencyCode] 
	FROM @CurrencyCode_TEMP AS t 
	WHERE NOT EXISTS (SELECT * FROM [dbo].[CurrencyCode] AS a WHERE  t.[CurrencyCode] = a.[CurrencyCode]);
END
GO

IF OBJECT_ID(N'[dbo].[Transaction]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[Transaction](
		[TransactionId]            INT IDENTITY(1,1) NOT NULL,
		[TransactionIdentificator] NVARCHAR(50) NOT NULL,
		[Amount]                   DECIMAL NOT NULL,
		[CurrencyCodeId]           INT NOT NULL,
		[TransactionDate]          DATETIME NOT NULL,
		[TransactionStatusId]      INT NOT NULL,
		[CreatedBy]                UNIQUEIDENTIFIER NOT NULL,
		[CreatedDateTime]          DATETIME NOT NULL CONSTRAINT DF_Transaction_CreatedDateTime DEFAULT GETUTCDATE(),
		[ModifiedBy]               UNIQUEIDENTIFIER NOT NULL,
		[ModifiedDateTime]         DATETIME NOT NULL CONSTRAINT DF_Transaction_ModifiedDateTime DEFAULT GETUTCDATE(),

		CONSTRAINT PK_Transaction_TransactionId PRIMARY KEY ([TransactionId]),
		CONSTRAINT FK_Transaction_TransactionStatus_TransactionStatusId FOREIGN KEY([TransactionStatusId]) REFERENCES [dbo].[TransactionStatus] ([TransactionStatusId]),
		CONSTRAINT FK_Transaction_CurrencyCode_CurrencyCodeId FOREIGN KEY([CurrencyCodeId]) REFERENCES [dbo].[CurrencyCode] ([CurrencyCodeId])		
  );
END
GO

DROP TABLE [dbo].[Transaction]
DROP TABLE [dbo].[TransactionStatus]
DROP TABLE [dbo].[CurrencyCode]
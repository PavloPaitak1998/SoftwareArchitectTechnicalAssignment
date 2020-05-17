IF  COL_LENGTH(N'[dbo].[Transaction]', N'TransactionIdentificator') IS NOT NULL AND
 NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'UQ_Transaction_TransactionIdentificator' AND TABLE_NAME =  'Transaction')
BEGIN
 ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT UQ_Transaction_TransactionIdentificator UNIQUE ([TransactionIdentificator]);
END
GO
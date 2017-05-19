SELECT
    [Project1].[NotaFiscalId] AS [NotaFiscalId],
    [Project1].[DataNotaFiscal] AS [DataNotaFiscal],
    [Project1].[C1] AS [C1],
    [Project1].[Total] AS [Total]
    FROM ( SELECT
				  [Limit1].[NotaFiscalId] AS [NotaFiscalId],
			   	  [Limit1].[DataNotaFiscal] AS [DataNotaFiscal],
				  [Limit1].[Total] AS [Total],
				  CASE WHEN ([Extent2].[PrimeiroNome] IS NULL) THEN N'' 
				     ELSE [Extent2].[PrimeiroNome] END + N' ' + CASE WHEN ([Extent2].[Sobrenome] IS NULL) THEN N'' 
					 ELSE [Extent2].[Sobrenome] 	    
		          END AS [C1]
        FROM   (SELECT --TOP (10) 
		              [Extent1].[NotaFiscalId] AS [NotaFiscalId], 
					  [Extent1].[ClienteId] AS [ClienteId], 
					  [Extent1].[DataNotaFiscal] AS [DataNotaFiscal], 
					  [Extent1].[Total] AS [Total]
            FROM [dbo].[NotaFiscal] AS [Extent1]
            ORDER BY [Extent1].[NotaFiscalId] ASC 
			    ) AS [Limit1]
        LEFT OUTER JOIN [dbo].[Cliente] AS [Extent2] ON [Limit1].[ClienteId] = [Extent2].[ClienteId]
    )  AS [Project1]
    ORDER BY [Project1].[NotaFiscalId] ASC
   -- OFFSET 10 ROWS




SELECT
    [Project1].[NotaFiscalId] AS [NotaFiscalId],
    [Project1].[DataNotaFiscal] AS [DataNotaFiscal],
    [Project1].[C1] AS [C1],
    [Project1].[Total] AS [Total]
    FROM ( SELECT
        [Extent1].[NotaFiscalId] AS [NotaFiscalId],
        [Extent1].[DataNotaFiscal] AS [DataNotaFiscal],
        [Extent1].[Total] AS [Total],
        [Extent2].[PrimeiroNome] + N' ' + [Extent2].[Sobrenome] AS [C1]
        FROM  [dbo].[NotaFiscal] AS [Extent1]
        INNER JOIN [dbo].[Cliente] AS [Extent2] ON [Extent1].[ClienteId] = [Extent2].[ClienteId]
    )  AS [Project1]
    ORDER BY [Project1].[NotaFiscalId] ASC
    OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY

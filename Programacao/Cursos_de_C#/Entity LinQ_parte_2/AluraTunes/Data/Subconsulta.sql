SELECT
    [Extent1].[NotaFiscalId] AS [NotaFiscalId],
    [Extent1].[DataNotaFiscal] AS [DataNotaFiscal],
    [Extent3].[PrimeiroNome] + N' ' + [Extent3].[Sobrenome] AS [C1],
    [Extent1].[Total] AS [Total]
    FROM   [dbo].[NotaFiscal] AS [Extent1]
    CROSS JOIN  (SELECT
        AVG([Extent2].[Total]) AS [A1]
        FROM [dbo].[NotaFiscal] AS [Extent2] ) AS [GroupBy1]
    INNER JOIN [dbo].[Cliente] AS [Extent3] ON [Extent1].[ClienteId] = [Extent3].[ClienteId]
    WHERE [Extent1].[Total] > [GroupBy1].[A1]

/* =========================================================
   SEED IDEMPOTENTE • Piezas (FDI) + Caras por pieza (enum)
   Enum TipoCara:
     0 Mesial, 1 Distal, 2 Vestibular, 3 Palatina, 4 Oclusal, 5 Lingual
   Regla:
     Cuadrantes 1,2,5,6 => Palatina(3)
     Cuadrantes 3,4,7,8 => Lingual(5)
   ========================================================= */

SET NOCOUNT ON;

BEGIN TRY
  BEGIN TRAN;

  DECLARE @now datetime2(7) = SYSDATETIME();

  /* ---------------------------------------------------------
     0) Migración defensiva a FDI si hubiera piezas < 11
     --------------------------------------------------------- */
  IF EXISTS (SELECT 1 FROM dbo.PiezaDental WHERE NumeroPieza < 11 AND IsDeleted = 0)
  BEGIN
    IF EXISTS (
      SELECT 1
      FROM dbo.PiezaDental p
      JOIN dbo.PiezaDental q
        ON q.NumeroPieza = p.Cuadrante*10 + p.NumeroPieza
       AND q.Id <> p.Id
       AND q.IsDeleted = 0
      WHERE p.NumeroPieza < 11 AND p.IsDeleted = 0
    )
    BEGIN
      THROW 50001, 'Conflicto al migrar NumeroPieza a FDI. Revise duplicados.', 1;
    END;

    UPDATE dbo.PiezaDental
      SET NumeroPieza = Cuadrante*10 + NumeroPieza
    WHERE NumeroPieza < 11 AND IsDeleted = 0;

    PRINT 'Migración a FDI realizada para piezas con NumeroPieza < 11.';
  END;

  /* ---------------------------------------------------------
     1) Catálogo FDI: permanentes (11–48) + temporales (51–85)
     --------------------------------------------------------- */
  ;WITH Permanentes AS (
    SELECT (q.Cuadrante*10 + n.Num) AS FDI,
           q.Cuadrante,
           CAST(1 AS bit) AS DenticionPermanente
    FROM (VALUES (1),(2),(3),(4)) q(Cuadrante)
    CROSS JOIN (VALUES (1),(2),(3),(4),(5),(6),(7),(8)) n(Num)
  ),
  Temporales AS (
    SELECT (q.Cuadrante*10 + n.Num) AS FDI,
           q.Cuadrante,
           CAST(0 AS bit) AS DenticionPermanente
    FROM (VALUES (5),(6),(7),(8)) q(Cuadrante)
    CROSS JOIN (VALUES (1),(2),(3),(4),(5)) n(Num)
  ),
  FuentePiezas AS (
    SELECT * FROM Permanentes
    UNION ALL
    SELECT * FROM Temporales
  )
  /* ---------------------------------------------------------
     2) Insertar piezas faltantes
     --------------------------------------------------------- */
  INSERT INTO dbo.PiezaDental (NumeroPieza, Cuadrante, DenticionPermanente, CreatedDate, IsDeleted)
  SELECT fp.FDI, fp.Cuadrante, fp.DenticionPermanente, @now, 0
  FROM FuentePiezas fp
  WHERE NOT EXISTS (
    SELECT 1 FROM dbo.PiezaDental p
    WHERE p.NumeroPieza = fp.FDI AND p.IsDeleted = 0
  );

  PRINT 'Piezas FDI verificadas/insertadas.';

  /* ---------------------------------------------------------
     3) Insertar CARAS por pieza respetando enum y arco
        Caras fijas: 0 Mesial, 1 Distal, 2 Vestibular, 4 Oclusal
        + Cara de superficie interna:
          - Palatina(3) para cuadrantes 1,2,5,6 (superiores)
          - Lingual (5) para cuadrantes 3,4,7,8 (inferiores)
     --------------------------------------------------------- */
  ;WITH P AS (
    SELECT Id, Cuadrante
    FROM dbo.PiezaDental
    WHERE IsDeleted = 0
  ),
  Fijas AS (
    SELECT 0 AS Cara UNION ALL
    SELECT 1 UNION ALL
    SELECT 2 UNION ALL
    SELECT 4
  ),
  Interna AS (
    SELECT p.Id AS PiezaDentalId,
           CASE WHEN p.Cuadrante IN (1,2,5,6) THEN 3 ELSE 5 END AS Cara -- 3=Palatina, 5=Lingual
    FROM P p
  ),
  Todas AS (
    SELECT p.Id AS PiezaDentalId, f.Cara
    FROM P p CROSS JOIN Fijas f
    UNION ALL
    SELECT i.PiezaDentalId, i.Cara FROM Interna i
  )
  INSERT INTO dbo.CaraDental (CaraDentaria, CreatedDate, IsDeleted, PiezaDentalId)
  SELECT t.Cara, @now, 0, t.PiezaDentalId
  FROM Todas t
  WHERE NOT EXISTS (
    SELECT 1 FROM dbo.CaraDental cd
    WHERE cd.PiezaDentalId = t.PiezaDentalId
      AND cd.CaraDentaria = t.Cara
      AND cd.IsDeleted = 0
  );

  COMMIT TRAN;
  PRINT 'Caras por pieza verificadas/insertadas con enum (0..5). Seed OK.';
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0 ROLLBACK TRAN;
  DECLARE @ErrMsg nvarchar(4000)=ERROR_MESSAGE(), @ErrSev int=ERROR_SEVERITY(), @ErrState int=ERROR_STATE();
  RAISERROR('Error en seed de piezas/caras: %s', @ErrSev, @ErrState, @ErrMsg);
END CATCH;
GO

/* Índice único sugerido (si no existe) para FDI activo */
IF NOT EXISTS (
  SELECT 1 FROM sys.indexes
  WHERE name = 'UQ_PiezaDental_NumeroPieza_FDI' AND object_id = OBJECT_ID('dbo.PiezaDental')
)
BEGIN
  CREATE UNIQUE INDEX UQ_PiezaDental_NumeroPieza_FDI
    ON dbo.PiezaDental (NumeroPieza)
    WHERE IsDeleted = 0;
END
GO

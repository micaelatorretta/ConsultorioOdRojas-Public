SET IDENTITY_INSERT [dbo].[Nomenclador] ON;

-----------------------------------------------------------
-- SWISS MEDICAL (ObraSocialId = 21)
-----------------------------------------------------------
INSERT INTO [dbo].[Nomenclador]
  ([Id], [CodigoNacional], [Descripcion], [Importe], [CreatedDate], [IsDeleted],
   [ObraSocialId], [ColorHexSugerido], [PermiteMultiple], [RequiereCara])
VALUES
  (101, N'CONS_OD',  N'Consulta odontológica inicial',               CAST(1500.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#2196F3', 0, 0),
  (102, N'PROF_SM',  N'Profilaxis y control de placa',              CAST(1200.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#4CAF50', 0, 0),
  (103, N'RAD_PER',  N'Radiografía periapical',                     CAST(700.00  AS Decimal(13,2)), GETDATE(), 0, 21, N'#9C27B0', 1, 0),
  (104, N'EMP_RES',  N'Restauración con resina compuesta',          CAST(2500.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#FF9800', 1, 1),
  (105, N'EXT_SIM',  N'Extracción simple',                          CAST(2000.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#F44336', 0, 0),
  (106, N'TR_COND',  N'Tratamiento de conducto unirradicular',      CAST(3500.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#00BCD4', 0, 1),
  (107, N'TR_BIRR',  N'Tratamiento de conducto birradicular',       CAST(4200.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#009688', 0, 1),
  (108, N'TR_MULT',  N'Tratamiento de conducto multirradicular',    CAST(4800.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#607D8B', 0, 1),
  (109, N'PRO_PAR',  N'Prótesis parcial acrílica',                  CAST(8500.00 AS Decimal(13,2)), GETDATE(), 0, 21, N'#795548', 0, 0),
  (110, N'FLUOR_SM', N'Fluorización tópica',                        CAST(900.00  AS Decimal(13,2)), GETDATE(), 0, 21, N'#8BC34A', 1, 0);

-----------------------------------------------------------
-- IOSPER (ObraSocialId = 22)
-----------------------------------------------------------
INSERT INTO [dbo].[Nomenclador]
  ([Id], [CodigoNacional], [Descripcion], [Importe], [CreatedDate], [IsDeleted],
   [ObraSocialId], [ColorHexSugerido], [PermiteMultiple], [RequiereCara])
VALUES
  (201, N'CONS_OD',  N'Consulta odontológica IOSPER',               CAST(1300.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#2196F3', 0, 0),
  (202, N'PROF_IO',  N'Profilaxis dental IOSPER',                   CAST(950.00  AS Decimal(13,2)), GETDATE(), 0, 22, N'#4CAF50', 0, 0),
  (203, N'RAD_PER',  N'Radiografía periapical IOSPER',              CAST(600.00  AS Decimal(13,2)), GETDATE(), 0, 22, N'#9C27B0', 1, 0),
  (204, N'EMP_RES',  N'Restauración simple (resina)',               CAST(2200.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#FF9800', 1, 1),
  (205, N'EXT_SIM',  N'Extracción simple IOSPER',                   CAST(1700.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#F44336', 0, 0),
  (206, N'TR_COND',  N'Tratamiento de conducto unirradicular',      CAST(2800.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#00BCD4', 0, 1),
  (207, N'TR_BIRR',  N'Tratamiento de conducto birradicular',       CAST(3500.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#009688', 0, 1),
  (208, N'TR_MULT',  N'Tratamiento de conducto multirradicular',    CAST(4000.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#607D8B', 0, 1),
  (209, N'PRO_PAR',  N'Prótesis parcial acrílica IOSPER',           CAST(7500.00 AS Decimal(13,2)), GETDATE(), 0, 22, N'#795548', 0, 0),
  (210, N'FLUOR_IO', N'Fluorización tópica IOSPER',                 CAST(800.00  AS Decimal(13,2)), GETDATE(), 0, 22, N'#8BC34A', 1, 0);

SET IDENTITY_INSERT [dbo].[Nomenclador] OFF;

-- ============================================================
-- Migración: Preferencia de idioma por usuario
-- ------------------------------------------------------------
-- Ejecutar este script UNA SOLA VEZ contra la base BDProyecto
-- (o el nombre que tenga tu base) ANTES de usar la nueva
-- funcionalidad de "Cambiar Idioma" en la aplicación.
--
-- Agrega la columna Idioma_54CS al final de la tabla Usuarios_54CS.
-- Como se define con DEFAULT 'es', SQL Server completa
-- automáticamente esa columna en todas las filas existentes con
-- 'es' (Español) sin necesidad de un UPDATE manual.
--
-- El script es seguro de ejecutar más de una vez: si la columna
-- ya existe, no hace nada.
-- ============================================================

-- Si tu base no se llama BDProyecto, cambiá el nombre acá:
-- USE BDProyecto;
-- GO

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE Name = N'Idioma_54CS' AND Object_ID = Object_ID(N'Usuarios_54CS')
)
BEGIN
    ALTER TABLE Usuarios_54CS
    ADD Idioma_54CS NVARCHAR(10) NOT NULL DEFAULT 'es';

    PRINT 'Columna Idioma_54CS agregada correctamente (usuarios existentes quedaron en ''es'').';
END
ELSE
BEGIN
    PRINT 'La columna Idioma_54CS ya existe, no se realizaron cambios.';
END
GO

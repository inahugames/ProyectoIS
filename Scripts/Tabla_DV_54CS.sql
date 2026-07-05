-- =============================================================================
-- Tabla especial DV (Dígito Verificador) - BDProyecto
-- =============================================================================
-- Guarda el DVH (Dígito Verificador Horizontal) y el DVV (Dígito Verificador
-- Vertical) de cada tabla de la BD, más una fila 'TOTAL_BD' con el DVH y el
-- DVV de toda la Base de Datos.
--
-- NO es necesario ejecutar este script manualmente: la aplicación crea la
-- tabla en forma automática la primera vez que calcula el Dígito Verificador
-- (ver DAL\DALDigitoVerificador_54CS.cs). Se incluye solo como documentación.
--
-- La tabla se recalcula por completo después de cada persistencia sobre la BD
-- (ver DAL\Conexion_54CS.cs, método Escribir) y queda excluida del cálculo del
-- propio Dígito Verificador.
-- =============================================================================

USE BDProyecto;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DV_54CS')
BEGIN
    CREATE TABLE DV_54CS (
        Tabla_54CS varchar(128) NOT NULL PRIMARY KEY, -- nombre de la tabla (o 'TOTAL_BD')
        DVH_54CS   bigint       NOT NULL,             -- Dígito Verificador Horizontal
        DVV_54CS   bigint       NOT NULL              -- Dígito Verificador Vertical
    );
END
GO

-- Para probar la detección de inconsistencias se puede modificar un dato de
-- cualquier tabla directamente desde SQL Server Management Studio (una
-- modificación "por fuera" del sistema no recalcula el DV) y luego intentar
-- iniciar sesión en la aplicación.

using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPPermisos_54CS
    {
        private DALFamilia_54CS _dalfam;
        private DALRoles_54CS _dalrol;
        private DALPermisos_54CS _dalperm;

        public MPPPermisos_54CS()
        {
            _dalfam = new DALFamilia_54CS();
            _dalrol = new DALRoles_54CS();
            _dalperm = new DALPermisos_54CS();
        }

        /// <summary>
        /// Reconstruye el árbol completo de Roles, con sus Familias y Permisos anidados.
        /// Este es el método principal que usarás para asignar roles a los usuarios.
        /// </summary>
        public List<Rol_54CS> ObtenerArbolDeRolesCompleto()
        {
            // 1. Obtener todos los datos crudos desde la DAL
            DataTable dtPermisos = _dalperm.ObtenerPermisos();
            DataTable dtFamilias = _dalfam.ObtenerFamilias();
            DataTable dtRoles = _dalrol.ObtenerRoles();

            DataTable dtFam_Per = _dalfam.ObtenerRelacionesFamiliaPermiso();
            DataTable dtRol_Fam = _dalrol.ObtenerRelacionesRolFamilia();
            DataTable dtRol_Per = _dalrol.ObtenerRelacionesRolPermiso();

            // 2. Diccionarios para acceso rápido por ID (O(1) en búsquedas)
            var dicPermisos = new Dictionary<int, Permiso_54CS>();
            var dicFamilias = new Dictionary<int, Familia_54CS>();
            var dicRoles = new Dictionary<int, Rol_54CS>();

            // --- FASE 1: Instanciar Hojas (Permisos) ---
            foreach (DataRow row in dtPermisos.Rows)
            {
                int id = Convert.ToInt32(row["IdPermiso_54CS"]);
                string nombre = row["Nombre_54CS"].ToString();

                var permiso = new Permiso_54CS(nombre) { ID = id };
                dicPermisos.Add(id, permiso);
            }

            // --- FASE 2: Instanciar Nodos Intermedios (Familias) ---
            foreach (DataRow row in dtFamilias.Rows)
            {
                int id = Convert.ToInt32(row["IdFamilia_54CS"]);
                string nombre = row["Nombre_54CS"].ToString();

                var familia = new Familia_54CS(nombre) { ID = id };
                dicFamilias.Add(id, familia);
            }

            // --- FASE 3: Anidar Permisos dentro de las Familias ---
            foreach (DataRow row in dtFam_Per.Rows)
            {
                int idFamilia = Convert.ToInt32(row["IdFamilia"]);
                int idPermiso = Convert.ToInt32(row["IdPermiso"]);

                if (dicFamilias.ContainsKey(idFamilia) && dicPermisos.ContainsKey(idPermiso))
                {
                    dicFamilias[idFamilia].Agregar(dicPermisos[idPermiso]);
                }
            }

            // --- FASE 4: Instanciar Raíces (Roles) ---
            foreach (DataRow row in dtRoles.Rows)
            {
                int id = Convert.ToInt32(row["IdRol_54CS"]);
                string descripcion = row["Nombre_54CS"].ToString();

                // Un Rol actúa como un contenedor principal, usamos 'Familia' para representarlo
                var rol = new Familia_54CS(descripcion) { ID = id };
                dicRoles.Add(id, rol);
            }

            // --- FASE 5: Anidar Familias y Permisos dentro de los Roles ---
            // 5.1 Vincular Roles con Familias
            foreach (DataRow row in dtRol_Fam.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idFamilia = Convert.ToInt32(row["IdFamilia"]);

                if (dicRoles.ContainsKey(idRol) && dicFamilias.ContainsKey(idFamilia))
                {
                    dicRoles[idRol].Agregar(dicFamilias[idFamilia]);
                }
            }

            // 5.2 Vincular Roles con Permisos Sueltos
            foreach (DataRow row in dtRol_Per.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idPermiso = Convert.ToInt32(row["IdPermiso"]);

                if (dicRoles.ContainsKey(idRol) && dicPermisos.ContainsKey(idPermiso))
                {
                    dicRoles[idRol].Agregar(dicPermisos[idPermiso]);
                }
            }

            // 3. Retornar solo la lista de Roles (ya contienen todo el árbol adentro)
            return new List<Rol_54CS>(dicRoles.Values);
        }

        /// <summary>
        /// Devuelve únicamente la lista de Permisos base.
        /// Útil para llenar el CheckedListBox al crear una nueva Familia.
        /// </summary>
        public List<Rol_54CS> ObtenerPermisosSueltos()
        {
            DataTable dtPermisos = _dalperm.ObtenerPermisos();
            List<Rol_54CS> listaPermisos = new List<Rol_54CS>();

            foreach (DataRow row in dtPermisos.Rows)
            {
                int id = Convert.ToInt32(row["IdPermiso_54CS"]);
                string nombre = row["Nombre_54CS"].ToString();

                listaPermisos.Add(new Permiso_54CS(nombre) { ID = id });
            }

            return listaPermisos;
        }

        /// <summary>
        /// Devuelve las Familias con sus permisos ya cargados.
        /// Útil para llenar el CheckedListBox al crear un nuevo Rol.
        /// </summary>
        public List<Rol_54CS> ObtenerFamiliasEnsambladas()
        {
            DataTable dtPermisos = _dalperm.ObtenerPermisos();
            DataTable dtFamilias = _dalfam.ObtenerFamilias();
            DataTable dtFam_Per = _dalfam.ObtenerRelacionesFamiliaPermiso();

            var dicPermisos = new Dictionary<int, Permiso_54CS>();
            var dicFamilias = new Dictionary<int, Familia_54CS>();

            foreach (DataRow row in dtPermisos.Rows)
            {
                int id = Convert.ToInt32(row["IdPermiso_54CS"]);
                dicPermisos.Add(id, new Permiso_54CS(row["Nombre_54CS"].ToString()) { ID = id });
            }

            foreach (DataRow row in dtFamilias.Rows)
            {
                int id = Convert.ToInt32(row["IdFamilia_54CS"]);
                dicFamilias.Add(id, new Familia_54CS(row["Nombre_54CS"].ToString()) { ID = id });
            }

            foreach (DataRow row in dtFam_Per.Rows)
            {
                int idFamilia = Convert.ToInt32(row["IdFamilia"]);
                int idPermiso = Convert.ToInt32(row["IdPermiso"]);

                if (dicFamilias.ContainsKey(idFamilia) && dicPermisos.ContainsKey(idPermiso))
                {
                    dicFamilias[idFamilia].Agregar(dicPermisos[idPermiso]);
                }
            }

            // Retorna las familias listas y cargadas (Casteadas a la clase base)
            return new List<Rol_54CS>(dicFamilias.Values);
        }

        public int InsertarRol(string nombre)
        {
            return _dalrol.InsertarRol(nombre);
        }

        public bool InsertarRelacionRolFamilia(int idrol, int idhijo)
        {
            return _dalrol.InsertarRelacionRolFamilia(idrol, idhijo) > 0;
        }

        public bool InsertarRelacionRolPermiso(int idrol, int idhijo)
        {
            return _dalrol.InsertarRelacionRolPermiso(idrol, idhijo) > 0;
        }

        public int InsertarFamilia(string nombre, string descripcion)
        {
            return _dalfam.InsertarFamilia(nombre, descripcion);
        }

        public void InsertarRelacionFamiliaPermiso(int idfam, int idhijo)
        {
            _dalfam.InsertarRelacionFamiliaPermiso(idfam, idhijo);
        }

        public bool ExisteRolEnUso(int id)
        {
            return _dalrol.ExisteRolEnUso(id);
        }

        public bool EliminarRelacionesDeRol(int id)
        {
            return _dalrol.EliminarRelacionesDeRol(id) > 0;
        }

        public bool EliminarRol(int id)
        {
            return _dalrol.EliminarRol(id) > 0;
        }

        public bool EliminarRolesDeUsuario(int id)
        {
            return _dalrol.EliminarRolesDeUsuario(id) > 0;
        }

        public bool AsignarRolAUsuario(int idusu, int idrol)
        {
            return _dalrol.AsignarRolAUsuario(idusu, idrol) > 0;
        }
    }
}

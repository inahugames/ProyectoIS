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
        public void EnTransaccion(Action accion) => Conexion_54CS.EnTransaccion(accion);
        private DALFamilia_54CS _dalfam;
        private DALRoles_54CS _dalrol;
        private DALPermisos_54CS _dalperm;

        public MPPPermisos_54CS()
        {
            _dalfam = new DALFamilia_54CS();
            _dalrol = new DALRoles_54CS();
            _dalperm = new DALPermisos_54CS();
        }

        public List<Rol_54CS> ObtenerArbolDeRolesCompleto()
        {
            DataTable dtPermisos = _dalperm.ObtenerPermisos();
            DataTable dtFamilias = _dalfam.ObtenerFamilias();
            DataTable dtRoles = _dalrol.ObtenerRoles();

            DataTable dtFam_Per = _dalfam.ObtenerRelacionesFamiliaPermiso();
            DataTable dtFam_Fam = _dalfam.ObtenerRelacionesFamiliaFamilia();
            DataTable dtRol_Fam = _dalrol.ObtenerRelacionesRolFamilia();
            DataTable dtRol_Per = _dalrol.ObtenerRelacionesRolPermiso();

            var dicPermisos = new Dictionary<int, Permiso_54CS>();
            var dicFamilias = new Dictionary<int, Familia_54CS>();
            var dicRoles = new Dictionary<int, Rol_54CS>();

            foreach (DataRow row in dtPermisos.Rows)
            {
                int id = Convert.ToInt32(row["IdPermiso_54CS"]);
                string nombre = row["Nombre_54CS"].ToString();

                var permiso = new Permiso_54CS(nombre) { ID = id };
                dicPermisos.Add(id, permiso);
            }

            foreach (DataRow row in dtFamilias.Rows)
            {
                int id = Convert.ToInt32(row["IdFamilia_54CS"]);
                string nombre = row["Nombre_54CS"].ToString();

                var familia = new Familia_54CS(nombre) { ID = id };
                dicFamilias.Add(id, familia);
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

            AnidarSubFamilias(dicFamilias, dtFam_Fam);

            foreach (DataRow row in dtRoles.Rows)
            {
                int id = Convert.ToInt32(row["IdRol_54CS"]);
                string descripcion = row["Nombre_54CS"].ToString();
                var rol = new Familia_54CS(descripcion) { ID = id };
                dicRoles.Add(id, rol);
            }

            foreach (DataRow row in dtRol_Fam.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idFamilia = Convert.ToInt32(row["IdFamilia"]);

                if (dicRoles.ContainsKey(idRol) && dicFamilias.ContainsKey(idFamilia))
                {
                    dicRoles[idRol].Agregar(dicFamilias[idFamilia]);
                }
            }

            foreach (DataRow row in dtRol_Per.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idPermiso = Convert.ToInt32(row["IdPermiso"]);

                if (dicRoles.ContainsKey(idRol) && dicPermisos.ContainsKey(idPermiso))
                {
                    dicRoles[idRol].Agregar(dicPermisos[idPermiso]);
                }
            }

            return new List<Rol_54CS>(dicRoles.Values);
        }

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

        public List<Rol_54CS> ObtenerFamiliasEnsambladas()
        {
            DataTable dtPermisos = _dalperm.ObtenerPermisos();
            DataTable dtFamilias = _dalfam.ObtenerFamilias();
            DataTable dtFam_Per = _dalfam.ObtenerRelacionesFamiliaPermiso();
            DataTable dtFam_Fam = _dalfam.ObtenerRelacionesFamiliaFamilia();

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

            AnidarSubFamilias(dicFamilias, dtFam_Fam);
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

        public void InsertarRelacionFamiliaFamilia(int idFamiliaPadre, int idFamiliaHijo)
        {
            _dalfam.InsertarRelacionFamiliaFamilia(idFamiliaPadre, idFamiliaHijo);
        }

        public bool EliminarRelacionFamiliaFamilia(int idFamiliaPadre, int idFamiliaHijo)
        {
            return _dalfam.EliminarRelacionFamiliaFamilia(idFamiliaPadre, idFamiliaHijo) > 0;
        }

        public bool ExisteFamiliaEnUso(int idFamilia)
        {
            return _dalfam.ExisteFamiliaEnUso(idFamilia);
        }

        public bool EliminarRelacionesDeFamilia(int idFamilia)
        {
            return _dalfam.EliminarRelacionesDeFamilia(idFamilia) > 0;
        }

        public bool EliminarFamilia(int idFamilia)
        {
            return _dalfam.EliminarFamilia(idFamilia) > 0;
        }

        private void AnidarSubFamilias(Dictionary<int, Familia_54CS> dicFamilias, DataTable dtFamFam)
        {
            if (dtFamFam == null)
                return;

            var hijosPorPadre = new Dictionary<int, List<int>>();
            foreach (DataRow row in dtFamFam.Rows)
            {
                int idPadre = Convert.ToInt32(row["IdFamiliaPadre"]);
                int idHijo = Convert.ToInt32(row["IdFamiliaHijo"]);

                if (!hijosPorPadre.ContainsKey(idPadre))
                    hijosPorPadre[idPadre] = new List<int>();
                hijosPorPadre[idPadre].Add(idHijo);
            }

            var ensambladas = new HashSet<int>();
            foreach (var idFamilia in dicFamilias.Keys.ToList())
            {
                EnsamblarFamilia(idFamilia, dicFamilias, hijosPorPadre, ensambladas);
            }
        }

        private void EnsamblarFamilia(int idFamilia, Dictionary<int, Familia_54CS> dicFamilias,
            Dictionary<int, List<int>> hijosPorPadre, HashSet<int> ensambladas)
        {
            if (ensambladas.Contains(idFamilia))
                return;
            ensambladas.Add(idFamilia);

            List<int> hijos;
            if (!hijosPorPadre.TryGetValue(idFamilia, out hijos))
                return;

            foreach (var idHijo in hijos)
            {
                if (!dicFamilias.ContainsKey(idFamilia) || !dicFamilias.ContainsKey(idHijo))
                    continue;

                EnsamblarFamilia(idHijo, dicFamilias, hijosPorPadre, ensambladas);
                dicFamilias[idFamilia].Agregar(dicFamilias[idHijo]);
            }
        }

        public bool EliminarRelacionFamiliaPermiso(int idFamilia, int idPermiso)
        {
            return _dalfam.EliminarRelacionFamiliaPermiso(idFamilia, idPermiso) > 0;
        }

        public bool EliminarRelacionRolFamilia(int idRol, int idFamilia)
        {
            return _dalrol.EliminarRelacionRolFamilia(idRol, idFamilia) > 0;
        }

        public bool EliminarRelacionRolPermiso(int idRol, int idPermiso)
        {
            return _dalrol.EliminarRelacionRolPermiso(idRol, idPermiso) > 0;
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

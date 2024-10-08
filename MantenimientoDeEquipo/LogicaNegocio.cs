using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    public class LogicaNegocio : LogicaNegocioBase
    {
        // Métodos específicos de LogicaNegocio
        public DataTable listaEquipos()
        {
            return EjecutarConsulta("SP_LISTAEQUIPOS");
        }

        public DataTable listaEstado()
        {
            return EjecutarConsulta("SP_LISTAESTADOS");
        }

        public DataTable listaTipoEquipos()
        {
            return EjecutarConsulta("SP_LISTATIPOEQUIPOS");
        }

        public string generaCodigo()
        {
            AbrirConexion();
            SqlCommand cmd = new SqlCommand("SP_ULTIMOEQUIPO", cn);
            string codigo = "EQ" + (int.Parse(cmd.ExecuteScalar().ToString().Substring(2, 4)) + 1).ToString("0000");
            CerrarConexion();
            return codigo;
        }

        public string nuevoEquipo(string codigo, string tipo, string descripcion, double precio, string estado)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "tip", tipo },
            { "des", descripcion },
            { "pre", precio },
            { "est", estado }
        };
            return EjecutarComando("SP_NUEVOEQUIPO", parametros);
        }

        public string actualizaEquipo(string codigo, string tipo, string descripcion, double precio, string estado)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "tip", tipo },
            { "des", descripcion },
            { "pre", precio },
            { "est", estado }
        };
            return EjecutarComando("SP_ACTUALIZAEQUIPO", parametros);
        }

        public string eliminaEquipo(string codigo)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo }
        };
            return EjecutarComando("SP_ELIMINAEQUIPO", parametros);
        }
    }
}

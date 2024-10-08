using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    public class LogicaNegocioTipoEquipo : LogicaNegocioBase
    {
        // Métodos específicos de LogicaNegocioTipoEquipo
        public DataTable listaTipoEquipos()
        {
            return EjecutarConsulta("SP_LISTATIPOEQUIPOS");
        }

        public string generaCodigoTipoEquipo()
        {
            AbrirConexion();
            SqlCommand cmd = new SqlCommand("SP_ULTIMOTIPOEQUIPO", cn);
            string codigo = "TIP" + (int.Parse(cmd.ExecuteScalar().ToString().Substring(3, 3)) + 1).ToString("0000");
            CerrarConexion();
            return codigo;
        }

        public string nuevoTipoEquipo(string codigo, string descripcion)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "des", descripcion }
        };
            return EjecutarComando("SP_NUEVOTIPOEQUIPO", parametros);
        }

        public string actualizaTipoEquipo(string codigo, string descripcion)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "des", descripcion }
        };
            return EjecutarComando("SP_ACTUALIZATIPOEQUIPO", parametros);
        }

        public string eliminaTipoEquipo(string codigo)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo }
        };
            return EjecutarComando("SP_ELIMINATIPOEQUIPO", parametros);
        }
    }
}

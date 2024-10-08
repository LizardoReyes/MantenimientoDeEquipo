using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    public class LogicaNegocioEstado : LogicaNegocioBase
    {
        // Métodos específicos de LogicaNegocioEstado
        public DataTable listaEstado()
        {
            return EjecutarConsulta("SP_LISTAESTADOEQUIPOS");
        }

        public string nuevoEstadoEquipo(string codigo, string descripcion)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "des", descripcion }
        };
            return EjecutarComando("SP_NUEVOESTADOEQUIPO", parametros);
        }

        public string actualizaEstadoEquipo(string codigo, string descripcion)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo },
            { "des", descripcion }
        };
            return EjecutarComando("SP_ACTUALIZAESTADOEQUIPO", parametros);
        }

        public string eliminaEstadoEquipo(string codigo)
        {
            var parametros = new Dictionary<string, object>
        {
            { "cod", codigo }
        };
            return EjecutarComando("SP_ELIMINAESTADOEQUIPO", parametros);
        }
    }
}

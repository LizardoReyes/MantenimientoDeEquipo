using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    public class LogicaNegocioBase
    {
        private Conexion objCon = new Conexion();
        protected SqlConnection cn;
        protected string mensaje;

        // Método que abre la conexión
        public void AbrirConexion()
        {
            cn = objCon.getConecta();
            cn.Open();
        }

        // Método que cierra la conexión
        public void CerrarConexion()
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        // Método para ejecutar procedimientos almacenados que devuelven una tabla
        public DataTable EjecutarConsulta(string procedimiento)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                SqlDataAdapter da = new SqlDataAdapter(procedimiento, cn);
                da.Fill(dt);
            }
            finally
            {
                CerrarConexion();
            }
            return dt;
        }

        // Método para ejecutar procedimientos almacenados con parámetros
        public string EjecutarComando(string procedimiento, Dictionary<string, object> parametros)
        {
            mensaje = "";
            try
            {
                AbrirConexion();
                SqlCommand cmd = new SqlCommand(procedimiento, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                int n = cmd.ExecuteNonQuery();
                mensaje = $"{n} REGISTRO(S) AFECTADO(S) CORRECTAMENTE";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                CerrarConexion();
            }

            return mensaje;
        }
    }
}

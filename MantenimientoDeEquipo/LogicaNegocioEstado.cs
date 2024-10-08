using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    class LogicaNegocioEstado
    {
        // Definicion global
        Conexion objCon = new Conexion();
        SqlConnection cn = new SqlConnection();
        string mensaje;
        // Método que lista los estados
        public DataTable listaEstado()
        {
            cn = objCon.getConecta();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTAESTADOEQUIPOS", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Método que registra un nuevo estado equipo
        public string nuevoEstadoEquipo(string codigo, string descripcion)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_NUEVOESTADOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " ESTADO EQUIPO REGISTRADO CORRECTAMENTE";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        // Método que actualiza los datos de un estado equipo
        public string actualizaEstadoEquipo(string codigo, string descripcion)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ACTUALIZAESTADOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " ESTADO EQUIPO ACTUALIZADO CORRECTAMENTE";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }
        // Método que elimina un registro de estado equipo
        public string eliminaEstadoEquipo(string codigo)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ELIMINAESTADOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " ESTADO EQUIPO ELIMINADO CORRECTAMENTE";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

    }
}

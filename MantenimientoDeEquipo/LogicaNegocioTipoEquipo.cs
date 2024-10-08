using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    class LogicaNegocioTipoEquipo
    {
        // Definicion global
        Conexion objCon = new Conexion();
        SqlConnection cn = new SqlConnection();
        string mensaje;
        // Método que lista tipo equipos
        public DataTable listaTipoEquipos()
        {
            cn = objCon.getConecta();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTATIPOEQUIPOS", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Método que genere un nuevo codigo de tipo equipo
        public string generaCodigoTipoEquipo()
        {
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ULTIMOTIPOEQUIPO", cn);
            return "TIP" + (int.Parse(cmd.ExecuteScalar().ToString().Substring(3, 3)) +
           1).ToString("0000");
        }
        // Método que registra un nuevo tipo equipo
        public string nuevoTipoEquipo(string codigo, string descripcion)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_NUEVOTIPOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " TIPO EQUIPO REGISTRADO CORRECTAMENTE";
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
        // Método que actualiza los datos de un tipo equipo
        public string actualizaTipoEquipo(string codigo, string descripcion)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ACTUALIZATIPOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " TIPO EQUIPO ACTUALIZADO CORRECTAMENTE";
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
        // Método que elimina un registro de tipo equipo
        public string eliminaTipoEquipo(string codigo)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ELIMINATIPOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " TIPO EQUIPO ELIMINADO CORRECTAMENTE";
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

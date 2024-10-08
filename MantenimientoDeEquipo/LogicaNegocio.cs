using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoDeEquipo
{
    class LogicaNegocio
    {
        // Defnicion global
        Conexion objCon = new Conexion();
        SqlConnection cn = new SqlConnection();
        string mensaje;
        // Método que lista los equipos
        public DataTable listaEquipos()
        {
            cn = objCon.getConecta();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTAEQUIPOS", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Método que lista los estados
        public DataTable listaEstado()
        {
            cn = objCon.getConecta();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTAESTADOS", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Método que lista los tipos
        public DataTable listaTipoEquipos()
        {
            cn = objCon.getConecta();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTATIPOEQUIPOS", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Método que genere un nuevo código de equipo
        public string generaCodigo()
        {
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ULTIMOEQUIPO", cn);
            return "EQ" + (int.Parse(cmd.ExecuteScalar().ToString().Substring(2, 4)) + 1).
           ToString("0000");
        }

        // Método que registra un nuevo equipo
        public string nuevoEquipo(string codigo, string tipo, string descripcion,
       double precio, string estado)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_NUEVOEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("tip", SqlDbType.Char).Value = tipo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("pre", SqlDbType.Money).Value = precio;
            cmd.Parameters.Add("est", SqlDbType.Char).Value = estado;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " EQUIPO REGISTRADO CORRECTAMENTE";
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

        // Método que actualiza los datos de un equipo
        public string actualizaEquipo(string codigo, string tipo, string descripcion,
       double precio, string estado)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ACTUALIZAEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            cmd.Parameters.Add("tip", SqlDbType.Char).Value = tipo;
            cmd.Parameters.Add("des", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("pre", SqlDbType.Money).Value = precio;
            cmd.Parameters.Add("est", SqlDbType.Char).Value = estado;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " EQUIPO ACTUALIZADO CORRECTAMENTE";
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

        // Método que elimina un registro de equipo
        public string eliminaEquipo(string codigo)
        {
            mensaje = "";
            cn = objCon.getConecta();
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_ELIMINAEQUIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cod", SqlDbType.Char).Value = codigo;
            try
            {
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " EQUIPO ELIMINADO CORRECTAMENTE";
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

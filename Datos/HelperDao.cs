using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Final_Prog2.Dominio;

namespace Final_Prog2.Datos
{
    public class HelperDao
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private static HelperDao instancia;

        public HelperDao()
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-HJBQUI6\SQLEXPRESS;Initial Catalog=Qatar2022;Integrated Security=True");
            cmd = new SqlCommand();
        }
        public static HelperDao ObtenerInstancia()
        {
            if(instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }
        public DataTable cargarCombo(string nombreSP)
        {
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }
        public bool GrabarEquipo(Equipo oEquipo, string MaestroSP,string DetalleSP)
        {
            bool ok = false;
            SqlTransaction transaction = null;
            try
            {
                cnn.Open();
                transaction = cnn.BeginTransaction();
                SqlCommand cmdMaestro = new SqlCommand(MaestroSP,cnn,transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Parameters.AddWithValue("@pais", oEquipo.Pais);
                cmdMaestro.Parameters.AddWithValue("@director_tecnico", oEquipo.DirectorTecnico);
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();
                int nroEquipo = (int)pOut.Value;
                SqlCommand cmdDetalle = null;
                foreach(Jugador j in oEquipo.jugadores)
                {
                    cmdDetalle = new SqlCommand(DetalleSP, cnn, transaction);
                    cmdDetalle.Parameters.AddWithValue("@id_equipo", nroEquipo);
                    cmdDetalle.Parameters.AddWithValue("@id_persona", j.Persona.IdPersona);
                    cmdDetalle.Parameters.AddWithValue("@camiseta", j.Camiseta);
                    cmdDetalle.Parameters.AddWithValue("@posicion", j.Posicion);
                }
                transaction.Commit();
                ok = true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                ok = false;
            }
            finally
            {
                if(cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
    }
}

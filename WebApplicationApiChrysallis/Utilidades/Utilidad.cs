using System;
using System.Data.SqlClient;

namespace WebApplicationApiChrysallis.Utilidades
{
    public static class Utilidad
    {
        public static String MensajeError(SqlException sqlEx)
        {
            String mensaje;
            switch (sqlEx.Number)
            {
                case -1:
                    mensaje = "Error de conexión con el servidor";
                    break;
                case 547:
                    mensaje = "Tiene datos relacionados";
                    break;
                case 2601:
                    mensaje = "Datos duplicados";
                    break;
                case 2627:
                    mensaje = "Datos duplicados";
                    break;
                case 4060:
                    mensaje = "No se encuentra la base de datos";
                    break;
                case 18456:
                    mensaje = "Usuario o contraseña incorrectos";
                    break;
                default:
                    mensaje = sqlEx.Number + " - " + sqlEx.Message;
                    break;
            }
            return mensaje;
        }
    }
}
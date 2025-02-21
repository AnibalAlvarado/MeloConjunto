using MeloconjuntoApi.Dtos.Puntajes;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Services
{
    public class PuntajeService
    {
        public bool EsPuntajeMejor(PuntajeDto nuevoPuntaje, Puntaje puntajeExistente)
        {
            // Primero comparamos los puntos
            if (nuevoPuntaje.PuntajePuntos > puntajeExistente.PuntajePuntos)
            {
                return true;
            }

            // Si los puntos son iguales, comparamos el tiempo
            if (nuevoPuntaje.PuntajePuntos == puntajeExistente.PuntajePuntos)
            {
                // Un tiempo menor es mejor
                return nuevoPuntaje.PuntajeTime < puntajeExistente.PuntajeTime;
            }

            // Si los puntos son menores, no es un mejor puntaje
            return false;
        }
    }
}

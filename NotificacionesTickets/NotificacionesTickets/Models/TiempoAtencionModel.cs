using System;

namespace NotificacionesTickets.Models
{
    public class TiempoAtencionModel
    {
        public int DiasInhabiles { get; set; }

        public DateTime FechaRecepcion { get; set; }

        public int Calculate()
        {
            int horasActuales, horasRecepcion, totalHoras;
            int horaInicio = 8;
            int horaFinal = 17;

            if (FechaRecepcion > DateTime.Now)
            {
                totalHoras = 0;
            }
            else if (FechaRecepcion.ToString("d") == DateTime.Today.ToString("d"))
            {
                totalHoras =(DateTime.Now.Hour<17)? Math.Abs(DateTime.Now.Hour - FechaRecepcion.Hour):9;
            }
            else
            {
                DateTime fechaInicio = FechaRecepcion.AddDays(1);
                DateTime fechaFin = DateTime.Today;
                TimeSpan dias = fechaFin.Date - fechaInicio.Date;
                horasActuales = DateTime.Now.Hour < 17 ? Math.Abs(DateTime.Now.Hour - horaInicio) : 9;
                horasRecepcion = FechaRecepcion.Hour < 17 ? Math.Abs(FechaRecepcion.Hour - horaFinal) : 0;
                totalHoras = horasActuales + horasRecepcion;
                
                while (fechaInicio <= fechaFin)
                {
                    //Se excluyen los fines de semana para restarlos en la fecha final
                    if (fechaInicio.DayOfWeek == DayOfWeek.Saturday || fechaInicio.DayOfWeek == DayOfWeek.Sunday)
                    {
                        DiasInhabiles += 1;
                    }
                    fechaInicio = fechaInicio.AddDays(1);
                }

                totalHoras = (totalHoras + (dias.Days * 9)) - (DiasInhabiles * 9);
            }

            return totalHoras;
        }

    }
}

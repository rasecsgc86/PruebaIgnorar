using System;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class TiempoAtencionModel
    {
        public int DiasInhabiles { get; set; }

        public DateTime FechaRecepcion { get; set; }

        public int Calculate()
        {
            int totalHoras;
            int horaInicio = 8;
            int horaFinal = 17;

            if (FechaRecepcion > DateTime.Now)
            {
                totalHoras = 0;
            }
            else if (FechaRecepcion.ToString("d") == DateTime.Today.ToString("d"))
            {
                if (DateTime.Now.Hour < horaInicio)
                    totalHoras = 0;
                else if ((DateTime.Now.Hour < horaFinal && DateTime.Now.Hour > horaInicio) || (DateTime.Now.Hour > horaFinal && FechaRecepcion.Hour < DateTime.Now.Hour))
                    totalHoras = Math.Abs(DateTime.Now.Hour - FechaRecepcion.Hour);
                else
                    totalHoras = Math.Abs(horaFinal - FechaRecepcion.Hour);
            }
            else
            {
                DateTime fechaInicio = FechaRecepcion.AddDays(1);
                DateTime fechaFin = DateTime.Today;
                TimeSpan dias = fechaFin.Date - fechaInicio.Date;
                int horasActuales = (DateTime.Now.Hour < horaFinal && DateTime.Now.Hour>horaInicio) ? Math.Abs(DateTime.Now.Hour - horaInicio) :(DateTime.Now.Hour < horaInicio)?0: 9;                
                int horasRecepcion = FechaRecepcion.Hour < horaFinal ? Math.Abs(FechaRecepcion.Hour - horaFinal) : 0;                
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

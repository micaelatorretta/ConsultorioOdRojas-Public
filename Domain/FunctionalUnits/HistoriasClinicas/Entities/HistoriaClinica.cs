using Domain.FunctionalUnits.HistoriasClinicas.Rules;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.ValueObjects;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.HistoriasClinicas.Entities
{
    public partial class HistoriaClinica : BaseAggregateEntity
    {

        public Paciente Paciente { get; set; } = null!;
        public int PacienteId { get; set; }
        public RespuestaConDetalle IntervencionesUlt2Anios { get; set; } = RespuestaConDetalle.No();
        public RespuestaConDetalle TratamientoMedico { get;  set; } = RespuestaConDetalle.No();
        public RespuestaConDetalle Alergias { get;  set; } = RespuestaConDetalle.No();
        public RespuestaConDetalle AnestesiasPrevias { get;  set; } = RespuestaConDetalle.No();
        public RespuestaConDetalle SangradoExcesivo { get;  set; } = RespuestaConDetalle.No();
        public bool CansancioAlCaminar { get;  set; } = false;

        public Habito Fuma { get;  set; } = Habito.No(); // ¿Cuántos?
        public Habito Bebe { get;  set; } = Habito.No(); // ¿Cuánto?
        public bool Embarazo { get;  set; } = false;


        //public Afecciones Afecciones { get;  set; } = new(AfeccionFlags.Ninguna);
        public RespuestaConDetalle MedicacionActual { get;  set; } = RespuestaConDetalle.No();
        public RespuestaConFecha Radiografias { get;  set; } = RespuestaConFecha.No();

        public string? Observaciones { get;  set; }

        // Comportamiento para setear con invariantes
        public void RegistrarIntervenciones(bool si, string? porQue)
            => IntervencionesUlt2Anios = si ? RespuestaConDetalle.SiCon(porQue, detalleObligatorio: true) : RespuestaConDetalle.No();

        public void RegistrarTratamientoMedico(bool si, string? cual)
            => TratamientoMedico = si ? RespuestaConDetalle.SiCon(cual, true) : RespuestaConDetalle.No();

        public void RegistrarAlergias(bool si, string? cual)
            => Alergias = si ? RespuestaConDetalle.SiCon(cual, true) : RespuestaConDetalle.No();

        public void RegistrarAnestesiasPrevias(bool si, string? cual)
            => AnestesiasPrevias = si ? RespuestaConDetalle.SiCon(cual, true) : RespuestaConDetalle.No();

        public void RegistrarSangrado(bool si, string? motivo)
            => SangradoExcesivo = si ? RespuestaConDetalle.SiCon(motivo, true) : RespuestaConDetalle.No();

        public void RegistrarCansancio(bool si) => CansancioAlCaminar = si;

        public void RegistrarFumador(bool si, int? cantidadPorDia)
            => Fuma = si ? Habito.Si(cantidadPorDia) : Habito.No();

        public void RegistrarBebedor(bool si, int? cantidad)
            => Bebe = si ? Habito.Si(cantidad) : Habito.No();

        public void RegistrarEmbarazo(bool si) => Embarazo = si;

        //public void RegistrarAfecciones(IEnumerable<AfeccionFlags> lista)
        //    => Afecciones = Afecciones.From(lista);

        public void RegistrarMedicacionActual(bool si, string? cuales)
            => MedicacionActual = si ? RespuestaConDetalle.SiCon(cuales, true) : RespuestaConDetalle.No();

        public void RegistrarRadiografias(bool si, DateTime? fecha)
            => Radiografias = si ? RespuestaConFecha.SiEn(fecha ?? throw new ArgumentException("Fecha requerida")) : RespuestaConFecha.No();

        public void CambiarObservaciones(string? obs) => Observaciones = string.IsNullOrWhiteSpace(obs) ? null : obs.Trim();
    }

    public partial class HistoriaClinica
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad HistoriaClinica (this) para validar.
                new DatosObligatoriosHistoriaClinicaRule(this)
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Resumen de la historia clinica.
        /// </summary>
        /// <returns></returns>
        public string ObtenerResumen()
        {
            var partes = new List<string>();

            if (IntervencionesUlt2Anios.Si)
                partes.Add($"Intervenciones en los últimos 2 años: {IntervencionesUlt2Anios.Detalle}");
            if (TratamientoMedico.Si)
                partes.Add($"Tratamiento médico actual: {TratamientoMedico.Detalle}");
            if (Alergias.Si)
                partes.Add($"Alergias: {Alergias.Detalle}");
            if (AnestesiasPrevias.Si)
                partes.Add($"Anestesias previas: {AnestesiasPrevias.Detalle}");
            if (SangradoExcesivo.Si)
                partes.Add($"Sangrado excesivo: {SangradoExcesivo.Detalle}");
            if (MedicacionActual.Si)
                partes.Add($"Medicación actual: {MedicacionActual.Detalle}");
            if (CansancioAlCaminar)
                partes.Add("Presenta cansancio al caminar.");
            if (Fuma.Tiene)
                partes.Add($"Fuma {Fuma.Cantidad} cigarrillos por día.");
            if (Bebe.Tiene)
                partes.Add($"Bebe {Bebe.Cantidad} unidades de alcohol por semana.");
            if (Embarazo)
                partes.Add("Paciente embarazada.");
            if (Radiografias.Si)
                partes.Add($"Radiografías tomadas el {Radiografias.Fecha:dd/MM/yyyy}.");
            if (!string.IsNullOrWhiteSpace(Observaciones))
                partes.Add($"Observaciones: {Observaciones}");

            // Si no hay datos relevantes, indicar historia sin particularidades
            if (partes.Count == 0)
                partes.Add("Historia clínica sin particularidades.");

            return string.Join(Environment.NewLine, partes);
        }

    }
}

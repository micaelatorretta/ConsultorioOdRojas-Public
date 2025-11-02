using Domain.FunctionalUnits.Odontogramas.Rules;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Portable.Enums;
using Shared.Domain.Base;
using Shared.Domain.Extensions;
using Shared.Portable.Enums.EntityState;

namespace Domain.FunctionalUnits.Odontogramas.Entities
{
    public partial class Odontograma : BaseAggregateEntity
    {
        public Odontograma() { }
        public Paciente Paciente { get; set; } = null!;
        /// <summary>
        /// Representa que es el odontograma vigente del paciente.
        /// Es decir, que es el odontograma que está abierto a edición actualmente.
        /// </summary>
        public bool Vigente { get; set; }
        /// <summary>
        /// Piezas dentales en las que se realizó una prestación.
        /// </summary>
        public List<OdontogramaPiezaDental> PiezasDentales { get; set; } = new();

    }

    public partial class Odontograma
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad (this) para validar.
                new DatosObligatoriosOdontogramaRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Agregar caras dentales si no existen, en una pieza dental del odontograma.
        /// </summary>
        /// <param name="carasDentales"></param>
        /// <param name="numeroPiezaDental"></param>
        public void AddCarasDentales(List<CaraDental> carasDentales, byte numeroPiezaDental)
        {
            // Buscar la pieza por número FDI
            var pieza = PiezasDentales.FirstOrDefault(p => p.PiezaDental.NumeroPieza == numeroPiezaDental);
            if (pieza == null) return; // o lanzar excepción

            // Asegurarse que la colección exista
            pieza.CarasDentales ??= new List<OdontogramaCaraDental>();

            foreach (var cara in carasDentales)
            {
                // Si no existe la cara dental en la pieza se agrega
                if (!pieza.CarasDentales.Any(cd => cd.CaraDental.CaraDentaria == cara.CaraDentaria))
                {
                    var nuevaCaraDental = new OdontogramaCaraDental
                    {
                        PiezaDental = pieza,
                        CaraDental = cara,
                        Nomenclador = null,               
                        ColorHexadecimal = null,
                        FechaPrestacion = DateTime.Now,
                        ObraSocial = null
                    };

                 //   nuevaCaraDental.EntityState = EntityStateMark.Added;
                    pieza.CarasDentales.Add(nuevaCaraDental);
                }
            }
        }

        /// <summary>
        /// Agrega una pieza dental al odontograma.
        /// </summary>
        /// <param name="piezaDental"></param>
        public void AddPiezaDental(PiezaDental piezaDental)
        {
            // Si la pieza existe en el odontograma sale sin hacer nada.
            if (PiezasDentales.Any(pd => pd.PiezaDental.NumeroPieza == piezaDental.NumeroPieza)) return;

            var nuevaPiezaDental = new OdontogramaPiezaDental()
            {
                Odontograma = this,
                PiezaDental = piezaDental
            };

            nuevaPiezaDental.EntityState = EntityStateMark.Added;
            PiezasDentales.Add(nuevaPiezaDental);

            // Agrego las caras dentales de la pieza.
            AddCarasDentales(piezaDental.CarasDentales, piezaDental.NumeroPieza);
        }

        /// <summary>
        /// Aplica una prestacion a una cara dental del odontograma.
        /// </summary>
        /// <param name="numeroPiezaDental"></param>
        public void AplicarCaraDental(byte numeroPiezaDental, TipoCara tipoCara, Nomenclador nomenclador, string? colorHexa)
        {
            // Busca la pieza dental en el odontograma.
            var piezaDentalOdontograma = PiezasDentales.Single(p => p.PiezaDental.NumeroPieza == numeroPiezaDental);
            
            foreach(var caraDentalOdontograma in piezaDentalOdontograma.CarasDentales)
            {
                caraDentalOdontograma.PiezaDental = piezaDentalOdontograma;

                // En caso de ser un arreglo de multiples caras.
                if (!nomenclador.RequiereCara)
                {
                    caraDentalOdontograma.Nomenclador = nomenclador;
                    caraDentalOdontograma.ObraSocial = nomenclador.ObraSocial;
                    caraDentalOdontograma.ColorHexadecimal = colorHexa;
                    if (caraDentalOdontograma.Id != 0)
                    {
                        caraDentalOdontograma.EntityState = EntityStateMark.Modified;
                    }
                    else
                    {
                        caraDentalOdontograma.EntityState = EntityStateMark.Added;
                    }
                }
                // En caso de ser una sola cara.
                else
                {
                    if (caraDentalOdontograma.CaraDental.CaraDentaria == tipoCara)
                    {
                        caraDentalOdontograma.Nomenclador = nomenclador;
                        caraDentalOdontograma.ObraSocial = nomenclador.ObraSocial;
                        caraDentalOdontograma.ColorHexadecimal = colorHexa;
                        if (caraDentalOdontograma.Id != 0)
                        {
                            caraDentalOdontograma.EntityState = EntityStateMark.Modified;
                        }
                        else
                        {
                            caraDentalOdontograma.EntityState = EntityStateMark.Added;
                        }
                    }
                }

            }

            if (piezaDentalOdontograma.Id != 0)
            {
                piezaDentalOdontograma.EntityState = EntityStateMark.Modified;
            }
            else
            {
                piezaDentalOdontograma.EntityState = EntityStateMark.Added;
            }

 
        }


        public void EliminarPrestacion(byte numeroPiezaDental, TipoCara tipoCara)
        {
            // Busca la pieza dental en el odontograma.
            var piezaDentalOdontograma = PiezasDentales.Single(p => p.PiezaDental.NumeroPieza == numeroPiezaDental);

            piezaDentalOdontograma.EntityState = EntityStateMark.Modified;
            foreach (var caraDentalOdontograma in piezaDentalOdontograma.CarasDentales)
            {
                caraDentalOdontograma.PiezaDental = piezaDentalOdontograma;

                // En caso de ser un arreglo de multiples caras.
                if (!caraDentalOdontograma.Nomenclador.RequiereCara)
                {
                    caraDentalOdontograma.EntityState = EntityStateMark.Deleted;
                }
                // En caso de ser una sola cara.
                else if (caraDentalOdontograma.CaraDental.CaraDentaria == tipoCara)
                {
                    caraDentalOdontograma.EntityState = EntityStateMark.Deleted;
                }

            }


        }
    }
}
using System.Runtime.CompilerServices;

namespace Portable.Enums
{
    /// <summary>
    /// Tipos de caras de una pieza dental (5).
    /// </summary>
    public enum TipoCara : byte
    {
        Mesial = 0,
        Distal = 1,
        Vestibular = 2,
        Palatina = 3,
        Oclusal = 4,
        Lingual = 5
    }

    public static class TipoCaraExtensions 
    {
        public static TipoCara MapTipoCara(string tipoCara)
        {
            switch (tipoCara.ToUpper().Trim())
            {
                case "O":
                    {
                        return TipoCara.Oclusal;
                    }
                case "V":
                    {
                        return TipoCara.Vestibular;
                    }
                case "M":
                    {
                        return TipoCara.Mesial;
                    }
                case "D":
                    {
                        return TipoCara.Distal;
                    }
                case "L":
                    {
                        return TipoCara.Lingual;
                    }
                default:
                    throw new Exception($"No se encontró el tipo de cara buscado: {tipoCara}.");
            }
             
        }
    }
}

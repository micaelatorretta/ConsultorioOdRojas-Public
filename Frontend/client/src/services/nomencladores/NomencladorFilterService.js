import { FilterOperator } from '../../constants/Filters/FilterOperator';

const NomencladorFilterService = {

  /**
   *   Método para obtener filtros válidos para la busqueda.
   * */
  getCommonFilters(searchQuery, obraSocialId) {
    const queryStr = String(searchQuery || '').trim(); // Convertir a string y limpiar espacios

    if (!queryStr) {
      return [
        {
          isGroup: true,
          isAnd: false,
          subConditions:
          [
            { 
              propertyName: 'ObraSocial.Id', value: obraSocialId, isGroup: false, condition: FilterOperator.EQUALS, isAnd: true
            }
          ]
        },
        

      ]
    }

    return [
      {
        isGroup: true,
        isAnd: true, // (OR de textos) AND (ObraSocial.Id = X)
        subConditions: [
          {
            isGroup: true,
            isAnd: false, // OR
            subConditions: [
              { isGroup: false, propertyName: 'CodigoNacional', value: queryStr, condition: FilterOperator.CONTAINS },
              { isGroup: false, propertyName: 'Descripcion',    value: queryStr, condition: FilterOperator.CONTAINS },
              // Si querés buscar por importe: solo si queryStr es numérico -> usar EQUALS o BETWEEN
              // { isGroup: false, propertyName: 'Importe', value: numStr, condition: FilterOperator.EQUALS },
            ]
          },
          {
            isGroup: false,
            propertyName: 'ObraSocial.Id',
            value: obraSocialId,
            condition: FilterOperator.EQUALS
          }
        ]
      }
    ];
  }

    
};

export default NomencladorFilterService;

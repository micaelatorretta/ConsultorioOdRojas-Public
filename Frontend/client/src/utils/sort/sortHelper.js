/**
 * Construye los criterios de ordenamiento a partir de los campos de ordenamiento seleccionados en un data table
 * @param sortByFields 
 * @returns 
 */
export const getSortDefinitions = (sortByFields) => {
  if (!sortByFields || sortByFields.length === 0) {
    return [];
  }

  return sortByFields.map(field => ({
    propertyName: field.key, 
    direction: field.order === 'asc' ? 0 : 1
  }));
};

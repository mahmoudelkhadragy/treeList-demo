declare var $: any;

function capitalizeFirstLetter(string: string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

function splitAndjoinBy(name: string, joind: string) {
  return name
    .split(joind)
    .map((name) => capitalizeFirstLetter(name))
    .join(' ');
}

export function toCapitalizeWord(name: string) {
  let captalizedName;
  if (name.includes('_')) {
    captalizedName = splitAndjoinBy(name, '_');
  } else if (name.includes(' ')) {
    captalizedName = splitAndjoinBy(name, ' ');
  } else if (name.includes('-')) {
    captalizedName = splitAndjoinBy(name, '-');
  } else {
    captalizedName = capitalizeFirstLetter(name);
  }
  return captalizedName === 'Id' ? 'ID' : captalizedName;
}

// hide columns by column name
export function hideColumn(columnName: string) {
  if (columnName === 'Id') {
    columnName = 'ID';
  }
  const columnHeadID = $(`table tr td[aria-label="Column ${columnName}"]`);
  let columnColIndexValue;
  if (columnHeadID) {
    if (columnName === 'ID') {
      $(`td[aria-label="Column ${columnName}"] .dx-treelist-text-content`).css(
        'display',
        'none'
      );
      columnColIndexValue = columnHeadID.attr('aria-colindex');
      $(
        `tr td[aria-colindex="${columnColIndexValue}"] .dx-treelist-text-content`
      ).css('display', 'none');
    } else {
      $(`td[aria-label="Column ${columnName}"]`).css('display', 'none');
      columnColIndexValue = columnHeadID.attr('aria-colindex');
      $(`tr td[aria-colindex="${columnColIndexValue}"]`).css('display', 'none');
    }
  }
}

// color icons

export function colorIcons(
  allowDelete: boolean,
  allowEdit: boolean,
  defaultColor: string,
  deleteColor: string,
  editColor: string
) {
  if (allowDelete && allowEdit) {
    $('.dx-treelist .dx-link').css('color', defaultColor);
  } else if (allowDelete) {
    $('.dx-treelist .dx-link.dx-link-delete').css('color', deleteColor);
  } else if (allowEdit) {
    $('.dx-treelist .dx-link.dx-icon-edit ').css('color', editColor);
  }
}

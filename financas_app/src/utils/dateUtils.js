
export const getMonthString = (date) => {
  var month = [12]
  month[0] = 'Janeiro'
  month[1] = 'Fevereiro'
  month[2] = 'Março'
  month[3] = 'Abril'
  month[4] = 'Mail'
  month[5] = 'Junho'
  month[6] = 'Julho'
  month[7] = 'Agosto'
  month[8] = 'Setembro'
  month[9] = 'Outubro'
  month[10] = 'Novembro'
  month[11] = 'Dezembro'
  return month[date.getMonth()]
}

export const getDateFromSeconds = (mileseconds) => {
  return new Date(mileseconds * 1000)
}

export const stringfyDate = (date) => {
  return date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear()
}

export const stringfyDateUTC = (date) => {
  return date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2)
}

export const convertDateStringToDate = (dateString) => {
  let a = dateString.split('/')
  return new Date(a[2] + '/' + a[1] + '/' + a[0])
}

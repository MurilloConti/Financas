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

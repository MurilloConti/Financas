import variance from 'mathjs'
import cov from 'compute-covariance'
export const calculateRisk = (stocks) => {
  stocks.forEach(element => {

  })
  calcDesvioPadrao()
}
const calcDesvioPadrao = (stock) => {
  let a = Math.pow(stock.idealPerc, 2)
  let varaicao = Math.pow(calculateVariance(stock))
  return a * varaicao
}
const calculateVariance = (stock) => {
// variance([1,2,3,4,5])
  return variance([1, 2, 3, 4, 5])
}
const calculateCovariance = (stock) => {
  // variance([1,2,3,4,5])
  return cov()
}

import { getDateFromSeconds, stringfyDate } from '../utils/dateUtils'
const fb = require('@/firebaseConfig.js')

export const saveAcaoToFirebase = (currentUserUId, acao) => {
  fb.usuariosCollection.doc(currentUserUId).collection('Acoes').doc(acao.Code)
    .set({
      Code: acao.Code,
      Cost: Number(acao.Cost),
      Name: acao.Name,
      Price: Number(acao.Price),
      dtOperation: new Date(acao.dtOperation.replace(/-/g, '/')),
      Qtd: Number(acao.Qtd),
      idWallet: Number(acao.idWallet),
      idealPerc: Number(acao.idealPerc)
    }, { merge: true })
    .then(() => {
      console.log('Document successfully written!')
    })
}

export const GetWalletFromFireStore = (currentUserUId) => {
  return new Promise((resolve, reject) => {
    fb.usuariosCollection.doc(currentUserUId).collection('Carteiras')
      .get()
      .then(function (querySnapshot) {
        let wallets = []
        querySnapshot.forEach(function (doc) {
          wallets.push(ConstructWalletObject(doc.data(), doc.id))
        })
        resolve(wallets)
      })
      .catch(function (e) {
        reject(e)
        console.log('Error getting documents: ', e)
      })
  })
}

export const createWalletFireStore = (currentUserUId, wallet, walletNickName) => {
  return new Promise((resolve, reject) => {
    fb.usuariosCollection.doc(currentUserUId).collection('Carteiras').doc(walletNickName)
      .set({
        id: wallet.id,
        name: wallet.name,
        custodiaTax: wallet.custodiaTax,
        corretagemTax: wallet.corretagemTax,
        equity: wallet.equity
      }, { merge: true })
      .then(() => {
        resolve(true)
      })
      .catch((e) => {
        console.log('Error createWalletFireStore', e)
        reject(e)
      })
  })
}

export const getStocksFromWallet = (currentUserUId) => {
  return new Promise((resolve, reject) => {
    fb.usuariosCollection.doc(currentUserUId).collection('Acoes')
      .get()
      .then((querySnapshot) => {
        let stocks = []
        querySnapshot.forEach(function (doc) {
          stocks.push(ConstructStockObject(doc.data()))
        })
        resolve(stocks)
      })
      .catch((e) => {
        console.log('ERROR: getStocksFromWallet: ', e)
        reject(e)
      })
  })
}

export const getOperationsFromStock = (currentUserUId, stock) => {
  return new Promise((resolve, reject) => {
    fb.usuariosCollection.doc(currentUserUId).collection('movimentacoes')
      .where('Code', '==', stock.Code)
      .get()
      .then((querySnapshot) => {
        let operations = []
        querySnapshot.forEach(function (doc) {
          operations.push(ConstructMovimentObject(doc.data()))
        })
        resolve(operations)
      })
      .catch((e) => {
        console.log('ERROR: getOperationFromStock: ', e)
        reject(e)
      })
  })
}

export const saveOperationToFirebase = (currentUserUId, operation) => {
  fb.usuariosCollection.doc(currentUserUId).collection('movimentacoes').doc()
    .set({
      Code: operation.Code,
      Qtd: operation.Qtd,
      Cost: Number(operation.Cost),
      Type: operation.Type,
      dtOperation: new Date(operation.dtOperation.replace(/-/g, '/')),
      idWallet: Number(operation.idWallet)
    }, { merge: true })
    .then(() => {
      console.log('Document successfully written!')
    })
}

export const ConstructStockObject = (docData) => {
  let stock = {
    Code: docData.Code,
    Name: docData.Name,
    Qtd: docData.Qtd,
    Cost: docData.Cost,
    Price: docData.Price,
    dtOperation: stringfyDate(getDateFromSeconds(docData.dtOperation.seconds)),
    idWallet: docData.idWallet,
    Operacao: null,
    idealPerc: docData.idealPerc
  }
  return stock
}

export const ConstructWalletObject = (docData, docId) => {
  let stock = {
    nickName: docId,
    corretagemTax: docData.corretagemTax,
    custodiaTax: docData.custodiaTax,
    equity: docData.equity,
    id: docData.id,
    name: docData.name
  }
  return stock
}

export const ConstructMovimentObject = (docData) => {
  let moviment = {
    Code: docData.Code,
    Cost: docData.Cost,
    Type: docData.Type,
    Qtd: docData.Qtd,
    dtOperation: getDateFromSeconds(docData.dtOperation.seconds),
    idWallet: docData.idWallet
  }
  return moviment
}

import firebase from 'firebase'
import 'firebase/firestore'

const firebaseConfig = {
  apiKey: 'AIzaSyAfyrXt_UCfgdGHJVyCOwWgU-ea0XBU7sw',
  authDomain: 'gestaofinanceira-bbc86.firebaseapp.com',
  databaseURL: 'https://gestaofinanceira-bbc86.firebaseio.com',
  projectId: 'gestaofinanceira-bbc86',
  storageBucket: 'gestaofinanceira-bbc86.appspot.com',
  messagingSenderId: '421034298843',
  appId: '1:421034298843:web:19c6691beae3c3ce932bfe'
}
firebase.initializeApp(firebaseConfig)

// firebase utils
const db = firebase.firestore()
const auth = firebase.auth()

// firebase collections
const propostasCollection = db.collection('Proposta')
const pitchCollection = db.collection('Apresentacao')
const desenvolvimentoCollection = db.collection('Desenvolvimento')
const UsersCollection = db.collection('Votantes')

export {
  db,
  auth,
  propostasCollection,
  pitchCollection,
  desenvolvimentoCollection,
  UsersCollection
}

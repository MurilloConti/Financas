use financas;
insert into Pessoas(Nome,CPF,DataNascimento,Email,Telefone)
values('Murillo Conti Rodrigues de Castro',31699217890,'19931030','murilloconti@hotmail.com','16981411756');
insert into Carteiras(Nome,ProprietarioId)
values('Carteira do murillo',1);
insert into Indices(nome,valor,DtAlteracao)VALUES('IBOVESPA',-0.23,NOW());
insert into Indices(nome,valor,DtAlteracao)VALUES('CDI',5.40,NOW());


-- CONSTANCIA FUNDAMENTO FIA
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('236942',11182064000177,'BRNPF2CTF001','Constância Fundamento FIA',2,15,0,0,2977.24785108,3.4222548,1,1,0);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',1,0,0,0,0);
call sp_Carteira_Inserts(20190228,1,0);
call sp_Carteira_Inserts(20190331,1,0);
call sp_Carteira_Inserts(20190430,1,0);
call sp_Carteira_Inserts(20190531,1,0);
call sp_Carteira_Inserts(20190630,1,0);
call sp_Carteira_Inserts(20190731,1,9975.27);
call sp_Carteira_Inserts(20190831,1,10062.41);
call sp_Carteira_Inserts(20190930,1,10181.65);

-- DAYCOVAL IBOVESPA ATIVO F.I.ACOES
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('289140',13155995000101,'BRDAY2CTF007','Daycoval Ibovespa Ativo FIA',2.5,15,0,0,9790.3110147,1.7018385,1,1,0);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',2,0,0,0,0);
call sp_Carteira_Inserts(20190228,2,0);
call sp_Carteira_Inserts(20190331,2,1508.02);
call sp_Carteira_Inserts(20190430,2,1519.95);
call sp_Carteira_Inserts(20190531,2,1543.73);
call sp_Carteira_Inserts(20190630,2,1593.06);
call sp_Carteira_Inserts(20190731,2,15929.57);
call sp_Carteira_Inserts(20190831,2,15985.47);
call sp_Carteira_Inserts(20190930,2,16417.96);

-- DAYCOVAL DEBÊNTURES INCENTIVADAS CP FIM
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('431923',26142614000100,'BRDDB1CTF003','Daycoval Debêntures Incentiva FIM C Priv',0.75,0,0,0,65214.2880558,1.2898925,2,1,0);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',3,0,0,0,6103.59);
call sp_Carteira_Inserts(20190228,3,6141.38);
call sp_Carteira_Inserts(20190331,3,16192.45);
call sp_Carteira_Inserts(20190430,3,16327.62);
call sp_Carteira_Inserts(20190531,3,39259.51);
call sp_Carteira_Inserts(20190630,3,39757.43);
call sp_Carteira_Inserts(20190731,3,83042.83);
call sp_Carteira_Inserts(20190831,3,82948.26);
call sp_Carteira_Inserts(20190930,3,83719.88);
  -- LCI/BB
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('0',0,'0','BANCO DO BRASIL	LCI',0,0,0,0,0,0,2,1,80);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',4,0,0,0,119899.50);
call sp_Carteira_Inserts(20190228,4,120372.70);
call sp_Carteira_Inserts(20190331,4,101150.67);
call sp_Carteira_Inserts(20190430,4,101529.61);
call sp_Carteira_Inserts(20190531,4,101970.46);
call sp_Carteira_Inserts(20190630,4,102352.74);
call sp_Carteira_Inserts(20190731,4,0);
call sp_Carteira_Inserts(20190831,4,0);
call sp_Carteira_Inserts(20190930,4,0);
-- LCI/DAYCOVAL 97.5%
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
 values('0',0,'0','BANCO DAYCOVAL S/A	LCA',0,0,0,0,0,0,2,1,97);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',5,0,0,0,24281.43);
call sp_Carteira_Inserts(20190228,5,22367.37);
call sp_Carteira_Inserts(20190331,5,22475.00);
call sp_Carteira_Inserts(20190430,5,22583.13);
call sp_Carteira_Inserts(20190531,5,0);
call sp_Carteira_Inserts(20190630,5,0);
call sp_Carteira_Inserts(20190731,5,37843.70);
call sp_Carteira_Inserts(20190831,5,38027.88);
call sp_Carteira_Inserts(20190930,5,38191.19);
-- LCI/DAYCOVAL 100%
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('0',0,'0','BANCO DAYCOVAL S/A	LCI',0,0,0,0,0,0,2,1,100);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',6,0,0,0,0);
call sp_Carteira_Inserts(20190228,6,0);
call sp_Carteira_Inserts(20190331,6,9013.30);
call sp_Carteira_Inserts(20190430,6,9057.78);
call sp_Carteira_Inserts(20190531,6,9106.97);
call sp_Carteira_Inserts(20190630,6,9151.92);
call sp_Carteira_Inserts(20190731,6,9203.88);
call sp_Carteira_Inserts(20190831,6,9250.06);
call sp_Carteira_Inserts(20190930,6,9291.02);
-- BRASIL PLURAL DEB. INC. 45 CP FIC FIM
insert into fundos(ANBIMA,CNPJ,ISIN,Nome,TxAdministracao,TxImpRenda,TxPerformance,txResgate,QtdCotas,ValorCota,BenchmarkId,CarteiraId,PercIndice)
values('425524',24623412000146,'BRBSP3CTF008','	BP Debêntures Incentiv 45 FIC FIM C Priv',0,0,20,0,0,0,2,1,0);
insert into Rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal)
values('20190101','20190131',7,0,0,0,824.34);
call sp_Carteira_Inserts(20190228,7,829.43);
call sp_Carteira_Inserts(20190331,7,0);
call sp_Carteira_Inserts(20190430,7,0);
call sp_Carteira_Inserts(20190531,7,0);
call sp_Carteira_Inserts(20190630,7,0);
call sp_Carteira_Inserts(20190731,7,0);
call sp_Carteira_Inserts(20190831,7,0);
call sp_Carteira_Inserts(20190930,7,0);

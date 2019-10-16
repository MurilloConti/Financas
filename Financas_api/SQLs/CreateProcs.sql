DROP PROCEDURE IF EXISTS sp_Carteira_Inserts;
DELIMITER |
  CREATE PROCEDURE sp_Carteira_Inserts (dtFinal datetime,IN idFundo INT, IN valoAtual decimal(20,5))
  BEGIN
      DECLARE valor decimal(20,5);
      DECLARE dtInicial datetime;    
      DECLARE plucro decimal(20,5);
      SELECT SaldoFinal, DATE_ADD(DataInicio, INTERVAL 1 MONTH)  INTO valor,dtInicial from rendimentos WHERE FundoId = idFundo ORDER BY DataFim DESC LIMIT 1;         
      insert INTO rendimentos(DataInicio,DataFim,FundoId,LuroApurado,PercLucro,SaldoAnterior,SaldoFinal) SELECT dtInicial,dtFinal,idFundo,(valoAtual - valor), fn_calculaRendimento(valoAtual,(valoAtual - valor)),valor,valoAtual;                  
  END|
DELIMITER;

drop function fn_calculaRendimento;
DELIMITER $$
   CREATE FUNCTION fn_calculaRendimento (valorAtual decimal(20,5), lucro decimal(20,5)) 
  RETURNS decimal(20,5)
    DETERMINISTIC
    BEGIN 
      DECLARE dist decimal(10,5);   
      SET dist = ((lucro*100)/valorAtual);
      return ifnull(dist,0);
    END$$
DELIMITER;



SELECT * from rendimentos;
-- evoluçao de patrimonio
  select CONCAT(MONTH(DataFim),'/',YEAR(DataFim)) as mmYYYY,sum(SaldoFinal)  as total FROM rendimentos GROUP BY(DataFim)
-- posição atual
  select rendimentos.SaldoFinal, fundos.Nome from rendimentos
    inner join fundos on fundos.Id = rendimentos.FundoId
    where rendimentos.DataFim = 20190930
    GROUP BY(FundoId);

  -- valorização dos fundos 
  select rendimentos.PercLucro, fundos.Nome from rendimentos
    inner join fundos on fundos.Id = rendimentos.FundoId
    where rendimentos.DataFim = 20190930
    GROUP BY(FundoId);

    
-- percentual de aumento de patrimonio
  select CONCAT(MONTH(DataFim),'/',YEAR(DataFim)) as mmYYYY, IFNULL(TRUNCATE(((sum(SaldoFinal) - sum(SaldoAnterior))*100)/sum(SaldoAnterior),2),0) as percentual from rendimentos GROUP BY(DataFim)

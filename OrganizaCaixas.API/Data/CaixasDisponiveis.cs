using OrganizaCaixas.Models;
using System.Collections.Generic;

namespace OrganizaCaixas.Data
{
    public static class CaixasDisponiveis
    {
        public static List<Caixa> ObterTodasCaixas()
        {
            return new List<Caixa>
            {
                new Caixa("Caixa 1", 30, 40, 80),
                new Caixa("Caixa 2", 80, 50, 40),
                new Caixa("Caixa 3", 50, 80, 60)
            };
        }
    }
}
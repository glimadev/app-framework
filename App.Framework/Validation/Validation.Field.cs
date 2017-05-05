using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace Vitali.Framework.Validation
{
    /// <summary>
    /// Classe para validar dados
    /// </summary>
    public static partial class Validation
    {
        //Validação de campos de formulário
        public static class Field
        {
            #region [ CPF ]

            /// <summary>
            /// Valida um CPF
            /// </summary>
            /// <param name="strCPF">CPF a ser validado</param>
            /// <returns>Boolean com o resultado da validação</returns>
            public static bool IsCpf(string cpf)
            {
                if (String.IsNullOrEmpty(cpf))
                    return false;

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }

            #endregion

            #region [ CNPJ ]

            /// <summary>
            /// Valida um CNPJ
            /// </summary>
            /// <param name="cnpj">CPF a ser validado</param>
            /// <returns>Boolean com o resultado da validação</returns>
            public static bool IsCnpj(string cnpj)
            {
                string[] invalids = { 
                                    "99999999999999", "88888888888888", "77777777777777", "66666666666666", 
                                    "55555555555555", "44444444444444", "33333333333333", "22222222222222",
                                    "11111111111111" 
                                  };

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                if (invalids.FirstOrDefault(x => x == cnpj) != null)
                    return false;
                if (cnpj.Length != 14)
                    return false;
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cnpj.EndsWith(digito);
            }

            #endregion

            #region [ Email ]

            /// <summary>
            /// Valida um e-mail baseado na expresão regular
            /// </summary>
            /// <param name="strEmail">E-mail a ser validado</param>
            /// <returns>Boolean com o resultado da validação</returns>
            public static bool IsEmail(string strEmail)
            {
                if (!String.IsNullOrEmpty(strEmail))
                {
                    string strExpresaoEmail = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    return Regex.IsMatch(strEmail, strExpresaoEmail);
                }

                return false;
            }

            #endregion

            #region [ Date ]

            /// <summary>
            /// Valida uma data no formato String, padrão dd/mm/yyyy
            /// </summary>
            /// <param name="strData"></param>
            /// <returns></returns>
            public static bool IsDateBR(string strData)
            {
                String strExpressaoData = @"^(?:(?:(?:0?[1-9]|1\d|2[0-8])\/(?:0?[1-9]|1[0-2]))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:(?:(?:31\/0?[13578]|1[02])|(?:(?:29|30)\/(?:0?[1,3-9]|1[0-2])))\/(?:(?:1[6-9]|[2-9]\d)\d{2}))$|^(?:29\/0?2\/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26]))))$";
                return Regex.IsMatch(strData, strExpressaoData);
            }

            #endregion
        }
    }
}

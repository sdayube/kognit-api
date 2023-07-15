namespace Kognit.API.Application.Interfaces
{
    public interface IModelHelper
    {
        /// <summary>
        ///     Retorna todos os campos da classe <typeparamref name="T" /> separados por vírgula.
        /// </summary>
        string GetModelFields<T>();

        /// <summary>
        ///     Verifica se os campos passados no parâmetro existem na classe <typeparamref name="T" />.
        /// </summary>
        /// <returns>Retorna uma string com os campos que existem na classe <typeparamref name="T" /> separados por vírgula.</returns>
        string ValidateModelFields<T>(string fields);
    }
}
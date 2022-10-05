using System.Threading.Tasks;

namespace LPC.Domain.Helpers.Interfaces;

public interface IValidationHelper
{
    Task<int> ToValidate(int id);
}

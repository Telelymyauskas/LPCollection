using System.Threading.Tasks;

namespace LPC.Domain.Helpers.Interfaces;

public interface IValidationHelper
{
    Task<bool> ValidateRecordDuplication(int id);
}

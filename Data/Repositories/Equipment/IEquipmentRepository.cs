using LabManagementSystem.Data.Models;

namespace LabManagementSystem.Data.Repositories.Equipment;

public interface IEquipmentRepository :
    IInsertableRepository<EquipmentModel>,
    IUpdatableRepository<EquipmentModel>,
    IEntirelySelectableRepository<EquipmentModel>,
    IOneKeySelectableRepository<EquipmentModel, int>,
    IOneKeyDeletableRepository<int>
{
    //object Equipment { get; }
}
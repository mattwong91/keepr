



namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepo;

  public KeepsService(KeepsRepository keepsRepo)
  {
    _keepsRepo = keepsRepo;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _keepsRepo.CreateKeep(keepData);
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    List<Keep> keeps = _keepsRepo.GetKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _keepsRepo.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception($"Keep with id: {keepId} does not exist.");
    }
    return keep;
  }

  internal Keep EditKeep(int keepId, string userId, Keep keepData)
  {
    Keep keepToUpdate = GetKeepById(keepId);
    if (keepToUpdate.CreatorId != userId)
    {
      throw new Exception($"Keep with id: {keepId} is not yours to edit");
    }

    keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
    keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;

    Keep keep = _keepsRepo.EditKeep(keepToUpdate);
    return keep;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception($"Keep with id: {keepId} is not yours to delete.");
    }

    _keepsRepo.DeleteKeep(keepId);

    return $"id:{keepId}, name:{keep.Name} has been deleted.";
  }
}
namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepo;
  private readonly VaultsService _vaultsService;

  public KeepsService(KeepsRepository keepsRepo, VaultsService vaultsService)
  {
    _keepsRepo = keepsRepo;
    _vaultsService = vaultsService;
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

  internal Keep GetKeepByIdAndIncrementViews(int keepId)
  {
    Keep keep = _keepsRepo.GetKeepById(keepId);
    keep.Views++;
    _keepsRepo.EditKeep(keep);
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

  internal void IncrementKeptCount(Keep keepToUpdate)
  {
    keepToUpdate.Kept++;
    _keepsRepo.EditKeptCount(keepToUpdate);
  }

  internal void DecrementKeptCount(Keep keepToUpdate)
  {
    keepToUpdate.Kept--;
    _keepsRepo.EditKeptCount(keepToUpdate);
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

  internal List<KeepInVault> GetKeepsByVaultId(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId && vault.IsPrivate == true)
    {
      throw new Exception("ACCESS DENIED. This vault is private.");
    }

    List<KeepInVault> keeps = _keepsRepo.GetKeepsByVaultId(vaultId);
    return keeps;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    List<Keep> keeps = _keepsRepo.GetKeepsByProfileId(profileId);
    return keeps;
  }
}
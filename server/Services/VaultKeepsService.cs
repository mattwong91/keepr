namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepo;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsService keepsService)
  {
    _vaultKeepsRepo = vaultKeepsRepo;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception($"The vault [{vault.Name}] is not yours to add to.");
    }

    VaultKeep vaultKeep = _vaultKeepsRepo.CreateVaultKeep(vaultKeepData);

    Keep keepToUpdate = _keepsService.GetKeepById(vaultKeep.KeepId);
    _keepsService.IncrementKeptCount(keepToUpdate);

    return vaultKeep;
  }

  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    Vault vault = _vaultsService.GetVaultById(vaultKeep.VaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception($"Vault keep: {vaultKeepId} is not in a vault you own.");
    }

    Keep keepToUpdate = _keepsService.GetKeepById(vaultKeep.KeepId);
    _keepsService.DecrementKeptCount(keepToUpdate);

    _vaultKeepsRepo.DeleteVaultKeep(vaultKeepId);
    return $"Vaultkeep: {vaultKeepId} has been deleted from the [{vault.Name}] vault.";
  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepo.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"{vaultKeepId} is not a valid vaultKeep id.");
    }

    return vaultKeep;
  }
}
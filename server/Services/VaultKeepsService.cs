namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepo;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService)
  {
    _vaultKeepsRepo = vaultKeepsRepo;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception($"The vault [{vault.Name}] is not yours to add to.");
    }

    VaultKeep vaultKeep = _vaultKeepsRepo.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }

  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception($"Vault keep: {vaultKeepId} is not yours to delete.");
    }

    _vaultKeepsRepo.DeleteVaultKeep(vaultKeepId);
    return $"Vaultkeep: {vaultKeepId} has been deleted from the [{vault.Name}] vault.";
  }
}
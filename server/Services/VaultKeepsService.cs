
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

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId);
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception($"The vault [{vault.Name}] is not yours to add to.");
    }

    VaultKeep vaultKeep = _vaultKeepsRepo.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }
}
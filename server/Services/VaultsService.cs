namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepo;

  public VaultsService(VaultsRepository vaultsRepo)
  {
    _vaultsRepo = vaultsRepo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _vaultsRepo.CreateVault(vaultData);
    return vault;
  }


  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"ACCESS DENIED. Vault with id: {vaultId} does not exist.");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("ACCESS DENIED. This is a private vault.");
    }
    return vault;
  }

  internal Vault EditVault(int vaultId, Vault vaultData, string userId)
  {
    Vault vaultToUpdate = GetVaultById(vaultId, userId);
    if (vaultToUpdate.CreatorId != userId)
    {
      throw new Exception($"Vault with id: {vaultId} is not yours to edit.");
    }

    vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
    vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

    Vault vault = _vaultsRepo.EditVault(vaultToUpdate);
    return vault;
  }

  internal string DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception($"Vault with id: {vaultId} is not yours to delete.");
    }

    _vaultsRepo.DeleteVault(vaultId);
    return $"Vault id: {vaultId} name: {vault.Name} has been deleted.";
  }

  internal List<Vault> GetVaultsByProfileId(string profileId, Account userInfo)
  {
    if (userInfo == null)
    {
      List<Vault> vaults = _vaultsRepo.GetPublicVaultsByProfileId(profileId);
      return vaults;
    }
    else
    {
      List<Vault> vaults = _vaultsRepo.GetVaultsByProfileId(profileId);
      return vaults;
    }
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> vaults = _vaultsRepo.GetMyVaults(userId);
    return vaults;
  }
}
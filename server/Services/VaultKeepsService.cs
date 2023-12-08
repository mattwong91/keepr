namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepo;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo)
  {
    _vaultKeepsRepo = vaultKeepsRepo;
  }
}
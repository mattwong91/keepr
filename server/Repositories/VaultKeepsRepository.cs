namespace keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO 
    vaultKeeps(creatorId, vaultId, keepId)
    VALUES(@CreatorId, @VaultId, @KeepId);

    SELECT *
    FROM vaultKeeps
    WHERE id = LAST_INSERT_ID();";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
    return vaultKeep;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";

    _db.Execute(sql, new { vaultKeepId });
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = "SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }
}
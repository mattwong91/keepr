namespace keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO 
    vaults(creatorId, name, description, img, isPrivate)
    VALUES(@CreatorId, @Name, @Description, @Img, @IsPrivate);

    SELECT
    v.*,
    acc.*
    FROM vaults v
    JOIN accounts acc ON acc.id = v.creatorId
    WHERE v.id = LAST_INSERT_ID();";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultData).FirstOrDefault();

    return vault;
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";

    _db.Execute(sql, new { vaultId });
  }

  internal Vault EditVault(Vault vaultToUpdate)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    isPrivate = @IsPrivate
    WHERE id = @Id LIMIT 1;

    SELECT
    v.*,
    acc.*
    FROM vaults v
    JOIN accounts acc ON acc.id = v.creatorId
    WHERE v.id = @Id;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultToUpdate).FirstOrDefault();

    return vault;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT
    v.*,
    acc.*
    FROM vaults v
    JOIN accounts acc ON acc.id = v.creatorId
    WHERE v.id = @vaultId;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, new { vaultId }).FirstOrDefault();

    return vault;
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    v.*,
    acc.*
    FROM vaults v
    JOIN accounts acc ON acc.id = v.creatorId
    WHERE v.creatorId = @profileId AND v.isPrivate = false;";

    List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, new { profileId }).ToList();

    return vaults;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    string sql = "SELECT * FROM vaults WHERE creatorId = @userId;";

    List<Vault> vaults = _db.Query<Vault>(sql, new { userId }).ToList();

    return vaults;
  }

  private Vault PopulateCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }
}
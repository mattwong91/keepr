namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO 
    keeps(creatorId, name, description, img)
    VALUES(@CreatorId, @Name, @Description, @Img);

    SELECT
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE k.id = LAST_INSERT_ID();";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepData).FirstOrDefault();

    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
    SELECT
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator).ToList();

    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE k.id = @keepId;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { keepId }).FirstOrDefault();

    return keep;
  }

  internal Keep EditKeep(Keep keepToUpdate)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description
    WHERE id = @Id LIMIT 1;

    SELECT
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE k.id = @Id;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepToUpdate).FirstOrDefault();

    return keep;
  }

  internal void EditKeptCount(Keep keepToUpdate)
  {
    string sql = @"
    UPDATE keeps
    SET
    kept = @Kept
    WHERE id = @Id LIMIT 1;";

    _db.Execute(sql, keepToUpdate);
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    _db.Execute(sql, new { keepId });
  }

  internal List<KeepInVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
    k.*,
    vk.*,
    acc.*
    FROM keeps k
    JOIN vaultKeeps vk ON vk.keepId = k.id
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE vk.vaultId = @vaultId
    ";

    List<KeepInVault> keeps = _db.Query<KeepInVault, VaultKeep, Profile, KeepInVault>(sql, (keepInVault, vaultKeep, profile) =>
    {
      keepInVault.Creator = profile;
      keepInVault.VaultKeepId = vaultKeep.Id;
      return keepInVault;
    }, new { vaultId }).ToList();

    return keeps;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    string sql = @"
    SELECT 
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON acc.id = k.creatorId
    WHERE k.creatorId = @profileId;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { profileId }).ToList();
    return keeps;
  }

  private Keep PopulateCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }

}